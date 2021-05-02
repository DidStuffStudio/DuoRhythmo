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
        public const string DISCONNECTED = "Disconnected,";
        public const string DRUM_NODE_CHANGED = "DrumNodeChanged,"; // DrumIndex,NodeIndex,IsActivated --> eg --> 1,11,1
        public const string NEW_PLAYER_CONNECTED = "NewPlayerConnected,";
        public const string NEW_PLAYER_UPDATE_TIME = "NewPlayerUpdateTime,";
        public const string REQUEST_PLAYER_NUMBERS = "RequestPlayerNumbers,";
        public const string SEND_PLAYER_NUMBER = "RequestPlayerNumber,";
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
        print("The message has changed to: " + _message);
        if (_message.Contains(MessageTypes.DISCONNECTED)) {
            // var disconnectedPlayerNumber = Int32.Parse(_message.Split(',')[1]);
            RealTimeInstance.Instance.numberPlayers--;
            print("PlayerDisconnected");
        }
        
        
        if (_message.Contains(MessageTypes.TIMER))
        {
            var time = Int32.Parse(_message.Split(',')[1]);
            
        
                MasterManager.Instance.timer.tempRoundTime = time;
                MasterManager.Instance.timer.ToggleTimer(true);
                
        }

        if (_message.Contains(MessageTypes.DRUM_NODE_CHANGED)) {
            // DrumIndex,NodeIndex,IsActivated
            var drumNodeChanged = _message.Split(',');
            var drumIndex = Int32.Parse(drumNodeChanged[1]);
            var nodeIndex = Int32.Parse(drumNodeChanged[2]);
            var activateNode = Int32.Parse(drumNodeChanged[3]);
            MasterManager.Instance.DrumNodeChangedOnServer(drumIndex, nodeIndex, activateNode == 1);
        }

        if (_message.Contains(MessageTypes.NEW_PLAYER_CONNECTED)) {
            // find all the networkmanager s existing currently in the hierarchy, and set their parents to the players holder gameobject
            var players = GameObject.FindObjectsOfType<NetworkManagerSync>();
            foreach (var player in players) {
                RealTimeInstance.Instance.SetParentOfPlayer(player.transform);
            }
            var splitMessage = _message.Split(',');
            var connectedPlayer = Int32.Parse(splitMessage[1]);
            
            if (connectedPlayer == MasterManager.Instance.localPlayerNumber) return;

            // send that player his new player number
            /*for (int i = 0; i < MasterManager.Instance.dataMaster.conectedPlayers.Length; i++) {
                // 0 <= (2 - 1) = 1 --> connectedPLayer = 1;
                // 1 < (2 - 1) = 1 --> connectedPlayer = 0; --> set the current player index
                if (i <= RealTimeInstance.Instance.numberPlayers) {
                    MasterManager.Instance.dataMaster.conectedPlayers[i] = 1;
                }
                else {
                    MasterManager.Instance.dataMaster.conectedPlayers[i] = 1;
                    SetMessage(MessageTypes.SET_PLAYER_NUMBER + i);
                    print("Sending the player number to player number " + i);
                    break;
                }
                // if (MasterManager.Instance.dataMaster.conectedPlayers[i] == 1) counter++;
            }*/
            var nextPlayer = MasterManager.Instance.player.transform.parent.childCount;
            SetMessage(MessageTypes.SET_PLAYER_NUMBER + nextPlayer);
            print("sending " + nextPlayer);
        } if (_message.Contains(MessageTypes.NEW_PLAYER_UPDATE_TIME)) {
            
            // var splitMessage = _message.Split(',');
            // var connectedPlayer = Int32.Parse(splitMessage[1]);
            // if (connectedPlayer == MasterManager.Instance.localPlayerNumber) return;
            // SetMessage(MessageTypes.TIMER+MasterManager.Instance.timer.timer);
        }

        if (_message.Contains(MessageTypes.SET_PLAYER_NUMBER)) {
            var playerIndex = Int32.Parse(_message.Split(',')[1]);
            if (!MasterManager.Instance.player.hasPlayerNumber)
            {
                MasterManager.Instance.localPlayerNumber = playerIndex;
                MasterManager.Instance.player.hasPlayerNumber = true;
            } 
            
            MasterManager.Instance.dataMaster.SetConnectedPlayer(playerIndex, true);
        }
        
        
        /*if(_message.Contains(MessageTypes.REQUEST_PLAYER_NUMBERS))
        {
            for (int i = 0; i < 10; i++) MasterManager.Instance.dataMaster.SetConnectedPlayer(i,false);
            
            if (!MasterManager.Instance.player.hasPlayerNumber) return;
            SetMessage(MessageTypes.SEND_PLAYER_NUMBER+MasterManager.Instance.localPlayerNumber);
        }

        if (_message.Contains(MessageTypes.SEND_PLAYER_NUMBER))
        {
            var splitMessage = _message.Split(',');
            var playerIdentified = Int32.Parse(splitMessage[1]);
            MasterManager.Instance.dataMaster.SetConnectedPlayer(playerIdentified, true);
        }*/
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

    // private void OnEnable() {
    //     if(RealTimeInstance.Instance.isSoloMode) {
    //         Destroy(GetComponent<RealtimeView>());
    //     }
    // }
}