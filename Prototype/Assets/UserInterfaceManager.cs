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
    private int currentPanel = 0; 
    [SerializeField] private String[] drumVolumeRtpcStrings = new string[5];
    public int bpm = 120;
    private ScreenSyncTest[] _screenSyncTests = new ScreenSyncTest[5];
    public CustomButton[] soloButtons = new CustomButton[10];
    private IEnumerator WaitUntilConnected() {
        
        while (true) {
            if(RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }
        var index = 0;
        foreach (var soloButton in GameObject.FindGameObjectsWithTag("SoloButton"))
        {
            soloButtons[index] = soloButton.GetComponent<CustomButton>();
            index++;
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
            var index = 0;
            foreach (var soloButton in GameObject.FindGameObjectsWithTag("SoloButton"))
            {
                soloButtons[index] = soloButton.GetComponent<CustomButton>();
                index++;
            }
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
        if (currentPanel >= 4) currentPanel = 0; //Change if adding more drums
        else currentPanel++;
        print(currentPanel);
        Solo(false);
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

    public void Solo(bool solo)
    {
        if (solo)
        {
            for (int i = 0; i < drumVolumeRtpcStrings.Length; i++)
            {
                AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[i], 10.0f);
            }
            
            AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[currentPanel], 100.0f);
            
        }
        else
        {
            for (int i = 0; i < drumVolumeRtpcStrings.Length; i++) AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[i], 100.0f);
            for (int i = 0; i < soloButtons.Length; i++) soloButtons[i].SetDefault();
        }
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
