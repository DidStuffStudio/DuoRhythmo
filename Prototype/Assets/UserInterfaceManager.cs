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
    public UI_Gaze_Button [] soloButtons;
    private static readonly int Tint = Shader.PropertyToID("_Tint");


    public List<GameObject> panels = new List<GameObject>(); //Put panels into array so we can change their layer to blur or render them over blur

    [SerializeField] private ForwardRendererData _forwardRenderer;
    private bool isRenderingAPanel = false;

    private void Start() {
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
                soloButtons[index] = soloButton.GetComponent<UI_Gaze_Button>();
                index++;
            }

            return;
        }
    }

    public void SetUpInterface(){
        
        var index = 0;
        soloButtons = new UI_Gaze_Button[MasterManager.Instance.numberInstruments * 2];

        foreach (var soloButton in GameObject.FindGameObjectsWithTag("SoloButton"))
        {
            soloButtons[index] = soloButton.GetComponent<UI_Gaze_Button>();
            index++;
        }

        startTimer = true;
    }
    
    public void PauseAnimation() {
        StartCoroutine(SwitchPanelRenderLayers());
        _uiAnimator.speed = 0.0f;
        _playerAnimator.speed = 0.0f;
        startTimer = true;
        timer = (int) MasterManager.Instance.timer.timer;
    }

    public void PlayAnimation() {
        _currentRenderPanel++;
        if (_currentRenderPanel > ((MasterManager.Instance.numberInstruments * 2)- 1)) _currentRenderPanel = 0;
        _currentPanel++;
        if (_currentPanel > (MasterManager.Instance.numberInstruments - 1)) _currentPanel = 0;
        Solo(false, 0);
        _uiAnimator.speed = 1.0f;
        _playerAnimator.speed = 1.0f;
        _playerAnimator.Play("PlayerCam");
        timerRunnning = false;
        // StopCoroutine(Timer());
        if(MasterManager.Instance.localPlayerNumber == 0) MasterManager.Instance.timer.ToggleTimer(restart: false);
    }

    public void Update() {
        if(!MasterManager.Instance.gameSetUpFinished) return;
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
        timer = (int) MasterManager.Instance.timer.timer;
        if (!startTimer || timerRunnning) return;
        // timer = roundTime;
        timerRunnning = true;
        // StartCoroutine(Timer());
        // if(MasterManager.Instance.localPlayerNumber == 0) MasterManager.Instance.timer.ToggleTimer(restart: true);
    }

    public void Solo(bool solo, int index) {
        if (solo) {
            for (int i = 0; i < drumVolumeRtpcStrings.Length; i++) {
                AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[i], 10.0f);
            }

            AkSoundEngine.SetRTPCValue(drumVolumeRtpcStrings[index], 100.0f);
        }
        else {
            foreach (var t in drumVolumeRtpcStrings) AkSoundEngine.SetRTPCValue(t, 100.0f);

            foreach (var t in soloButtons) t.Deactivate();
        }
    }

    public IEnumerator SwitchPanelRenderLayers() {
        // Loop through panels 
        for (int i = 0; i < panels.Count; i++) {
            foreach (var transform in panels[i].GetComponentsInChildren<Transform>()
            ) //Loop through children of the panel
            {
                transform.gameObject.layer =
                    LayerMask.NameToLayer("Default"); //Change their layer to default so they are blurred
            }
        }
        
        isRenderingAPanel = false;

        while (!isRenderingAPanel)
        {
            // send a ray from the middle of the camera to see which panel he's currently looking at
            var ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("RenderTarget"))
                {
                    foreach (var t in hit.transform.GetComponentsInChildren<Transform>()
                    ) //Loop through children of the panel
                    {
                        t.gameObject.layer =
                            LayerMask.NameToLayer("RenderPanel"); //Set current panel to render over blur
                    }

                    isRenderingAPanel = true;
                }
            }
            
            yield return new WaitForSeconds(0.1f);
        }


    }

    private void OnEnable() {
        foreach (var feature in _forwardRenderer.rendererFeatures) feature.SetActive(true);
    }

    private void OnDisable() {
        foreach (var feature in _forwardRenderer.rendererFeatures) feature.SetActive(false);
    }
}