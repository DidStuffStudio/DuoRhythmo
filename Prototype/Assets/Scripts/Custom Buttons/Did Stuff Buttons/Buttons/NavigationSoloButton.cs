using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class NavigationSoloButton : OneShotButton
    {
        private CarouselManager _carouselManager;
        [SerializeField] private bool forward;

        private void Start() {
            _carouselManager = MasterManager.Instance.carouselManager;
            CoolDownTime = 2.0f;
        }

        protected override void ButtonClicked() {
            base.ButtonClicked();
            print("Clicked");
            _carouselManager.PlayAnimation(forward);
        }
        
    }
}

