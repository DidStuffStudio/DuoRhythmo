using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementButton : CustomButton
{

    protected override void FixedUpdate()
    {
        if (isHover && !isActive) {
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

    public void Navigation(bool forward)
    {
        MasterManager.Instance.userInterfaceManager.PlayAnimation(forward);
    }
    public void ActivateDwellSettings()
    {
        bool dwellActive = MasterManager.Instance.DwellSettingsActive;
        
        if (dwellActive)
        {
            MasterManager.Instance.DwellSettingsActive = false;
            MasterManager.Instance.SetDwellSettingsActive(false);
        }
        else
        {
            MasterManager.Instance.DwellSettingsActive = true;
            MasterManager.Instance.SetDwellSettingsActive(true);
        }

        
    }
}
