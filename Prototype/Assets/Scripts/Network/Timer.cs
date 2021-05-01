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
    public bool newPlayer = false;
    public int tempRoundTime = 30;
    public bool reset = false;

    private void Start() {
        timer = roundTime;
        tempRoundTime = roundTime;
    }

    public void ToggleTimer(bool restart) {
        if (restart)
        {
            StopCoroutine("Time");
            StartCoroutine("Time");
        }
        else StopCoroutine("Time");
    }



    private IEnumerator Time()
    {

        var startTime = RealTimeInstance.Instance.GetRoomTime();

        while (timer > 0.0f)
        {
            yield return new WaitForSeconds(0.1f);
            var roomTimeDelta = (float) (RealTimeInstance.Instance.GetRoomTime() - startTime);
            timer = (int) (tempRoundTime - roomTimeDelta);
            //RealTimeInstance.Instance._testStringSync.SetMessage(TestStringSync.MessageTypes.TIMER + localTimer);

        }
        
            tempRoundTime = roundTime;
            timer = tempRoundTime;
            MasterManager.Instance.userInterfaceManager.PlayAnimation(true);
            newPlayer = false;
       
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