using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationButton : OneShotButton
{
    private UserInterfaceManager _userInterfaceManager;
    [SerializeField] private bool forward;
    private void Start()
    {
        _userInterfaceManager = MasterManager.Instance.userInterfaceManager; 
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        _userInterfaceManager.PlayAnimation(forward);
    }
}
