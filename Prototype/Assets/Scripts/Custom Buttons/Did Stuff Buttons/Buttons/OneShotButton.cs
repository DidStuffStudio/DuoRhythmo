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
            ActivatedScaleFeedback();
        }
    }
}
