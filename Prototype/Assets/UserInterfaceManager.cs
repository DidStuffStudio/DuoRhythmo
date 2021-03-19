using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    private Animator _uiAnimator;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private int roundTime = 30, timer;
    public Text timerDisplay;
    public bool startTimer, timerRunnning;
    
    private IEnumerator WaitUntilConnected() {
        while (!RealTimeInstance.Instance.isConnected) yield return null;
        startTimer = true;
    }
    void Start()
    {
        _uiAnimator = GetComponent<Animator>();
        _uiAnimator.speed = 0.0f;
        _playerAnimator.speed = 0.0f;
        StartCoroutine(WaitUntilConnected());
    }

    public void PauseAnimation()
    {
        _uiAnimator.speed = 0.0f;
        _playerAnimator.speed = 0.0f;
        startTimer = true;
    }

    public void PlayAnimation()
    {
        _uiAnimator.speed = 1.0f;
        _playerAnimator.speed = 1.0f;
        _playerAnimator.Play("PlayerCam");
        timerRunnning = false;
        StopCoroutine(Timer());
    }

    public void Update()
    {
        timerDisplay.text = timer.ToString();
        if (!startTimer || timerRunnning) return;
        timer = roundTime;
        timerRunnning = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while(timerRunnning)
        {
            yield return new WaitForSeconds(1.0f);
            timer--;
            
            if (timer <= 0.0f)
            {
                startTimer = false;
                PlayAnimation();
            }
        }
    }


}
