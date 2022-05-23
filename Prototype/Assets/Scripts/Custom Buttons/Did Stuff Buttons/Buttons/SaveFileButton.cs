using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class SaveFileButton : AbstractDidStuffButton
    {
        protected override void GetTheChildren()
        {

            if(GetComponentsInChildren<RectTransform>().Where(r => r.CompareTag("DwellGfx")).ToArray()[0] != null)
                _dwellGfx = GetComponentsInChildren<RectTransform>().Where(r => r.CompareTag("DwellGfx")).ToArray()[0];
     
            DwellGfxImg = _dwellGfx.GetComponent<Image>();
			

        }

        protected override void ToggleButton(bool activate) { }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SetCanHover(false);
        }

        protected override void StartInteractionCoolDown() { }
    }
}
