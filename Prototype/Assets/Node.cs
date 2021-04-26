using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


public class Node : CustomButton {
    public NodeManager nodeManager;
    public int drumIndex;
    public DrumType drumType;

    public bool canPlay=true;

    public int indexValue;

    public ScreenSync _screenSync;

    public List<Image> subNodes = new List<Image>();

    public AK.Wwise.Event kickEvent, snareEvent, hiHatEvent, tomTomEvent, cymbalEvent;
    private VisualEffect _vfx;

    protected override void Start()
    {
        _vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
        base.Start();
    }
    

    public void Activate(bool activate) {
        if(activate) SetActive();
        else SetDefault();
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    protected override void SetActive()
    {
        if (!RealTimeInstance.Instance.isSoloMode) _screenSync.SetIndexValue(indexValue);
        base.SetActive();
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    protected override void SetDefault()
    {
        if (!RealTimeInstance.Instance.isSoloMode) _screenSync.SetIndexValue(indexValue);
        base.SetDefault();
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    public void PlayDrum()
    {
        if (!isActive || !canPlay) return;
        
        StartCoroutine(AudioVFX());
        
        
        
        switch (drumType)
        {
            case DrumType.Kick:
                kickEvent.Post(gameObject);
                break;
            case DrumType.Snare:
                snareEvent.Post(gameObject);
                break;
            case DrumType.HiHat:
                hiHatEvent.Post(gameObject);
                break;
            case DrumType.TomTom:
                tomTomEvent.Post(gameObject);
                break;
            case DrumType.Cymbal:
                cymbalEvent.Post(gameObject);
                break;
        }
    }
    private IEnumerator AudioVFX()
    {
        _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") + 1.0f);
        bool run = true;
        while (run)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            if (_vfx.GetFloat("SphereSize") > 1.1f) _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") - 0.1f);
            else run = false;
        }
    }
    
}