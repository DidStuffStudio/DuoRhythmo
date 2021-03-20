using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Realtime))]
[RequireComponent(typeof(NetworkManagerSync))]
public class RealTimeInstance : MonoBehaviour {
    private static RealTimeInstance _instance;
    public static RealTimeInstance Instance => _instance;
    
    private Realtime _realtime;
    private NetworkManagerSync _networkManagerSync;
    public bool isConnected;
    public int numberPlayers;
    public bool isSoloMode = false;

    private void Awake() {
        _instance = this;
        _realtime = GetComponent<Realtime>();
        _networkManagerSync = GetComponent<NetworkManagerSync>();
        RegisterToEvents();
    }

    private void RegisterToEvents() {
        // Notify us when Realtime connects to or disconnects from the room
        _realtime.didConnectToRoom += DidConnectToRoom;
        _realtime.didDisconnectFromRoom += DidDisconnectFromRoom;
    }

    private void Update() {
        numberPlayers = _networkManagerSync.NumberPlayers;
    }

    private void DidConnectToRoom(Realtime realtime) {
        isConnected = true;
        _networkManagerSync.SetNumberOfPlayers(numberPlayers + 1);
    }
    
    private void DidDisconnectFromRoom(Realtime realtime) {
        isConnected = false;
        _networkManagerSync.SetNumberOfPlayers(numberPlayers - 1);
    }

    private void OnDisable() {
        _realtime.didConnectToRoom -= DidConnectToRoom;
        _realtime.didDisconnectFromRoom -= DidDisconnectFromRoom;
    }
}