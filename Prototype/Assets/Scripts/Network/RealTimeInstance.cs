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
        StartCoroutine(CheckNumberOfPlayers());
    }

    public IEnumerator CheckNumberOfPlayers() {
        while (true) {
            var players = FindObjectsOfType<Player>();
            numberPlayers = players.Length;
            if (numberPlayers != previousNumberPlayers) {
                var counter = 0;
                for (int i = 0; i < MasterManager.Instance.dataMaster.conectedPlayers.Length; i++) {
                    if (numberPlayers > i) MasterManager.Instance.dataMaster.conectedPlayers[i] = 1;
                    else MasterManager.Instance.dataMaster.conectedPlayers[i] = 0;
                }
                previousNumberPlayers = numberPlayers;
            }

            yield return new WaitForSeconds(0.1f);
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
    }

    private void DidConnectToRoom(Realtime realtime) {
        isConnected = true;

        networkManager = Realtime.Instantiate(networkManagerPrefab.name, true, true);
        var realtimeView = networkManager.GetComponent<RealtimeView>();
        MasterManager.Instance.localPlayerNumber = realtimeView.ownerIDSelf;

        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length; // get the number of players

        Realtime.Instantiate(playerCanvasPrefab.name, true, true);
        print("This is my player number: " + MasterManager.Instance.localPlayerNumber );
        stringSync.SetNewPlayerUpdateTime(MasterManager.Instance.localPlayerNumber);
        stringSync.SetNewPlayerConnected(MasterManager.Instance.localPlayerNumber);
        StartCoroutine(SeniorPlayer());
    }

    IEnumerator SeniorPlayer() {
        yield return new WaitForSeconds(1.0f);
        isNewPlayer = false;
    }

    private void OnDisable() => _realtime.didConnectToRoom -= DidConnectToRoom;
}