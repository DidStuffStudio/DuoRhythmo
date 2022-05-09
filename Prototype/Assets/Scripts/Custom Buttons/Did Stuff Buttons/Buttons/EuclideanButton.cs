using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;

public class EuclideanButton : AbstractDidStuffButton
{
    [SerializeField] private List<GameObject> buttonsToToggle = new List<GameObject>();

    private void Start()
    {
        ToggleButtons();
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
      ToggleButtons();
    }

    private void ToggleButtons()
    {
        foreach (var btn in buttonsToToggle)
        {
            btn.SetActive(_isActive);
        }
    }
}
