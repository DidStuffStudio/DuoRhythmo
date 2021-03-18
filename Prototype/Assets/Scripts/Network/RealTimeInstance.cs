using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Normal.Realtime;
using UnityEngine;

public class RealTimeInstance : MonoBehaviour {
    private static RealTimeInstance _instance;
    public static RealTimeInstance Instance => _instance;

    private void Awake() => _instance = this;
}
