using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Unity.Mathematics;
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

    public int numberPlayers;
    public int localPlayerNumber = 0;

    public int numberInstruments = 5;
    // private GameObject[] panels;

    [SerializeField] private GameObject nodesPanelPrefab;
    [SerializeField] private GameObject effectsPanelPrefab;

    public GameObject[] nodesPanels;
    public GameObject[] effectsPanels;

    public DrumType DrumTypes;
    public Color[] DrumColors;
    public Color[] defaultNodeColors;

    // nodes stuff
    public List<NodeManager> _nodeManagers = new List<NodeManager>();

    public UserInterfaceManager userInterfaceManager;

    // public Dictionary<NodeManager, int> nodesGlossary = new Dictionary<NodeManager, int>();
    // public Dictionary<DrumType, int> subIndex = new Dictionary<DrumType, int>();

    public int bpm = 0;

    [SerializeField] private ScreenSync [] _screenSyncs;
    public bool gameSetupFinished = false;

    private void Start() {
        if (_instance == null) {
            _instance = this;
            // make instance persistent across scenes
            DontDestroyOnLoad(gameObject);
        }

        Initialize();
    }

  
    private void Initialize() {
        // panels = new GameObject[numberInstruments * 2];
        // _nodeManagers = new NodeManager[numberInstruments];

        nodesPanels = new GameObject[numberInstruments];
        effectsPanels = new GameObject[numberInstruments];
        
        // _screenSyncs = new ScreenSync[numberInstruments];
        // DrumColors = new Color[numberInstruments];
        // defaultNodeColors = new Color[numberInstruments];

        // create the rotations for the panels -->  (360.0f / (numberInstruments * 2) * -1)  degrees difference between each other in the Y axis
        Vector3[] panelRotations = new Vector3[10]; // number of instruments * 2
        var rotationValue =
            360.0f / (numberInstruments * 2) * -1; // if 5 instruments --> 360.0f / (5 * 2) * -1 = -36.0f; 
        for (int i = 0; i < panelRotations.Length; i++) {
            panelRotations[i] = new Vector3(0, i * rotationValue, 0);
        }

        // TODO: Set the size of the panels' canvas (radius distance to the origin point)

        // if (RealTimeInstance.Instance.isSoloMode && localPlayerNumber == 1) InstantiatePanels();
        // else StartCoroutine(WaitUntilConnected());
        StartCoroutine(WaitUntilConnected());
    }


    private IEnumerator WaitUntilConnected() {
        while (true) {
            // if (RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            if(RealTimeInstance.Instance.isSoloMode && RealTimeInstance.Instance.isConnected) break;
            if (RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }
        print("local player number: " + localPlayerNumber);
        InstantiatePanels();
    }


    // call this method when the players have connected
    private void InstantiatePanels() {
        int panelCounter = 1; // to keep track of the panels for the userInterfaceManager
        // instantiate and set up effects panels
        var effectsPanelsGo = new GameObject("Effects panels");
        effectsPanelsGo.transform.SetParent(userInterfaceManager.transform);
        Vector3 rotationValue = new Vector3(0, 180.0f, 0);
        for (int i = 0; i < effectsPanels.Length; i++) {
            effectsPanels[i] = Instantiate(effectsPanelPrefab, transform.position, Quaternion.Euler(rotationValue));
            effectsPanels[i].transform.SetParent(effectsPanelsGo.transform);
            effectsPanels[i].name = "EffectsPanel_" + (DrumType) i;
            // effectsPanels[i] = Realtime.Instantiate(effectsPanelPrefab.name, transform.position,
            //    Quaternion.Euler(rotationValue), ownedByClient: false);
            rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1 * 2, 0);
            userInterfaceManager.panels[panelCounter] = effectsPanels[i];
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
            // nodesPanels[i] = Realtime.Instantiate(nodesPanelPrefab.name, transform.position,
            //     Quaternion.Euler(rotationValue), ownedByClient: false);
            rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1 * 2, 0);

            // set up nodes managers
            // set up the drum type, drum color, and default color of each nodeManager
            // var currentDrumType = Enum.GetValues(typeof(DrumType));
            // _nodeManagers[i].drumType = currentDrumType.GetValue(i) is DrumType ? (DrumType) currentDrumType.GetValue(i) : DrumType.Kick;
            var nodeManager = nodesPanels[i].transform.GetChild(0).GetChild(0).GetComponent<NodeManager>();
            
            if (nodeManager == null) Debug.LogError(_nodeManagers[i].name + " has an error getting its NodeManager. Check that it has a NodeManager");
            _nodeManagers.Add(nodeManager);
            nodeManager._screenSync = _screenSyncs[i];
            nodeManager.drumType = (DrumType) i;
            nodeManager.subNodeIndex = i;
            nodeManager.defaultColor = defaultNodeColors[i];
            nodeManager.drumColor = DrumColors[i];
            
            // initialize the knob sliders for this current node manager
            var knobs = effectsPanels[i].GetComponentsInChildren<SliderKnob>();
            nodeManager.sliders = new SliderKnob[knobs.Length];
            for (int j = 0; j < knobs.Length; j++) {
                nodeManager.sliders[j] = knobs[j];
            }

            nodeManager.SetUpNode();
            
            userInterfaceManager.panels[panelCounter] = nodesPanels[i];
            panelCounter += 2;
        }

        userInterfaceManager.SwitchPanelRenderLayers();
        gameSetupFinished = true;
        userInterfaceManager.SetUpInterface();
    }

    // whenever a nodes is activated / deactivated on any panel, call this method to update the corresponding subNode in the other NodeManagers
    public void UpdateSubNodes(int node, bool activated, int nodeManagerSubNodeIndex) {
        foreach (var nodeManager in _nodeManagers) {
            nodeManager.SetSubNode(node, activated, nodeManagerSubNodeIndex);
        }
    }
    
    public void SetBPM(int value)
    {
        foreach (var nodeManager in _nodeManagers)
        {
            nodeManager.bpm = value;
            nodeManager.sliders[3].currentValue = value;
        }
    }
}