using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class InGamePanelSwitch : AbstractDidStuffButton
    {
        
        [SerializeField] private int panelToActivate = -1, panelToDeactivate = -1;
        protected override void StartInteractionCoolDown(){ }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            IsHover = false;
            DeactivateButton();
            if(panelToActivate > -1) InGameMenuManager.Instance.ActivatePanel(panelToActivate);
            if(panelToDeactivate > -1) InGameMenuManager.Instance.DeactivatePanel(panelToDeactivate);
        }
    }
}
