using System.Collections;
using System.Collections.Generic;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;

namespace DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons{
    public class ButtonTester : OneShotButton {
        [SerializeField] private AbstractDidStuffButton _buttonToTestNavigationVote;

        protected override void ButtonClicked() {
            base.ButtonClicked();
            _buttonToTestNavigationVote.ClickAndCallEvents();
        }
    }
}
