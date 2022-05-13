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


        public List<DidStuffNode> _nodes = new List<DidStuffNode>();
        public int subNodeIndex; // the subnodes on the other panels that this nodemanager corresponds to
        public Color drumColor { get; set; }
        public Color defaultColor { get; set; }

        [SerializeField] private Text drumText;
        [SerializeField] private RectTransform rhythmIndicator;
        private float rotation = 0;
        private int bpm = 120;
        private float secondsFor360 = 1;
        private Color noAlpha = Color.clear;

        public DrumType drumType;
        
        [Range(0.0f, 100.0f)] [SerializeField] private float[] levels = new float[4];

        private string[] effectNames = new string[4];
        
        
        private int previousEffectValue;
        private Color _inactiveHover, _activeHover;
        private bool _nodeIsSetup;
        private EuclideanRhythm _euclideanRhythm;
        private int[] _savedValues = new int[16];
        private bool canRotate = true;
        public List<float> _nodeangles = new List<float>();
        public float currentRotation = 0.0f;
        private AudioSource _audioSource;
        private List<AudioClip> _drumSamples = new List<AudioClip>();
        private Animator _rhythmAnimator;
        private DidStuffSoloButton _soloButton;
        private EuclideanButton _euclideanButton;
        private static readonly int Rps = Animator.StringToHash("RPS");

        private void Awake() {
            _euclideanRhythm = GetComponent<EuclideanRhythm>();
            _audioSource = GetComponent<AudioSource>();
            _rhythmAnimator = rhythmIndicator.GetComponent<Animator>();
            _soloButton = GetComponentInChildren<DidStuffSoloButton>();
            _euclideanButton = GetComponentInChildren<EuclideanButton>();
            GetComponentInParent<Canvas>().worldCamera = Camera.main;
        }
    
        
        
        public void ForceSoloOff() => _soloButton.ForceDeactivate();
        public void SetUpNode() {
            
            InitialiseNodes();
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
                    effectNames[i] = "Cymbal" + effects[i];

            
            _nodeIsSetup = true;
        }

        public void SetDrumType(int i, AudioClip[] clips, AudioMixerGroup mixer)
        {
            drumType = (DrumType) i;
            _drumSamples = clips.ToList();
            _audioSource.outputAudioMixerGroup = mixer;

        }
        
        private void InitialiseNodes() {
            numberOfNodes = MasterManager.Instance.numberOfNodes;

            // position all the nodes (existing nodes and to-be-created ones)
            for (int i = 0; i < _nodes.Count; i++)
            {
                // if the current node doesn't exist, then create one and add it to the nodes list

                var n = _nodes[i].GetComponentInChildren<DidStuffNode>();
                _nodes[i].transform.parent.name = "Node " + (i + 1);
                n.nodeIndex = i;
                n.nodeManager = this;
                
                _nodes[i].drumType = drumType switch
                {
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
                
                n.InitialiseSubNodes();
                _nodeangles.Add(n.GetAngle());
                _nodes[i].nodeInitialised = true;
            }
            rotation = 0.0f;
            rhythmIndicator.gameObject.GetComponentInChildren<Image>().enabled = true;
        }

        public void InitialiseSoloButton() => _soloButton.drumTypeIndex = (int)drumType;

        public void InitialiseEuclideanButton() =>
            _euclideanButton.SetIncrementColors(drumColor, defaultColor);
        
        public void PlayDrum(int drumType)
        {
            _audioSource.PlayOneShot(_drumSamples[drumType]);
        }

        public void SetBpm(int bpm)
        {
            var rpm = (float) bpm / (numberOfNodes); //12bpm at 12 nodes = 1 revolution per minute
            var rps = rpm / 60.0f;
            rps *= 4;
            _rhythmAnimator.SetFloat(Rps, rps);
        }

        private void Update()
        {
            currentRotation = 360 - rhythmIndicator.localRotation.eulerAngles.z;
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
                if (value == 1 && !_nodes[i].IsActive)
                {
                    _nodes[i].ActivateButton();
                    MasterManager.Instance.UpdateSubNodes(i, true, (int)drumType);
                }
                else if(value == 0 && _nodes[i].IsActive)
                {
                    _nodes[i].DeactivateButton();
                    MasterManager.Instance.UpdateSubNodes(i, false, (int)drumType);
                }
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
                        MasterManager.Instance.UpdateSubNodes(i, true, (int)drumType);
                    }
                    else if(value == 0 && _nodes[i].IsActive)
                    {
                        _nodes[i].DeactivateButton();
                        MasterManager.Instance.UpdateSubNodes(i, false, (int)drumType);
                    }
                
                }
            }
            else {
                _euclideanRhythm.GetEuclideanRhythm();
                for (int i = 0; i < _nodes.Count; i++) {
                    var euclideanValue = _euclideanRhythm.euclideanValues[i];
                    // if the euclidean value is 1, then it means it should be active, so activate
                    if (euclideanValue == 1 && !_nodes[i].IsActive)
                    {
                        _nodes[i].ActivateButton();
                        MasterManager.Instance.UpdateSubNodes(i, true, (int)drumType);
                    
                    }
                    else if(euclideanValue == 0 && _nodes[i].IsActive)
                    {
                        _nodes[i].DeactivateButton();
                        MasterManager.Instance.UpdateSubNodes(i, false, (int)drumType);
                    }
                
                }
            }
        }
    }
}