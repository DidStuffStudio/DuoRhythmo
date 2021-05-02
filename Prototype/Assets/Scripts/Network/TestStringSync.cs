using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class TestStringSync : RealtimeComponent<TestString> {

    private string _message;
    public string Message => _message;

    public string newMessage;
    private string _previousMessage;

    
    public struct MessageTypes {
        public static string SET_PLAYER_NUMBER = "SetPlayerNumber,";
        public const string TIMER = "Timer,";
        public const string NEW_PLAYER_CONNECTED = "NewPlayerConnected,";
        public const string NEW_PLAYER_UPDATE_TIME = "NewPlayerUpdateTime,";
        public const string DRUM_NODES_SINGLE_DRUM = "DrumNodesSingle,";
        public const string DRUM_NODES_ALL_DRUM = "DrumNodesALL,";
        public const string ANIMATOR_TIME = "AnimatorTime,";
    }

    protected override void OnRealtimeModelReplaced(TestString previousModel, TestString currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.messageDidChange -= MessageDidChange;
        }

        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                currentModel.message = _message;
            }

            // Update the mesh render to match the new model
            UpdateMessage();

            // Register for events so we'll know if the color changes later
            currentModel.messageDidChange += MessageDidChange;
        }
    }

    private void UpdateMessage() => _message = model.message;

    private void MessageDidChange(TestString model, string value) {
        _message = model.message;

        if (_message.Contains(MessageTypes.TIMER))
        {
            var time = int.Parse(_message.Split(',')[1]);
            
                MasterManager.Instance.timer.tempRoundTime = time;
                MasterManager.Instance.timer.ToggleTimer(true);
                
        }

        if (_message.Contains(MessageTypes.DRUM_NODES_SINGLE_DRUM))
        {
            var drumNodeChanged = _message.Split(',');
            if (Int32.Parse(drumNodeChanged[1]) == MasterManager.Instance.localPlayerNumber) return;
            var nodeCharArray = drumNodeChanged[3].ToCharArray();
            for (int i = 0; i < 16; i++) MasterManager.Instance.DrumNodeChangedOnServer(int.Parse(drumNodeChanged[2]), i, Int32.Parse(nodeCharArray[i].ToString()) == 1);
        }
        
        if (_message.Contains(MessageTypes.DRUM_NODES_ALL_DRUM))
        {
            var drumNodeChanged = _message.Split(',');
            if (int.Parse(drumNodeChanged[1]) == MasterManager.Instance.localPlayerNumber || !RealTimeInstance.Instance.isNewPlayer) return;
            
            for (int j = 2; j < drumNodeChanged.Length; j++)
            {
                var nodeCharArray = drumNodeChanged[j].ToCharArray();
                for (int i = 0; i < 16; i++)
                {
                    MasterManager.Instance.DrumNodeChangedOnServer(j-2, i, int.Parse(nodeCharArray[i].ToString()) == 1);
                }
            }
            
        }


        if (_message.Contains(MessageTypes.ANIMATOR_TIME))
        {
            var time = _message.Split(',');
            MasterManager.Instance.userInterfaceManager.SetUpRotationForNewPlayer(float.Parse(time[1]));
        }
        
        if (_message.Contains(MessageTypes.NEW_PLAYER_CONNECTED)) {
            
            
            if (!RealTimeInstance.Instance.isNewPlayer)
            {
                MasterManager.Instance.dataMaster.SendNodes(0,true);
                SetMessage(MessageTypes.ANIMATOR_TIME + MasterManager.Instance.userInterfaceManager.currentRotationOfUI);
            }
            
            
            // find all the networkmanager s existing currently in the hierarchy, and set their parents to the players holder gameobject
            var players = GameObject.FindObjectsOfType<NetworkManagerSync>();
            foreach (var player in players) {
                RealTimeInstance.Instance.SetParentOfPlayer(player.transform);
            }
            var splitMessage = _message.Split(',');
            var connectedPlayer = int.Parse(splitMessage[1]);
            
            if (connectedPlayer == MasterManager.Instance.localPlayerNumber) return;
            
            var nextPlayer = MasterManager.Instance.player.transform.parent.childCount;
            SetMessage(MessageTypes.SET_PLAYER_NUMBER + nextPlayer);
            print("sending " + nextPlayer);
        } if (_message.Contains(MessageTypes.NEW_PLAYER_UPDATE_TIME)) {
            
            var splitMessage = _message.Split(',');
            var connectedPlayer = int.Parse(splitMessage[1]);
            if (connectedPlayer == MasterManager.Instance.localPlayerNumber) return;
            SetMessage(MessageTypes.TIMER+MasterManager.Instance.timer.timer);
        }

        if (_message.Contains(MessageTypes.SET_PLAYER_NUMBER)) {
            var playerIndex = int.Parse(_message.Split(',')[1]);
            if (!MasterManager.Instance.player.hasPlayerNumber)
            {
                MasterManager.Instance.localPlayerNumber = playerIndex;
                MasterManager.Instance.player.hasPlayerNumber = true;
            } 
            
            MasterManager.Instance.dataMaster.SetConnectedPlayer(playerIndex, true);
        }
        
    }
    
    public void SetMessage(string value) {
        if(RealTimeInstance.Instance.isSoloMode) return;
        model.message = value;
        print("Sent a new message to the server: " + value);
    }

    private void Update() {
        if (_previousMessage != newMessage) {
            SetMessage(newMessage);
            _previousMessage = newMessage;
        }
    }
    
}