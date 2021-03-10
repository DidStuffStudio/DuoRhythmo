using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class TEST_UI : CleanUI
{
    // Start is called before the first frame update
    protected override Type ElementType { get; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GazePoint currentGazePoint = TobiiAPI.GetGazePoint();
        if (!currentGazePoint.IsRecent())
        {
            return;
        }

        if (UIElements.Count == 0)
        {
            HasFocus = false;
            return;
        }

        UpdateFocus(currentGazePoint);

        if (HasFocus)
        {
            print("I have focus");
        }
    }
    
    private void UpdateFocus(GazePoint currentGazePoint)
    {
        if (BoundsOverride != new Rect())
        {
            CurrentBounds = BoundsOverride;
        }
        else
        {
            CurrentBounds = GetViewportBounds();
        }
        HasFocus = CurrentBounds.Contains(currentGazePoint.Viewport);
    }


    
    protected override void SetElementOpacity(Component element, float opacity)
    {

    }

    protected override Rect GetViewportBounds()
    {
        return Rect.zero;
    }
}
