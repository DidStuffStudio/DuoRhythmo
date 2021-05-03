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
    public Text timerDisplay;
    public Material skybox;
    private float _timeLeft = 10.0f;
    private Color _targetVFXColor, _targetSkyColor;
    private VisualEffect _vfx;
    private int _currentPanel = 0, _currentRenderPanel = 0;

    [SerializeField] private String[] drumVolumeRtpcStrings = new string[5];
    public int bpm = 120;
    public UI_Gaze_Button [] soloButtons;
    public DwellSpeedButton[] dwellSpeedButtons;
    private static readonly int Tint = Shader.PropertyToID("_Tint");
    [SerializeField] private int numberOfDwellSpeeds;
    public float currentRotationOfUI = 0.0f;

    public List<GameObject> panels = new List<GameObject>(); //Put panels into array so we can change their layer to blur or render them over blur

    [SerializeField] private ForwardRendererData _forwardRenderer;
    private bool isRenderingAPanel = false;

    public GameObject dwellSpeedPrefab;
    private bool animateUIBackward = false;
    private bool ignoreEvents;

    private void Start() {
       
        _vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
        _uiAnimator = GetComponent<Animator>();
        _uiAnimator.Play("Rotation", 0, currentRotationOfUI);
        _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
        _playerAnimator.speed = 0.0f;
    }
    

    public void SetUpRotationForNewPlayer(float time)
    {
        _uiAnimator.Play("Rotation", 0, time);
    }
    
    public void PauseAnimation()
    {
        if (ignoreEvents) return;
        StartCoroutine(SwitchPanelRenderLayers());
        _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
        _playerAnimator.speed = 0.0f;
        //timer = (int) MasterManager.Instance.timer.timer;
        SetAnimatorTime();
        if(!RealTimeInstance.Instance.isSoloMode) StartCoroutine(MasterManager.Instance.timer.MainTime());
    }

    public void PlayAnimation(bool forward)
    {
        if(RealTimeInstance.Instance.isSoloMode)StartCoroutine(IgnoreEvents());
        if (forward)
        {
            _currentRenderPanel++;
            if (_currentRenderPanel > ((MasterManager.Instance.numberInstruments * 2) - 1)) _currentRenderPanel = 0;
            _currentPanel++;
            if (_currentPanel > (MasterManager.Instance.numberInstruments - 1)) _currentPanel = 0;
            Solo(false, 0);
            _uiAnimator.SetFloat("SpeedMultiplier", 1.0f);
            if (!MasterManager.Instance.isInPosition)
            {
                _playerAnimator.speed = 1.0f;
                _playerAnimator.Play("PlayerCam");
            }
        }
        else
        {
            _currentRenderPanel--;
            if (_currentRenderPanel < 0) _currentRenderPanel =  (MasterManager.Instance.numberInstruments * 2) - 1;
            _currentPanel--;
            if (_currentPanel <= 0) _currentPanel = MasterManager.Instance.numberInstruments - 1;
            Solo(false, 0);
            animateUIBackward = true;
            _uiAnimator.SetFloat("SpeedMultiplier", -1.0f);
            print("Rotating back");
        }
    }

    public void Update() {
        
        if (_timeLeft <= Time.deltaTime) {
            // transition complete
            // assign the target color
            _vfx.SetVector4("ParticleColor", _targetVFXColor);
            _vfx.SetVector4("Core color", _targetVFXColor);
            // start a new transition
            var index = Random.Range(0, MasterManager.Instance.numberInstruments);
            _targetVFXColor = MasterManager.Instance.drumColors[_currentPanel];
            _timeLeft = 10.0f;
        }
        else {
            // transition in progress
            // calculate interpolated color
            
            _vfx.SetVector4("ParticleColor",
                Color.Lerp(_vfx.GetVector4("ParticleColor"), _targetVFXColor, Time.deltaTime / _timeLeft));
            _vfx.SetVector4("Core color", Color.Lerp(_vfx.GetVector4("Core color"), _targetVFXColor, Time.deltaTime / _timeLeft));

            // update the timer
            _timeLeft -= Time.deltaTime;
        }
        
        timerDisplay.text = MasterManager.Instance.timer.timer.ToString();
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

    public void SetAnimatorTime() => currentRotationOfUI = _uiAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    

    private void OnEnable() {
        foreach (var feature in _forwardRenderer.rendererFeatures) feature.SetActive(true);
    }

    private void OnDisable() {
        foreach (var feature in _forwardRenderer.rendererFeatures) feature.SetActive(false);
    }

    public void InstantiateDwellSpeedPrefab()
    {
        Instantiate(dwellSpeedPrefab, Camera.main.transform);
    }


    private IEnumerator IgnoreEvents()
    {
        ignoreEvents = true;
        yield return new WaitForSeconds(0.1f);
        ignoreEvents = false;
    }
}