using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMatchmakingManager : MonoBehaviour {
    private static UiMatchmakingManager _instance;
    public static UiMatchmakingManager Instance => _instance;

    private void Start() {
        print("Initializing uimatchmakingmanager");
        if (_instance == null) _instance = this;
    }

    public void SetMatchmakingStatusText(string text) {
        
    }
}
