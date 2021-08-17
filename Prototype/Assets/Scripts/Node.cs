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

    public bool canPlay = true;

    public int indexValue;
    

    public List<Image> subNodes = new List<Image>();

    //public AK.Wwise.Event kickEvent, snareEvent, hiHatEvent, tomTomEvent, cymbalEvent;


    private VisualEffect _vfx;

    protected override void Start() {
        _vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
        base.Start();
    }


    public void Activate(bool activate) {
        if (activate) SetActive();
        else SetDefault();
    }

    public void SetNodeFromServer(bool activate) {
        
        if (activate)
        {
            if(changeTextColor) buttonText.color = activeTextColor;
            mainButtonImage.color = activeColor;
            confirmScaler.GetComponent<Image>().color = defaultColor;
            isActive = true;
            isDefault = false;
        }
        else
        {
            mainButtonImage.color = defaultColor;
            if(changeTextColor)buttonText.color = defaultTextColor;
            confirmScaler.GetComponent<Image>().color = activeColor;
            isDefault = true;
            isActive = false;

        }
        
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    protected override void SetActive() {
        base.SetActive();
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    protected override void SetDefault() {
        base.SetDefault();
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    public void Deactivate() => SetDefault();

    public void PlayDrum() {
        if (!isActive || !canPlay) return;
        // StartCoroutine(Wait());
        StartCoroutine(AudioVFX());

        MasterManager.Instance.PlayDrum(drumType);

        
    }

    private IEnumerator AudioVFX() {
        _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") + 1.0f);
        bool run = true;
        while (run) {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            if (_vfx.GetFloat("SphereSize") > 1.1f) _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") - 0.1f);
            else run = false;
        }
    }
    private IEnumerator Wait()
    {
        canPlay = false;
        yield return new WaitForSeconds(0.5f);
        canPlay = true;
    }
}