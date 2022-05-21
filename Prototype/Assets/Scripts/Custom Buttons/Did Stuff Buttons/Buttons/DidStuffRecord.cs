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
            if(_isActive) StartRecording();
            else StopRecording();
        }


        private void StartRecording()
        {
            MasterManager.Instance.saveToWav.StartRecording();
        }

        private void StopRecording()
        {
            MasterManager.Instance.saveToWav.StopRecording();
            //Todo Show toast on stop recording
        }
    }
}
