using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab.Scripts.Main_Menu;
using DidStuffLab.Scripts.Managers;
using UnityEngine;

public class InteractionUiPanel : UIPanel
{

    [SerializeField] private InteractionManager interactionManager;

    private void OnEnable()
    {
        interactionManager.ActivateTobiiRay = true;
    }

    private void OnDisable()
    {
       interactionManager.ActivateTobiiRay = false;
    }
    
}
