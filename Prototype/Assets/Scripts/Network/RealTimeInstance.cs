using System;
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
    public int numberPlayers, previousNumberPlayers;
    public bool isSoloMode = true;
    public TestStringSync _testStringSync;

    [SerializeField] private GameObject realtimeInstancesHolderPrefab;
    private Transform _realtimeInstancesHolder;
    [SerializeField] private string[] roomNames = new string[10];
    private int roomToJoinIndex;

    [SerializeField] private GameObject playerCanvasPrefab;
    [SerializeField] private Transform playersHolder;

    private void Awake() {
        _instance = this;
        _realtime = GetComponent<Realtime>();
        RegisterToEvents();
    }

    private void Update() {
        if (!isSoloMode) {
            numberPlayers = playersHolder.childCount;
            if (numberPlayers != previousNumberPlayers) {
                MasterManager.Instance.timer.CheckForOwner();
                previousNumberPlayers = numberPlayers;
            }
        }
    }


    public void SetToSoloMode(bool value) => isSoloMode = value;

    public void SetRoomIndex(int i) => roomToJoinIndex = i;

    public void Play() {
        if (!isSoloMode) _realtime.Connect(roomNames[roomToJoinIndex]);
        MasterManager.Instance.Initialize();
    }

    public void SetParentOfPlayer(Transform p) => p.SetParent(playersHolder);
    

    public double GetRoomTime() {
        return _realtime.room.time;
    }

    private void RegisterToEvents() {
        // Notify us when Realtime connects to or disconnects from the room
        _realtime.didConnectToRoom += DidConnectToRoom;
        _realtime.didDisconnectFromRoom += DidDisconnectFromRoom;
    }

    private void DidConnectToRoom(Realtime realtime) {
        // get rid of all the possible existing realtime timers left alive previously from other game-times in the same room
        foreach (var timer in FindObjectsOfType<Timer>()) {
            print("Getting rid of realtime timer");
            timer.GetComponent<RealtimeView>().RequestOwnership();
            Realtime.Destroy(timer.gameObject);
            Destroy(timer);
        }

        isConnected = true;
        _testStringSync.SetMessage(TestStringSync.MessageTypes.NEW_PLAYER_CONNECTED);
        networkManager = Realtime.Instantiate(networkManagerPrefab.name);

        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length; // get the number of players
        MasterManager.Instance.localPlayerNumber =
            numberPlayers - 1; // set this local player's player number to the current player number (index value)
        _testStringSync.SetMessage(TestStringSync.MessageTypes.NUM_PLAYERS + numberPlayers);

        var gfx = Realtime.Instantiate(playerCanvasPrefab.name, true, true, true);
        gfx.transform.GetComponent<RealtimeTransform>().RequestOwnership();
    }

    private void DidDisconnectFromRoom(Realtime realtime) {
        isConnected = false;
    }

    private void OnDisable() {
        _realtime.didConnectToRoom -= DidConnectToRoom;
        _realtime.didDisconnectFromRoom -= DidDisconnectFromRoom;
    }

    private void OnApplicationQuit() {
        _testStringSync.SetMessage(TestStringSync.MessageTypes.DISCONNECTED +
                                   MasterManager.Instance.localPlayerNumber);
        if (numberPlayers <= 1) {
            print("All players left");
            MasterManager.Instance.timer.GetComponent<RealtimeView>().RequestOwnership();
            Realtime.Destroy(MasterManager.Instance.timer.gameObject);
        }

        else {
            MasterManager.Instance.Players.Remove(MasterManager.Instance.player);
            MasterManager.Instance.timer.gameObject.GetComponent<RealtimeView>().ClearOwnership();
            MasterManager.Instance.timer.CheckForOwner();
        }

    }
}