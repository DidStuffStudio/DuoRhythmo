using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.VFX;

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
        private int _numberOfNodes = 4;
        public List<DidStuffNode> nodes = new List<DidStuffNode>();
        private int _subNodeIndex; // the subnodes on the other panels that this nodemanager corresponds to
        [SerializeField] private TextMeshProUGUI drumText;
        [SerializeField] private RectTransform rhythmIndicator;
        private DrumType _drumType;
        private EuclideanRhythm _euclideanRhythm;
        private int[] _savedValues = new int[16];
        private bool _canRotate = true;
        private List<float> _nodeAngles = new List<float>();
        public float currentRotation = 0.0f;
        private AudioSource _audioSource;
        private List<AudioClip> _drumSamples = new List<AudioClip>();
        private Animator _rhythmAnimator;
        private DidStuffSoloButton _soloButton;
        private EuclideanButton _euclideanButton;
        private static readonly int Rps = Animator.StringToHash("RPS");
        private List<int> _panelIndices = new List<int>{0, 1, 2, 3, 4};
        [SerializeField] private List<AbstractDidStuffButton> ColorCodedButtons;
        [SerializeField] private List<GameObject> voteSkipToasts = new List<GameObject>();
        private Color drumColor { get; set; }
        private Color[] defaultColor { get; set; }
        
        public List<float> NodeAngles
        {
            get => _nodeAngles;
            set => _nodeAngles = value;
        }

        public DrumType TypeOfDrum
        {
            get => _drumType;
            set => _drumType = value;
        }

        public int SubNodeIndex
        {
            get => _subNodeIndex;
            set => _subNodeIndex = value;
        }

        public int NumberOfNodes => _numberOfNodes;

        private void Awake() {
            _euclideanRhythm = GetComponent<EuclideanRhythm>();
            _audioSource = GetComponent<AudioSource>();
            _rhythmAnimator = rhythmIndicator.GetComponent<Animator>();
            _soloButton = GetComponentInChildren<DidStuffSoloButton>();
            _euclideanButton = GetComponentInChildren<EuclideanButton>();
            GetComponentInParent<Canvas>().worldCamera = Camera.main;
        }

        private void Start()
        {
            SetBpm(MasterManager.Instance.Bpm);
        }

        private void OnEnable() {
            MasterManager.OnBpmChanged += SetBpm;
        }

        private void Update()
        {
            currentRotation = 360 - rhythmIndicator.localRotation.eulerAngles.z;
        }

        public void InitialisePanel(int i, AudioClip[] drumClips, AudioMixerGroup mixer ,Color[] defaultColors, Color activeColor, int numberNodes, string drumName, VisualEffect vfx)
        {
            _numberOfNodes = numberNodes;
            SubNodeIndex = i;
            defaultColor = defaultColors;
            drumColor = activeColor;
            SetDrumType(i, drumClips, mixer);
            SetUpNamesAndColours(drumName);
            InitialiseNodes(vfx);
            InitialiseSoloButton();
            _panelIndices.Remove((int) _drumType);
        }
        
        private void SetDrumType(int i, AudioClip[] clips, AudioMixerGroup mixer)
        {
            TypeOfDrum = (DrumType) i;
            _drumSamples = clips.ToList();
            _audioSource.outputAudioMixerGroup = mixer;

        }
        
        public void EnableToasts(int index, bool enable) => voteSkipToasts[index].SetActive(enable);
        public void DisableAllToasts()
        {
            foreach (var toast in voteSkipToasts) toast.SetActive(false);
        }        
        
        private void SetUpNamesAndColours(string drumName)
        {
            drumText.color = drumColor;
            drumText.text = drumName;
            foreach (AbstractDidStuffButton btn in ColorCodedButtons) {
                btn.SetActiveColoursExplicit(drumColor, defaultColor[0]);
            }
        }
        
        
        private void InitialiseNodes(VisualEffect vfx)
        {

            var toggleDefaultColor = false;
            // position all the nodes (existing nodes and to-be-created ones)
            for (int i = 0; i < nodes.Count; i++)
            {
                // if the current node doesn't exist, then create one and add it to the nodes list

                var n = nodes[i].GetComponentInChildren<DidStuffNode>();
                nodes[i].transform.parent.name = "Node " + (i + 1);
                n.nodeIndex = i;
                n.nodeManager = this;
                
                nodes[i].drumType = TypeOfDrum switch
                {
                    DrumType.Kick => DrumType.Kick,
                    DrumType.Snare => DrumType.Snare,
                    DrumType.HiHat => DrumType.HiHat,
                    DrumType.Tom => DrumType.Tom,
                    DrumType.Cymbal => DrumType.Cymbal,
                    _ => nodes[i].drumType
                };

                if(i % (_numberOfNodes / 4) == 0) toggleDefaultColor = !toggleDefaultColor;

                var currentDefaultColor = toggleDefaultColor ? defaultColor[0] : defaultColor[1];
                
                var btn = nodes[i].GetComponentInChildren<DidStuffNode>();
                btn.SetActiveColoursExplicit(drumColor, currentDefaultColor);

                n.InitialiseSubNodes();
                n.SetText((i + 1).ToString());
                NodeAngles.Add(n.GetAngle());
                nodes[i].SetVfx(vfx);
                nodes[i].nodeInitialised = true;
            }
            rhythmIndicator.gameObject.GetComponentInChildren<Image>().enabled = true;
        }

        private void InitialiseSoloButton() => _soloButton.drumTypeIndex = (int)TypeOfDrum;
        
        
        public void PlayDrum(int d)
        {
            _audioSource.PlayOneShot(_drumSamples[d]);
        }

        public void ForceSoloOff() => _soloButton.ForceDeactivate();

        
        public void SetBpm(byte newBpm)
        {
            var rpm = (float) newBpm / 4;
            var rps = rpm / 60.0f;
            _rhythmAnimator.SetFloat(Rps, rps);
        }
        
        /// <summary>
        /// Update the subnodes when a node has been updated (activated / deactivated)
        /// </summary>
        /// <param name="nodeIndex">Node index that changed</param>
        /// <param name="activated">is this subnode to be activated or deactivated?</param>
        /// <param name="subNodeIndexNumber">NodeManager index - to set the correct subNode index</param>
        public void SetSubNode(int nodeIndex, bool activated, int panelIndex)
        {
            if (!_panelIndices.Contains(panelIndex)) return;
            var node = nodes[nodeIndex];
            var subNodeIndex = _panelIndices.IndexOf(panelIndex);
            var subNode = node.subNodes[subNodeIndex];
            subNode.color = activated ? MasterManager.Instance.drumColors[panelIndex] :  Color.clear;
        }


        public void RotateRhythm(bool right)
        {
            var nodesActive = new int[nodes.Count];
            if (right)
            {
                if (nodes[nodes.Count-1].IsActive) nodesActive[0] = 1;
                else nodesActive[0] = 0;
                for (int i = 0; i < nodes.Count -1; i++)
                {
                    if (nodes[i].IsActive) nodesActive[i + 1] = 1;
                    else nodesActive[i + 1] = 0;
                }
            }

            else
            {
                if (nodes[0].IsActive) nodesActive[nodes.Count-1] = 1;
                else nodesActive[nodes.Count-1] = 0;
                for (int i = nodes.Count-1; i > 0; i--)
                {
                    if (nodes[i].IsActive) nodesActive[i - 1] = 1;
                    else nodesActive[i - 1] = 0;
                } 
            }

            RotateNodesRoutine(nodesActive);
        }

        private void RotateNodesRoutine(int[] values)
        {
            if (!_canRotate) return;
            _canRotate = false;
            for (int i = 0; i < nodes.Count; i++)
            {
                var value = values[i];
                // if the euclidean value is 1, then it means it should be active, so activate
                if (value == 1 && !nodes[i].IsActive)
                {
                    nodes[i].ActivateButton();
                    MasterManager.Instance.UpdateSubNodes(i, true, (int)TypeOfDrum);
                }
                else if(value == 0 && nodes[i].IsActive)
                {
                    nodes[i].DeactivateButton();
                    MasterManager.Instance.UpdateSubNodes(i, false, (int)TypeOfDrum);
                }
            }
            
            _canRotate = true;
        }

        public void StoreRhythm()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].IsActive) _savedValues[i] = 1;
                else _savedValues[i] = 0;
            }
        }
    
        // euclidean rhythm
        public void StartEuclideanRhythmRoutine(bool activate) {
            ActivateEuclideanRhythm(activate);
        }

        private void ActivateEuclideanRhythm(bool activate) {
            if (!activate){
                for (int i = 0; i < nodes.Count; i++) {
                    var value = _savedValues[i];
                    // if the euclidean value is 1, then it means it should be active, so activate
                    if (value == 1 && !nodes[i].IsActive)
                    {
                        nodes[i].ActivateButton();
                        MasterManager.Instance.UpdateSubNodes(i, true, (int)TypeOfDrum);
                    }
                    else if(value == 0 && nodes[i].IsActive)
                    {
                        nodes[i].DeactivateButton();
                        MasterManager.Instance.UpdateSubNodes(i, false, (int)TypeOfDrum);
                    }
                
                }
            }
            else {
                _euclideanRhythm.GetEuclideanRhythm();
                for (int i = 0; i < nodes.Count; i++) {
                    var euclideanValue = _euclideanRhythm.euclideanValues[i];
                    // if the euclidean value is 1, then it means it should be active, so activate
                    if (euclideanValue == 1 && !nodes[i].IsActive)
                    {
                        nodes[i].ActivateButton();
                        MasterManager.Instance.UpdateSubNodes(i, true, (int)TypeOfDrum);
                    
                    }
                    else if(euclideanValue == 0 && nodes[i].IsActive)
                    {
                        nodes[i].DeactivateButton();
                        MasterManager.Instance.UpdateSubNodes(i, false, (int)TypeOfDrum);
                    }
                
                }
            }
        }

        private void OnDisable() {
            MasterManager.OnBpmChanged -= SetBpm;
        }
    }
}