using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class EuclideanButton : AbstractDidStuffButton
    {
        [SerializeField] private EuclideanRhythm euclideanRhythm;
        [SerializeField] private NodeManager nodeManager;
        [SerializeField] private List<Emoji> emojis = new List<Emoji>();

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            if(_isActive)nodeManager.StoreRhythm();
            ActivateText(_isActive);
            nodeManager.StartEuclideanRhythmRoutine(_isActive);
        }
        
        protected override void ChangeToActiveState() {
            base.ChangeToActiveState();
            foreach (var emoji in emojis) emoji.Enabled = true;
        }

        protected override void ChangeToInactiveState() {
            base.ChangeToInactiveState();
            foreach (var emoji in emojis) emoji.Enabled = false;
        }

        public void ChangePulse(bool increment) => euclideanRhythm.ChangePulse(increment);
    
    }
}
