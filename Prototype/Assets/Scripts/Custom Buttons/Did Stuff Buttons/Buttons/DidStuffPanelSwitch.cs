using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;

public class DidStuffPanelSwitch : AbstractDidStuffButton
{
    public GameObject panelToDeactivate, panelToActivate;
    
    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        SetCanHover(false);
        panelToActivate.SetActive(true);
        StartCoroutine(WaitBeforeEvent());
    }

    protected override void StartInteractionCoolDown() { }

    IEnumerator WaitBeforeEvent()
    {
        yield return new WaitForSeconds(0.02f);
        SetCanHover(true);
        InvokeOnClickUnityEvent();
        SetCanHover(true);
        panelToDeactivate.SetActive(false);
    }


    }
