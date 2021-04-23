using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class SliderKnob : CustomButton
{


    [SerializeField] private RectTransform _slider;

    private Vector2 _lastMousePosition, _lastEyePosition;
    private Camera _mainCamera;
    private RectTransform _knobRectTransform;
    private float _minValue, _maxValue;
    public int maximumValue = 100, minimumValue = 0;
    [SerializeField] private int sliderIndex; // Pass to euclidean manager to tell which slider corresponds to which effect

    private Text _text;
    public Transform upperLimit, lowerLimit;
    public float currentValue,previousValue;
    
    
    //Events
    public delegate void SliderChangeAction(int index);

    public event SliderChangeAction OnSliderChange;

 


    protected override void Start()
    {
        base.Start();
    
        _text = GetComponentInChildren<Text>();
        _mainCamera = Camera.main;
        _knobRectTransform = GetComponent<RectTransform>();
        
        
                var rect = _slider.rect;
                _minValue = 0;
                _maxValue = rect.height;
                _knobRectTransform.anchoredPosition = new Vector2(0, _minValue);
    }

    protected override void Update()
    {
        base.Update();
        
      
        _knobRectTransform.anchoredPosition = new Vector2(0.0f, Map(currentValue, minimumValue,maximumValue , _minValue, _maxValue));
        
        // Mouse Delta
        if (isActive)
        {
            var knobScreenPoint = _mainCamera.WorldToScreenPoint(_knobRectTransform.position);
            
            if (_usingEyeTracking)
            {
                GazePoint gazePoint = TobiiAPI.GetGazePoint();
                knobScreenPoint.y = gazePoint.Screen.y;
            }
           
            else knobScreenPoint.y = Input.mousePosition.y;
                
                _knobRectTransform.position = _mainCamera.ScreenToWorldPoint(knobScreenPoint);
                
                if (_knobRectTransform.position.y < lowerLimit.position.y) _knobRectTransform.position = lowerLimit.position;
                if (_knobRectTransform.position.y > upperLimit.position.y) _knobRectTransform.position = upperLimit.position;


                currentValue = Map(_knobRectTransform.anchoredPosition.y, _minValue, _maxValue, minimumValue, maximumValue);
            
                
                if (!Mathf.Approximately(currentValue, previousValue)) OnSliderChange?.Invoke(sliderIndex);
                
                previousValue = currentValue;
                
                
        }
        
        
    }

    protected override void FixedUpdate()
    {
        if (isHover && !isActive) {
            if (_confirmScalerRT.localScale.x < 1.0f)
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            else {
                _confirmScalerRT.localScale = Vector3.zero;
                SetActive();
                OnActivation?.Invoke();
 
            }
        }

        else {
            if (_confirmScalerRT.localScale.x < 0.0f) return;
            _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
        }
    }

    protected override void UnHover()
    {
        base.UnHover();
        mouseOver = false;
        SetDefault();
    }


    private void SliderChanged(int index) {
       
        UpdateSliderText();
    }

    public void UpdateSliderText() {
        var value = (int) currentValue;
        _text.text = value.ToString();
    }
    
    
    private float Map(float value, float min1, float max1, float min2, float max2) {
        return min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
    }
    
    private void OnEnable()
    {
        OnSliderChange += SliderChanged;
    }

    private void OnDisable()
    {
        OnSliderChange -= SliderChanged;
    }
    
}
