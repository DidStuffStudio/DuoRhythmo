using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using DidStuffLab.Scripts.Managers;
using UnityEngine;

namespace DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class DidStuffPanelSwitch : AbstractDidStuffButton
    {

        [SerializeField] private int panelToActivate = -1, panelToDeactivate = -1;
        protected override void StartInteractionCoolDown(){ }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            IsHover = false;
            DeactivateButton();
            if(panelToActivate > -1) MainMenuManager.Instance.ActivatePanel(panelToActivate);
            if(panelToDeactivate > -1) MainMenuManager.Instance.DeactivatePanel(panelToDeactivate);
        }
    }
}
