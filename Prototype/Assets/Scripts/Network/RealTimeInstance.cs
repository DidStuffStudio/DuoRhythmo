using Normal.Realtime;
using UnityEngine;

[RequireComponent(typeof(Realtime))]
public class RealTimeInstance : MonoBehaviour {
    private static RealTimeInstance _instance;
    public static RealTimeInstance Instance => _instance;

    private Realtime _realtime;
    [SerializeField] private GameObject networkManagerPrefab;
    public GameObject networkManager;
    private NetworkManagerSync _networkManagerSync;
    public bool isConnected;
    public int numberPlayers;
    public bool isSoloMode = false;

    [SerializeField] private TestStringSync _testStringSync;

    [SerializeField] private GameObject realtimeInstancesHolderPrefab;
    private Transform _realtimeInstancesHolder;

    private void Awake() {
        _instance = this;
        _realtime = GetComponent<Realtime>();
        RegisterToEvents();
    }

    public void JoinRoom(string roomName) {
        _realtime.Connect(roomName: roomName);
    }


    private void RegisterToEvents() {
        // Notify us when Realtime connects to or disconnects from the room
        _realtime.didConnectToRoom += DidConnectToRoom;
        _realtime.didDisconnectFromRoom += DidDisconnectFromRoom;
    }

    // private void LateUpdate() {
    //     if (isSoloMode) return;
    //     var realtimeModels = _realtime.room.datastore.sceneViewModels;
    //     byte playersCounter = 0;
    //     foreach (var rm in realtimeModels) {
    //         if (rm.Value.realtimeView.gameObject.GetComponent<NetworkManagerSync>()) playersCounter++;
    //     }
    //     numberPlayers = playersCounter;
    //     // numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length;
    //     // numberPlayers = _realtimeInstancesHolder.childCount - 1;
    // }

    private void DidConnectToRoom(Realtime realtime) {
        isConnected = true;
        networkManager = Realtime.Instantiate(networkManagerPrefab.name);
        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length; // get the number of players
        MasterManager.Instance.localPlayerNumber =
            numberPlayers - 1; // set this local player's player number to the current player number (index value)
        _testStringSync.SetMessage(TestStringSync.MessageTypes.NUM_PLAYERS + numberPlayers);
    }

    private void DidDisconnectFromRoom(Realtime realtime) {
        isConnected = false;
    }

    private void OnDisable() {
        _realtime.didConnectToRoom -= DidConnectToRoom;
        _realtime.didDisconnectFromRoom -= DidDisconnectFromRoom;
    }

    private void OnApplicationQuit() {
        // _testStringSync.SetMessage("Disconnected," + MasterManager.Instance.localPlayerNumber);
        // _networkManagerSync.PlayerDisconnected();
        _realtime.Disconnect();
    }
}