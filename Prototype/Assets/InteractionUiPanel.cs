using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
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
