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

    public ScreenSync _screenSync;

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
            if (isActive) return;
            isActive = true;
            isDefault = false;
        }
        else
        {
            mainButtonImage.color = defaultColor;
            if(changeTextColor)buttonText.color = defaultTextColor;
            confirmScaler.GetComponent<Image>().color = activeColor;
            if (isDefault) return;
            isDefault = true;
            isActive = false;
        }
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    protected override void SetActive() {
        if (!RealTimeInstance.Instance.isSoloMode)
        {
            //RealTimeInstance.Instance._testStringSync.SetMessage(TestStringSync.MessageTypes.DRUM_NODE_CHANGED + (int)drumType + "," + indexValue + "," + 1);
            
        }
        //MasterManager.Instance.dataMaster.nodes[drumIndex] = 1;
        MasterManager.Instance.dataMaster.nodesActivated[(int)drumType, indexValue] = 1;
        MasterManager.Instance.dataMaster.SendNodes((int)drumType, false);
        base.SetActive();
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    protected override void SetDefault() {
        if (!RealTimeInstance.Instance.isSoloMode)
        {
            //RealTimeInstance.Instance._testStringSync.SetMessage(TestStringSync.MessageTypes.DRUM_NODE_CHANGED + (int)drumType + "," + indexValue + "," + 0);
        }
        //MasterManager.Instance.dataMaster.nodes[drumIndex] = 0;
        MasterManager.Instance.dataMaster.nodesActivated[(int)drumType, indexValue] = 0;
        MasterManager.Instance.dataMaster.SendNodes((int)drumType, false);
        base.SetDefault();
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    public void Deactivate() => SetDefault();

    public void PlayDrum() {
        if (!isActive || !canPlay) return;
        StartCoroutine(Wait());
        //StartCoroutine(AudioVFX());

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
    }    private IEnumerator Wait()
    {
        canPlay = false;
        yield return new WaitForSeconds(0.5f);
        canPlay = true;
    }
}