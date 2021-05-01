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
    public float localTimer;
    private double startingRoomTime;
    private RealtimeView _realtimeView;
    private bool blinking;

    private void Start() {
        localTimer = roundTime;
    }

    public void ToggleTimer(bool restart) {
        if (restart) StartCoroutine(Time());
        else StopCoroutine(Time());
    }
    

    private IEnumerator Time() {
        while (localTimer > 0.0f) {
            yield return new WaitForSeconds(1.0f);
            localTimer--;
            RealTimeInstance.Instance._testStringSync.SetMessage(TestStringSync.MessageTypes.TIMER + localTimer);
            
        }

        localTimer = roundTime;
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