using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NetworkManagerSync : RealtimeComponent<NetworkManagerModel>
{
    private int _numberPlayers;
    public int NumberPlayers {
        get => _numberPlayers;
        set => _numberPlayers = value;
    }

    protected override void OnRealtimeModelReplaced(NetworkManagerModel previousModel, NetworkManagerModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.numberPlayersDidChange -= NumberPlayersDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                currentModel.numberPlayers = _numberPlayers;
            }
        
            // Update the mesh render to match the new model
            UpdateNetwork();
            
            // Register for events so we'll know if the color changes later
            currentModel.numberPlayersDidChange += NumberPlayersDidChange;
        }
    }

    private void NumberPlayersDidChange(NetworkManagerModel networkManagerModel, int value) => _numberPlayers = model.numberPlayers;

    private void UpdateNetwork() => _numberPlayers = model.numberPlayers;
}
