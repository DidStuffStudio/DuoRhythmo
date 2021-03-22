using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
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
    private VisualEffect vfx;

    public int bpm = 120;
    private ScreenSyncTest[] _screenSyncTests = new ScreenSyncTest[5];
    private IEnumerator WaitUntilConnected() {
        
        while (true) {
            if(RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }
        startTimer = true;
    }
    void Start()
    {
        int i = 0;
        foreach (var screenSync in GetComponentsInChildren<ScreenSyncTest>())
        {
            _screenSyncTests[i] = screenSync;
            i++;
        }
        vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
        _uiAnimator = GetComponent<Animator>();
        _uiAnimator.speed = 0.0f;
        _playerAnimator.speed = 0.0f;
        if (RealTimeInstance.Instance.isSoloMode)
        {
            startTimer = true;
            return;
        }
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

        for (int i = 0; i < 5; i++)
        {
            _screenSyncTests[i].bpm = bpm;
        }
        
        if (timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            skybox.SetColor("_Tint", targetColor);
            vfx.SetVector4("ParticleColor", targetColor);
            vfx.SetVector4("Core color", targetColor);
            // start a new transition
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 30.0f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            
            skybox.SetColor("_Tint", Color.Lerp(skybox.GetColor("_Tint"), targetColor, Time.deltaTime / timeLeft));
            vfx.SetVector4("ParticleColor",Color.Lerp(skybox.GetColor("_Tint"), targetColor, Time.deltaTime / timeLeft));
            vfx.SetVector4("Core color",Color.Lerp(skybox.GetColor("_Tint"), targetColor, Time.deltaTime / timeLeft));
 
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
