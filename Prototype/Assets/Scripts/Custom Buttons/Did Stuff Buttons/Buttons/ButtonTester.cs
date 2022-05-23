using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using UnityEngine;

namespace DidStuffLab {
    public class ButtonTester : OneShotButton {
        [SerializeField] private AbstractDidStuffButton _buttonToTestNavigationVote;

        protected override void ButtonClicked() {
            base.ButtonClicked();
            _buttonToTestNavigationVote.ActivateAndCallEvents();
        }
    }
}
