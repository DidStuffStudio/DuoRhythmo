/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.UI;
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
    private Color timerColor;
    private bool isBlinking = false;

    private void Start() {
        timer = roundTime;
        
    }




    public IEnumerator TemporaryTime()
    {
        var startTime = RealTimeInstance.Instance.GetRoomTime();

        while (timer > 0.0f && !mainTimerRunning)
        {
            yield return new WaitForSeconds(0.1f);
            var roomTimeDelta = (float)(RealTimeInstance.Instance.GetRoomTime() - startTime);
            timer = (int)(tempRoundTime - roomTimeDelta);
        }
        if (!mainTimerRunning)
        {
            timer = roundTime;
            MasterManager.Instance.userInterfaceManager.PlayAnimation(true);
        }
    }
    
    public IEnumerator MainTime() {
        mainTimerRunning = true;
        var startTime = RealTimeInstance.Instance.GetRoomTime();

        while (timer > 0.0f) {
            yield return new WaitForSeconds(0.1f);
            var roomTimeDelta = (float) (RealTimeInstance.Instance.GetRoomTime() - startTime);
            timer = (int) (roundTime - roomTimeDelta);
            if (timer <= 15 && !isBlinking)
            {
                StartCoroutine(BlinkTimer());
                isBlinking = true;
            }
        }

        timer = roundTime;
        MasterManager.Instance.userInterfaceManager.PlayAnimation(true);
    }

    private IEnumerator BlinkTimer() {
        {
            while (timer <= 15)
            {
                var tempTimerColor = MasterManager.Instance.userInterfaceManager.timerDisplay.color;
                tempTimerColor.a = 0.0f;
                yield return new WaitForSeconds(0.2f);
                MasterManager.Instance.userInterfaceManager.timerDisplay.color = tempTimerColor;
                tempTimerColor.a = 1.0f;
                yield return new WaitForSeconds(0.2f);
                MasterManager.Instance.userInterfaceManager.timerDisplay.color = tempTimerColor;
            }

            isBlinking = false;
        }
    }
}*/