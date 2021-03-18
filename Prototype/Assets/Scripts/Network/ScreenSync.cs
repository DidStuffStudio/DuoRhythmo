using System;
using UnityEngine;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine.PlayerLoop;

public class ScreenSync: RealtimeComponent<ScreenSyncModel> {
    private int _bpm = 120;
    private int _numberOfNodes = 8;
    
    // getters
    public int Bpm => _bpm;
    public int NumberOfNodes => _numberOfNodes;
    
    // setters
    public void SetBpm(int value) => model.bpm = value;
    public void SetNumberOfNodes(int value) => model.numberOfNodes = value;

    protected override void OnRealtimeModelReplaced(ScreenSyncModel previousModel, ScreenSyncModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.bpmDidChange -= BpmDidChange;
            previousModel.numberOfNodesDidChange -= NumberNodesDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                currentModel.bpm = _bpm;
                currentModel.numberOfNodes = _bpm;
            }
        
            // Update the mesh render to match the new model
            UpdateNode();
            
            // Register for events so we'll know if the color changes later
            currentModel.bpmDidChange += BpmDidChange;
            currentModel.numberOfNodesDidChange += NumberNodesDidChange;
        }
    }

    private void BpmDidChange(ScreenSyncModel model, int value) => _bpm = model.bpm;
    private void NumberNodesDidChange(ScreenSyncModel model, int value) => _numberOfNodes = model.numberOfNodes;


    private void UpdateNode() {
        _bpm = model.bpm;
        _numberOfNodes = model.numberOfNodes;
    }
}