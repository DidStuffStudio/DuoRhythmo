using System;
using UnityEngine;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Realtime))]
public class ScreenSync: RealtimeComponent<ScreenSyncModel> {
    private Realtime _realtime;
    private int _bpm = 120;
    private int _numberOfNodes = 8;
    private List<int> _nodesValues = new List<int>();
    
    [SerializeField] private GameObject nodePrefab;
    
    // getters
    public int Bpm => _bpm;
    public int NumberOfNodes => _numberOfNodes;
    public List<int> NodesValues => _nodesValues;
    
    // setters
    public void SetBpm(int value) => model.bpm = value;
    public void SetNumberOfNodes(int value) => model.numberOfNodes = value;
    
    public void SetArrayOfNodes(int [] value) {
        for (int i = 0; i < model.nodesArray.Count; i++) {
            print(model.nodesArray[i].nodeIndex = value[i]);
        }
    }

    private void Awake() {
        _realtime = GetComponent<Realtime>();
    }
    
    protected override void OnRealtimeModelReplaced(ScreenSyncModel previousModel, ScreenSyncModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.bpmDidChange -= BpmDidChange;
            previousModel.numberOfNodesDidChange -= NumberNodesDidChange;
            previousModel.nodesArray.modelAdded -= NodeAdded;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                currentModel.bpm = _bpm;
                currentModel.numberOfNodes = _bpm;
                for (int i = 0; i < _nodesValues.Count; i++) {
                    model.nodesArray.Add(new NodeModel());
                    model.nodesArray[i].nodeIndex = _nodesValues[i];
                }
            }
        
            // Update the mesh render to match the new model
            UpdateNode();
            
            // Register for events so we'll know if the color changes later
            currentModel.bpmDidChange += BpmDidChange;
            currentModel.numberOfNodesDidChange += NumberNodesDidChange;
            currentModel.nodesArray.modelAdded += NodeAdded;
        }
    }

    private void BpmDidChange(ScreenSyncModel model, int value) => _bpm = model.bpm;
    private void NumberNodesDidChange(ScreenSyncModel model, int value) => _numberOfNodes = model.numberOfNodes;

    private void NodeAdded(RealtimeArray<NodeModel> nodeModels, NodeModel nodeModel, bool remote) => _nodesValues.Add(nodeModel.nodeIndex);

    private void UpdateNode() {
        _bpm = model.bpm;
        _numberOfNodes = model.numberOfNodes;
        for (int i = 0; i < model.nodesArray.Count; i++) {
            _nodesValues[i] = model.nodesArray[i].nodeIndex;
        }
    }
}