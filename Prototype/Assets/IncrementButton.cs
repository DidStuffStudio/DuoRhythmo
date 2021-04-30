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
            }
        }

        else {
            if (_confirmScalerRT.localScale.x < 0.0f) return;
            _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
        }
    }

    public void activateDwellSettings()
    {
        bool dwellActive = MasterManager.Instance.DwellSettingsActive;
        
        if (dwellActive)
        {
            MasterManager.Instance.DwellSettingsActive = false;
            //GameObject.FindGameObjectWithTag("DwellSettings").SetActive(false);
            //MasterManager.Instance.dwellSettingsPrefab.SetActive(false);
            MasterManager.Instance.setDwellSettingsActive(false);
        }
        else
        {
            MasterManager.Instance.DwellSettingsActive = true;
            //GameObject.FindGameObjectWithTag("DwellSettings").SetActive(true);
            //MasterManager.Instance.dwellSettingsPrefab.SetActive(true);
            MasterManager.Instance.setDwellSettingsActive(true);
        }

        
    }
}
