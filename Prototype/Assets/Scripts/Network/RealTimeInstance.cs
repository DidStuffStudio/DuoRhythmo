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

    private void Awake() {
        _instance = this;
        _realtime = GetComponent<Realtime>();
        RegisterToEvents();
    }

    private void RegisterToEvents() {
        // Notify us when Realtime connects to or disconnects from the room
        _realtime.didConnectToRoom += DidConnectToRoom;
        _realtime.didDisconnectFromRoom += DidDisconnectFromRoom;
    }

    private void Update()
    {
        if (isSoloMode) return;
        // numberPlayers = _networkManagerSync.NumberPlayers;
        // print("This is the number of players according to the network model: " + numberPlayers);
        if(numberPlayers < 2) numberPlayers = GameObject.FindObjectsOfType<NetworkManagerSync>().Length;
        // print("This is the number of players according to the amount ot NetworkManager instances: " + numberPlayers);
    }

    private void DidConnectToRoom(Realtime realtime) {
        networkManager = Realtime.Instantiate(networkManagerPrefab.name, true);
        _networkManagerSync = networkManager.GetComponent<NetworkManagerSync>();
        _networkManagerSync.PlayerConnected();
        isConnected = true;
    }
    
    private void DidDisconnectFromRoom(Realtime realtime) {
        _networkManagerSync.PlayerDisconnected();
        _networkManagerSync = networkManager.GetComponent<NetworkManagerSync>();
        isConnected = false;
    }

    private void OnDisable() {
        _realtime.didConnectToRoom -= DidConnectToRoom;
        _realtime.didDisconnectFromRoom -= DidDisconnectFromRoom;
    }
}