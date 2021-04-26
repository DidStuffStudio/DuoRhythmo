using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Drums")] 
    public int numberInstruments = 5;
    [SerializeField] private GameObject nodesPanelPrefab;
    [SerializeField] private GameObject effectsPanelPrefab;

    public GameObject[] nodesPanels;
    public GameObject[] effectsPanels;

    public Color[] drumColors;
    public Color[] defaultNodeColors;

    // nodes stuff
    public List<NodeManager> _nodeManagers = new List<NodeManager>();
    public float dwellTimeSpeed = 100.0f;
    
    public UserInterfaceManager userInterfaceManager;

    [SerializeField] private ScreenSync[] _screenSyncs;

    [Space] [Header("Timer")]
    public int bpm = 280;
    [SerializeField] private GameObject timerPrefab;
    private GameObject timerGameObject;
    public Timer timer;

    [Space] [Header("Player")] 
    public Camera playerCamera;
    public List<Player> Players = new List<Player>();
    private Transform _playerPosition;
    private float startTime, journeyLength;
    [SerializeField] private float positionSpeed = 10.0f;
    [SerializeField] private Transform playerStartPosition, playerPositionDestination;
    private bool _canPositionPlayer = false;

    public int localPlayerNumber = 0;
    public bool gameSetUpFinished;

    
    
    private void Start() {
        if (_instance == null) {
            _instance = this;
            // make instance persistent across scenes
            // DontDestroyOnLoad(gameObject);
        }
        
        Initialize();
         // Change to UI stuff later
    }

    private void Update() {
        if (_canPositionPlayer && !RealTimeInstance.Instance.isSoloMode) {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * positionSpeed;
            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;
            var playerCameraTransform = playerCamera.transform;
            playerCamera.transform.position = Vector3.Lerp(playerCameraTransform.position, playerPositionDestination.position, fractionOfJourney);
            playerCamera.transform.rotation = Quaternion.Lerp(playerCameraTransform.rotation, playerPositionDestination.rotation, fractionOfJourney);
        }
    }

    private void Initialize() {
        nodesPanels = new GameObject[numberInstruments];
        effectsPanels = new GameObject[numberInstruments];

        // create the rotations for the panels -->  (360.0f / (numberInstruments * 2) * -1)  degrees difference between each other in the Y axis
        Vector3[] panelRotations = new Vector3[10]; // number of instruments * 2
        var rotationValue =
            360.0f / (numberInstruments * 2) * -1; // if 5 instruments --> 360.0f / (5 * 2) * -1 = -36.0f; 
        for (int i = 0; i < panelRotations.Length; i++) {
            panelRotations[i] = new Vector3(0, i * rotationValue, 0);
        }
        
        // TODO: Set the size of the panels' canvas (radius distance to the origin point)
        
        if (RealTimeInstance.Instance.isSoloMode) {
            SetPlayerPosition();
            InstantiatePanelsSoloMode();
            return;
        }
        // position player at the top view
        playerCamera.transform.position = playerStartPosition.position;
        StartCoroutine(WaitUntilConnected());
    }

    private void InstantiatePanelsSoloMode() {
        Vector3 rotationValue = Vector3.zero;
        var nodesPanelsGo = new GameObject("Nodes panels");
        nodesPanelsGo.transform.SetParent(userInterfaceManager.transform);
        var effectsPanelsGo = new GameObject("Effects panels");
        effectsPanelsGo.transform.SetParent(userInterfaceManager.transform);

        for (int i = 0; i < nodesPanels.Length; i++) {
            nodesPanels[i] = Instantiate(nodesPanelPrefab, transform.position, Quaternion.Euler(rotationValue));
            nodesPanels[i].transform.SetParent(nodesPanelsGo.transform);
            nodesPanels[i].name = "NodesPanel_" + (DrumType) i;
            rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1, 0);
            effectsPanels[i] = Instantiate(effectsPanelPrefab, transform.position, Quaternion.Euler(rotationValue));
            effectsPanels[i].transform.SetParent(effectsPanelsGo.transform);
            effectsPanels[i].name = "EffectsPanel_" + (DrumType) i;
            var effectsUigazeButtons = effectsPanels[i].GetComponentsInChildren<UI_Gaze_Button>();
            var nodesUigazeButtons = nodesPanels[i].GetComponentsInChildren<UI_Gaze_Button>();
            foreach (var uigazeButton in effectsUigazeButtons) {
                uigazeButton.drumTypeIndex = i;
                break;
            }

            foreach (var uigazeButton in nodesUigazeButtons) {
                uigazeButton.drumTypeIndex = i;
                break;
            }

            userInterfaceManager.panels.Add(effectsPanels[i]);
            rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1, 0);
            var nodeManager = nodesPanels[i].GetComponentInChildren<NodeManager>();
            if (nodeManager == null)
                Debug.LogError(_nodeManagers[i].name +
                               " has an error getting its NodeManager. Check that it has a NodeManager");
            _nodeManagers.Add(nodeManager);
            nodeManager._screenSync = _screenSyncs[i];
            nodeManager.drumType = (DrumType) i;
            nodeManager.subNodeIndex = i;
            nodeManager.defaultColor = defaultNodeColors[i];
            nodeManager.drumColor = drumColors[i];

            // initialize the knob sliders for this current node manager
            var knobs = effectsPanels[i].GetComponentsInChildren<SliderKnob>();
            nodeManager.sliders = new SliderKnob[knobs.Length];
            for (int j = 0; j < knobs.Length; j++) {
                nodeManager.sliders[j] = knobs[j];
            }

            nodeManager.SetUpNode();

            userInterfaceManager.panels.Add(nodesPanels[i]);
        }

        timerGameObject = Instantiate(timerPrefab);
        timer = timerGameObject.GetComponent<Timer>();
        userInterfaceManager.SetUpInterface();
        StartCoroutine(userInterfaceManager.SwitchPanelRenderLayers());
        gameSetUpFinished = true;
    }


    private IEnumerator WaitUntilConnected() {
        while (true) {
            // if (RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            if (RealTimeInstance.Instance.isSoloMode && RealTimeInstance.Instance.isConnected) break;
            if (RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }

        print("local player number: " + localPlayerNumber);
        SetPlayerPosition();
        InstantiatePanels();
    }

    private void SetPlayerPosition() {
        // rotate the parent of the camera around the degrees dependant on the number of players and number of instruments
        // players should be opposite to each other --> so differentiate between even and uneven numbers --> 180 degrees difference between them
        float degrees = 0;
        float degreesPerPlayer = 360.0f / (numberInstruments * 2);
        // if even player --> degrees = -36 * evenPlayerNumberCounter
        if (localPlayerNumber % 2 == 0) degrees = -degreesPerPlayer * (localPlayerNumber / 2.0f);
        // if uneven player --> degrees = 180 - 36 * unevenPlayerNumber
        else degrees = 180 - (degreesPerPlayer * ((localPlayerNumber - 1) / 2.0f));
        // rotate the parent of the camera by the number of degrees around the y axis
        // Transform playerParent = playerCamera.transform.parent;
        // Vector3 playerParentPos = playerParent.position;
        // Quaternion playerParentRot = playerParent.rotation;
        playerCamera.transform.parent.Rotate(0, degrees, 0);
    }

    private IEnumerator WaitToPositionPlayer() {
        while (timer.timer >= 3.0f) {
            yield return new WaitForEndOfFrame();
        }

        _canPositionPlayer = true;
    }

    // call this method when the players have connected
    private void InstantiatePanels() {
        int panelCounter = 1; // to keep track of the panels for the userInterfaceManager
        // instantiate and set up effects panels
        var effectsPanelsGo = new GameObject("Effects panels");
        effectsPanelsGo.transform.SetParent(userInterfaceManager.transform);
        nodesPanels = new GameObject[numberInstruments];
        effectsPanels = new GameObject[numberInstruments];
        
        Vector3 rotationValue = new Vector3(0, 180.0f, 0);
        for (int i = 0; i < effectsPanels.Length; i++) {
            effectsPanels[i] = Instantiate(effectsPanelPrefab, transform.position, Quaternion.Euler(rotationValue));
            effectsPanels[i].transform.SetParent(effectsPanelsGo.transform);
            effectsPanels[i].name = "EffectsPanel_" + (DrumType) i;
            rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1 * 2, 0);
            userInterfaceManager.panels.Add(effectsPanels[i]);
            // userInterfaceManager.panels[panelCounter] = effectsPanels[i];
            panelCounter += 2;
        }

        panelCounter = 0;

        // instantiate and set up nodes panels
        rotationValue = Vector3.zero;

        var nodesPanelsGo = new GameObject("Nodes panels");
        nodesPanelsGo.transform.SetParent(userInterfaceManager.transform);
        for (int i = 0; i < nodesPanels.Length; i++) {
            nodesPanels[i] = Instantiate(nodesPanelPrefab, transform.position, Quaternion.Euler(rotationValue));
            nodesPanels[i].transform.SetParent(nodesPanelsGo.transform);
            nodesPanels[i].name = "NodesPanel_" + (DrumType) i;
            rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1 * 2, 0);

            // set up nodes managers
            // set up the drum type, drum color, and default color of each nodeManager
            var nodeManager = nodesPanels[i].transform.GetComponentInChildren<NodeManager>();

            if (nodeManager == null)
                Debug.LogError(_nodeManagers[i].name +
                               " has an error getting its NodeManager. Check that it has a NodeManager");
            _nodeManagers.Add(nodeManager);
            nodeManager._screenSync = _screenSyncs[i];
            nodeManager.drumType = (DrumType) i;
            nodeManager.subNodeIndex = i;
            nodeManager.defaultColor = defaultNodeColors[i];
            nodeManager.drumColor = drumColors[i];

            // initialize the knob sliders for this current node manager
            var knobs = effectsPanels[i].GetComponentsInChildren<SliderKnob>();
            nodeManager.sliders = new SliderKnob[knobs.Length];
            for (int j = 0; j < knobs.Length; j++) {
                nodeManager.sliders[j] = knobs[j];
            }

            nodeManager.SetUpNode();

            userInterfaceManager.panels.Add(nodesPanels[i]);
            // userInterfaceManager.panels[panelCounter] = nodesPanels[i];
            panelCounter += 2;
        }
        
        
        // only the first player that connects to the room should start the timer - and as Timer is a RealTime instance object, it updates in all clients
        if (localPlayerNumber == 0) {
                timerGameObject = Realtime.Instantiate(prefabName: timerPrefab.name, ownedByClient: true);
                timer = timerGameObject.GetComponent<Timer>();
                userInterfaceManager.SetUpInterface();
                gameSetUpFinished = true;
        }
        else StartCoroutine(FindTimer());
    }

    // whenever a nodes is activated / deactivated on any panel, call this method to update the corresponding subNode in the other NodeManagers
    public void UpdateSubNodes(int node, bool activated, int nodeManagerSubNodeIndex) {
        foreach (var nodeManager in _nodeManagers) {
            nodeManager.SetSubNode(node, activated, nodeManagerSubNodeIndex);
        }
    }

    public void SetBpm(int value) {
        bpm = value;
        foreach (var nodeManager in _nodeManagers) {
            nodeManager.bpm = bpm;
            nodeManager._screenSync.SetBpm(bpm);
        }
    }

    private IEnumerator FindTimer() {
        if (RealTimeInstance.Instance.isSoloMode) yield break;
        var timerFound = false;
        while (!timerFound) {
            timer = FindObjectOfType<Timer>();
            if (timer != null) {
                timerGameObject = timer.gameObject;
                userInterfaceManager.SetUpInterface();
                
                
                
                gameSetUpFinished = true;
                timerFound = true;
                startTime = Time.time;
                StartCoroutine(WaitToPositionPlayer());
                // Calculate the journey length.
                journeyLength = Vector3.Distance(playerStartPosition.position, playerPositionDestination.position);
            }
            else {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void ResetLocalPlayerNumber(int disconnectedPlayerNumber) { }
}