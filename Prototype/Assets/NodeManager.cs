using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DrumType {
    Kick,
    Snare,
    HiHat,
    TomTom,
    Cymbal
}

public class NodeManager : MonoBehaviour {
    public int numberOfNodes = 4;
    public float radius = 3.0f;
    public GameObject nodePrefab;


    public List<Node> _nodes = new List<Node>();
    public int subNodeIndex; // the subnodes on the other panels that this nodemanager corresponds to
    public Color drumColor { get; set; }
    public Color defaultColor { get; set; }

    [SerializeField] private Text drumText;
    [SerializeField] private RectTransform _ryhtmIndicator;
    private float rotation = 0;
    public int bpm = 120;
    private float secondsFor360 = 1;

    public DrumType drumType;

    public ScreenSync _screenSync;

    [Range(0.0f, 100.0f)] [SerializeField] private float[] levels = new float[3];

    private string[] effectNames = new string[3];

    public SliderKnob[] sliders = new SliderKnob[3];

    private int previousEffectValue;

    public int drumIndex;

    private List<Vector2> nodeSpawningPositions = new List<Vector2>();

    private PanelMaster _panelMaster;

    private Color _inactiveHover, _activeHover;

    private bool _nodeIsSetup;

    public void SetUpNode() {
        Color.RGBToHSV(defaultColor, out var uH, out var uS, out var uV);
        uV -= 0.3f;
        Color.RGBToHSV(drumColor, out var aH, out var aS, out var aV);
        aV -= 0.3f;

        _inactiveHover = Color.HSVToRGB(uH, uS, uV);
        _activeHover = Color.HSVToRGB(aH, aS, aV);

        _inactiveHover.a = 1;
        _activeHover.a = 1;

        // _screenSync = GetComponentInParent<ScreenSync>();
        _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = false;
        if (RealTimeInstance.Instance.isSoloMode) {
            SpawnNodes();
            rotation = 0;
            _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = true;
        }
        else StartCoroutine(WaitUntilConnected());

        rotation = 0;

        for (int i = 0; i < sliders.Length - 1; i++) sliders[i].OnSliderChange += ChangeEffectValue;

        sliders[3].OnSliderChange += ChangeBpm;

        string[] effects = {"_Effect_1", "_Effect_2", "_Effect_3"};
        if (drumType == DrumType.Kick)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "Kick" + effects[i];
        else if (drumType == DrumType.Snare)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "Snare" + effects[i];
        else if (drumType == DrumType.HiHat)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "HiHat" + effects[i];
        else if (drumType == DrumType.TomTom)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "TomTom" + effects[i];
        else if (drumType == DrumType.Cymbal)
            for (int i = 0; i < effects.Length; i++)
                effectNames[i] = "Cymbol" + effects[i];

        _nodeIsSetup = true;
    }

    private IEnumerator WaitUntilConnected() {
        while (true) {
            if (RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }

        SpawnNodes();
        rotation = 0;
        _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = true;
    }

    private void Update() {
        if (!_nodeIsSetup) return;


        for (int i = 0; i < levels.Length; i++) AkSoundEngine.SetRTPCValue(effectNames[i], levels[i]);

        if (_screenSync.NumberOfNodes != numberOfNodes && RealTimeInstance.Instance.isConnected) {
            numberOfNodes = _screenSync.NumberOfNodes;
            if (numberOfNodes < 2) numberOfNodes = 2; // force the minimum amount of nodes to be 2
            SpawnNodes();
        }

        //bpm = _screenSync.Bpm;
    }

    private void LateUpdate() {
        // check for changes of the effects from the server side
        CheckForChangesSliders();
    }

    private void CheckForChangesSliders() {
        if (RealTimeInstance.Instance.isSoloMode) {
            levels[0] = sliders[0].currentValue;
            levels[1] = sliders[1].currentValue;
            levels[2] = sliders[2].currentValue;
            bpm = MasterManager.Instance.bpm;
            sliders[3].currentValue = bpm;
            sliders[3].UpdateSliderText();
        }
        else {
            levels[0] = sliders[0].currentValue = _screenSync.Effect1;
            levels[1] = sliders[1].currentValue = _screenSync.Effect2;
            levels[2] = sliders[2].currentValue = _screenSync.Effect3;
            bpm = _screenSync.Bpm;
            sliders[3].currentValue = bpm;
            sliders[3].UpdateSliderText();
        }

        // if(MasterManager.Instance.bpm != bpm) MasterManager.Instance.SetBpm(bpm);
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

        drumText.text = drumType.ToString();
        drumText.color = drumColor;
    }

    private void PositionNode(int i) {
        var radians =
            (i * 2 * Mathf.PI) / (-numberOfNodes) +
            (Mathf.PI / 2); // set them starting from 90 degrees = PI / 2 radians
        var y = Mathf.Sin(radians);
        var x = Mathf.Cos(radians);
        var spawnPos = new Vector2(x, y) * radius;
        nodeSpawningPositions.Add(spawnPos / radius);


        // if the current node doesn't exist, then create one and add it to the nodes list
        if (i >= _nodes.Count) {
            // var node = Realtime.Instantiate(nodePrefab.name, transform.position, Quaternion.identity, false, false, true, _realTime);
            var node = Instantiate(nodePrefab, transform);
            var n = node.GetComponent<Node>();
            // node.transform.parent = transform;
            // node.transform.position = transform.position;
            node.name = "Node " + (i + 1);
            _nodes.Add(n);
            n.indexValue = i;
            n._screenSync = _screenSync;
            _screenSync._nodes.Add(n);
            // node.GetComponent<Node>()._nodesVisualizer = _nodesVisualizer;
            n.nodeManager = this;

            _nodes[i].drumType = drumType switch {
                DrumType.Kick => DrumType.Kick,
                DrumType.Snare => DrumType.Snare,
                DrumType.HiHat => DrumType.HiHat,
                DrumType.TomTom => DrumType.TomTom,
                DrumType.Cymbal => DrumType.Cymbal,
                _ => _nodes[i].drumType
            };

            foreach (var customButton in _nodes[i].GetComponentsInChildren<CustomButton>()) //Set up button Colors
            {
                customButton.activeColor = drumColor;
                customButton.defaultColor = defaultColor;
                customButton.inactiveHoverColor = _inactiveHover;
                customButton.activeHoverColor = _activeHover;
                // customButton.hintColor = _panelMaster.hintColor;
            }
        }

        var rt = _nodes[i].GetComponent<RectTransform>();
        rt.localRotation = Quaternion.Euler(0, 0, i * (360.0f / -numberOfNodes));
        rt.anchoredPosition = spawnPos;
        rt.localScale = Vector3.one;
        var text = _nodes[i].transform.GetComponentInChildren<Text>();
        text.text = (i + 1).ToString();
    }

    private void FixedUpdate() {
        if (!MasterManager.Instance.gameSetUpFinished) return;
        var rpm = (float) bpm / (numberOfNodes); //12bpm at 12 nodes = 1 revolution per minute
        var rps = rpm / 60.0f;
        var revolutionsPerWaitingSeconds = rps * Time.fixedDeltaTime; //Convert to revolutions per millisecond
        var degreesPerWaitingSeconds = (360.0f - 360.0f / numberOfNodes) * revolutionsPerWaitingSeconds;
        rotation -= degreesPerWaitingSeconds;
        _ryhtmIndicator.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    void ChangeBpm(int index) {
        var sliderValue = (int) sliders[3].currentValue;
        bpm = sliderValue;
        MasterManager.Instance.SetBpm(bpm);
        // _screenSync.SetBpm(sliderValue);
    }

    private void ChangeEffectValue(int index) {
        var sliderValue = (int) sliders[index].currentValue;
        // levels[index] = sliderValue;
        if (!RealTimeInstance.Instance.isSoloMode) _screenSync.SetEffectValue(index, sliderValue);
    }

    /// <summary>
    /// Update the subnodes when a node has been updated (activated / deactivated)
    /// </summary>
    /// <param name="nodeIndex">Node index that changed</param>
    /// <param name="activated">is this subnode to be activated or deactivated?</param>
    /// <param name="subNodeIndexNumber">NodeManager index - to set the correct subNode index</param>
    public void SetSubNode(int nodeIndex, bool activated, int subNodeIndexNumber) {
        print("Setting subnode");
        var node = _nodes[nodeIndex];
        var subNode = node.subNodes[subNodeIndexNumber];
        if (activated) subNode.color = MasterManager.Instance.drumColors[subNodeIndexNumber];
        else subNode.color = defaultColor;
    }
}