using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementButton : CustomButton
{

    protected override void FixedUpdate()
    {
        
        if (isEyeHover && !isActive) {
            if (_confirmScalerRT.localScale.x < 1.0f)
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            else {
                _confirmScalerRT.localScale = Vector3.zero;
                StartCoroutine(InteractionBreakTime());
                OnActivation?.Invoke();
                SetDefault();
            }
        }

        else {
            if (_confirmScalerRT.localScale.x < 0.0f) return;
            _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
        }
        
        
    }

    protected override void MouseInteraction()
    {
        if (mouseOver && Input.GetMouseButtonDown(0)) OnActivation?.Invoke();

    }

    public void Navigation(bool forward)
    {
        MasterManager.Instance.userInterfaceManager.PlayAnimation(forward);
    }
    public void ActivateDwellSettings(bool activate)
    {
        SetDefault();
        _canHover = true;
        
        MasterManager.Instance.DwellSettingsActive = activate;
        MasterManager.Instance.SetDwellSettingsActive(activate);
        
        
    }
}
