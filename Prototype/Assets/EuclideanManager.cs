using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EuclideanRythm))]
public class EuclideanManager : MonoBehaviour {
    private EuclideanRythm _euclideanRythm;
    public int numberOfNodes = 4;
    private int previousNumberOfNodes;
    private bool _beatsUpdated = false;
    public float radius = 3.0f;
    public GameObject nodePrefab;
    private List<Node> _nodes = new List<Node>();
    [SerializeField] private RectTransform _ryhtmIndicator;
    private float rotation = 0;
    public int bpm = 120;
    private float secondsFor360 = 1;

    private AudioSource _audioSource;

    private List<int> _storedRhythm = new List<int>();

    public float secondsBetweenBeats = 0.25f;
    public bool isKick, isSnare;

    [SerializeField] private ScreenSync _screenSync;

    private void Start() {
        _euclideanRythm = GetComponent<EuclideanRythm>();
        _audioSource = GetComponent<AudioSource>();
        previousNumberOfNodes = numberOfNodes;
        SpawnNodes();
        //StartCoroutine(PlayNodes());
        StartCoroutine(Beats());
    }

    // private void Update() {
    //     // 120 beats per minute / 8 nodes = 15 beats per minute per node
    //     // 15 beats / 60s = 0.25  // full circle per second
    //
    //     // rotate a line around the 
    //
    //
    //     // change this functionality so it's an event instead of inside the update
    //     int pulses = 0;
    //     for (int i = 0; i < _nodesGameObjects.Length; i++) {
    //         if (_nodes[i].activated) pulses++;
    //     }
    //     // get the euclidean rhythm
    //     var er = _euclideanRythm.GetEuclideanRythm(steps: numberOfNodes, pulses: pulses).ToArray();
    //     // compare the euclidean rhythm array with the nodes array 
    //     for (int i = 0; i < _nodes.Length; i++) {
    //         if (!_nodes[i].activated && er[i] == 1) {
    //             _nodes[i].button.SetHint();
    //         }
    //         else if (_nodes[i].button.isHint) {
    //             _nodes[i].button.SetDefault();
    //         }
    //         
    //     }
    //
    // }

    private void Update() {
        if (_screenSync.NumberOfNodes != numberOfNodes) {
            numberOfNodes = _screenSync.NumberOfNodes;
            if (numberOfNodes < 2) numberOfNodes = 2; // force the minimum amount of nodes to be 2
            SpawnNodes();
        }
        
        if(_screenSync.Bpm != bpm) print(bpm = _screenSync.Bpm);
    }

    private void SpawnNodes() {
        // if more nodes exist than is needed, delete them
        if (_nodes.Count > numberOfNodes) {
            for (int i = numberOfNodes - 1; i < _nodes.Count; i++) {
                Destroy(_nodes[i].gameObject);
                _nodes.Remove(_nodes[i]);
            }

            // _nodes.RemoveRange(numberOfNodes - 1, _nodes.Count - numberOfNodes);

            // update the rotation value of the rhythm signifier so that it makes sense
            var newRotation = 360.0f / numberOfNodes;
            var difference = rotation - newRotation;
            rotation -= difference;

            _beatsUpdated = true;
        }

        // position all the nodes (existing nodes and to-be-created ones)
        for (int i = 0; i < numberOfNodes; i++) {
            PositionNode(i);
        }

        _beatsUpdated = false; // they have finished positioning - so start beats coroutine again
        // StartCoroutine(PlayNodes());
    }

    private void PositionNode(int i) {
        var radians =
            (i * 2 * Mathf.PI) / (-numberOfNodes) +
            (Mathf.PI / 2); // set them starting from 90 degrees = PI / 2 radians
        var y = Mathf.Sin(radians);
        var x = Mathf.Cos(radians);
        var spawnPos = new Vector2(x, y) * radius;

        // if the current node doesn't exist, then create one and add it to the nodes list
        if (i >= _nodes.Count) {
            var node = Instantiate(nodePrefab, transform) as GameObject;
            node.name = "Node " + (i + 1).ToString();
            _nodes.Add(node.GetComponent<Node>());
            _beatsUpdated = true;
            if (isKick) _nodes[i].isKick = true;
            else _nodes[i].isSnare = true;
        }

        var rt = _nodes[i].GetComponent<RectTransform>();
        rt.localRotation = Quaternion.Euler(0, 0, i * (360 / -numberOfNodes));
        rt.anchoredPosition = spawnPos;
        rt.localScale = Vector3.one;
        var text = _nodes[i].transform.GetChild(0).GetChild(0).GetComponent<Text>();
        text.text = (i + 1).ToString();
    }

    private IEnumerator Beats() {
        rotation = 0;
        while (true) {
            float secondsPerBeat = 60.0f / bpm;
            float waitingTimeSeconds = 0.01f;
            yield return new WaitForSeconds(waitingTimeSeconds);
            var radiansPerSecond = 2 * Mathf.PI / 60.0f * bpm; // angular frequency
            var degreesPerSecond = (Mathf.Rad2Deg * radiansPerSecond);
            rotation -= (degreesPerSecond * waitingTimeSeconds);
            _ryhtmIndicator.localRotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}