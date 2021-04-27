using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public int roundTime;
    public float timer;
    private void Start() {
        ToggleTimer(restart: true);
        MasterManager.Instance.userInterfaceManager.startTimer = true;
    }

    public void ToggleTimer(bool restart) {
        if(restart) StartCoroutine(Time());
        else StopCoroutine(Time());
    }

    /*private void FixedUpdate()
    {
        if (roundTime > 0)
        {
            timer -= UnityEngine.Time.fixedDeltaTime;
        }
    }*/

    private IEnumerator Time() {
        timer = roundTime;
        while (true) {
            yield return new WaitForSeconds(1.0f);
            timer--;
            if (timer <= 0.0f) {
                timer = roundTime;
                MasterManager.Instance.userInterfaceManager.PlayAnimation();
                MasterManager.Instance.userInterfaceManager.startTimer = false;
            }
        }
    }
}