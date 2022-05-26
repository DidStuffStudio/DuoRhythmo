using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUiPanel : UIPanel
{

    [SerializeField] private InteractionManager interactionManager;
    [SerializeField] private bool inGame;
    
    private void OnEnable()
    {
        if (inGame) InteractionManager.Instance.ActivateTobiiRay = true;
        else interactionManager.ActivateTobiiRay = true;
    }

    private void OnDisable()
    {
        if (inGame) InteractionManager.Instance.ActivateTobiiRay = false;
        else interactionManager.ActivateTobiiRay = false;
    }
    
}
