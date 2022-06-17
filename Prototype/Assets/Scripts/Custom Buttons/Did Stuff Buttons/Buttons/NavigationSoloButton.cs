using DidStuffLab;
using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class NavigationSoloButton : OneShotButton
    {
        [SerializeField] private bool forward;

        
        protected override void Start()
        {
            base.Start();
            CoolDownTime = 2.0f;
        }

        protected override void ButtonClicked() {
            base.ButtonClicked();
            CarouselLerpMove.Instance.RotateCarouselSolo(forward);
            
        }
    }
}

