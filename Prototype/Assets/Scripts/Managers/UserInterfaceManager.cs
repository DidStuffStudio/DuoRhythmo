using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour {
        private Animator _uiAnimator;
        [SerializeField] private Animator _playerAnimator;
        public Text timerDisplay;
        public Material skybox;
        private float _timeLeft = 10.0f;
        private Color _targetVFXColor, _targetSkyColor;
        public VisualEffect _vfx;
        private int _currentPanel = 0, _lastPanel = 0;

        [SerializeField] private String[] drumVolumeRtpcStrings = new string[5];
        public int bpm = 120;
        public DidStuffSolo[] soloButtons;
        private static readonly int Tint = Shader.PropertyToID("_Tint");
        [SerializeField] private int numberOfDwellSpeeds;
        public float currentRotationOfUI = 0.0f;

        public List<GameObject> panels = new List<GameObject>(); //Put panels into list so we can change their layer to blur or render them over blur

        [SerializeField] private ForwardRendererData _forwardRenderer;
        private bool isRenderingAPanel = false;
        private bool animateUIBackward = false;
        public bool ignoreEvents;
        [SerializeField] private GameObject roomFullToast;
        public bool playingAnim;
        public bool justJoined;
        public bool isSoloMode;
        
        private void Start() {
            //roomFullToast = GameObject.FindWithTag("RoomFullToast");
            // _vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
            _vfx.transform.gameObject.SetActive(false);
            _uiAnimator = GetComponent<Animator>();
            _targetVFXColor = MasterManager.Instance.drumColors[0];
   
            if(justJoined) return;
            _uiAnimator.Play("Rotation", 0, currentRotationOfUI);
            _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
        }
        public void ToggleVFX(bool activate)
        {
            if(_vfx.transform.gameObject.activeInHierarchy == activate) return;
            _vfx.transform.gameObject.SetActive(activate);
        }
        public void SetUpRotationForNewPlayer(float time)
        {
            _uiAnimator.Play("Rotation", 0, time-(int)time);
            _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
            currentRotationOfUI = time - (int) time;
            justJoined = false;
        }
    
        public void PauseAnimation()
        {
        
            if (ignoreEvents) return;
        
            BlurBackground();
            _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
            SetAnimatorTime();
            StartCoroutine(WaitToEnableRot());
        }

        public void PlayAnimation(bool forward)
        {
        
            if(isSoloMode && _currentPanel == _lastPanel)StartCoroutine(IgnoreEvents(0.5f));
            else if(isSoloMode)StartCoroutine(IgnoreEvents(0.1f));
            _lastPanel = _currentPanel;
            // TODO --> turn off solo appropriately
            if (forward)
            {
                if(_currentPanel < panels.Count - 1)_currentPanel++;
                else _currentPanel = 0;
                _uiAnimator.SetFloat("SpeedMultiplier", 1.0f);
            }
            else
            {
                if(_currentPanel > 0)_currentPanel--;
                else _currentPanel = panels.Count -1 ;
                animateUIBackward = true;
                _uiAnimator.SetFloat("SpeedMultiplier", -1.0f);
            }
        }

        public void Update()
        {

            if (!MasterManager.Instance.gameSetUpFinished) return;
            if (_timeLeft <= Time.deltaTime) {
                // transition complete
                // assign the target color
                _vfx.SetVector4("ParticleColor", _targetVFXColor);
                _vfx.SetVector4("Core color", _targetVFXColor);
                // start a new transition
                var index = 0;
                if (isSoloMode)
                {
                    if (_currentPanel % 2 == 1) index = _currentPanel - 1;
                    else index = _currentPanel;

                    index /= 2;
                }
                else
                {
                    index = _currentPanel;
                    if (_currentPanel > 4) index = _currentPanel - 5;
                }

                _targetVFXColor = MasterManager.Instance.drumColors[index];
                _timeLeft = 5.0f;
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
        }

        private void BlurBackground()
        {
            foreach (var t in panels[_lastPanel].GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer =
                    LayerMask.NameToLayer("Default"); //Change their layer to default so they are blurred
            }
            foreach (var t in panels[_currentPanel].GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer =
                    LayerMask.NameToLayer("RenderPanel"); //Change their layer to default so they are blurred
            }
        
        }
        public void InitialiseBlur() {
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
            var panelLandedOn = 0;
            while (!isRenderingAPanel)
            {
                // send a ray from the middle of the camera to see which panel he's currently looking at
                var ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.CompareTag("RenderTarget"))
                    {
                        panelLandedOn = panels.IndexOf(hit.transform.gameObject);
                        foreach (var t in hit.transform.GetComponentsInChildren<Transform>()
                        ) //Loop through children of the panel
                        {
                            t.gameObject.layer =
                                LayerMask.NameToLayer("RenderPanel"); //Set current panel to render over blur
                        }

                        isRenderingAPanel = true;
                        _currentPanel = panelLandedOn;
                    }
                }
            }
        }

        public void SetAnimatorTime()
        {
            // set animation time when new player joins
        }

        private IEnumerator IgnoreEvents(float ignoreTime)
        {
            ignoreEvents = true;
            yield return new WaitForSeconds(ignoreTime);
            ignoreEvents = false;
        }
    
        private IEnumerator WaitToEnableRot()
        {
            yield return new WaitForSeconds(1.0f);
            playingAnim = false;
        }

        public void OpenFeedbackSite()=>Application.OpenURL("https://duorhythmo.frill.co/b/zv9dw6m1/feature-ideas");

    }
}