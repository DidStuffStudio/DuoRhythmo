using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class NetworkManagerSync : RealtimeComponent<NetworkManagerModel>
{
    private bool _nodesInstantiated;
    public bool NodesInstantiated => _nodesInstantiated;
    
    protected override void OnRealtimeModelReplaced(NetworkManagerModel previousModel, NetworkManagerModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.nodesInstantiatedDidChange -= NodesInstantiatedDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                currentModel.nodesInstantiated = _nodesInstantiated;
            }
        
            // Update the mesh render to match the new model
            UpdateInstantiation();
            
            // Register for events so we'll know if the color changes later
            currentModel.nodesInstantiatedDidChange += NodesInstantiatedDidChange;
        }
    }

    private void NodesInstantiatedDidChange(NetworkManagerModel networkManagerModel, bool value) {
        if(!_nodesInstantiated) _nodesInstantiated = true;
    }

    private void UpdateInstantiation() {
        _nodesInstantiated = model.nodesInstantiated;
    }

    public void SetNodesInstantiated(bool value) {
        model.nodesInstantiated = value;
    }
}
