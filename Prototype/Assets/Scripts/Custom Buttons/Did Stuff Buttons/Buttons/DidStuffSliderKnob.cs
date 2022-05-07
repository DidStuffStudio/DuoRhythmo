using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;

public class DidStuffSliderKnob : AbstractDidStuffButton
{
   
    [SerializeField] private RectTransform _slider;
    private Vector2 _lastMousePosition, _lastEyePosition;
    private RectTransform _knobRectTransform;
    private float _minValue, _maxValue;
    public int maximumValue = 101, minimumValue = 0;
    [SerializeField] private int sliderIndex; // Pass to euclidean manager to tell which slider corresponds to which effect
    public Transform upperLimit, lowerLimit;
    public float currentValue,previousValue;
    private bool isDraggingWithMouse = false; 
    public RectTransform fillRect;

    
    public void SetSliderValue(int value)
    {
        previousValue = currentValue;
        currentValue = value;
    }
}
