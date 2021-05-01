using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

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

    public IEnumerator CheckNumberOfPlayers() {
        while (true) {
            var players = FindObjectsOfType<Player>();
            numberPlayers = players.Length;
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
        
        networkManager = Realtime.Instantiate(networkManagerPrefab.name);
        
        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length; // get the number of players
        MasterManager.Instance.localPlayerNumber =
            numberPlayers - 1; // set this local player's player number to the current player number (index value)
        

        var gfx = Realtime.Instantiate(playerCanvasPrefab.name, true, true, true);
        gfx.transform.GetComponent<RealtimeTransform>().RequestOwnership();


    }
    
    private void OnDisable()
    {
        _realtime.didConnectToRoom -= DidConnectToRoom;
    }

}