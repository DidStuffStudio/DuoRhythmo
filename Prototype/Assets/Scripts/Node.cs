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
    private NodeSync _nodeSync;
    //private NodeDictionarySync _nodeDictionarySync;

    public bool activatedFromServer;

    protected override void Start()
    {
        _vfx = MasterManager.Instance.userInterfaceManager._vfx;
        _nodeSync = MasterManager.Instance.transform.GetComponentInChildren<NodeSync>();
        //_nodeDictionarySync = MasterManager.Instance.transform.GetComponentInChildren<NodeDictionarySync>();
        base.Start();
    }


    public void ActivateFromEuclideanOrRotate(bool activate) {
        if (activate) SetActive(true);
        else SetDefault(true);
    }

    public void SetNodeFromServer(bool activate)
    {
        
        if (activate == isActive) return;
        if (activate)
        {
            
            //Call node sync get node
            
            
            if(changeTextColor) buttonText.color = activeTextColor;
            mainButtonImage.color = activeColor;
            confirmScaler.GetComponent<Image>().color = defaultColor;
            isActive = true;
            isDefault = false;
            //MasterManager.Instance.dataMaster.nodesActivated[(int)drumType, indexValue] = 1;
        }
        else
        {
            
            //Call node sync get node
            
            mainButtonImage.color = defaultColor;
            if(changeTextColor)buttonText.color = defaultTextColor;
            confirmScaler.GetComponent<Image>().color = activeColor;
            isDefault = true;
            isActive = false;
            //MasterManager.Instance.dataMaster.nodesActivated[(int) drumType, indexValue] = 0;
        }
        
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }



    protected override void SetActive(bool sendToServer) {
        if (!RealTimeInstance.Instance.isSoloMode && sendToServer)
        {
         
            //Call node sync set node
            _nodeSync.SetNodeOnServer(drumType, indexValue, true);
            //_nodeDictionarySync.SetNodeOnServer(drumType, indexValue, true);
            

            //MasterManager.Instance.dataMaster.nodesActivated[(int)drumType, indexValue] = 1;
            
            //MasterManager.Instance.dataMaster.SendNodes((int)drumType, false);
        }
        
        base.SetActive(false);
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    protected override void SetDefault(bool sendToServer) {
        if (!RealTimeInstance.Instance.isSoloMode && sendToServer)
        {
            //Call node sync set node
             _nodeSync.SetNodeOnServer(drumType, indexValue, false);
            //_nodeDictionarySync.SetNodeOnServer(drumType, indexValue, false);
            
            //MasterManager.Instance.dataMaster.nodesActivated[(int)drumType, indexValue] = 0;
            //MasterManager.Instance.dataMaster.SendNodes((int)drumType, false);
        }
        base.SetDefault(false);
        MasterManager.Instance.UpdateSubNodes(indexValue, isActive, nodeManager.subNodeIndex);
    }

    public void Deactivate() => SetDefault(false);

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