using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class DidStuffRecord : AbstractDidStuffButton
    {
        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            if(_isActive) Debug.Log("Recording");//start recording
            else Debug.Log("Stopped recording"); //stop recording, save and show toast
        }

    }
}
