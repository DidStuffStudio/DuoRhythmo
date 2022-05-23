using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class OneShotButton : AbstractDidStuffButton
    {
      

        protected override void ToggleButton(bool activate)
        {
            ChangeToInactiveState();
            //_currentDwellTime = DwellTime;
            if(Initialised)ActivatedScaleFeedback();
        }

        public void SetInactive() => gameObject.SetActive(false);

        protected override void OnDisable()
        {
            base.OnDisable();
            _dwellGfx.localScale = dwellScaleX ? new Vector3(0, _originaldwellScaleY, 1) : Vector3.zero;
			ToggleDwellGfx(false);
        }
    }
}
