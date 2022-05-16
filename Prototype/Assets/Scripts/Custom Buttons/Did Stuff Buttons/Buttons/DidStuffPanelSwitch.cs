using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using UnityEngine;

public class DidStuffPanelSwitch : AbstractDidStuffButton
{

    [SerializeField] private MainMenuManager menuManager;
    [SerializeField] private int panelToActivate = -1, panelToDeactivate = -1;
    protected override void StartInteractionCoolDown(){ }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        _isHover = false;
        DeactivateButton();
        if(panelToActivate > -1) menuManager.ActivatePanel(panelToActivate);
        if(panelToDeactivate > -1) menuManager.DeactivatePanel(panelToDeactivate);
    }
}
