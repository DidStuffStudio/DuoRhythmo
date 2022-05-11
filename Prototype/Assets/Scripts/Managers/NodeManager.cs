using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using Mirror;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Managers
{
    public enum DrumType {
        Kick,
        Snare,
        HiHat,
        Tom,
        Cymbal
    }

    public class NodeManager : MonoBehaviour {
        public int numberOfNodes = 4;
        public float radius = 3.0f;
        public GameObject nodePrefab;


        public List<DidStuffNode> _nodes = new List<DidStuffNode>();
        public int subNodeIndex; // the subnodes on the other panels that this nodemanager corresponds to
        public Color drumColor { get; set; }
        public Color defaultColor { get; set; }

        [SerializeField] private Text drumText;
        [SerializeField] private RectTransform _ryhtmIndicator;
        private float rotation = 0;
        public int bpm = 120;
        private float secondsFor360 = 1;
        private Color noAlpha = Color.clear;

        public DrumType drumType;

        public ScreenSync _screenSync;

        [Range(0.0f, 100.0f)] [SerializeField] private float[] levels = new float[4];

        private string[] effectNames = new string[4];

        public DidStuffSliderKnob[] sliders = new DidStuffSliderKnob[4];

        public DidStuffSliderKnob bpmSlider;
        private int previousEffectValue;
        private PanelMaster _panelMaster;
        private Color _inactiveHover, _activeHover;
        private bool _nodeIsSetup;
        private EuclideanRythm _euclideanRythm;
        public OneShotButton[] navigationButtons = new OneShotButton[2];
        private int[] _savedValues = new int[16];
        private bool canRotate = true;
        public List<float> _nodeangles = new List<float>();
        public float currentRotation = 0.0f;
        private AudioSource _audioSource;
        private List<AudioClip> _drumSamples = new List<AudioClip>();
    
        private void Awake() {
            _euclideanRythm = GetComponent<EuclideanRythm>();
            _audioSource = GetComponent<AudioSource>();
        
        }
    
        public void SetUpNode() {
            
            _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = false;

            SpawnNodes();

            rotation = 0.0f;
            

            string[] effects = {"_Effect_1", "_Effect_2", "_Effect_3", "_Effect_4"};
            if (drumType == DrumType.Kick)
                for (int i = 0; i < effects.Length; i++)
                    effectNames[i] = "Kick" + effects[i];
            else if (drumType == DrumType.Snare)
                for (int i = 0; i < effects.Length; i++)
                    effectNames[i] = "Snare" + effects[i];
            else if (drumType == DrumType.HiHat)
                for (int i = 0; i < effects.Length; i++)
                    effectNames[i] = "HiHat" + effects[i];
            else if (drumType == DrumType.Tom)
                for (int i = 0; i < effects.Length; i++)
                    effectNames[i] = "TomTom" + effects[i];
            else if (drumType == DrumType.Cymbal)
                for (int i = 0; i < effects.Length; i++)
                    effectNames[i] = "Cymbol" + effects[i];

            
            _nodeIsSetup = true;
        }

        private void Update() {
            if (!_nodeIsSetup) return;
            
            if (_screenSync.NumberOfNodes != numberOfNodes && RealTimeInstance.Instance.isConnected) {
                numberOfNodes = _screenSync.NumberOfNodes;
                if (numberOfNodes < 2) numberOfNodes = 2; // force the minimum amount of nodes to be 2
                SpawnNodes();
            }
            
        }

        public void SetDrumType(int i, AudioClip[] clips, AudioMixerGroup mixer)
        {
            drumType = (DrumType) i;
            _drumSamples = clips.ToList();
            _audioSource.outputAudioMixerGroup = mixer;

        }
        
        private void SpawnNodes() {
            numberOfNodes = MasterManager.Instance.numberOfNodes;

            // position all the nodes (existing nodes and to-be-created ones)
            for (int i = 0; i < numberOfNodes; i++) {
                var radians =
                    (i * 2 * Mathf.PI) / (-numberOfNodes) +
                    (Mathf.PI / 2); // set them starting from 90 degrees = PI / 2 radians
                var y = Mathf.Sin(radians);
                var x = Mathf.Cos(radians);
                var spawnPos = new Vector2(x, y) * radius;
        
                // if the current node doesn't exist, then create one and add it to the nodes list
                if (i >= _nodes.Count) {
                    var node = Instantiate(nodePrefab, transform);
                    var n = node.GetComponentInChildren<DidStuffNode>();
                    node.name = "Node " + (i + 1);
                    _nodes.Add(n);
                    n.nodeIndex = i;
                    n.screenSync = _screenSync;
                    _screenSync._nodes.Add(n);
                    n.nodeManager = this;
                        

                    _nodes[i].drumType = drumType switch {
                        DrumType.Kick => DrumType.Kick,
                        DrumType.Snare => DrumType.Snare,
                        DrumType.HiHat => DrumType.HiHat,
                        DrumType.Tom => DrumType.Tom,
                        DrumType.Cymbal => DrumType.Cymbal,
                        _ => _nodes[i].drumType
                    };
                
                    foreach (var btn in _nodes[i].GetComponentsInChildren<DidStuffNode>()) //Set up button Colors
                    {
                        btn.SetActiveColoursExplicit(drumColor, defaultColor);
                    }
                }
                var rt = _nodes[i].GetComponent<RectTransform>();
                rt.localRotation = Quaternion.Euler(0, 0, i * (360.0f / -numberOfNodes));
                rt.anchoredPosition = spawnPos;
            
            }
        
        
        
        
            rotation = 0.0f;
            _ryhtmIndicator.gameObject.GetComponentInChildren<Image>().enabled = true;
        }

        public void PlayDrum(int drumType)
        {
            _audioSource.PlayOneShot(_drumSamples[drumType]);
        }


        private void FixedUpdate() {
            if (!MasterManager.Instance.gameSetUpFinished) return;
            var rpm = (float) bpm / (numberOfNodes); //12bpm at 12 nodes = 1 revolution per minute
            var rps = rpm / 60.0f;
            var revolutionsPerWaitingSeconds = rps * Time.fixedDeltaTime; //Convert to revolutions per millisecond
            var degreesPerWaitingSeconds = (360.0f - 360.0f / numberOfNodes) * revolutionsPerWaitingSeconds;
            rotation -= degreesPerWaitingSeconds * 4.0f;
            _ryhtmIndicator.localRotation = Quaternion.Euler(0, 0, rotation);
            currentRotation = -rotation % 360;
        }

        /// <summary>
        /// Update the subnodes when a node has been updated (activated / deactivated)
        /// </summary>
        /// <param name="nodeIndex">Node index that changed</param>
        /// <param name="activated">is this subnode to be activated or deactivated?</param>
        /// <param name="subNodeIndexNumber">NodeManager index - to set the correct subNode index</param>
        public void SetSubNode(int nodeIndex, bool activated, int subNodeIndexNumber) {
            var node = _nodes[nodeIndex];
            var subNode = node.subNodes[subNodeIndexNumber];
            subNode.color = activated ? MasterManager.Instance.drumColors[subNodeIndexNumber] : noAlpha;
        }


        public void RotateRhythm(bool right)
        {
            var nodesActive = new int[_nodes.Count];
            if (right)
            {
                if (_nodes[_nodes.Count-1].IsActive) nodesActive[0] = 1;
                else nodesActive[0] = 0;
                for (int i = 0; i < _nodes.Count -1; i++)
                {
                    if (_nodes[i].IsActive) nodesActive[i + 1] = 1;
                    else nodesActive[i + 1] = 0;
                }
            }

            else
            {
                if (_nodes[0].IsActive) nodesActive[_nodes.Count-1] = 1;
                else nodesActive[_nodes.Count-1] = 0;
                for (int i = _nodes.Count-1; i > 0; i--)
                {
                    if (_nodes[i].IsActive) nodesActive[i - 1] = 1;
                    else nodesActive[i - 1] = 0;
                } 
            }

            RotateNodesRoutine(nodesActive);
        }

        private void RotateNodesRoutine(int[] values)
        {
            if (!canRotate) return;
            canRotate = false;
            for (int i = 0; i < _nodes.Count; i++)
            {
                var value = values[i];
                // if the euclidean value is 1, then it means it should be active, so activate
                if (value == 1 && !_nodes[i].IsActive) _nodes[i].ActivateButton();
                else _nodes[i].DeactivateButton();
            }

            canRotate = true;
        }

        public void StoreRythm()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                if (_nodes[i].IsActive) _savedValues[i] = 1;
                else _savedValues[i] = 0;
            }
        }
    
        // euclidean rhythm
        public void StartEuclideanRhythmRoutine(bool activate) {
            ActivateEuclideanRhythm(activate);
        }

        private void ActivateEuclideanRhythm(bool activate) {
            if (!activate){
                for (int i = 0; i < _nodes.Count; i++) {
                    var value = _savedValues[i];
                    // if the euclidean value is 1, then it means it should be active, so activate
                    if (value == 1 && !_nodes[i].IsActive)
                    {
                        _nodes[i].ActivateButton();
                    }
                    else
                    {
                        _nodes[i].DeactivateButton();
                    }
                
                }
            }
            else {
                _euclideanRythm.GetEuclideanRythm();
                for (int i = 0; i < _nodes.Count; i++) {
                    var euclideanValue = _euclideanRythm._euclideanValues[i];
                    // if the euclidean value is 1, then it means it should be active, so activate
                    if (euclideanValue == 1 && !_nodes[i].IsActive)
                    {
                        _nodes[i].ActivateButton();
                    
                    }
                    else
                    {
                        _nodes[i].DeactivateButton();
                    }
                
                }
            }
        }

        public void SetEffectsFromServer(int effectIndex, int effectValue) {
            sliders[effectIndex].SetCurrentValue(effectValue);
            MasterManager.Instance.dataMaster.effectValues[(int)drumType, effectIndex] = effectValue;
        }
    }
}