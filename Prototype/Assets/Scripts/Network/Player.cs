using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour {
    public int playerNumber;
    public bool isPaired; // does this player have someone to play the drums with?
    public Player pairedPlayer;
    public Transform playerPosition;
    public bool isWaitingInLobby = true;
    [SerializeField] private GameObject gfxCanvasPrefab;
    private Transform _canvasOffset;

    private void Start() {
        if(RealTimeInstance.Instance.isSoloMode) return;
        MasterManager.Instance.player = this;
        //_canvasOffset = Camera.main.transform.GetChild(0);
        /*var gfx = Realtime.Instantiate(gfxCanvasPrefab.name, true, true, true);
        gfx.transform.SetParent(_canvasOffset);
        gfx.transform.localPosition = Vector3.zero;
        gfx.transform.localRotation = Quaternion.identity;*/
        playerNumber = MasterManager.Instance.localPlayerNumber;
        // once this player has been instantiated into the scene - add it to the master manager
        MasterManager.Instance.Players.Add(this);
        // go through each player and check if he's paired with someone else. If not, then pair this player to that other one
        /*foreach (var player in MasterManager.Instance.Players) {
            if (!player.isPaired) {
                // pair the other player with this one
                player.isPaired = true;
                player.pairedPlayer = this;
                // and vice-versa
                isPaired = true;
                pairedPlayer = player;
                // start the coroutine to place the user in the correct position based on the current timer
                // StartCoroutine(WaitToPositionCamera());
                // rotate accordingly
                transform.Rotate(0, MasterManager.Instance.playerCamera.transform.rotation.y ,0);
            }
            break;
        }*/

        StartCoroutine(WaitToPositionCamera());
    }

    private void OnApplicationQuit()
    {
        if (!pairedPlayer) return;
        pairedPlayer.isPaired = false;
        pairedPlayer.pairedPlayer = null;
    }
    
    private IEnumerator WaitToPositionCamera() {
        yield return new WaitForSeconds(3.0f);
        isWaitingInLobby = false;
    }
}