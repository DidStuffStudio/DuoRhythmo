using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class IncrementEuclidean :  OneShotButton {
        private Transform _transform;
        private TweenSharp _tween;
        public float duration = 2.3f;
        [SerializeField] private float targetY;

        private bool _enabled;
        
        public bool Enabled {
            get => _enabled;
            set {
                _enabled = value;
                if (_enabled) Enable();
                else Disable();
            }
        }

        

        protected override void OnEnable() {
            base.OnEnable();
            _transform = transform;
            _transform.localPosition = Vector3.zero;
            _transform.localScale = Vector3.zero;
        }
        
        private void Enable() {
            print("Enable");
            _tween = new TweenSharp(gameObject, duration, new {
                scale = 1.0f,
                localY = targetY,
                alpha = 1.0f,
                ease = Elastic.EaseOut
            });
            _tween.Restart();
        }

        private void Disable() {
            print("Disable");
            _transform = transform;
            _tween = new TweenSharp(gameObject, duration, new {
                scale = 0f,
                localY = 0f,
                alpha = 0.0f,
                ease = Quad.EaseIn
            });
            _tween.Restart();
        }

        protected override void ButtonClicked() {
            base.ButtonClicked();
        }
    }
}