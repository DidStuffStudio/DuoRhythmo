using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UserInterfaceManager : MonoBehaviour
{
    private Animator _uiAnimator;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private int roundTime = 30, timer;
    public Text timerDisplay;
    public bool startTimer, timerRunnning;
    public Material skybox;
    private float timeLeft;
    private Color targetColor;
    private IEnumerator WaitUntilConnected() {
        
        while (true) {
            if(RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }
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
        if (timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            skybox.SetColor("_Tint", targetColor);
            
            // start a new transition
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 30.0f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            skybox.SetColor("_Tint", Color.Lerp(skybox.GetColor("_Tint"), targetColor, Time.deltaTime / timeLeft));
 
            // update the timer
            timeLeft -= Time.deltaTime;
        }
        
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
