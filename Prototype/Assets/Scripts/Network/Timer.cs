using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Normal.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour {
    public int roundTime;
    public int timer;
    public float localTimer;
    private double startingRoomTime;
    private RealtimeView _realtimeView;
    private bool blinking;
    public int tempRoundTime = 30;

    private void Start() {
        timer = roundTime;
        tempRoundTime = roundTime;
    }

    public void ToggleTimer(bool restart) {
        if (restart) {
            StopCoroutine(nameof(Time));
            StartCoroutine(nameof(Time));
        }
        else StopCoroutine(nameof(Time));
    }


    private IEnumerator Time() {
        var startTime = RealTimeInstance.Instance.GetRoomTime();

        while (timer > 0.0f) {
            yield return new WaitForSeconds(0.1f);
            var roomTimeDelta = (float) (RealTimeInstance.Instance.GetRoomTime() - startTime);
            timer = (int) (tempRoundTime - roomTimeDelta);
        }

        tempRoundTime = roundTime;
        timer = tempRoundTime;
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