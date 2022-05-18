using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class OneShotButton : AbstractDidStuffButton
    {
        private void Start()
        {
            _dwellGfx.sizeDelta = Vector2.zero;
            //ToggleDwellGfx(false);
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            //ToggleDwellGfx(true);
        }

        protected override void ToggleButton(bool activate)
        {
            ChangeToInactiveState();
            if(Initialised)ActivatedScaleFeedback();
        }
        
        
    }
}
