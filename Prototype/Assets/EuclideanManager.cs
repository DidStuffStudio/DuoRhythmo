using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EuclideanRythm))]
public class EuclideanManager : MonoBehaviour {
    private EuclideanRythm _euclideanRythm;
    public int numberOfNodes = 4;
    public float radius = 3.0f;
    public GameObject nodePrefab;
    private Node[] _nodes;
    [SerializeField] private RectTransform _ryhtmIndicator;
    private float rotation = -180;
    public int bpm = 120;
    private float secondsFor360 = 1;

    private List<int> _storedRhythm = new List<int>();

    public float secondsBetweenBeats = 0.25f;

    private void Start() {
        _euclideanRythm = GetComponent<EuclideanRythm>();
        SpawnNodes();
        StartCoroutine(PlayNodes());
        StartCoroutine(Beats());
    }

    private void Update() {
        // 120 beats per minute / 8 nodes = 15 beats per minute per node
        // 15 beats / 60s = 0.25  // full circle per second

        // rotate a line around the 


        // change this functionality so it's an event instead of inside the update
        // int pulses = 0;
        // for (int i = 0; i < _nodesGameObjects.Length; i++) {
        //     if (_nodes[i].activated) pulses++;
        // }
        // // get the euclidean rhythm
        // var er = _euclideanRythm.GetEuclideanRythm(steps: numberOfNodes, pulses: pulses).ToArray();
        // // compare the euclidean rhythm array with the nodes array 
        // for (int i = 0; i < _nodes.Length; i++) {
        //     if (!_nodes[i].activated && er[i] == 1) {
        //         _nodes[i].button.SetHint();
        //     }
        //     else if (_nodes[i].button.isHint) {
        //         _nodes[i].button.SetDefault();
        //     }
        //     
        // }
    }

    public void SpawnNodes() {
        _nodes = new Node[numberOfNodes];

        for (int i = 0; i < numberOfNodes; i++) {

            //var radians = Mathf.Deg2Rad * (i * (360 / numberOfNodes));
            var radians = (i * 2 * Mathf.PI) / (-numberOfNodes) + (Mathf.PI / 2);
            // if i == 0  --> radians = 0 / 8 + PI/2
            //  --> y = sin PI/2 = 1
            //  --> x = cos PI/2 = 0 // if i == 0  --> radians = 0 + PI/2

            // if i == 1  --> radians = 2PI / 1 + PI/2 = 0 + PI/2
            //  --> y = sin PI/2 = 1
            //  --> x = cos PI/2 = 0
            
            // if i == 2  --> radians = 2 * 2PI + PI/2 = 0 + PI/2
            //  --> y = sin PI/2 = 1
            //  --> x = cos PI/2 = 0

            var y = Mathf.Sin(radians);
            var x = Mathf.Cos(radians);
            var spawnPos = new Vector2(x, y) * radius;

            //  sin 0 = 0
            // cos 0 = 1
            // sin PI / 2 = 1
            // cos PI / 2 = 0


            var node = Instantiate(nodePrefab, transform) as GameObject;
            _nodes[i] = node.GetComponent<Node>();

            var rt = node.GetComponent<RectTransform>();

            rt.rotation = Quaternion.Euler(0, 0, i * (360 / -numberOfNodes));
            //rt.pivot = new Vector2(0.5f,0.5f);
            rt.anchoredPosition = spawnPos;

            rt.localScale = Vector3.one;
            var text = node.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            text.text = (i + 1).ToString();
        }

        // System.Array.Reverse(_nodes);
    }


    public IEnumerator PlayNodes() {
        while (true) {
            for (int i = 0; i < _nodes.Length; i++) {
                yield return new WaitForSecondsRealtime(60.0f / bpm);
                _nodes[i].Play();
                print("BEAT");
            }
        }
    }

    private IEnumerator Beats() {
        while (true) {
            float secondsPerBeat = 60.0f / bpm; // 0.5 seconds per beat
            float beatsPerSecond = bpm / 60.0f; // 2 beats per second
            yield return new WaitForSecondsRealtime(time: 0.01f);
            secondsFor360 = secondsPerBeat * numberOfNodes;
            rotation -= 360.0f / (secondsFor360 / 0.01f);
            // rotation -= 360 / 0.01f * secondsFor360;// / secondsFor360 * Time.deltaTime;
            _ryhtmIndicator.rotation = Quaternion.Euler(0, 0, rotation % 360);
        }
    }
}