using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public enum DrumType {
    kick,
    snare,
    hiHat,
    tomTom,
    cymbal
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
    private ScreenSync _screenSync;

    [SerializeField] private Realtime _realTime;

    public float waitingTime = 1;
    private float beatTime;

    [Range(0.0f, 100.0f)] [SerializeField] private float[] levels = new float[3];

    private string[] effectNames = new string[3];

    public Slider[] sliders = new Slider[3];
    private bool effectsChangedOnServer = false;

    private int previousEffectValue;

    private void Start() {
        _euclideanRythm = GetComponent<EuclideanRythm>();
        previousNumberOfNodes = numberOfNodes;
        _screenSync = GetComponentInParent<ScreenSync>();
        _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = false;
        if (RealTimeInstance.Instance.isSoloMode)
        {
            SpawnNodes();
            rotation = 0;
            _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = true;
        }
        else StartCoroutine(WaitUntilConnected());
        
        rotation = 0;
        
        //_realTime = RealTimeInstance.Instance.GetComponent<Realtime>();
        int sliderIndex = 0;
        foreach (var slider in sliders) {
            var index = sliderIndex;
            slider.onValueChanged.AddListener(delegate { ChangeEffectValue(index: index); });
            sliderIndex++;
        }

        string[] effects = {"_Effect_1", "_Effect_2", "_Effect_3"};
        if (drumType == DrumType.kick)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "Kick" + effects[i];
        else if (drumType == DrumType.snare)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "Snare" + effects[i];
        else if (drumType == DrumType.hiHat)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "HiHat" + effects[i];
        else if (drumType == DrumType.tomTom)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "TomTom" + effects[i];
        else if (drumType == DrumType.cymbal)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "Cymbol" + effects[i];
    }

    private IEnumerator WaitUntilConnected() {
        while (true) {
            if(RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }
        SpawnNodes();
        rotation = 0;
        _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = true;
    }

    private void Update() {
        for (int i = 0; i < levels.Length; i++) AkSoundEngine.SetRTPCValue(effectNames[i], levels[i]);
        
        if (_screenSync.NumberOfNodes != numberOfNodes && _realTime.connected) {
            numberOfNodes = _screenSync.NumberOfNodes;
            if (numberOfNodes < 2) numberOfNodes = 2; // force the minimum amount of nodes to be 2
            SpawnNodes();
        }

        bpm = _screenSync.Bpm;
    }

    private void LateUpdate() {
        // check for changes of the effects from the server side
        CheckForChangesEffects();
    }

    private void CheckForChangesEffects() {
        sliders[0].value = _screenSync.Effect1;
        sliders[1].value = _screenSync.Effect2;
        sliders[2].value = _screenSync.Effect3;
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

    private void MessageReceived(Room room, int senderid, byte[] data, bool reliable) {
        if (_realTime.room.name != room.name) return;
        foreach (var d in data) {
            print(d);
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
            // var node = Realtime.Instantiate(nodePrefab.name, transform.position, Quaternion.identity, false, false, true, _realTime);
            var node = Instantiate(nodePrefab, transform);
            // node.transform.parent = transform;
            // node.transform.position = transform.position;
            node.name = "Node " + (i + 1).ToString();
            _nodes.Add(node.GetComponent<Node>());
            node.GetComponent<Node>().indexValue = i;
            _screenSync._nodes.Add(node.GetComponent<Node>());

            _nodes[i].drumType = drumType switch {
                DrumType.kick => DrumType.kick,
                DrumType.snare => DrumType.snare,
                DrumType.hiHat => DrumType.hiHat,
                DrumType.tomTom => DrumType.tomTom,
                DrumType.cymbal => DrumType.cymbal,
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
        float rpm = (float) bpm / (numberOfNodes); //12bpm at 12 nodes = 1 revolution per minute
        var rps = (float) rpm / 60.0f;
        var revolutionsPerWaitingSeconds = rps * Time.fixedDeltaTime; //Convert to revolutions per millisecond
        var degreesPerWaitingSeconds = (360.0f - 360.0f / numberOfNodes) * revolutionsPerWaitingSeconds;
        rotation -= degreesPerWaitingSeconds;
        _ryhtmIndicator.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    public void ChangeEffectValue(int index) {
        print("It's changed" + index);
        var sliderValue = (int) sliders[index].value;
        levels[index] = sliderValue;
        _screenSync.SetEffectValue(index, sliderValue);
        effectsChangedOnServer = false;
        
    }
}