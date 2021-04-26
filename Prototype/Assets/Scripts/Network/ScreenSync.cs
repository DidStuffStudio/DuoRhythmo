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
    
    public int Effect1 => _effect1;
    public int Effect2 => _effect2;
    public int Effect3 => _effect3;
    public int Effect4 => _effect4;
    
    public List<Node> _nodes = new List<Node>();
    
    // getters
    public int Bpm => _bpm;
    public int NumberOfNodes => _numberOfNodes;
    public int IndexValue => _indexValue;

    // setters
    public void SetBpm(int value) {
        if(RealTimeInstance.Instance.isSoloMode) return;
        model.bpm = value;
    }
    public void SetNumberOfNodes(int value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.numberOfNodes = value;
    }
    public void SetIndexValue(int value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        model.indexValue = value;
    }

    public void SetEffectValue(int index, int effectValue) {
        if(RealTimeInstance.Instance.isSoloMode) return;
        switch (index) {
            case 0:
                _effect1 = effectValue;
                model.effect1 = effectValue;
                print("This is the model that has been sent: " + effectValue + " and this is the one on the server: " + model.effect1);
                break;
            case 1:
                _effect2 = effectValue;
                model.effect2 = effectValue;
                break;
            case 2:
                _effect3 = effectValue;
                model.effect3 = effectValue;
                break;
            case 3:
                _effect4 = effectValue;
                model.effect4 = effectValue;
                break;
            default: Debug.LogError("Effect index (" + index + ") not found");
                break;
        }
    }

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
        _nodes[value].Activate();
    }
    
    private void Effect1DidChange(ScreenSyncModel model, int value) => _effect1 = model.effect1;
    private void Effect2DidChange(ScreenSyncModel model, int value) => _effect2 = model.effect2;
    private void Effect3DidChange(ScreenSyncModel model, int value) => _effect3 = model.effect3;
    private void Effect4DidChange(ScreenSyncModel model, int value) => _effect4 = model.effect4;


    private void Start() {
        // GetComponent<RealtimeView>().realtime = RealTimeInstance.Instance;
        
    }

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