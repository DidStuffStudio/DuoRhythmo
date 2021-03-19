using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Normal.Realtime;
using UnityEngine;

public class RealTimeInstance : MonoBehaviour {
    private static RealTimeInstance _instance;
    public static RealTimeInstance Instance => _instance;
    private Realtime _realtime;
    public bool nodesInstantiated;
    public bool isConnected;
    
    private void Awake()
    {
        _instance = this;
        _realtime = GetComponent<Realtime>();
    }

   
    private void Update()
    {
        if (!_realtime.connected) return;
        isConnected = true;
    }
}
