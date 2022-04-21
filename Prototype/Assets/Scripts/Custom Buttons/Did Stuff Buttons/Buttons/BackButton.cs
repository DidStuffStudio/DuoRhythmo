using System.Collections;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class BackButton : AbstractDidStuffButton
    {
        protected override void ButtonClicked()
        {
            SetCanHover(false);
            StartCoroutine(WaitBeforeEvent());
        }

        IEnumerator WaitBeforeEvent()
        {
            yield return new WaitForSeconds(0.1f);
            SetCanHover(true);
            InvokeOnClickUnityEvent();
        }
    }
}