using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace DidStuffLab.Scripts.Managers {
    public class CarouselManager : MonoBehaviour {
        private float _timeLeft = 10.0f;
        private Color _targetVFXColor;
        public VisualEffect _vfx;
        private int _currentPanel = 0, _lastPanel = 0, _currentPanelBuddy = 5, _lastPanelBuddy = 5;
        private static readonly int Tint = Shader.PropertyToID("_Tint");
        public float currentRotationOfUI = 0.0f;
        [SerializeField] private int[] panelsToStartUnblurred = new int[2];

        public List<GameObject>
            panels = new List<GameObject>(10); //put panels in list so we can change their layer to blur or render them over blur

        [SerializeField] private ForwardRendererData _forwardRenderer;
        private bool isRenderingAPanel = false;
        public bool justJoined;
        public bool isSoloMode = false;
        [SerializeField] private InGameInteractionManager interactionManager;
        [SerializeField] private NavigationVoteSync[] _navigationVoteSyncs;
        private GraphicRaycaster _currentGraphicRaycaster;
        public delegate void MoveCarouselAction();
        public static event MoveCarouselAction OnMovedCarousel;
        
        public bool votedToMoveLocally = false;

        private EmojiSync _emojiSync;

        public EmojiSync EmojiSync {
            get => _emojiSync;
            set {
                _emojiSync = value;
                for (byte index = 0; index < emojiSpawners.Length; index++) {
                    if(emojiParticles.ContainsKey(index)) break; // stop loop, cause it's already been iterated
                    var es = emojiSpawners[index];
                    emojiParticles.Add(index, es.GetComponentsInChildren<ParticleSystem>());
                }
            }
        }
        public GameObject [] emojiSpawners;
        public Dictionary<byte, ParticleSystem[]> emojiParticles { get; private set; } = new Dictionary<byte, ParticleSystem[]>();
        
        private void OnEnable() {
            _forwardRenderer.rendererFeatures[0].SetActive(true);
        }

        private void Start() {
            _vfx.transform.gameObject.SetActive(false);
            _targetVFXColor = MasterManager.Instance.drumColors[0];
            if (justJoined) return;
        }

        public void InitialiseBlur()
        {
            foreach (var t1 in panelsToStartUnblurred)
            {
                foreach (var t in panels[t1].GetComponentsInChildren<Transform>()) {
                    t.gameObject.layer =
                        LayerMask.NameToLayer("RenderPanel");
                }
            }
        }

        public void InitialiseGraphicRaycast()
        {
            _currentGraphicRaycaster = panels[_currentPanel].GetComponentInChildren<GraphicRaycaster>();
            _currentGraphicRaycaster.enabled = true;
        }

        public void ToggleVFX(bool activate) {
            if (_vfx.transform.gameObject.activeInHierarchy == activate) return;
            _vfx.transform.gameObject.SetActive(activate);
        }

        private void ChangedPanel() {
            MasterManager.Instance.ForceSoloOffGlobal();
            BlurBackground();
        }

        public void BlurSolo(bool forward) {
            _lastPanel = _currentPanel;
            _lastPanelBuddy = _currentPanelBuddy;
            if (forward) {
                if (_currentPanel < panels.Count - 1) _currentPanel++;
                else _currentPanel = 0;
            }
            else {
                if (_currentPanel > 0) _currentPanel--;
                else _currentPanel = panels.Count - 1;
            }

            _currentGraphicRaycaster.enabled = false;
            _currentGraphicRaycaster = panels[_currentPanel].GetComponentInChildren<GraphicRaycaster>();
            _currentGraphicRaycaster.enabled = true;
            interactionManager.SwitchDrumPanel(_currentGraphicRaycaster);
            ChangedPanel();
        }

        public void SwapBlurForPlayer(int current, int last, int currentBuddy, int lastBuddy) {
            _currentPanel = current;
            _lastPanel = last;
            _currentPanelBuddy = currentBuddy;
            _lastPanelBuddy = lastBuddy;
            interactionManager.SwitchDrumPanel(panels[current].GetComponent<GraphicRaycaster>());
        }

        public void BlurFromServer(bool forward) {
            _lastPanel = _currentPanel;
            _lastPanelBuddy = _currentPanelBuddy;
            
            if (forward) {
                if (_currentPanel < panels.Count - 1) _currentPanel++;
                else _currentPanel = 0;
                if (_currentPanelBuddy < panels.Count - 1) _currentPanelBuddy++;
                else _currentPanelBuddy = 0;
            }
            else {
                if (_currentPanel > 0) _currentPanel--;
                else _currentPanel = panels.Count - 1;
                if (_currentPanelBuddy > 0) _currentPanelBuddy--;
                else _currentPanelBuddy = panels.Count - 1;
            }

            foreach (var navsync in _navigationVoteSyncs) {
                if(navsync.VotingValue > 0) navsync.ResetVoting();
            }

            _currentGraphicRaycaster.enabled = false;
            _currentGraphicRaycaster = panels[_currentPanel].GetComponentInChildren<GraphicRaycaster>();
            _currentGraphicRaycaster.enabled = true;
            interactionManager.SwitchDrumPanel(_currentGraphicRaycaster);
            ChangedPanel();
            OnMovedCarousel?.Invoke();
        }

        public void Update() {
            if(CarouselLerpMove.Instance != null) transform.rotation = CarouselLerpMove.Instance.transform.rotation;
            if (!MasterManager.Instance.gameSetUpFinished || !isSoloMode) return;
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

        public void UpdateVoteToMoveToServer(bool activate, bool forward) {
            var navSync = forward ? _navigationVoteSyncs[0] : _navigationVoteSyncs[1];
            var offset = (activate ? 1 : -1);
            print("Local player wants to move forward by " + offset + " - " + forward);
            var newValue = navSync.VotingValue + offset;
            if (newValue > 1) {
                // tell the server to lerp move the carousel
                CarouselLerpMove.Instance.UpdateLerpMoveRotation(forward);
            }
            else navSync.ChangeValue((byte) (newValue));
        }
        
        public void UpdateNavSyncTexts(string username, bool forward) {
            var navSync = forward ? _navigationVoteSyncs[0] : _navigationVoteSyncs[1];
            var voteMessage = forward ? " wants to move right" : " wants to move left";
            foreach (var text in navSync.texts) {
                text.text = username + voteMessage;
            }
        }

        public void OpenFeedbackSite() => Application.OpenURL("https://duorhythmo.frill.co/b/zv9dw6m1/feature-ideas");

        private void OnDisable() {
            _forwardRenderer.rendererFeatures[0].SetActive(false);
        }
    }
}