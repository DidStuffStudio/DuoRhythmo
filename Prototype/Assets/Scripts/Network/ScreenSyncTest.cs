using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class ScreenSyncTest : MonoBehaviour {
    [SerializeField] private int _numberOfNodes = 8;
    private int _previousNumberOfNodes = 0;
    [SerializeField] private int _bpm = 120;
    private int _previousBpm;
    private ScreenSync _screenSync;
    private void Start() {
        _screenSync = GetComponent<ScreenSync>();
    }

    private void Update() {
        if (_bpm != _previousBpm) {
            _screenSync.SetBpm(_bpm);
            _previousBpm = _bpm;
        }

        if (_numberOfNodes != _previousNumberOfNodes) {
            _screenSync.SetNumberOfNodes(_numberOfNodes);
            _previousNumberOfNodes = _numberOfNodes;
        }
    }
}

