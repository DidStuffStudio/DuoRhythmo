using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class Emoji : OneShotButton
    {
        private static GameObject _gameObject;
        private RectTransform _rectTransform;
        private TweenSharp tween;
        public float d = 10f;
        protected void Start()
        {
          
            
        }
        

        protected override void OnEnable()
        {
            _gameObject = gameObject;
            _rectTransform = GetComponent<RectTransform>();
            //_rectTransform.localScale = new Vector3(0, 0, 1);
            tween = new TweenSharp(_gameObject, d, new
            {
                scale = 1.0f,
                ease = Bounce.EaseOut
            });
            
            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();
            print(tween.Progress);
        }
    }
}
