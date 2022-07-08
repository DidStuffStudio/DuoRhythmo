using System;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using DidStuffLab.Scripts.Managers;
using UnityEngine;

namespace DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons
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
