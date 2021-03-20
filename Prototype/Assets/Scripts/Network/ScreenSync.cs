using System;
using UnityEngine;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine.PlayerLoop;

public class ScreenSync: RealtimeComponent<ScreenSyncModel> {
    // rhythm (beats)
    private int _bpm = 120;
    private int _numberOfNodes = 12;
    private int _indexValue = 0;
    
    // effects
    private int _effect1 = 0;
    private int _effect2 = 0;
    private int _effect3 = 0;
    private int _effect4 = 0;
    
    public List<Node> _nodes = new List<Node>();
    
    // getters
    public int Bpm => _bpm;
    public int NumberOfNodes => _numberOfNodes;
    public int IndexValue => _indexValue;

    public int Effect1 {
        get => _effect1;
        set => _effect1 = value;
    }

    public int Effect2 {
        get => _effect2;
        set => _effect2 = value;
    }

    public int Effect3 {
        get => _effect3;
        set => _effect3 = value;
    }

    public int Effect4 {
        get => _effect4;
        set => _effect4 = value;
    }


    // setters
    public void SetBpm(int value) => model.bpm = value;
    public void SetNumberOfNodes(int value) => model.numberOfNodes = value;
    public void SetIndexValue(int value) => model.indexValue = value;
    
    

    protected override void OnRealtimeModelReplaced(ScreenSyncModel previousModel, ScreenSyncModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            
            // beats
            previousModel.bpmDidChange -= BpmDidChange;
            previousModel.numberOfNodesDidChange -= NumberNodesDidChange;
            previousModel.indexValueDidChange -= IndexValueDidChange;
            
            // effects
            previousModel.effect1DidChange -= Effect1DidChange;
            previousModel.effect2DidChange -= Effect2DidChange;
            previousModel.effect3DidChange -= Effect3DidChange;
            previousModel.effect4DidChange -= Effect4DidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                // beats
                currentModel.bpm = _bpm;
                currentModel.numberOfNodes = _bpm;
                currentModel.indexValue = _indexValue;
                
                // effects
                currentModel.effect1 = _effect1;
            }
        
            // Update the mesh render to match the new model
            UpdateNode();
            
            // Register for events so we'll know if the color changes later
            
            // beats
            currentModel.bpmDidChange += BpmDidChange;
            currentModel.numberOfNodesDidChange += NumberNodesDidChange;
            currentModel.indexValueDidChange += IndexValueDidChange;
            
            // effects
            currentModel.effect1DidChange += Effect1DidChange;
            currentModel.effect2DidChange += Effect2DidChange;
            currentModel.effect3DidChange += Effect3DidChange;
            currentModel.effect4DidChange += Effect4DidChange;
        }
    }

    private void BpmDidChange(ScreenSyncModel model, int value) => _bpm = model.bpm;
    private void NumberNodesDidChange(ScreenSyncModel model, int value) => _numberOfNodes = model.numberOfNodes;
    private void IndexValueDidChange(ScreenSyncModel model, int value) {
        _indexValue = model.indexValue;
        _nodes[value].Activate(true);
    }
    
    private void Effect1DidChange(ScreenSyncModel model, int value) => _effect1 = model.effect1;
    private void Effect2DidChange(ScreenSyncModel model, int value) => _effect2 = model.effect2;
    private void Effect3DidChange(ScreenSyncModel model, int value) => _effect3 = model.effect3;
    private void Effect4DidChange(ScreenSyncModel model, int value) => _effect4 = model.effect4;


    private void UpdateNode() {
        // beats
        _bpm = model.bpm;
        _numberOfNodes = model.numberOfNodes;
        _indexValue = model.indexValue;
        
        // effects
        _effect1 = model.effect1;
        _effect2 = model.effect2;
        _effect3 = model.effect3;
        _effect4 = model.effect4;
    }
}