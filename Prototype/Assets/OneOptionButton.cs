using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOptionButton : CustomButton
{
    [SerializeField] private OneOptionButton[] otherButtonsToDisable;

    protected override void SetActive()
    {
        base.SetActive();
        foreach (var button in otherButtonsToDisable)
        {
            button.Deactivate();
        }
    }

    public void Deactivate()
    {
        base.SetDefault();
    }
}
