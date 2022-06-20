using System;
using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class DidStuffRecord : AbstractDidStuffButton
    { 
        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            MasterManager.Instance.Record(_isActive);
        }
    }
}
