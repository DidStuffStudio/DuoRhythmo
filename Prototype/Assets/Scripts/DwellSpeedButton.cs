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
        if (activateOnStart)
        {
            SetActive(false);
            _canHover = false;
        }
    }

    protected override void FixedUpdate()
    {
       
        if (isEyeHover && !isActive)
        {
            if (_confirmScalerRT.localScale.x < 1.0f)
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            else if(_canHover)
            {
                _confirmScalerRT.localScale = Vector3.zero;
                SetActive(false);
                OnActivation?.Invoke();

            }
        }

        else
        {
            if (_confirmScalerRT.localScale.x < 0.0f) return;
            _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
        }
        
        MouseInteraction();
    }


    public void Deactivate()
    {

        SetDefault(false);
       
    }

    
    protected override void SetActive(bool sendToServer)
    {

        _dwellSpeedMaster.UpdateButtons();
        base.SetActive(false);
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
            SetActive(false);
        }
    }
}
