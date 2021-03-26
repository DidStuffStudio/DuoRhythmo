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
    private float _timeLeft;
    private Color _targetColor;
    private VisualEffect _vfx;
    private int _currentPanel = 0; 
    [SerializeField] private String[] drumVolumeRtpcStrings = new string[5];
    public int bpm = 120;
    private ScreenSyncTest[] _screenSyncTests = new ScreenSyncTest[5];
    public CustomButton[] soloButtons = new CustomButton[10];
    private static readonly int Tint = Shader.PropertyToID("_Tint");

    private IEnumerator WaitUntilConnected() {
        
        while (true) {
            if(RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1) break;
            yield return new WaitForEndOfFrame();
        }
        var index = 0;
        foreach (var soloButton in GameObject.FindGameObjectsWithTag("SoloButton")) //Each euclidean manager should spawn a solo button and assign itself here
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
        _vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
        _uiAnimator = GetComponent<Animator>();
        _uiAnimator.speed = 0.0f;
        _playerAnimator.speed = 0.0f;
        if (RealTimeInstance.Instance.isSoloMode)
        {
            startTimer = true;
            var index = 0;
            foreach (var soloButton in GameObject.FindGameObjectsWithTag("SoloButton")) //Each euclidean manager should spawn a solo button and assign itself here
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
        if (_currentPanel >= 4) _currentPanel = 0; //Change if adding more drums
        else _currentPanel++;
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
        
        if (_timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            skybox.SetColor(Tint, _targetColor);
            _vfx.SetVector4("ParticleColor", _targetColor);
            _vfx.SetVector4("Core color", _targetColor);
            // start a new transition
            _targetColor = new Color(Random.value, Random.value, Random.value);
            _timeLeft = 30.0f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            
            skybox.SetColor(Tint, Color.Lerp(skybox.GetColor(Tint), _targetColor, Time.deltaTime / _timeLeft));
            _vfx.SetVector4("ParticleColor",Color.Lerp(skybox.GetColor(Tint), _targetColor, Time.deltaTime / _timeLeft));
            _vfx.SetVector4("Core color",Color.Lerp(skybox.GetColor(Tint), _targetColor, Time.deltaTime / _timeLeft));
 
            // update the timer
            _timeLeft -= Time.deltaTime;
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
            
            AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[_currentPanel], 100.0f);
            
        }
        else
        {
            foreach (var t in drumVolumeRtpcStrings) AkSoundEngine.SetRTPCValue(t, 100.0f);

            foreach (var t in soloButtons) t.SetDefault();
        }
    }
    private IEnumerator Timer()
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
