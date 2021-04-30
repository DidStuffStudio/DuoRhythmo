using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DwellSpeedButton : CustomButton
{

    [SerializeField] private Text _text;
    [SerializeField] private String _string;
    public bool activateOnStart;
    public bool isDwellActive;

    [SerializeField] private DwellSpeedMaster _dwellSpeedMaster;
    

    protected override void Start()
    {
        base.Start();
        //_text.text = _string;
        if (activateOnStart) SetActive();
    }

    protected override void FixedUpdate()
    {
        if (isHover && !isActive)
        {
            if (_confirmScalerRT.localScale.x < 1.0f)
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
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
            _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
        }
    }


    public void Deactivate()
    {

        SetDefault();
       
    }

    
    protected override void SetActive()
    {

        _dwellSpeedMaster.UpdateButtons();
        base.SetActive();
        isDwellActive = true;
    }


    public void ChangeDwellSpeed()
    {
       // MasterManager.Instance.dwellTimeSpeed = localDwellTimeSpeed;
        
    }

    public void setThisOne()
    {
        if (isDwellActive)
        {
            SetActive();
        }
    }
}
