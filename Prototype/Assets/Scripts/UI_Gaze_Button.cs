using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;


public class UI_Gaze_Button : CustomButton {
    
    [SerializeField] private Text _text;
    [SerializeField] private String _string;
    public int drumTypeIndex;

    protected override void Start()
    {
        base.Start();
        _text.text = _string;
    }

   
    public void Deactivate() {
        
            SetDefault(false);

    }

    public void SoloButtonActivate(bool activate) {
        MasterManager.Instance.userInterfaceManager.Solo(activate, drumTypeIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }



}