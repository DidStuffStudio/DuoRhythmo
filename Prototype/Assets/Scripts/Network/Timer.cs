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
    private bool mainTimerRunning;

    private void Start() {
        timer = roundTime;
        tempRoundTime = roundTime;
    }

   


    public IEnumerator TemporaryTime() {
        var startTime = RealTimeInstance.Instance.GetRoomTime();

        while (timer > 0.0f && !mainTimerRunning ) {
            yield return new WaitForSeconds(0.1f);
            var roomTimeDelta = (float) (RealTimeInstance.Instance.GetRoomTime() - startTime);
            timer = (int) (tempRoundTime - roomTimeDelta);
        }
        timer = roundTime;
        MasterManager.Instance.userInterfaceManager.PlayAnimation(true);
    }   
    
    public IEnumerator MainTime() {
        mainTimerRunning = true;
        var startTime = RealTimeInstance.Instance.GetRoomTime();

        while (timer > 0.0f) {
            yield return new WaitForSeconds(0.1f);
            var roomTimeDelta = (float) (RealTimeInstance.Instance.GetRoomTime() - startTime);
            timer = (int) (roundTime - roomTimeDelta);
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