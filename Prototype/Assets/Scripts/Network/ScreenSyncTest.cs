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
    private void Start() {
        _screenSync = GetComponent<ScreenSync>();
        _previousNodes = new int[_nodes.Length];
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

        for (int i = 0; i < _nodes.Length; i++) {
            if(_nodes[i] != _previousNodes[i]) {
                print("NOT EQUAL");
                _previousNodes[i] = _nodes[i];
                _screenSync.SetArrayOfNodes(_nodes);
                break;
            }
        }
    }
}
