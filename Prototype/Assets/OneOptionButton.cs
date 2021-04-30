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
        if (activateOnStart) base.SetActive();
    }

    protected override void SetActive() {
        base.SetActive();
        foreach (var button in otherButtonsToDisable) {
            button.Deactivate();
        }
    }

    public void Deactivate() {
        base.SetDefault();
    }

    protected override void FixedUpdate() {
        if (isHover && !isActive)
        {
            if (_confirmScalerRT.localScale.x < 1.0f)
                if (!isDwellTimeSetter)
                    _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
                else
                    _confirmScalerRT.localScale += Vector3.one / localDwellTimeSpeed;
            else
            {
                _confirmScalerRT.localScale = Vector3.zero;
                SetActive();
                OnActivation?.Invoke();
            }
        }
        else
        {
            if (_confirmScalerRT.localScale.x < 0.0f) return;

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
        MasterManager.Instance.dwellTimeSpeed = localDwellTimeSpeed;
    }
}