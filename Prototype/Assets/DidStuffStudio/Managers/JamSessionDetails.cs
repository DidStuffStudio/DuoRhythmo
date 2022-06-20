using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidStuffLab;
using Managers;
using UnityEngine.SceneManagement;

public class JamSessionDetails : MonoBehaviour {
    private static JamSessionDetails _instance;

    public static JamSessionDetails Instance {
        get {
            if (_instance != null) return _instance;
            var JamSessionDetailsGO = new GameObject();
            _instance = JamSessionDetailsGO.AddComponent<JamSessionDetails>();
            JamSessionDetailsGO.name = typeof(JamSessionDetails).ToString();
            return _instance;
        }
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Server details
    public string ServerIpAddress { get; private set; } = string.Empty;
    public ushort ServerPort { get; private set; } = ushort.MinValue;
    
    
    public int DrumTypeIndex { get; set; }
    public bool isSoloMode { get; set; }
    public string LocalUsername { get; set; }
    public string LocalAvatarName { get; set; }

    public List<Player> players = new List<Player>();
    public Player localPlayer, otherPlayer;
    public Transform otherPlayerEyeFollowTransform;

    public MasterManagerData loadedBeatData;
    public bool loadingBeat { get; set; }

    public void StartMatchmaking(bool isRandom) {
        isSoloMode = false;
        if (SceneManager.GetActiveScene().buildIndex != 0) return;
        Matchmaker.Instance.SelectDrumAndStartMatch(DrumTypeIndex.ToString(), isRandom);
    }
    
    public void AddPlayer(Player player) {
        players.Add(player);
        if (!player.isLocalPlayer) {
            otherPlayer = player;
            var otherPlayerEyeFollow = otherPlayer.transform.GetComponentInChildren<PlayerEyeFollow>();
            otherPlayerEyeFollowTransform = otherPlayerEyeFollow.transform;
        }
        else localPlayer = player;
    }

    public void ClearDetails() {
        DrumTypeIndex = 0;
        isSoloMode = true;
        loadingBeat = false;
        LocalAvatarName = "Avatar1";
        players.Clear();
        otherPlayer = null;
        ServerIpAddress = string.Empty;
        ServerPort = ushort.MinValue;
    }
    
    public void SetMultiplayerMatchDetails(string username, string localAvatar, byte drumIndex, string ipAddress, ushort port) {
        // if (SceneManager.GetActiveScene().buildIndex != 0) return;
        isSoloMode = false;
        LocalUsername = username;
        LocalAvatarName = localAvatar;
        DrumTypeIndex = drumIndex;
        ServerIpAddress = ipAddress;
        ServerPort = port;
        // ClientStartup.Instance.SetServerInstanceDetails(ipAddress, port);
        SceneManager.LoadScene(2);
    }

    public void SetLoadedBeat()
    {
        StartCoroutine(SetValues());
    }
    private IEnumerator SetValues()
    {
        while (!MasterManager.Instance.gameSetUpFinished)
        {
            yield return new WaitForEndOfFrame();
        }
        // Wait for duorythmo to fully load (strange things will occur if you don't wait)
        yield return new WaitForSeconds(1);
        MasterManager masterManager = MasterManager.Instance;

        List<NodeManager> nodeManagers = new List<NodeManager>();

        List<EffectsManager> effectsManagers = new List<EffectsManager>();

        for (int i = 0; i < MasterManager.Instance.effectsManagers.Count; i++)
        {
            effectsManagers.Add(MasterManager.Instance.effectsManagers[i]);
        }

        MasterManager.Instance.Bpm = (byte) loadedBeatData.BPM;
        
        // Same as in SaveIntoJson just in reverse
        for (int i = 0; i < MasterManager.Instance.nodeManagers.Count; i++)
        {
            nodeManagers.Add(MasterManager.Instance.nodeManagers[i]);
        }

        for (int i = 0; i < nodeManagers.Count; i++)
        {
            for (int j = 0; j < nodeManagers[i].nodes.Count; j++)
            {
                if (loadedBeatData.nodeManagersData[i].nodesData[j].isActive)
                {
                    nodeManagers[i].nodes[j].ToggleState();
                }
            }
            for (int j = 1; j < effectsManagers[i].sliders.Count; j++)
            {
                
                effectsManagers[i].sliders[j].SetCurrentValue((byte)loadedBeatData.nodeManagersData[i].sliderValues[j-1]);

            }
        }

        yield return null;
    }
}