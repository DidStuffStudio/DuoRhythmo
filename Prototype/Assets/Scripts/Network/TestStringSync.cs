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

    [SerializeField] private Text networkInfo;

    public struct MessageTypes {
        public const string NUM_PLAYERS = "NumberPlayers,";
        public const string TIMER = "Timer,";
        public const string DISCONNECTED = "Disconnected,";
        public const string DRUM_NODE_CHANGED = "DrumNodeChanged,"; // DrumIndex,NodeIndex,IsActivated --> eg --> 1,11,1
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
        }

        if (_message.Contains(MessageTypes.NUM_PLAYERS)) {
            // split the string
            var numPlayers = Int32.Parse(_message.Split(',')[1]);
            print("The number of players has changed from the server to: " + numPlayers);
            RealTimeInstance.Instance.numberPlayers = numPlayers;
        }
        
        if (_message.Contains(MessageTypes.TIMER)) {
            MasterManager.Instance.timer.timer = Int32.Parse(_message.Split(',')[1]);
        }

        if (_message.Contains(MessageTypes.DRUM_NODE_CHANGED)) {
            // DrumIndex,NodeIndex,IsActivated
            var drumNodeChanged = _message.Split(',');
            var drumIndex = Int32.Parse(drumNodeChanged[1]);
            var nodeIndex = Int32.Parse(drumNodeChanged[2]);
            var activateNode = Int32.Parse(drumNodeChanged[3]);
            MasterManager.Instance.DrumNodeChangedOnServer(drumIndex, nodeIndex, activateNode == 1);
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

    // private void OnEnable() {
    //     if(RealTimeInstance.Instance.isSoloMode) {
    //         Destroy(GetComponent<RealtimeView>());
    //     }
    // }
}