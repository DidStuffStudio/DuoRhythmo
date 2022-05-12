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
        public List<NodeManager> _nodeManagers = new List<NodeManager>();
        public List<EffectsManager> _effectsManagers = new List<EffectsManager>();

        public CarouselManager carouselManager;

        public int bpm = 120;

        [Space] [Header("Player")] public Camera playerCamera;

        private Transform _playerPosition;
        private float startTime, journeyLength;
        [SerializeField] private float positionSpeed = 10.0f;
        public Transform playerStartPosition, playerPositionDestination;
        public int localPlayerNumber = -1;
        public bool gameSetUpFinished; public bool isInPosition = false;
        public bool isWaitingInLobby = true;

        [SerializeField] private GameObject mainSignifier;

        public GameObject exitButtonPanel;
        [SerializeField] private string[] classicDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Crash"};
        [SerializeField] private string[] djembeDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Crash"};

        [SerializeField] private string[] electronicDrumNames =
            {"Kick", "Snare", "Open Hi-hat", "Closed Hi-hat", "Crash"};

        [SerializeField] private string[] handpanDrumNames = {"B note", "E note", "Ab note", "C# note", "Gb note"};
        [SerializeField] private string[] ambientDrumNames = {"Kick", "Snare", "Hi-hat", "Tom", "Cymbal"};
        private Dictionary<int, string[]> drumNames = new Dictionary<int, string[]>();
        private Dictionary<float, bool> playerTransforms = new Dictionary<float, bool>();
        private int currentDrumKitIndex = 0;
        public AudioManager audioManager;

        private void Awake() {
            if (_instance == null) _instance = this;
        }

        private void Start() {
            
            
            for (int i = 0; i < numberInstruments * 2; i++) {
                playerTransforms.Add(-36 * i, false);
            }
            
            drumNames.Add(0, classicDrumNames);
            drumNames.Add(1, djembeDrumNames);
            drumNames.Add(2, electronicDrumNames);
            drumNames.Add(3, handpanDrumNames);
            drumNames.Add(4, ambientDrumNames);
            SwitchDrumKits(JamSessionDetails.Instance.DrumTypeIndex);

            Initialize();
        }
        
        public void SetExitButtonActive(bool active) => exitButtonPanel.SetActive(active);

        private void FixedUpdate() {
            if (!isWaitingInLobby && !isInPosition) {
                // Distance moved equals elapsed time times speed..
                float distCovered = (Time.time - startTime) * positionSpeed;
                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;
                var playerCameraTransform = playerCamera.transform;
                playerCamera.transform.position = Vector3.Lerp(playerCameraTransform.position,
                    playerPositionDestination.position, fractionOfJourney);
                playerCamera.transform.rotation = Quaternion.Lerp(playerCameraTransform.rotation,
                    playerPositionDestination.rotation, fractionOfJourney);

                if (Vector3.Distance(playerCameraTransform.position, playerPositionDestination.position) < 0.1f) {
                    isInPosition = true;
                    mainSignifier.SetActive(true);
                    //StartCoroutine(carouselManager.SwitchPanelRenderLayers());
                    carouselManager.InitialiseBlur();
                    SetExitButtonActive(true);
                }
            }
        }

        public void Initialize() {

            // create the rotations for the panels -->  (360.0f / (numberInstruments * 2) * -1)  degrees difference between each other in the Y axis
            Vector3[] panelRotations = new Vector3[10]; // number of instruments * 2
            var rotationValue =
                360.0f / (numberInstruments * 2) * -1; // if 5 instruments --> 360.0f / (5 * 2) * -1 = -36.0f; 
            for (int i = 0; i < panelRotations.Length; i++) {
                panelRotations[i] = new Vector3(0, i * rotationValue, 0);
            }

            InstantiatePanelsSoloMode();
            StartCoroutine(WaitToPositionCamera(0.5f));
        }

        private void InstantiatePanelsSoloMode() {
            Vector3 rotationValue = Vector3.zero;


            for (int i = 0; i < nodePanels.Count; i++) {
                nodePanels[i].name = "NodesPanel_" + drumNames[currentDrumKitIndex][i];
                rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1, 0);
                effectPanels[i].name = "EffectsPanel_" + drumNames[currentDrumKitIndex][i];

                carouselManager.panels.Add(nodePanels[i]);
                carouselManager.panels.Add(effectPanels[i]);
                rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1, 0);
                SetUpEffectsManager(i);
                SetUpNodeManager(i);
                SetUpNamesAndColours(nodePanels[i], i, "");
                SetUpNamesAndColours(effectPanels[i], i, " Effects");
            }
            //SetBpm(bpm);
            gameSetUpFinished = true;
            carouselManager.ToggleVFX(true);
        }


        private void SetUpNamesAndColours(GameObject parentToSearch, int index, string titleEnd) {
            foreach (Transform child in parentToSearch.transform.GetComponentsInChildren<Transform>()) {
                if (child.CompareTag("PanelTitle")) {
                    var t = child.GetComponent<Text>();
                    t.color = drumColors[index];
                    t.text = drumNames[currentDrumKitIndex][index] + titleEnd;
                }
                else if (child.CompareTag("UI_Drum_Colour"))
                    child.GetComponent<AbstractDidStuffButton>()
                        .SetActiveColoursExplicit(drumColors[index], defaultNodeColors[index]);
            }
        }

        private void SetUpEffectsManager(int i) {
            var effectsManager = effectPanels[i].GetComponentInChildren<EffectsManager>();
            _effectsManagers.Add(effectPanels[i].GetComponentInChildren<EffectsManager>());
            effectsManager.drumType = (DrumType) i;
            effectsManager.SetColours(drumColors[i], defaultNodeColors[i]);
            effectsManager.InitialiseSliders();
            effectsManager.InitialiseSoloButton();
            if(i == numberInstruments-1) effectsManager.InitialiseBpm(bpm);
        }

        private void SetUpNodeManager(int i) {
            var nodeManager = nodePanels[i].GetComponentInChildren<NodeManager>();
            _nodeManagers.Add(nodeManager);
            nodeManager.subNodeIndex = i;
            nodeManager.defaultColor = defaultNodeColors[i];
            nodeManager.drumColor = drumColors[i];
            var clips = audioManager.SampleDictionary[currentDrumKitIndex];
            var mixGroup = audioManager.mixers[i];
            nodeManager.SetDrumType(i, clips, mixGroup);
            nodeManager.SetUpNode();
            nodeManager.InitialiseSoloButton();
        }


        public void SwitchDrumKits(int drumKitIndex) {
            currentDrumKitIndex = drumKitIndex;
        }

        public void ForceSoloOffGlobal()
        {
            for (int i = 0; i < nodePanels.Count; i++)
            {
                _nodeManagers[i].ForceSoloOff();
                _effectsManagers[i].ForceSoloOff();
            }
        }
        
        public void SetPlayerPosition() {
            journeyLength = Vector3.Distance(playerStartPosition.position, playerPositionDestination.position);

            // rotate the parent of the camera around the degrees dependant on the number of players and number of instruments
            // players should be opposite to each other --> so differentiate between even and uneven numbers --> 180 degrees difference between them
            float degrees = 0;
            float degreesPerPlayer = 360.0f / (numberInstruments * 2);
            // if even player --> degrees = -36 * evenPlayerNumberCounter
            if (localPlayerNumber % 2 == 0) degrees = -degreesPerPlayer * (localPlayerNumber / 2.0f);
            // if uneven player --> degrees = 180 - 36 * unevenPlayerNumber
            else degrees = 180 - (degreesPerPlayer * ((localPlayerNumber - 1) / 2.0f));
        }


        // call this method when the players have connected
        /*private void InstantiatePanelsMultiplayer() {
            // instantiate and set up effects panels
            var effectsPanelsGo = new GameObject("Effects panels");
            effectsPanelsGo.transform.SetParent(carouselManager.transform);

            carouselManager.soloButtons = new DidStuffSolo[numberInstruments * 2];
            for (int i = 0; i < numberInstruments * 2; i++) {
                carouselManager.panels.Add(null);
            }

            Vector3 rotationValue = new Vector3(0, 180.0f, 0);
            for (int i = 0; i < numberInstruments; i++) {
                effectsPanels[i] = Instantiate(effectsPanelPrefab, transform.position, Quaternion.Euler(rotationValue));
                effectsPanels[i].transform.SetParent(effectsPanelsGo.transform);
                // effectsPanels[i].name = "EffectsPanel_" + drumNames[currentDrumKitIndex][i];
                rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1 * 2, 0);
                //carouselManager.panels.Add(effectsPanels[i]);
                var effectsSoloButtons = effectsPanels[i].GetComponentsInChildren<DidStuffSolo>();
                foreach (var soloButton in effectsSoloButtons) {
                    carouselManager.soloButtons[i + 5] = soloButton;
                    soloButton.drumTypeIndex = i;
                }

                foreach (Transform child in effectsPanels[i].transform.GetComponentsInChildren<Transform>()) {
                    if (child.CompareTag("PanelTitle"))
                        child.GetComponent<Text>().color =
                            new Color(drumColors[i].r, drumColors[i].g, drumColors[i].b, 255);
                    // if (child.CompareTag("PanelTitle")) child.GetComponent<Text>().text = drumNames[currentDrumKitIndex][i] + " Effects";
                    if (child.CompareTag("UI_Drum_Colour")) child.GetComponent<Text>().color = drumColors[i];
                }
            }


            // instantiate and set up nodes panels
            rotationValue = Vector3.zero;

            var nodesPanelsGo = new GameObject("Nodes panels");
            nodesPanelsGo.transform.SetParent(carouselManager.transform);
            for (int i = 0; i < numberInstruments; i++) {
                //nodesPanels[i] = Instantiate(nodesPanelPrefab, transform.position, Quaternion.Euler(rotationValue));
                nodesPanels[i] = Instantiate(nodesPanelPrefab, transform.position, Quaternion.identity);
                nodesPanels[i].transform.SetParent(nodesPanelsGo.transform);
                nodesPanels[i].name = "NodesPanel_" + (DrumType) i; //drumNames[currentDrumKitIndex][i];
                rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1 * 2, 0);

                foreach (Text child in nodesPanels[i].transform.GetComponentsInChildren<Text>()) {
                    if (child.transform.CompareTag("PanelTitle"))
                        child.color = new Color(drumColors[i].r, drumColors[i].g, drumColors[i].b, 255);
                    ;
                    // if (child.transform.CompareTag("PanelTitle")) child.text = drumNames[currentDrumKitIndex][i];
                }

                // set up nodes managers
                // set up the drum type, drum color, and default color of each nodeManager
                var nodeManager = nodesPanels[i].transform.GetComponentInChildren<NodeManager>();

                if (nodeManager == null)
                    Debug.LogError(_nodeManagers[i].name +
                                   " has an error getting its NodeManager. Check that it has a NodeManager");
                _nodeManagers.Add(nodeManager);
                nodeManager.subNodeIndex = i;
                nodeManager.defaultColor = defaultNodeColors[i];
                nodeManager.drumColor = drumColors[i];

                var clips = audioManager.SampleDictionary[currentDrumKitIndex];
                var mixGroup = audioManager.mixers[i];
                nodeManager.SetDrumType(i, clips, mixGroup);


                nodeManager.SetUpNode();

                var nodesSoloButtons = nodesPanels[i].GetComponentsInChildren<DidStuffSolo>();
                foreach (var soloButton in nodesSoloButtons) {
                    carouselManager.soloButtons[i] = soloButton;
                    soloButton.drumTypeIndex = i;
                }
            }


            for (int i = 0; i < nodesPanels.Length; i++) {
                if (i % 2 == 1) carouselManager.panels[i + 5] = nodesPanels[i];
                else carouselManager.panels[i] = nodesPanels[i];
            }

            for (int i = 0; i < effectsPanels.Length; i++) {
                if (i % 2 == 0) carouselManager.panels[i + 5] = effectsPanels[i];
                else carouselManager.panels[i] = effectsPanels[i];
            }

            for (int i = 0; i < numberInstruments * 2; i++) {
                carouselManager.panels[i].transform.rotation = Quaternion.Euler(new Vector3(0, -36 * i, 0));
            }

            carouselManager.ToggleVFX(true);
            gameSetUpFinished = true;
        }
        */

        // whenever a nodes is activated / deactivated on any panel, call this method to update the corresponding subNode in the other NodeManagers
        public void UpdateSubNodes(int node, bool activated, int nodeManagerSubNodeIndex) {
            foreach (var nodeManager in _nodeManagers) {
                nodeManager.SetSubNode(node, activated, nodeManagerSubNodeIndex);
            }
        }

        public void SetBpm(int value, EffectsManager effectsManager)
        {
            bpm = value;
            for (var index = 0; index < _nodeManagers.Count; index++)
            {
                _nodeManagers[index].SetBpm(bpm);
                if(_effectsManagers[index] != effectsManager) _effectsManagers[index].SetBpmSlider(bpm);
            }
        }

        private IEnumerator WaitToPositionCamera(float time) {
            SetPlayerPosition();
            yield return new WaitForSeconds(time);
            mainSignifier.SetActive(false);
            isWaitingInLobby = false;
        }
    }
}