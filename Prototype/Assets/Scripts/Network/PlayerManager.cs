using UnityEngine;
using UnityEngine.Animations;
using Normal.Realtime;

public class PlayerManager : MonoBehaviour {
    [SerializeField] private GameObject _playerPrefab;

    private Realtime _realtime;


    private void Awake() {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime) {
        // Instantiate the Player for this client once we've successfully connected to the room
        GameObject playerGameObject = Realtime.Instantiate(prefabName: _playerPrefab.name, // Prefab name
            ownedByClient: true, // Make sure the RealtimeView on this prefab is owned by this client
            preventOwnershipTakeover: true, // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance: realtime); // Use the instance of Realtime that fired the didConnectToRoom event.

        // Get a reference to the player
        // Player player = playerGameObject.GetComponent<Player>();
    }
}