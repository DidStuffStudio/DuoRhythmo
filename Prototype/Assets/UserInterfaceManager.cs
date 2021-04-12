using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class UserInterfaceManager : MonoBehaviour {
    private Animator _uiAnimator;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private int roundTime = 30, timer;
    public Text timerDisplay;
    public bool startTimer, timerRunnning;
    public Material skybox;
    private float _timeLeft;
    private Color _targetColor;
    private VisualEffect _vfx;
    private int _currentPanel = 0, _currentRenderPanel = 0;

    [SerializeField] private String[] drumVolumeRtpcStrings = new string[5];
    public int bpm = 120;
    public CustomButton [] soloButtons;
    private static readonly int Tint = Shader.PropertyToID("_Tint");


    public GameObject[]
        panels = new GameObject[10]; //Put panels into array so we can change their layer to blur or render them over blur

    [SerializeField] private ForwardRendererData _forwardRenderer;

    void Start() {
        int i = 0;
        _vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
        _uiAnimator = GetComponent<Animator>();
        _uiAnimator.speed = 0.0f;
        _playerAnimator.speed = 0.0f;
        if (RealTimeInstance.Instance.isSoloMode) {
            startTimer = true;
            var index = 0;
            foreach (var soloButton in GameObject.FindGameObjectsWithTag("SoloButton")
            ) //Each euclidean manager should spawn a solo button and assign itself here
            {
                soloButtons[index] = soloButton.GetComponent<CustomButton>();
                index++;
            }

            return;
        }

        StartCoroutine(WaitUntilConnected());
    }

    private IEnumerator WaitUntilConnected() {
        while (true) {
            if (RealTimeInstance.Instance.isConnected && RealTimeInstance.Instance.numberPlayers > 1 &&
                MasterManager.Instance.gameSetupFinished) break;
            yield return new WaitForEndOfFrame();
        }

        var index = 0;
        soloButtons = new CustomButton[MasterManager.Instance.numberInstruments];
        foreach (var soloButton in GameObject.FindGameObjectsWithTag("SoloButton")
        ) //Each node manager should spawn a solo button and assign itself here
        {
            soloButtons[index] = soloButton.GetComponent<CustomButton>();
            index++;
        }

        startTimer = true;
    }

    public void PauseAnimation() {
        SwitchPanelRenderLayers();
        _uiAnimator.speed = 0.0f;
        _playerAnimator.speed = 0.0f;
        startTimer = true;
    }

    public void PlayAnimation() {
        _currentRenderPanel++;
        if (_currentRenderPanel > 9) _currentRenderPanel = 0;
        if (_currentPanel >= 4) _currentPanel = 0; //Change if adding more drums
        else _currentPanel++;
        Solo(false);
        _uiAnimator.speed = 1.0f;
        _playerAnimator.speed = 1.0f;
        _playerAnimator.Play("PlayerCam");
        timerRunnning = false;
        StopCoroutine(Timer());
    }

    public void Update() {
        if (_timeLeft <= Time.deltaTime) {
            // transition complete
            // assign the target color
            skybox.SetColor(Tint, _targetColor);
            _vfx.SetVector4("ParticleColor", _targetColor);
            _vfx.SetVector4("Core color", _targetColor);
            // start a new transition
            _targetColor = new Color(Random.value, Random.value, Random.value);
            _timeLeft = 30.0f;
        }
        else {
            // transition in progress
            // calculate interpolated color

            skybox.SetColor(Tint, Color.Lerp(skybox.GetColor(Tint), _targetColor, Time.deltaTime / _timeLeft));
            _vfx.SetVector4("ParticleColor",
                Color.Lerp(skybox.GetColor(Tint), _targetColor, Time.deltaTime / _timeLeft));
            _vfx.SetVector4("Core color", Color.Lerp(skybox.GetColor(Tint), _targetColor, Time.deltaTime / _timeLeft));

            // update the timer
            _timeLeft -= Time.deltaTime;
        }

        timerDisplay.text = timer.ToString();
        if (!startTimer || timerRunnning) return;
        timer = roundTime;
        timerRunnning = true;
        StartCoroutine(Timer());
    }

    public void Solo(bool solo) {
        if (solo) {
            for (int i = 0; i < drumVolumeRtpcStrings.Length; i++) {
                AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[i], 10.0f);
            }

            AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[_currentPanel], 100.0f);
        }
        else {
            foreach (var t in drumVolumeRtpcStrings) AkSoundEngine.SetRTPCValue(t, 100.0f);

            // foreach (var t in soloButtons) t.SetDefault();
        }
    }

    public void SwitchPanelRenderLayers() {
        for (int i = 0; i < panels.Length; i++) //Loop through panels
        {
            foreach (var transform in panels[i].GetComponentsInChildren<Transform>()
            ) //Loop through children of the panel
            {
                transform.gameObject.layer =
                    LayerMask.NameToLayer("Default"); //Change their layer to default so they are blurred
            }
        }

        foreach (var transform in panels[_currentRenderPanel].GetComponentsInChildren<Transform>()
        ) //Loop through children of the panel
        {
            transform.gameObject.layer = LayerMask.NameToLayer("RenderPanel"); //Set current panel to render over blur
        }
    }

    private IEnumerator Timer() {
        while (timerRunnning) {
            yield return new WaitForSeconds(1.0f);
            timer--;

            if (timer <= 0.0f) {
                startTimer = false;
                PlayAnimation();
            }
        }
    }

    private void OnEnable() {
        foreach (var feature in _forwardRenderer.rendererFeatures) feature.SetActive(true);
    }

    private void OnDisable() {
        foreach (var feature in _forwardRenderer.rendererFeatures) feature.SetActive(false);
    }
}