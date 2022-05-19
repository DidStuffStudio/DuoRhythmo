using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidStuffLab;
using Managers;

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

    public int DrumTypeIndex { get; set; }
    public bool isSoloMode { get; set; }
    public string LocalAvatarName { get; set; }

    public List<Player> players = new List<Player>();
    public Player otherPlayer;

    public MasterManagerData loadedBeatData;
    public bool loadingBeat { get; set; }

    public void StartMatchmaking(bool isRandom) {
        // TODO --> Probably check if the we're in the main menu scene for this - otherwise return void
        isSoloMode = false;
        Matchmaker.Instance.SelectDrumAndStartMatch(DrumTypeIndex.ToString(), isRandom);
    }
    
    public void AddPlayer(Player player) {
        players.Add(player);
        if (!player.isLocalPlayer) otherPlayer = player;
    }

    public void ClearDetails() {
        DrumTypeIndex = 0;
        isSoloMode = true;
        LocalAvatarName = "Avatar1";
        players.Clear();
        otherPlayer = null;
    }
    
    public void SetMultiplayerMatchDetails(string localAvatar, byte drumIndex, string ipAddress, ushort port) {
        LocalAvatarName = localAvatar;
        DrumTypeIndex = drumIndex;
        ClientStartup.Instance.SetServerInstanceDetails(ipAddress, port);
    }

    public void SetLoadedBeat()
    {
        StartCoroutine(SetValues());
    }
    public IEnumerator SetValues()
    {
        while (!MasterManager.Instance.gameSetUpFinished)
        {
            yield return new WaitForSeconds(1);
        }
        // Wait for duorythmo to fully load (strange things will occur if you don't wait)
        yield return new WaitForSeconds(1);
        MasterManager masterManager = MasterManager.Instance;

        List<NodeManager> nodeManagers = new List<NodeManager>();
        List<EffectsManager> effectsManagers = new List<EffectsManager>();
        masterManager.bpm = loadedBeatData.BPM;
        
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
                effectsManagers[i].sliders[j].SetCurrentValue(loadedBeatData.nodeManagersData[i].sliderValues[j]);
            }
        }

        yield return null;
    }
}