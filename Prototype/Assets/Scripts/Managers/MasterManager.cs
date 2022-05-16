using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Custom_Buttons.Did_Stuff_Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers {
    public class MasterManager : MonoBehaviour {
        private static MasterManager _instance;

        public static MasterManager Instance {
            get {
                if (_instance != null) return _instance;
                var masterManagerGameObject = new GameObject();
                _instance = masterManagerGameObject.AddComponent<MasterManager>();
                masterManagerGameObject.name = typeof(MasterManager).ToString();
                return _instance;
            }
        }

        public List<GameObject> nodePanels = new List<GameObject>();
        public List<GameObject> effectPanels = new List<GameObject>();


        [Header("Drums")] public int numberInstruments = 5;
        public int numberOfNodes = 16;

        public Color[] drumColors;
        public Color[] defaultNodeColors;

        // nodes stuff
        public List<NodeManager> nodeManagers = new List<NodeManager>();
        public List<EffectsManager> effectsManagers = new List<EffectsManager>();

        public CarouselManager carouselManager;

        public int bpm = 120;

        [Space] [Header("Player")] public Camera playerCamera;
        
        private float _startTime, _journeyLength;
        [SerializeField] private float positionSpeed = 10.0f;
        public Transform playerPositionDestination;
        public bool gameSetUpFinished; public bool isInPosition = false;

        [SerializeField] private GameObject mainSignifier;

        public GameObject exitButtonPanel;
        [SerializeField] private string[] classicDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Crash"};
        [SerializeField] private string[] djembeDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Crash"};

        [SerializeField] private string[] electronicDrumNames =
            {"Kick", "Snare", "Open Hi-hat", "Closed Hi-hat", "Crash"};

        [SerializeField] private string[] handpanDrumNames = {"B note", "E note", "Ab note", "C# note", "Gb note"};
        [SerializeField] private string[] ambientDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Cymbal"};
        private Dictionary<int, string[]> _drumNames = new Dictionary<int, string[]>();
        private int _currentDrumKitIndex = 0;
        public AudioManager audioManager;
        
        public Transform startTransform, destinationTransform, oppositeDestinationTransform;

        private void Awake() {
            if (_instance == null) _instance = this;
        }

        private void Start() {

            _drumNames.Add(0, classicDrumNames);
            _drumNames.Add(1, djembeDrumNames);
            _drumNames.Add(2, electronicDrumNames);
            _drumNames.Add(3, handpanDrumNames);
            _drumNames.Add(4, ambientDrumNames);
            SwitchDrumKits(JamSessionDetails.Instance.DrumTypeIndex);

            Initialize();
        }
        
        public void SetExitButtonActive(bool active) => exitButtonPanel.SetActive(active);

        public void PlayerReachedDestination() {
            isInPosition = true;
            //mainSignifier.SetActive(true);
            //carouselManager.InitialiseBlur();
            //SetExitButtonActive(true);
        }

        private void Initialize() {

            // create the rotations for the panels -->  (360.0f / (numberInstruments * 2) * -1)  degrees difference between each other in the Y axis
            Vector3[] panelRotations = new Vector3[10]; // number of instruments * 2
            var rotationValue =
                360.0f / (numberInstruments * 2) * -1; // if 5 instruments --> 360.0f / (5 * 2) * -1 = -36.0f; 
            for (int i = 0; i < panelRotations.Length; i++) {
                panelRotations[i] = new Vector3(0, i * rotationValue, 0);
            }

            InitialisePanels();
            StartCoroutine(WaitToPositionCamera(0.5f));
        }

        private void InitialisePanels() {
            
            for (int i = 0; i < nodePanels.Count; i++) {
                nodePanels[i].name = "NodesPanel_" + _drumNames[_currentDrumKitIndex][i];
                effectPanels[i].name = "EffectsPanel_" + _drumNames[_currentDrumKitIndex][i];
                AddPanelsToCarouselManager(i);
                SetUpEffectsManagers(i);
                SetUpNodeManagers(i);
            }
            gameSetUpFinished = true;
            carouselManager.ToggleVFX(true);
        }


        private void AddPanelsToCarouselManager(int index)
        {
            if (carouselManager.isSoloMode)
            {
                carouselManager.panels.Add(nodePanels[index]);
                carouselManager.panels.Add(effectPanels[index]);
            }
            else
            {
                for (int i = 0; i < nodePanels.Count; i++)
                {
                    if (i % 2 == 1) carouselManager.panels[i + 5] = nodePanels[i];
                    else carouselManager.panels[i] = nodePanels[i];
                }

                for (int i = 0; i < effectPanels.Count; i++)
                {
                    if (i % 2 == 0) carouselManager.panels[i + 5] = effectPanels[i];
                    else carouselManager.panels[i] = effectPanels[i];
                }
            }
        }
        
        private void SetUpEffectsManagers(int i) {
            var effectsManager = effectPanels[i].GetComponentInChildren<EffectsManager>();
            effectsManagers.Add(effectPanels[i].GetComponentInChildren<EffectsManager>());
            effectsManager.InitialisePanel(i, defaultNodeColors[0], drumColors[i], bpm,numberInstruments);
        }

        private void SetUpNodeManagers(int i) {
            var nodeManager = nodePanels[i].GetComponentInChildren<NodeManager>();
            nodeManagers.Add(nodeManager);
            var clips = audioManager.SampleDictionary[_currentDrumKitIndex];
            var mixGroup = audioManager.mixers[i];
            nodeManager.InitialisePanel(i, clips, mixGroup, defaultNodeColors, drumColors[i], numberOfNodes);
        }


        private void SwitchDrumKits(int drumKitIndex) {
            _currentDrumKitIndex = drumKitIndex;
        }

        public void ForceSoloOffGlobal()
        {
            for (int i = 0; i < nodePanels.Count; i++)
            {
                nodeManagers[i].ForceSoloOff();
                effectsManagers[i].ForceSoloOff();
            }
        }
        
        
        // whenever a nodes is activated / deactivated on any panel, call this method to update the corresponding subNode in the other NodeManagers
        public void UpdateSubNodes(int node, bool activated, int nodeManagerSubNodeIndex) {
            foreach (var nodeManager in nodeManagers) {
                nodeManager.SetSubNode(node, activated, nodeManagerSubNodeIndex);
            }
        }

        public void SetBpm(int value, EffectsManager effectsManager)
        {
            bpm = value;
            for (var index = 0; index < nodeManagers.Count; index++)
            {
                nodeManagers[index].SetBpm(bpm);
                if(effectsManagers[index] != effectsManager) effectsManagers[index].SetBpmSlider(bpm);
            }
        }

        private IEnumerator WaitToPositionCamera(float time) {
            yield return new WaitForSeconds(time);
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(playerCamera.transform.position, playerPositionDestination.position);
            mainSignifier.SetActive(false);
        }
    }
}