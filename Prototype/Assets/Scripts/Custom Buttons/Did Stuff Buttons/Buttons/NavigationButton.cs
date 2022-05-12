using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationButton : OneShotButton
{
    private CarouselManager _carouselManager;
    [SerializeField] private bool forward;
    private void Start()
    {
        _carouselManager = MasterManager.Instance.carouselManager; 
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        _carouselManager.PlayAnimation(forward);
    }
}
