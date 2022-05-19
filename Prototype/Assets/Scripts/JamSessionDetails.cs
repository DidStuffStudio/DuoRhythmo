using System.Collections.Generic;
using UnityEngine;
using DidStuffLab;

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
}