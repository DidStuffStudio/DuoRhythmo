using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Normal.Realtime;
using UnityEngine;

public class Stopwatch : RealtimeComponent<StopwatchModel>
{

    [SerializeField] private int roundTime = 30;
    private bool isBlinking;

    private void Start()
    {
        if (RealTimeInstance.Instance.isSoloMode) return;
        StartCoroutine(BlinkTimer());
    }

    public float GetAnimatorTime() {
        return model.animatorTime;
    }

    public void SetAnimatorTime(float t)
    {
        //GetComponent<RealtimeView>().RequestOwnership();
        model.animatorTime = t;
    }

    public int time {
        get {
            // Return 0 if we're not connected to the room yet.
            if (model == null) return roundTime;

            // Make sure the stopwatch is running
            if (model.startTime == 0.0) return roundTime;

            // Calculate how much time has passed
            var t = (int) (roundTime - (realtime.room.time - model.startTime));
            if (t <= 0)
            {
                t = 30;

                if (!MasterManager.Instance.userInterfaceManager.playingAnim)
                {
                    MasterManager.Instance.userInterfaceManager.playingAnim = true;
                    MasterManager.Instance.userInterfaceManager.PlayAnimation(true);
                }
            }
            return t;
        }
    }

    public void SetFirstPlayer() => model.firstPlayer = true;
    
    public void StartStopwatch() {
        //if(!MasterManager.Instance.isFirstPlayer) return;
        model.startTime = realtime.room.time;
        
    }
    
    private IEnumerator BlinkTimer()
    {
        while (true)
        {
            while (time <= 15)
            {
                var tempTimerColor = MasterManager.Instance.userInterfaceManager.timerDisplay.color;
                tempTimerColor.a = 0.0f;
                yield return new WaitForSeconds(0.2f);
                MasterManager.Instance.userInterfaceManager.timerDisplay.color = tempTimerColor;
                tempTimerColor.a = 1.0f;
                yield return new WaitForSeconds(0.2f);
                MasterManager.Instance.userInterfaceManager.timerDisplay.color = tempTimerColor;
            }
            yield return new WaitForSeconds(0.5f);
            MasterManager.Instance.userInterfaceManager.timerDisplay.color = Color.white;
        }
    }
}