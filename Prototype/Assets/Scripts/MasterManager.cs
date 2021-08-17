
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using UnityEngine;
using UnityEngine.UI;

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

    [Header("Drums")] public int numberInstruments = 5;
    public int numberOfNodes = 16;
    [SerializeField] private GameObject nodesPanelPrefab;
    [SerializeField] private GameObject effectsPanelPrefab;

    public GameObject[] nodesPanels;
    public GameObject[] effectsPanels;

    public Color[] drumColors;
    public Color[] defaultNodeColors;

    [SerializeField] private AK.Wwise.Event[] classicDrums = new AK.Wwise.Event[5];
    [SerializeField] private AK.Wwise.Event[] djembeDrums = new AK.Wwise.Event[5];
    [SerializeField] private AK.Wwise.Event[] edmDrums = new AK.Wwise.Event[5];
    [SerializeField] private AK.Wwise.Event[] hangDrums = new AK.Wwise.Event[5];
    [SerializeField] private AK.Wwise.Event[] ambientDrums = new AK.Wwise.Event[5];

    public AK.Wwise.Event[] currentDrums = new AK.Wwise.Event[5];

    [SerializeField] private Dictionary<int, AK.Wwise.Event[]> drumDictionary = new Dictionary<int, AK.Wwise.Event[]>();
    // nodes stuff
    public List<NodeManager> _nodeManagers = new List<NodeManager>();
    public float dwellTimeSpeed = 100.0f;

    public UserInterfaceManager userInterfaceManager;
    public int bpm;

    [Space] [Header("Player")] public Camera playerCamera;
    // public List<Player> Players = new List<Player>();

    private Transform _playerPosition;
    private float startTime, journeyLength;
    [SerializeField] private float positionSpeed = 10.0f;
    public Transform playerStartPosition, playerPositionDestination;
    public bool gameSetUpFinished;
    public float currentRotationOfUI = 0.0f;
    public bool isInPosition = false;

    [SerializeField] private GameObject mainSignifier;

    [SerializeField] private GameObject timerUI;
    public bool DwellSettingsActive;
    public GameObject exitButtonPanel;
    
    private void Start() {
        if (_instance == null) _instance = this;


        // #if !UNITY_IOS && !UNITY_ANDROID
        // Debug.Log("We're not in IOS nor Android, so set the resolution");
        var height =  Screen.currentResolution.height;

        var width = height * 2736/ 1824;
        Screen.SetResolution(width,height,false);
    // #endif
        
        drumDictionary.Add(0, classicDrums);
        drumDictionary.Add(1, djembeDrums);
        drumDictionary.Add(2, edmDrums);
        drumDictionary.Add(3, hangDrums);
        drumDictionary.Add(4, ambientDrums);
        
        currentDrums = classicDrums;

        GameObject.FindWithTag("DwellSettings").transform.GetChild(0).GetComponent<Canvas>().worldCamera = Camera.main;
        dwellTimeSpeed = DontDestroyDwell.Instance.dwellTimeSpeed;
        
    }

    public void SetExitButtonActive(bool active) => exitButtonPanel.SetActive(active);


    private void FixedUpdate() {
        if (!isInPosition) {
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
                StartCoroutine(userInterfaceManager.SwitchPanelRenderLayers());
                SetExitButtonActive(true);
            }
        }
    }

    public void Initialize() {
        nodesPanels = new GameObject[numberInstruments];
        effectsPanels = new GameObject[numberInstruments];

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
        userInterfaceManager.soloButtons = new UI_Gaze_Button[numberInstruments*2];
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
            
            foreach(Transform child in effectsPanels[i].transform.GetComponentsInChildren<Transform>())
            {

                if (child.CompareTag("EffectTitle")) child.GetComponent<Text>().color = new Color(drumColors[i].r, drumColors[i].g, drumColors[i].b, 0);;
                if (child.CompareTag("EffectTitle")) child.GetComponent<Text>().text = (DrumType) i + " Effects";
                if (child.CompareTag("UI_Drum_Colour")) child.GetComponent<Text>().color = drumColors[i];
                
            }

            userInterfaceManager.panels.Add(effectsPanels[i]);
            rotationValue += new Vector3(0, 360.0f / (numberInstruments * 2) * -1, 0);
            var nodeManager = nodesPanels[i].GetComponentInChildren<NodeManager>();
            if (nodeManager == null)
                Debug.LogError(_nodeManagers[i].name +
                               " has an error getting its NodeManager. Check that it has a NodeManager");
            _nodeManagers.Add(nodeManager);
            nodeManager.drumType = (DrumType) i;
            nodeManager.subNodeIndex = i;
            nodeManager.defaultColor = defaultNodeColors[i];
            nodeManager.drumColor = drumColors[i];
            foreach (var incButton in nodeManager.incrementButtons) incButton.activeColor = drumColors[i];
            nodeManager.euclideanButton.activeColor = drumColors[i];
            
            /*// initialize the knob sliders for this current node manager ////////UNCOMMENT FOR RADIAL SLIDERS
            nodeManager.bpmSlider = effectsPanels[i].GetComponentInChildren<SliderKnob>();
            var knobs = effectsPanels[i].GetComponentsInChildren<RadialSlider>();
            nodeManager.sliders = new RadialSlider[knobs.Length];
            for (int j = 0; j < knobs.Length; j++) {
                nodeManager.sliders[j] = knobs[j];
                knobs[j].activeColor = drumColors[i];
                knobs[j].GetComponentInParent<Image>().color = drumColors[i];
                knobs[j].knobBorder.color = drumColors[i];
                foreach (var quad in knobs[j].quadrants) quad.transform.GetComponent<Image>().color = drumColors[i];
            }*/
            
            var knobs = effectsPanels[i].GetComponentsInChildren<SliderKnob>();
            nodeManager.sliders = new SliderKnob[knobs.Length-1];
            var indexCount = 0;
            for (int j = 0; j < knobs.Length; j++)
            {
                if (knobs[j].transform.CompareTag("BPM_Slider")) nodeManager.bpmSlider = knobs[j];
                else
                {
                    nodeManager.sliders[indexCount] = knobs[j];
                    knobs[j].knobBorder.color = drumColors[i];
                    knobs[j].fillRect.GetComponent<Image>().color = drumColors[i];
                    knobs[j].activeColor = drumColors[i];
                    indexCount++;
                }
            }
            var nodesSoloButtons = nodesPanels[i].GetComponentsInChildren<UI_Gaze_Button>();
            foreach (var uigazeButton in nodesSoloButtons)
            {
                userInterfaceManager.soloButtons[i] = uigazeButton;
                uigazeButton.drumTypeIndex = i;
            }
            var effectsSoloButtons = effectsPanels[i].GetComponentsInChildren<UI_Gaze_Button>();
            foreach (var uigazeButton in effectsSoloButtons)
            {
                userInterfaceManager.soloButtons[i + 5] = uigazeButton;
                uigazeButton.drumTypeIndex = i;
            }
            nodeManager.SetUpNode();
            
            userInterfaceManager.panels.Add(nodesPanels[i]);
        }
        
        gameSetUpFinished = true;
    }

    public void PlayDrum(DrumType drum)
    {
        switch (drum)
        {
            case DrumType.Kick:
                currentDrums[0].Post(gameObject);
                break;
            case DrumType.Snare:
                currentDrums[1].Post(gameObject);
                break;
            case DrumType.HiHat:
                currentDrums[2].Post(gameObject);
                break;
            case DrumType.Tom:
                currentDrums[3].Post(gameObject);
                break;
            case DrumType.Cymbal:
                currentDrums[4].Post(gameObject);
                break;
        }
    }

    public void SwitchDrumKits(int drumKitIndex) => currentDrums = drumDictionary[drumKitIndex];

    public void SetPlayerPosition() => journeyLength = Vector3.Distance(playerStartPosition.position, playerPositionDestination.position);
    
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
        }
    }
    

    private IEnumerator WaitToPositionCamera(float time) {
        SetPlayerPosition();
        yield return new WaitForSeconds(time);
        mainSignifier.SetActive(false);
    }

    public void SetDwellSettingsActive(bool set)
    {
        GameObject.FindWithTag("DwellSettings").transform.GetChild(0).gameObject.SetActive(set);
        if(isInPosition)SetExitButtonActive(!set);
    }
    /// <summary>
    /// Map a value from one interval to another interval.
    /// </summary>
    /// <param name="value">Value to map</param>
    /// <param name="min1">Minimum value of the first interval</param>
    /// <param name="max1">Maximum value of the second interval</param>
    /// <param name="min2">Minimum value of the second interval</param>
    /// <param name="max2">Maximum value of the second interval</param>
    /// <returns>Mapped value</returns>
    public float Map(float value, float min1, float max1, float min2, float max2) {
        return min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
    }
}