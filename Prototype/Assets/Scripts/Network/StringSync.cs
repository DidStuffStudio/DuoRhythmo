using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class StringSync : RealtimeComponent<StringModel> {
    private string _message;
    private int _playerNumber;
    private int _timer;
    private int _newPlayerConnected;
    private int _newPlayerUpdateTime;
    private string _drumNodesSingle;
    private string _drumNodesAllDrum;
    private float _animatorTime;
    private string _effectsValues;

    protected override void OnRealtimeModelReplaced(StringModel previousModel, StringModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.messageDidChange -= MessageDidChange;
            previousModel.setPlayerNumberDidChange -= SetPlayerNumberDidChange;
            previousModel.timerDidChange -= TimerDidChange;
            previousModel.newPlayerConnectedDidChange -= NewPlayerConnectedDidChange;
            previousModel.newPlayerUpdateTimeDidChange -= NewPlayerUpdateTimeDidChange;
            previousModel.drumNodesSingleDidChange -= DrumNodesSingleDidChange;
            previousModel.drumNodesALLDidChange -= DrumNodesAllDidChange;
            previousModel.animatorTimeDidChange -= AnimatorTimeDidChange;
            previousModel.effectsValuesDidChange -= EffectsValuesDidChange;
        }

        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                currentModel.message = _message;
                currentModel.setPlayerNumber = _playerNumber.ToString();
                currentModel.timer = _timer.ToString();
                currentModel.newPlayerConnected = _newPlayerConnected.ToString();
                currentModel.newPlayerUpdateTime = _newPlayerUpdateTime.ToString();
                currentModel.drumNodesSingle = _drumNodesSingle;
                currentModel.drumNodesALL = _drumNodesAllDrum;
                currentModel.animatorTime = _animatorTime.ToString();
                currentModel.effectsValues= _effectsValues;
            }

            // Update the strings to match their corresponding models
            UpdateStrings();

            // Register for events so we'll know if the strings change later
            currentModel.messageDidChange += MessageDidChange;
            currentModel.setPlayerNumberDidChange += SetPlayerNumberDidChange;
            currentModel.timerDidChange += TimerDidChange;
            currentModel.newPlayerConnectedDidChange += NewPlayerConnectedDidChange;
            currentModel.newPlayerUpdateTimeDidChange += NewPlayerUpdateTimeDidChange;
            currentModel.drumNodesSingleDidChange += DrumNodesSingleDidChange;
            currentModel.drumNodesALLDidChange += DrumNodesAllDidChange;
            currentModel.animatorTimeDidChange += AnimatorTimeDidChange;
            currentModel.effectsValuesDidChange += EffectsValuesDidChange;
        }
    }

    private void MessageDidChange(StringModel model, string value) {
        print("Received new message from the server: " + model.message);
        _message = model.message;
    }
    
    private void DrumNodesAllDidChange(StringModel stringModel, string value) {
        var drumNodeChanged = value.Split(',');
        if (int.Parse(drumNodeChanged[0]) == MasterManager.Instance.localPlayerNumber ||
            !RealTimeInstance.Instance.isNewPlayer) return;

        for (int j = 1; j < drumNodeChanged.Length; j++) {
            var nodeCharArray = drumNodeChanged[j].ToCharArray();
            for (int i = 0; i < 16; i++) {
                MasterManager.Instance.DrumNodeChangedOnServer(j - 1, i, int.Parse(nodeCharArray[i].ToString()) == 1);
            }
        }
    }

    private void EffectsValuesDidChange(StringModel stringModel, string value) {
        var drumValueDidChanged = value.Split(',');
        if (int.Parse(drumValueDidChanged[0]) == MasterManager.Instance.localPlayerNumber ||
            !RealTimeInstance.Instance.isNewPlayer) return;
            
        for (int i = 1; i < drumValueDidChanged.Length; i++) { // go through each drum

            var separatedValues = drumValueDidChanged[i].Split('-');
            int[] separatedValuesArray = new int[4];
            for (int j = 0; i < 4; j++)
            {
                separatedValuesArray[j] = int.Parse(separatedValues[j]);
            }

            MasterManager.Instance.bpm = int.Parse(drumValueDidChanged[drumValueDidChanged.Length - 1]);
            MasterManager.Instance.EffectsDidChangeOnServer(i - 1, separatedValuesArray);
        }
    }

    private void AnimatorTimeDidChange(StringModel stringModel, string value) =>
        MasterManager.Instance.userInterfaceManager.SetUpRotationForNewPlayer(float.Parse(value));

    private void DrumNodesSingleDidChange(StringModel stringModel, string value) {
        var drumNodeChanged = value.Split(',');
        if (Int32.Parse(drumNodeChanged[0]) == MasterManager.Instance.localPlayerNumber) return;
        var nodeCharArray = drumNodeChanged[2].ToCharArray();
        for (int i = 0; i < 16; i++)
            MasterManager.Instance.DrumNodeChangedOnServer(int.Parse(drumNodeChanged[1]), i,
                Int32.Parse(nodeCharArray[i].ToString()) == 1);
    }

    private void NewPlayerUpdateTimeDidChange(StringModel stringModel, string value) {
        var splitMessage = value.Split(',');
        var connectedPlayer = int.Parse(splitMessage[0]);
        if (connectedPlayer == MasterManager.Instance.localPlayerNumber) return;
        SetTimer(MasterManager.Instance.timer.timer);
    }

    private void NewPlayerConnectedDidChange(StringModel stringModel, string value) {
        if (!RealTimeInstance.Instance.isNewPlayer) {
            MasterManager.Instance.dataMaster.SendNodes(0, true);
            MasterManager.Instance.dataMaster.SendEffects(0, true);
            SetAnimatorTime(MasterManager.Instance.userInterfaceManager.currentRotationOfUI);
        }


        // find all the networkmanager s existing currently in the hierarchy, and set their parents to the players holder gameobject
        var players = GameObject.FindObjectsOfType<NetworkManagerSync>();
        foreach (var player in players) {
            RealTimeInstance.Instance.SetParentOfPlayer(player.transform);
        }

        var connectedPlayer = int.Parse(value);

        if (connectedPlayer == MasterManager.Instance.localPlayerNumber) return;

        var nextPlayer = MasterManager.Instance.player.transform.parent.childCount;
        SetPlayerNumber(nextPlayer);
        print("sending " + nextPlayer);
    }

    private void TimerDidChange(StringModel stringModel, string value) {
        var time = int.Parse(value.Split(',')[0]);
        MasterManager.Instance.timer.tempRoundTime = time;
        StartCoroutine(MasterManager.Instance.timer.TemporaryTime());
    }

    private void SetPlayerNumberDidChange(StringModel stringModel, string value) {
        var playerIndex = int.Parse(value);
        if (!MasterManager.Instance.player.hasPlayerNumber) {
            MasterManager.Instance.localPlayerNumber = playerIndex;
            MasterManager.Instance.player.hasPlayerNumber = true;
        }

        MasterManager.Instance.dataMaster.SetConnectedPlayer(playerIndex, true);
    }

    private void UpdateStrings() {
        _message = model.message;
        _playerNumber = int.Parse(model.setPlayerNumber);
        _timer = int.Parse(model.timer);
        _newPlayerConnected = int.Parse(model.newPlayerConnected);
        _newPlayerUpdateTime = int.Parse(model.newPlayerUpdateTime);
        _drumNodesSingle = model.drumNodesSingle;
        _drumNodesAllDrum = model.drumNodesALL;
        _animatorTime = float.Parse(model.animatorTime);
        _effectsValues = model.effectsValues;
    }

    public void SetMessage(string value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.message = value;
        print("Sent a new message to the server: " + value);
    }
    public void SetPlayerNumber(int value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.setPlayerNumber = value.ToString();
        print("Sent a new player number to the server: " + value);
    }

    public void SetTimer(int value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.timer = value.ToString();
        print("Sent timer to the server: " + value);
    }
    public void SetNewPlayerConnected(int value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.newPlayerConnected = value.ToString();
        print("Sent new player connected to the server: " + value);
    }
    public void SetNewPlayerUpdateTime(int value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.newPlayerUpdateTime = value.ToString();
        print("Sent new player update time to the server: " + value);
    }

    public void SetAnimatorTime(float value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.animatorTime = value.ToString();
        print("Sent animator time to the server: " + value);
    }
    public void SetDrumNodesSingle(string value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.drumNodesSingle = value;
        print("Sent drum nodes single to the server: " + value);
    }
    public void SetDrumNodesAllDrums(string value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.drumNodesALL = value;
        print("Sent a drum nodes all drums to the server: " + value);
    }
    public void SetEffectValues(string value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.effectsValues = value;
        print("Sent effect values to the server: " + value);
    }
}