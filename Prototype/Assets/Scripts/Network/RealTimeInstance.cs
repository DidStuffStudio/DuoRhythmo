using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Realtime))]
public class RealTimeInstance : MonoBehaviour {
    private static RealTimeInstance _instance;
    public static RealTimeInstance Instance => _instance;

    private Realtime _realtime;
    public List<int> clientIds = new List<int>();
    public Dictionary<int, GameObject> clients = new Dictionary<int, GameObject>();
    [SerializeField] private GameObject networkManagerPrefab;
    public GameObject networkManager;
    private NetworkManagerSync _networkManagerSync;
    public bool isConnected;
    public int numberPlayers, previousNumberPlayers;
    public bool isSoloMode = true;
    public StringSync stringSync;

    [SerializeField] private GameObject realtimeInstancesHolderPrefab;
    private Transform _realtimeInstancesHolder;
    [SerializeField] private string[] roomNames = new string[10];
    private int roomToJoinIndex;

    [SerializeField] private GameObject playerCanvasPrefab;
    [SerializeField] private Transform playersHolder;
    public bool isNewPlayer = true;

    private void Awake() {
        _instance = this;
        _realtime = GetComponent<Realtime>();
        RegisterToEvents();
        StartCoroutine(CheckNumberOfPlayers());
    }

    public IEnumerator CheckNumberOfPlayers() {
        while (true) {
            var players = FindObjectsOfType<Player>();
            numberPlayers = players.Length;
            if (numberPlayers != previousNumberPlayers) {
                var counter = 0;
                for (int i = 0; i < MasterManager.Instance.dataMaster.conectedPlayers.Length; i++) {
                    if (numberPlayers > i) MasterManager.Instance.dataMaster.conectedPlayers[i] = 1;
                    else MasterManager.Instance.dataMaster.conectedPlayers[i] = 0;
                }
                previousNumberPlayers = numberPlayers;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetToSoloMode(bool value) => isSoloMode = value;

    public void SetRoomIndex(int i) => roomToJoinIndex = i;

    public void Play() {
        if (!isSoloMode) _realtime.Connect(roomNames[roomToJoinIndex]);
        MasterManager.Instance.Initialize();
    }

    public void SetParentOfPlayer(Transform p) => p.SetParent(playersHolder);


    public double GetRoomTime() {
        return _realtime.room.time;
    }

    private void RegisterToEvents() {
        // Notify us when Realtime connects to or disconnects from the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime) {
        isConnected = true;

        networkManager = Realtime.Instantiate(networkManagerPrefab.name);

        numberPlayers = FindObjectsOfType<NetworkManagerSync>().Length; // get the number of players
        //MasterManager.Instance.localPlayerNumber =
        //numberPlayers - 1; // set this local player's player number to the current player number (index value)

        if (numberPlayers <= 10)
        {
            List<int> occupiedRotations = new List<int>();
            var partnerFound = false;
            foreach (var playerCanvas in FindObjectsOfType<CanvasFollowPlayer>())
            {
                print(playerCanvas.name);
                if (!playerCanvas.RaycastSearchForPartner())
                {
                    print("Player " + playerCanvas + "does not have a partner");
                    if (numberPlayers == 1) break;
                    MasterManager.Instance.playerPositionDestination.position = playerCanvas.transform.forward * 408;
                    MasterManager.Instance.playerPositionDestination.LookAt(Vector3.zero);
                    partnerFound = true;
                    break;
                }
                else occupiedRotations.Add((int) playerCanvas.transform.rotation.y);
            }

            if (!partnerFound && numberPlayers > 1)
            {
                for (int i = 360; i > 0; i -= 36)
                {
                    if (!occupiedRotations.Contains(i) || !occupiedRotations.Contains(i + 180))
                    {
                        // 360 || 0 --> -180
                        // 359 --> -179 --> Abs(-179 + 180) = Abs(1) = 1
                        if (numberPlayers == 1) break;
                        MasterManager.Instance.playerCamera.transform.parent.Rotate(0, i, 0);
                        break;
                    }
                }
            }
        }
        else {
            // TODO: add to queue, and if someone disconnects, check if there is someone in queue. If there is, set the first person in queue's position
            MasterManager.Instance.playerPositionDestination.position = MasterManager.Instance.playerStartPosition.position;
            MasterManager.Instance.playerPositionDestination.LookAt(Vector3.zero);
        }

        var gfx = Realtime.Instantiate(playerCanvasPrefab.name, true, true, true);
        gfx.transform.GetComponent<RealtimeTransform>().RequestOwnership();
        MasterManager.Instance.localPlayerNumber = Random.Range(0, 10000000);
        stringSync.SetNewPlayerUpdateTime(MasterManager.Instance.localPlayerNumber);
        stringSync.SetNewPlayerConnected(MasterManager.Instance.localPlayerNumber);
        StartCoroutine(SeniorPlayer());
    }

    IEnumerator SeniorPlayer() {
        yield return new WaitForSeconds(1.0f);
        isNewPlayer = false;
    }

    private void OnDisable() => _realtime.didConnectToRoom -= DidConnectToRoom;
}