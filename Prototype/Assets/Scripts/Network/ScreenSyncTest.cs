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
    [SerializeField] private int [] _nodes;
    private int [] _previousNodes;

    private ScreenSync _screenSync;
    private void Start() => _screenSync = GetComponent<ScreenSync>();

    private void Update() {
        if (_bpm != _previousBpm) {
            _screenSync.SetBpm(_bpm);
            _previousBpm = _bpm;
        }

        if (_numberOfNodes != _previousNumberOfNodes) {
            _screenSync.SetNumberOfNodes(_numberOfNodes);
            _previousNumberOfNodes = _numberOfNodes;
        }
        
        if (_nodes != _previousNodes) {
            _screenSync.SetArrayOfNodes(_nodes);
            _previousNodes = _nodes;
        }
    }
}
