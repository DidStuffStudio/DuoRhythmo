using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOptionButton : CustomButton {
    [SerializeField] private OneOptionButton[] otherButtonsToDisable;
    [SerializeField] private bool activateOnStart = false;
    [SerializeField] private bool isDwellTimeSetter;
    public float localDwellTimeSpeed = 100.0f;
    

    protected override void Start() {
        base.Start();
        if (activateOnStart)
        {
            base.SetActive(false);
            _canHover = false;
        }
    }

    protected override void SetActive(bool sendToServer) {
        base.SetActive(false);
        _canHover = false;
        foreach (var button in otherButtonsToDisable) {
            button.Deactivate();
            button._canHover = true;
        }
    }

    protected override void MouseInteraction()
    {
        if (!_canHover) return;
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector3.forward, out var hit, LayerMask.GetMask("RenderPanel"))) {
                if (hit.transform == this.transform) {
                    OnActivation?.Invoke();
                    SetActive(false);
                    _canHover = false;
                }
            }
        }
        if((!mouseOver || !Input.GetMouseButtonDown(0))) return;
        if (!isDefault) return;
        OnActivation?.Invoke();
        SetActive(false);
        _canHover = false;
    }

    public void Deactivate() {
        base.SetDefault(false);
    }

    protected override void FixedUpdate() {
        
        if (isEyeHover && !isActive)
        {
            ToggleConfirmScaler(true);
            if (_confirmScalerRT.localScale.x < 1.0f)
                if (!isDwellTimeSetter)
                    _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
                else
                    _confirmScalerRT.localScale += Vector3.one / localDwellTimeSpeed;
            else
            {
                _confirmScalerRT.localScale = Vector3.zero;
                ToggleConfirmScaler(false);
                SetActive(false);
                OnActivation?.Invoke();
            }
        }
        else if(_canHover)
        {
            if (_confirmScalerRT.localScale.x < 0.0f)
            {
                ToggleConfirmScaler(false);
                return;
            }

            if (!isDwellTimeSetter)
            {
                _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;

            }
            else
            {
                _confirmScalerRT.localScale -= Vector3.one / localDwellTimeSpeed;
            }

           
        }
        
    }
    public void ChangeDwellSpeed()
    {
        DontDestroyDwell.Instance.dwellTimeSpeed = localDwellTimeSpeed;
        MasterManager.Instance.dwellTimeSpeed = localDwellTimeSpeed;
    }
}