using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Normal.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour {
    public int roundTime;
    public float timer;
    private double startingRoomTime;
    private RealtimeView _realtimeView;
    private bool blinking;

    private void Start() {
        timer = roundTime;
        _realtimeView = GetComponent<RealtimeView>();
    }

    public void ToggleTimer(bool restart) {
        if (!RealTimeInstance.Instance.isSoloMode) {
            if (!_realtimeView.isOwnedLocallyInHierarchy) return;
        }

        if (restart) StartCoroutine(Time());
        else StopCoroutine(Time());
    }

    public void CheckForOwner() {
        if (RealTimeInstance.Instance.isSoloMode) return;
        foreach (var player in MasterManager.Instance.Players) {
            if (player) {
                player.RequestOwnership(_realtimeView);
                break;
            }
        }
    }

    private IEnumerator Time() {
        while (timer > 0.0f) {
            yield return new WaitForSeconds(1.0f);
            timer--;
            // if (Math.Abs(timer - 15.0f) < 0.1f) StartCoroutine(BlinkTimer());
            if (!RealTimeInstance.Instance.isSoloMode)
                RealTimeInstance.Instance._testStringSync.SetMessage(TestStringSync.MessageTypes.TIMER + timer);
        }

        timer = roundTime;
        MasterManager.Instance.userInterfaceManager.PlayAnimation(true);
    }

    // private IEnumerator BlinkTimer() {
    //     while (true) {
    //         yield return new WaitForSeconds(0.1f);
    //         // decrease blink alpha
    //         yield return new WaitForSeconds(0.1f);
    //         // increase blink alpha 
    //     }
    // }
}