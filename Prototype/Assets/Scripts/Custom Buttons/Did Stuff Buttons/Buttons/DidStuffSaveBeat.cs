using System;
using Managers;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class DidStuffSaveBeat : OneShotButton
    {
        private void Start()
        {
            CoolDownTime = 1.2f;
        }

        public void SaveBeat()
        {
            MasterManager.Instance.saveBeat.SaveAndShowToast();
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SaveBeat();
        }
    }
}
