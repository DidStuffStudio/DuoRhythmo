using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Realtime))]
public class RealTimeInstance : MonoBehaviour {
    private static RealTimeInstance _instance;
    public static RealTimeInstance Instance => _instance;

    private Realtime _realtime;
    public List<int> clientIds = new List<int>();
    public Dictionary<int, GameObject> clients = new Dictionary<int, GameObject>();
    [SerializeField] private GameObject networkManagerPrefab;
    public GameObject networkManager;
    private NetworkManagerSync _networkManagerSync;
    public bool isConnected;
    public int numberPlayers, previousNumberPlayers;
    public bool isSoloMode = true;
    public StringSync stringSync;

    [SerializeField] private GameObject realtimeInstancesHolderPrefab;
    private Transform _realtimeInstancesHolder;
    [SerializeField] private string[] roomNames = new string[10];
    private int roomToJoinIndex;

    [SerializeField] private GameObject playerCanvasPrefab;
    [SerializeField] private Transform playersHolder;
    public bool isNewPlayer = true;


    private void Awake() {
        _instance = this;
        _realtime = GetComponent<Realtime>();
        RegisterToEvents();
    }
    
    public IEnumerator CheckNumberOfPlayers() {
        while (true) {
            numberPlayers = _realtime.room.datastore.prefabViewModels.Count / 2;
            int smallestOwnerId = 10;
            foreach (var viewModel in _realtime.room.datastore.prefabViewModels) {
                var networkManager = viewModel.realtimeView.gameObject.GetComponent<NetworkManagerSync>();
                if (networkManager) {
                    var realtimeView = networkManager.GetComponent<RealtimeView>();
                    if (realtimeView) {
                        var ownerId = realtimeView.ownerIDSelf;
                        if (smallestOwnerId > ownerId) smallestOwnerId = ownerId;
                    }
                }
            }
            if(MasterManager.Instance.localPlayerNumber == smallestOwnerId) stringSync.SetMessage(MasterManager.Instance.timer.timer.ToString());
            yield return new WaitForSeconds(0.2f);
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
        _realtime.didDisconnectFromRoom += DidDisconnectToRoom;
    }

    private void DidDisconnectToRoom(Realtime realtime) {
        _realtime.room.connectionStateChanged -= ConnectionStateChanged;
    }

    private void ConnectionStateChanged(Room room, Room.ConnectionState previousconnectionstate, Room.ConnectionState connectionstate) {
        if(_realtime.room.connectionState == Room.ConnectionState.Error) Play();
    }


    private void DidConnectToRoom(Realtime realtime) {
        networkManager = Realtime.Instantiate(networkManagerPrefab.name, true, true);
        var realtimeView = networkManager.GetComponent<RealtimeView>();
        realtimeView.RequestOwnership();
        MasterManager.Instance.localPlayerNumber = realtimeView.ownerIDSelf;
        if (_realtime.room.datastore.prefabViewModels.Count < 3 && MasterManager.Instance.localPlayerNumber == 0)
            MasterManager.Instance.isFirstPlayer = true;
        isConnected = true;
        var gfx = Realtime.Instantiate(playerCanvasPrefab.name);
        gfx.GetComponent<RealtimeView>().RequestOwnership();
        gfx.GetComponent<RealtimeTransform>().RequestOwnership();
        stringSync.SetNewPlayerUpdateTime(MasterManager.Instance.localPlayerNumber);

        StartCoroutine(CheckNumberOfPlayers());
        StartCoroutine(SeniorPlayer());
    }

    IEnumerator SeniorPlayer() {
        yield return new WaitForSeconds(5.0f);
        isNewPlayer = false;
    }

    private void OnDisable() {
        _realtime.didConnectToRoom -= DidConnectToRoom;
    }
}