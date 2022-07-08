using System.Collections;
using System.Collections.Generic;
using DidStuffLab.Scripts.Audio;
using UnityEngine;

namespace DidStuffLab.Scripts.Managers {
    public class MasterManager : MonoBehaviour {

        public static MasterManager Instance { get; private set; }

        public string currentDrumKitName
        {
            get => _currentDrumKitName;
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

        private byte _bpm = 120;

        public byte Bpm {
            get => _bpm;
            set {
                _bpm = value;
                OnBpmChanged?.Invoke(_bpm);
            }
        }

        [Space] [Header("Player")] public Camera playerCamera;
        
        private float _startTime, _journeyLength;
        [SerializeField] private float positionSpeed = 10.0f;
        public Transform playerPositionDestination;
        public bool gameSetUpFinished; public bool isInPosition = false;
        
        
        [SerializeField] private string[] classicDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Crash"};
        [SerializeField] private string[] djembeDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Crash"};

        [SerializeField] private string[] electronicDrumNames =
            {"Kick", "Snare", "Open Hi-hat", "Closed Hi-hat", "Crash"};

        [SerializeField] private string[] handpanDrumNames = {"B note", "E note", "Ab note", "C# note", "Gb note"};
        [SerializeField] private string[] ambientDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Cymbal"};

        public SaveBeat saveBeat;
        public SaveIntoWav saveToWav;
        private Dictionary<int, string[]> _drumNames = new Dictionary<int, string[]>();
        private int _currentDrumKitIndex = 0;
        public AudioManager audioManager;
        public Camera screenShotCam;
        public Transform startTransform, destinationTransform;

        private string[] _drumKitNames = {"Rock drums", "Djembe", "Electronic", "Handpan", "Ambient"};
        private string _currentDrumKitName;

        public delegate void BpmChangedAction(byte newBpm);
        public static event BpmChangedAction OnBpmChanged;
        
        private void Awake() {
            // If there is an instance, and it's not me, kill myself.
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }

        private void Start() {

            _drumNames.Add(0, classicDrumNames);
            _drumNames.Add(1, djembeDrumNames);
            _drumNames.Add(2, electronicDrumNames);
            _drumNames.Add(3, handpanDrumNames);
            _drumNames.Add(4, ambientDrumNames);
            SwitchDrumKits(JamSessionDetails.Instance.DrumTypeIndex);
            Initialise();
        }


        public void PlayerReachedDestination()
        {
            isInPosition = true;
            carouselManager.InitialiseGraphicRaycast();
        }


        private void Initialise() {
            
            StartCoroutine(WaitToPositionCamera(0.5f));
            if (carouselManager.isSoloMode) carouselManager.panels = new List<GameObject>();
            InitialisePanels();
            gameSetUpFinished = true;
            if(JamSessionDetails.Instance.loadingBeat)JamSessionDetails.Instance.SetLoadedBeat();
            carouselManager.InitialiseBlur();
        }


        private void InitialisePanels()
        {
            for (var i = 0; i < numberInstruments; i++)
            {
                if (carouselManager.isSoloMode)
                {
                    carouselManager.panels.Add(nodePanels[i]);
                    carouselManager.panels.Add(effectPanels[i]);
                }
                else
                {
                    if (i % 2 == 1)
                    {
                        carouselManager.panels[i + 5] = nodePanels[i];
                        carouselManager.panels[i] = effectPanels[i];
                    }
                    else
                    {
                            carouselManager.panels[i] = nodePanels[i];
                            carouselManager.panels[i + 5] = effectPanels[i];
                    }
                    
                }
                
                StartCoroutine(SetUpNodeManagers(i));
                SetUpEffectsManagers(i);
            }
            carouselManager.ToggleVFX(true);
            gameSetUpFinished = true;
        }
       
        
        private void SetUpEffectsManagers(int i) {
            var effectsManager = effectPanels[i].GetComponentInChildren<EffectsManager>();
            effectsManagers.Add(effectsManager);
            effectPanels[i].name = "EffectsPanel_" + _drumNames[_currentDrumKitIndex][i];
            effectsManager.InitialisePanel(i, defaultNodeColors[0], drumColors[i], _bpm,numberInstruments, _drumNames[_currentDrumKitIndex][i]);
        }

        private IEnumerator SetUpNodeManagers(int i) {
            while (!audioManager.setUp) yield return new WaitForEndOfFrame(); // TODO --> Make the player wait in place until this is set up
            var nodeManager = nodePanels[i].GetComponentInChildren<NodeManager>();
            nodeManagers.Add(nodeManager);
            nodePanels[i].name = "NodesPanel_" + _drumNames[_currentDrumKitIndex][i];
            var clips = audioManager.SampleDictionary[_currentDrumKitIndex];
            var mixGroup = audioManager.mixers[i];
            nodeManager.InitialisePanel(i, clips, mixGroup, defaultNodeColors, drumColors[i], numberOfNodes,_drumNames[_currentDrumKitIndex][i], carouselManager._vfx);
        }


        private void SwitchDrumKits(int drumKitIndex) {
            _currentDrumKitIndex = drumKitIndex;
            _currentDrumKitName = _drumKitNames[drumKitIndex];
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

        public void Record(bool record)
        {
            for (var i = 0; i < numberInstruments; i++)
            {
                nodeManagers[i].RecordingAudio(record);
                effectsManagers[i].RecordingAudio(record);
            }
            if(record)saveToWav.StartRecording();
            else saveToWav.StopRecording();
            
        }

        private IEnumerator WaitToPositionCamera(float time) {
            while(!audioManager.setUp) yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(time);
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(playerCamera.transform.position, playerPositionDestination.position);
        }
    }
}