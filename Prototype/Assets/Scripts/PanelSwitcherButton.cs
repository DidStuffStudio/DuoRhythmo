using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcherButton : CustomButton
{
    protected override void FixedUpdate()
    {
        
        if (isEyeHover && !isActive) {
            if (_confirmScalerRT.localScale.x < 1.0f)
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            else {
                _confirmScalerRT.localScale = Vector3.zero;
                StartCoroutine(InteractionBreakTime());
                SetDefault();
                OnActivation?.Invoke();
            }
        }

        else {
            if (_confirmScalerRT.localScale.x < 0.0f) return;
            _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
        }
        
        
    }

    protected override void OnMouseOver()
    {
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
        if(gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        mouseOver = true;
        if (!colorsSet)
        {
            Color.RGBToHSV(defaultColor, out var uH, out var uS, out var uV);
            uV -= 0.3f;
            Color.RGBToHSV(activeColor, out var aH, out var aS, out var aV);
            aV -= 0.3f;

            inactiveHoverColor = Color.HSVToRGB(uH, uS, uV);
            activeHoverColor = Color.HSVToRGB(aH, aS, aV);
            inactiveHoverColor.a = 1;
            activeHoverColor.a = 1;
            
            colorsSet = true;
        }

        if (!_canHover || gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        
        mainButtonImage.color = inactiveHoverColor;

    }

    protected override void SetActive()
    {
        if(changeTextColor) buttonText.color = activeTextColor;
        if (isActive) return;
        isActive = true;
        isDefault = false;
    }

    protected override void SetDefault()
    {
        mouseOver = false;
        if(changeTextColor)buttonText.color = defaultTextColor;
        mainButtonImage.color = defaultColor;
        if (isDefault) return;
        isDefault = true;
        isActive = false;
    }

    protected override void MouseInteraction()
    {
        if(!mouseOver) return;
        if (Input.GetMouseButton(0)) SetActive();
        if (Input.GetMouseButtonUp(0))
        {
            SetDefault();
            OnActivation?.Invoke();
        }
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector3.forward, out var hit, LayerMask.GetMask("RenderPanel"))) {
                if (hit.transform == this.transform) {
                    if (touch.phase == TouchPhase.Began) {
                        SetActive();
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        SetDefault();
                        OnActivation?.Invoke();
                    }
                }
            }
        }
        
    }

    
    public void ActivateDwellSettings(bool activate)
    {
        SetDefault();
        _canHover = true;
        MasterManager.Instance.DwellSettingsActive = activate;
        MasterManager.Instance.SetDwellSettingsActive(activate);
    }
    
}
