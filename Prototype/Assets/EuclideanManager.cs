using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.UI;

public enum DrumType {
    kick,
    snare,
    hiHat,
    tomTom
}

[RequireComponent(typeof(EuclideanRythm))]
public class EuclideanManager : MonoBehaviour {
    private EuclideanRythm _euclideanRythm;
    public InteractionMethod interactionMethod;
    public int numberOfNodes = 4;
    private int previousNumberOfNodes;
    public float radius = 3.0f;
    public GameObject nodePrefab;
    private List<Node> _nodes = new List<Node>();
    [SerializeField] private RectTransform _ryhtmIndicator;
    private float rotation = 0;
    public int bpm = 120;
    private float secondsFor360 = 1;

    private List<int> _storedRhythm = new List<int>();
    public DrumType drumType;
    [SerializeField] private ScreenSync _screenSync;

    [SerializeField] private Realtime _realTime;

    public float waitingTime = 1;
    private float beatTime;


    [Range(0.0f, 100.0f)] [SerializeField] private float reverbLevel;
    [Range(0.0f, 100.0f)] [SerializeField] private float distortionLevel;
    [Range(0.0f, 100.0f)] [SerializeField] private float vibratoLevel;

    private void Start() {
        _euclideanRythm = GetComponent<EuclideanRythm>();
        previousNumberOfNodes = numberOfNodes;
        StartCoroutine(WaitUntilConnected());
        // _realTime = RealTimeInstance.Instance.GetComponent<Realtime>();
    }

    private IEnumerator WaitUntilConnected() {
        while (!_realTime.connected) {
            yield return new WaitForEndOfFrame();
        }

        SpawnNodes();
        rotation = 0;
    }

    private void Update() {
        AkSoundEngine.SetRTPCValue("Kick_Reverb_Level", reverbLevel);
        AkSoundEngine.SetRTPCValue("Vibrato", vibratoLevel);
        AkSoundEngine.SetRTPCValue("Distortion_Level", distortionLevel);

        if (_screenSync.NumberOfNodes != numberOfNodes && _realTime.connected) {
            numberOfNodes = _screenSync.NumberOfNodes;
            if (numberOfNodes < 2) numberOfNodes = 2; // force the minimum amount of nodes to be 2
            SpawnNodes();
        }

        bpm = _screenSync.Bpm;
    }


    private void SpawnNodes() {
        // if more nodes exist than is needed, delete them
        if (_nodes.Count > numberOfNodes) {
            for (int i = numberOfNodes - 1; i < _nodes.Count; i++) {
                Destroy(_nodes[i].gameObject);
                _nodes.Remove(_nodes[i]);
            }

            // update the rotation value of the rhythm signifier so that it makes sense
            var newRotation = 360.0f / numberOfNodes;
            var difference = rotation - newRotation;
            rotation -= difference;
        }

        // position all the nodes (existing nodes and to-be-created ones)
        for (int i = 0; i < numberOfNodes; i++) {
            PositionNode(i);
        }
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
            var node = Realtime.Instantiate(nodePrefab.name, transform.position, Quaternion.identity, true, false, true, _realTime);
            // var node = Instantiate(nodePrefab, transform);
            
            node.transform.parent = transform;
            node.transform.position = transform.position;
            node.name = "Node " + (i + 1).ToString();
            _nodes.Add(node.GetComponent<Node>());

            _nodes[i].drumType = drumType switch {
                DrumType.kick => DrumType.kick,
                DrumType.snare => DrumType.snare,
                DrumType.hiHat => DrumType.hiHat,
                DrumType.tomTom => DrumType.tomTom,
                _ => _nodes[i].drumType
            };
            _nodes[i].interactionMethod = interactionMethod switch {
                InteractionMethod.contextSwitch => InteractionMethod.contextSwitch,
                InteractionMethod.dwellFeedback => InteractionMethod.dwellFeedback,
                InteractionMethod.spock => InteractionMethod.spock,
                _ => _nodes[i].interactionMethod
            };
        }

        var rt = _nodes[i].GetComponent<RectTransform>();
        rt.localRotation = Quaternion.Euler(0, 0, i * (360 / -numberOfNodes));
        rt.anchoredPosition = spawnPos;
        rt.localScale = Vector3.one;
        var text = _nodes[i].transform.GetChild(0).GetChild(0).GetComponent<Text>();
        text.text = (i + 1).ToString();

    }

    private void FixedUpdate() {
        if (!_realTime.connected) return;
        beatTime += Time.fixedDeltaTime;
        float rpm = (float)bpm / (numberOfNodes); //12bpm at 12 nodes = 1 revolution per minute
        var rps = (float) rpm / 60.0f;
        var revolutionsPerWaitingSeconds = rps * Time.fixedDeltaTime; //Convert to revolutions per millisecond
        var degreesPerWaitingSeconds = (360.0f - 360.0f / numberOfNodes) * revolutionsPerWaitingSeconds;
        rotation -= degreesPerWaitingSeconds;
        _ryhtmIndicator.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
