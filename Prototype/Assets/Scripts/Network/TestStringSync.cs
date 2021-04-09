using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class TestStringSync : RealtimeComponent<TestString> {

    private string _message;
    public string Message => _message;

    public string newMessage;
    private string _previousMessage;

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
    }

    public void SetMessage(string value) {
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