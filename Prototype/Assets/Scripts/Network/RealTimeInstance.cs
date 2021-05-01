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
        StartCoroutine(CheckNumberOfPlayers());
    }

    public IEnumerator CheckNumberOfPlayers() {
        while (true) {
            var players = FindObjectsOfType<Player>();
            numberPlayers = players.Length;
            if (numberPlayers != previousNumberPlayers) {
                var counter = 0; 
                // foreach (var possiblyConnectedPlayer in MasterManager.Instance.dataMaster.conectedPlayers) {
                //     // if (possiblyConnectedPlayer == 1) counter++;
                //     possiblyConnectedPlayer = 1;
                // }
                
                _testStringSync.SetMessage(TestStringSync.MessageTypes.REQUEST_PLAYER_NUMBERS);
                /*for (int i = 0; i < MasterManager.Instance.dataMaster.conectedPlayers.Length; i++) {
                    if (numberPlayers > i) MasterManager.Instance.dataMaster.conectedPlayers[i] = 1;
                    else MasterManager.Instance.dataMaster.conectedPlayers[i] = 0;
                    // if (MasterManager.Instance.dataMaster.conectedPlayers[i] == 1) counter++;
                }*/

                // if (counter != numberPlayers) {
                //     // there's something off - someone has disconnected
                //     MasterManager.Instance.dataMaster.
                // }
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
        
        networkManager = Realtime.Instantiate(networkManagerPrefab.name);
        
        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length; // get the number of players
        //MasterManager.Instance.localPlayerNumber =
            //numberPlayers - 1; // set this local player's player number to the current player number (index value)
        

        var gfx = Realtime.Instantiate(playerCanvasPrefab.name, true, true, true);
        gfx.transform.GetComponent<RealtimeTransform>().RequestOwnership();
        if (numberPlayers == 1)
        {
            MasterManager.Instance.localPlayerNumber = 0;
            MasterManager.Instance.player.hasPlayerNumber = true;
        }
        _testStringSync.SetMessage(TestStringSync.MessageTypes.NEW_PLAYER_CONNECTED+MasterManager.Instance.localPlayerNumber);

    }
    
    private void OnDisable()
    {
        _realtime.didConnectToRoom -= DidConnectToRoom;
    }

}