using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ctsalidis;
using UnityEngine;


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

    public List<Player> players = new List<Player>();
    private PlayerPositionSync _playerPositionSync;

    public PlayerPositionSync PlayerPositionSync {
        get => _playerPositionSync;
        set {
            _playerPositionSync = value;
            // NotifyPlayersOfPositionSyncInitialized();
        }
    }

    public void AddPlayer(Player player) => players.Add(player);

    /*
    private void NotifyPlayersOfPositionSyncInitialized() {
        print("Notifying players that the position sync object has been initialized");
        foreach (var player in players.Where(player => player.isLocalPlayer)) player.CheckForCorrespondingPositioning();
    }
    */
}