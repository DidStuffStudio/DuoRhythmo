using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementButton : CustomButton
{
    [SerializeField] private SliderKnob bpmSlider;
    protected override void FixedUpdate()
    {
        
        if (isEyeHover && !isActive) {
            if (_confirmScalerRT.localScale.x < 1.0f)
            {
                ToggleConfirmScaler(true);
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            }
            else {
                _confirmScalerRT.localScale = Vector3.zero;
                ToggleConfirmScaler(false);
                StartCoroutine(InteractionBreakTime());
                OnActivation?.Invoke();
                SetDefault(false);
            }
        }

        else {
            if (_confirmScalerRT.localScale.x < 0.0f)
            {
                ToggleConfirmScaler(false);
                return;
            }
            _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
        }
        
        
    }

    protected override void SetActive(bool sendToServer)
    {
        if(changeTextColor) buttonText.color = activeTextColor;
        if (isActive) return;
        isActive = true;
        isDefault = false;
    }

    protected override void SetDefault(bool sendToServer)
    {
        if(changeTextColor)buttonText.color = defaultTextColor;
        mainButtonImage.color = defaultColor;
        if (isDefault) return;
        isDefault = true;
        isActive = false;
    }

    protected override void MouseInteraction()
    {
        if(!mouseOver) return;
        if (Input.GetMouseButton(0)) SetActive(false);
        if (Input.GetMouseButtonUp(0))
        {
            ToggleConfirmScaler(true);
            _confirmScalerRT.localScale = Vector3.one;
            SetDefault(false);
            OnActivation?.Invoke();
        }
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector3.forward, out var hit, LayerMask.GetMask("RenderPanel"))) {
                if (hit.transform == this.transform) {
                    if (touch.phase == TouchPhase.Began) {
                        SetActive(false);
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        ToggleConfirmScaler(true);
                        _confirmScalerRT.localScale = Vector3.one;
                        SetDefault(false);
                        OnActivation?.Invoke();
                    }
                }
            }
        }
        
    }

    public void Navigation(bool forward)
    {
        MasterManager.Instance.userInterfaceManager.PlayAnimation(forward);
    }
    public void ActivateDwellSettings(bool activate)
    {
        SetDefault(false);
        _canHover = true;
        
        MasterManager.Instance.DwellSettingsActive = activate;
        MasterManager.Instance.SetDwellSettingsActive(activate);
        
        
    }

    public void IncrementBpm(bool increase)
    {
        if (RealTimeInstance.Instance.isSoloMode)
        {
            if (increase && bpmSlider.currentValue < bpmSlider.maximumValue)MasterManager.Instance.bpm++;
            else if(bpmSlider.currentValue > bpmSlider.minimumValue ) MasterManager.Instance.bpm--;
        }
        else
        {
            bpmSlider.SetCurrentValue(increase ? bpmSlider.currentValue++ : bpmSlider.currentValue--);
        }
    }
}
