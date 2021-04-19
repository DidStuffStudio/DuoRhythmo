using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    private void LateUpdate() {
        if (isSoloMode) return;
        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length;
        // MasterManager.Instance.localPlayerNumber = numberPlayers;
        // numberPlayers = _realtimeInstancesHolder.childCount - 1;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        isConnected = true;
        networkManager = Realtime.Instantiate(networkManagerPrefab.name);
        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length; // get the number of players
        // realtime.Connect("Test room");
        // try to find the realtime instance of the realtimeInstancesHolder. If it doesn't exist, realtimeinstantiate it. Then use this as the 
        // if(numberPlayers == 0) _realtimeInstancesHolder = Realtime.Instantiate(realtimeInstancesHolderPrefab.name).transform;
        // _realtimeInstancesHolder = GameObject.FindWithTag("RealtimeInstancesHolder").transform;
        // networkManager.transform.SetParent(_realtimeInstancesHolder);
        // _networkManagerSync = networkManager.GetComponent<NetworkManagerSync>();
        // _networkManagerSync.PlayerConnected();
        // isConnected = true;
        MasterManager.Instance.localPlayerNumber = numberPlayers - 1; // set this local player's player number to the current player number (index value)
    }

    private void DidDisconnectFromRoom(Realtime realtime) {
        // _networkManagerSync.PlayerDisconnected();
        // _networkManagerSync = networkManager.GetComponent<NetworkManagerSync>();
        isConnected = false;
    }

    private void OnDisable() {
        _realtime.didConnectToRoom -= DidConnectToRoom;
        _realtime.didDisconnectFromRoom -= DidDisconnectFromRoom;
    }

    private void OnApplicationQuit() {
        _testStringSync.SetMessage("Disconnected," + MasterManager.Instance.localPlayerNumber);
        // _networkManagerSync.PlayerDisconnected();
        _realtime.Disconnect();
    }
}