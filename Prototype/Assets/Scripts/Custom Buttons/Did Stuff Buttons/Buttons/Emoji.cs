using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons {
    
    public class Emoji : OneShotButton {
        private TweenSharp _tween;
        public float duration = 2.3f;
        [SerializeField] private Vector3 targetPosition;

        protected override void OnEnable() {
            base.OnEnable();
            _tween = new TweenSharp(gameObject, duration, new {
                scale = 1.0f,
                localX = targetPosition.x,
                localY = targetPosition.y,
                ease = Bounce.EaseOut
            });
            _tween.Restart();
        }
    }
}