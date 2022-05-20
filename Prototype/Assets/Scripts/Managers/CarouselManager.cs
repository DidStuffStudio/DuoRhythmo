using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace Managers {
    public class CarouselManager : MonoBehaviour {
        private Animator _uiAnimator;
        private float _timeLeft = 10.0f;
        private Color _targetVFXColor, _targetSkyColor;
        public VisualEffect _vfx;
        private int _currentPanel = 0, _lastPanel = 9, _currentPanelBuddy = 5, _lastPanelBuddy = 4;
        private static readonly int Tint = Shader.PropertyToID("_Tint");
        public float currentRotationOfUI = 0.0f;

        public List<GameObject>
            panels = new List<GameObject>(); //put panels in list so we can change their layer to blur or render them over blur

        [SerializeField] private ForwardRendererData _forwardRenderer;
        private bool isRenderingAPanel = false;
        private bool animateUIBackward = false;
        public bool ignoreEvents;
        public bool justJoined;
        public bool isSoloMode = false;

        [SerializeField] private NavigationVoteSync[] _navigationVoteSyncs;
        public delegate void MoveCarouselAction();
        public static event MoveCarouselAction OnMovedCarousel;
        private bool alreadyLocallyMovedCarousel; // for multiplayer

        private void OnEnable() {
            _forwardRenderer.rendererFeatures[0].SetActive(true);
            NavigationVoteSync.OnVotingCompletedFromServerAction += VotingCompletedFromServer;
        }

        private void Start() {
            _vfx.transform.gameObject.SetActive(false);
            _targetVFXColor = MasterManager.Instance.drumColors[0];
            _uiAnimator = GetComponent<Animator>();
            if (justJoined) return;
            _uiAnimator.Play("Rotation", 0, currentRotationOfUI);
            _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
        }

        public void InitialiseBlur() {
            foreach (var t in panels[0].GetComponentsInChildren<Transform>()) {
                t.gameObject.layer =
                    LayerMask.NameToLayer("RenderPanel"); //Change their layer to default so they are blurred
            }

            //if (isSoloMode) return;

            foreach (var t in panels[5].GetComponentsInChildren<Transform>()) {
                t.gameObject.layer =
                    LayerMask.NameToLayer("RenderPanel"); //Change their layer to default so they are blurred
            }
        }

        public void ToggleVFX(bool activate) {
            if (_vfx.transform.gameObject.activeInHierarchy == activate) return;
            _vfx.transform.gameObject.SetActive(activate);
        }

        public void SetUpRotationForNewPlayer(float time) {
            _uiAnimator.Play("Rotation", 0, time - (int) time);
            _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
            currentRotationOfUI = time - (int) time;
            justJoined = false;
        }

        public void PauseAnimation() {
            if (ignoreEvents) return;
            MasterManager.Instance.ForceSoloOffGlobal();
            BlurBackground();
            _uiAnimator.SetFloat("SpeedMultiplier", 0.0f);
            SetAnimatorTime();
        }

        public void PlayAnimation(bool forward) {
            if (isSoloMode && _currentPanel == _lastPanel) StartCoroutine(IgnoreEvents(0.5f));
            else if (isSoloMode) StartCoroutine(IgnoreEvents(0.1f));
            _lastPanel = _currentPanel;
            _lastPanelBuddy = _currentPanelBuddy;
            // TODO --> turn off solo appropriately
            if (forward) {
                if (_currentPanel < panels.Count - 1) _currentPanel++;
                else _currentPanel = 0;
                _uiAnimator.SetFloat("SpeedMultiplier", 1.0f);
                if (_currentPanelBuddy < panels.Count - 1) _currentPanelBuddy++;
                else _currentPanelBuddy = 0;
                _uiAnimator.SetFloat("SpeedMultiplier", 1.0f);
            }
            else {
                if (_currentPanel > 0) _currentPanel--;
                else _currentPanel = panels.Count - 1;
                if (_currentPanelBuddy > 0) _currentPanelBuddy--;
                else _currentPanelBuddy = panels.Count - 1;
                animateUIBackward = true;
                _uiAnimator.SetFloat("SpeedMultiplier", -1.0f);
            }
        }

        public void Update() {
            if (!MasterManager.Instance.gameSetUpFinished) return;
            if (_timeLeft <= Time.deltaTime) {
                // transition complete
                // assign the target color
                _vfx.SetVector4("ParticleColor", _targetVFXColor);
                _vfx.SetVector4("Core color", _targetVFXColor);
                // start a new transition
                var index = 0;
                if (isSoloMode) {
                    if (_currentPanel % 2 == 1) index = _currentPanel - 1;
                    else index = _currentPanel;

                    index /= 2;
                }
                else {
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
                _vfx.SetVector4("Core color",
                    Color.Lerp(_vfx.GetVector4("Core color"), _targetVFXColor, Time.deltaTime / _timeLeft));

                // update the timer
                _timeLeft -= Time.deltaTime;
            }
        }

        public void BlurBackground() {
            foreach (var t in panels[_lastPanel].GetComponentsInChildren<Transform>()) {
                t.gameObject.layer =
                    LayerMask.NameToLayer("Default"); //Change their layer to default so they are blurred
            }

            if (!isSoloMode) {
                foreach (var t in panels[_lastPanelBuddy].GetComponentsInChildren<Transform>()) {
                    t.gameObject.layer =
                        LayerMask.NameToLayer("Default"); //Change their layer to default so they are blurred
                }
            }

            foreach (var t in panels[_currentPanel].GetComponentsInChildren<Transform>()) {
                t.gameObject.layer =
                    LayerMask.NameToLayer("RenderPanel"); //Change their layer to default so they are blurred
            }

            if (!isSoloMode) {
                foreach (var t in panels[_currentPanelBuddy].GetComponentsInChildren<Transform>()) {
                    t.gameObject.layer =
                        LayerMask.NameToLayer("RenderPanel"); //Change their layer to default so they are blurred
                }
            }
        }

        public void UpdateVoteToMove(bool activate, bool forward) {
            var navSync = forward ? _navigationVoteSyncs[0] : _navigationVoteSyncs[1];
            var offset = (activate ? 1 : -1);
            if (offset < 0 || alreadyLocallyMovedCarousel) {
                alreadyLocallyMovedCarousel = false;
                return;
            }
            print("Local player wants to move forward by " + offset + " - " + forward);
            var newValue = navSync.VotingValue + offset;
            if (newValue > 1) {
                PlayAnimation(forward);
                // if carousel has moved, invoke the moved carousel event
                OnMovedCarousel?.Invoke();
                alreadyLocallyMovedCarousel = true;
            }
            if (forward) navSync.ChangeValue((byte) (navSync.VotingValue + offset));
            else navSync.ChangeValue((byte) (navSync.VotingValue + offset));
        }

        private void VotingCompletedFromServer(bool forward) {
            PlayAnimation(forward);
            /*
            // if carousel has moved, invoke the moved carousel event
            OnMovedCarousel?.Invoke();
            
            // reset the voting
            var navSync = forward ? _navigationVoteSyncs[0] : _navigationVoteSyncs[1];
            navSync.ResetVoting();
            */
        }

        public void SetAnimatorTime() {
            // set animation time when new player joins
        }

        private IEnumerator IgnoreEvents(float ignoreTime) {
            ignoreEvents = true;
            yield return new WaitForSeconds(ignoreTime);
            ignoreEvents = false;
        }


        public void OpenFeedbackSite() => Application.OpenURL("https://duorhythmo.frill.co/b/zv9dw6m1/feature-ideas");

        private void OnDisable() {
            _forwardRenderer.rendererFeatures[0].SetActive(false);
            NavigationVoteSync.OnVotingCompletedFromServerAction -= VotingCompletedFromServer;
        }
    }
}