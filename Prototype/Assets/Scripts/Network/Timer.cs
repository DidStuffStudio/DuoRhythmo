using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class Timer : MonoBehaviour {
    public int roundTime;
    public float timer;
    private double startingRoomTime;
    private void Start() {
        /*ToggleTimer(restart: true);
        MasterManager.Instance.userInterfaceManager.startTimer = true;*/
        timer = roundTime;
    }

    public void ToggleTimer(bool restart) {
        if(restart) StartCoroutine(Time());
        else StopCoroutine(Time());
    }

    private void FixedUpdate()
    {
        
        if (roundTime > 0 && MasterManager.Instance.userInterfaceManager.startTimer && RealTimeInstance.Instance.isSoloMode)
        {
            timer -= UnityEngine.Time.fixedDeltaTime;
            if (timer <= 0.0f) {
                timer = roundTime;
                MasterManager.Instance.userInterfaceManager.PlayAnimation();
                MasterManager.Instance.userInterfaceManager.startTimer = false;
            }
        }
        else if (MasterManager.Instance.userInterfaceManager.startTimer)
        {
            StartCoroutine(Time());
            MasterManager.Instance.userInterfaceManager.startTimer = false;
        }
    }

    private IEnumerator Time() {
        timer = roundTime;
        var startRoomTime = RealTimeInstance.Instance.GetRoomTime();
        while (timer > 0) {
            
            yield return new WaitForEndOfFrame();
            timer -= (float)(RealTimeInstance.Instance.GetRoomTime()-startRoomTime);
            
        }
    }
}