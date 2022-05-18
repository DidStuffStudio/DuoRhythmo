using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NetworkManagerSync : RealtimeComponent<NetworkManagerModel>
{
    private int _numberPlayers;
    public int NumberPlayers => _numberPlayers;

    protected override void OnRealtimeModelReplaced(NetworkManagerModel previousModel, NetworkManagerModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.numberPlayersDidChange -= NumberPlayersDidChange;
        }
        
        if (currentModel != null) {
            if (currentModel.isFreshModel) {
                currentModel.numberPlayers = _numberPlayers;
            }
        
            UpdateNetwork();
            
            // register to events
            currentModel.numberPlayersDidChange += NumberPlayersDidChange;
        }
    }

    private void NumberPlayersDidChange(NetworkManagerModel networkManagerModel, int value) {
        _numberPlayers = model.numberPlayers;
    }

    private void UpdateNetwork() => _numberPlayers = model.numberPlayers;
    

    public void PlayerConnected() {
        if (RealTimeInstance.Instance.isSoloMode) return;
        _numberPlayers++;
        model.numberPlayers++;
    }

    public void PlayerDisconnected() {
        if (RealTimeInstance.Instance.isSoloMode) return;
        _numberPlayers--;
        model.numberPlayers--;
    }
}