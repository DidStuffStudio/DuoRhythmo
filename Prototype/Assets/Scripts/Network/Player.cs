using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int playerNumber;
    public bool isPaired; // does this player have someone to play the drums with?
    public Player pairedPlayer;
    public bool isWaitingToBePositioned = false;
    public Transform playerPosition;

    private void Start() {
        if(RealTimeInstance.Instance.isSoloMode) return;
        this.playerNumber = MasterManager.Instance.localPlayerNumber;
        // once this player has been instantiated into the scene - add it to the master manager
        MasterManager.Instance.Players.Add(this);
        // go through each player and check if he's paired with someone else. If not, then pair this player to that other one
        foreach (var player in MasterManager.Instance.Players) {
            if (!player.isPaired) {
                // pair the other player with this one
                player.isPaired = true;
                player.pairedPlayer = this;
                // and vice-versa
                this.isPaired = true;
                this.pairedPlayer = player;
                // start the coroutine to place the user in the correct position based on the current timer
                // StartCoroutine(WaitToPositionCamera());
                // rotate aaccordingly
                // transform.Rotate(0, MasterManager.Instance.playerCamera.transform.rotation.y ,0);
            }
            break;
        }
    }

    private IEnumerator WaitToPositionCamera() {
        while (MasterManager.Instance.timer.timer >= 3.0f) {
            yield return new WaitForEndOfFrame();
        }
    }
}