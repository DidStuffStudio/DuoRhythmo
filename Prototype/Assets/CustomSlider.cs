using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Event = AK.Wwise.Event;


public class CustomSlider : MonoBehaviour
{
    public enum SliderDirection
    {
        Horizontal,
        Vertical
    }

    private float _maxValue, _minValue;
    public SliderDirection sliderDirection;
    [SerializeField] private RectTransform slider, knob;
    public float currentValue = 0.0f;
    private float _previousValue = 0.0f;
    private bool _focused = false;

   
    
    
    private void Start()
    {
        switch (sliderDirection)
        {
            case SliderDirection.Horizontal:
            {
                
                break;
            }

            case SliderDirection.Vertical:
            {
                
                break;
            }
        }
    }


    private void Update()
    {
        if (!_focused) return;
        
        if (currentValue != _previousValue)
        {
            _previousValue = currentValue;
        }
    }

    

    // map a value from one interval to another one
    


}
