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
    [SerializeField] private bool isHorizontal;
    private Vector2 _lastMousePosition, _lastEyePosition;
    private Camera _mainCamera;
    private RectTransform _knobRectTransform;
    private float _minValue, _maxValue;
    public int maximumValue = 100, minimumValue = 0;
    [SerializeField] private int sliderIndex; // Pass to euclidean manager to tell which slider corresponds to which effect
    
    private Text _text;
    public Transform upperLimit, lowerLimit;
    public float currentValue,previousValue;
    private bool isDraggingWithMouse = false; 
    public RectTransform fillRect;
    public Image knobBorder;

    
    //Events
    public delegate void SliderChangeAction(int index);

    public event SliderChangeAction OnSliderChange;


    protected override void GetImageComponent()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        
        _text = GetComponentInChildren<Text>();
        
        _mainCamera = Camera.main;
        _knobRectTransform = GetComponent<RectTransform>();
        
        UpdateSliderText();
        FillSlider();

        var rect = _slider.rect;
                if (isHorizontal)
                {
                    _minValue = 0;
                    _maxValue = rect.width;
                    _knobRectTransform.anchoredPosition = new Vector2(rect.width/2, 0);
                }
                else
                {
                    _minValue = 0;
                    _maxValue = rect.height;
                    _knobRectTransform.anchoredPosition = new Vector2(0, rect.height/2);
                }

                
    }
    
    protected override void SetActive()
    {
        if(changeTextColor) buttonText.color = activeTextColor;
        mainButtonImage.color = activeHoverColor;
        confirmScaler.GetComponent<Image>().color = defaultColor;
        if (isActive) return;
        isActive = true;
        isDefault = false;
    }
    
    public void SetCurrentValue(float value)
    {
        if (isHorizontal)
        {
            currentValue = value;
            OnSliderChange?.Invoke(sliderIndex);
            _knobRectTransform.anchoredPosition =
                new Vector2(Map(currentValue, minimumValue, maximumValue, _minValue, _maxValue),00.0f);
        }
        else
        {
            currentValue = value;
            OnSliderChange?.Invoke(sliderIndex);
            _knobRectTransform.anchoredPosition =
                new Vector2(0.0f, Map(currentValue, minimumValue, maximumValue, _minValue, _maxValue));
        }

        FillSlider();
    }

    protected override void MouseInteraction()
    {

        if (mouseOver && Input.GetMouseButton(0))
        {
            SetActive();
            isDraggingWithMouse = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetDefault();
            isDraggingWithMouse = false;
        }
        
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector3.forward, out var hit, LayerMask.GetMask("RenderPanel"))) {
                if (hit.transform == this.transform) {
                    switch (touch.phase) {
                        case TouchPhase.Began:
                            SetActive();
                            break;
                        case TouchPhase.Ended:
                            SetDefault();
                            break;
                    }
                }
            }
        }
        
    }

    private void FillSlider()
    {
        var anchoredPosition = _knobRectTransform.anchoredPosition;
        fillRect.sizeDelta = isHorizontal ? new Vector2(anchoredPosition.y,20) : new Vector2(20, anchoredPosition.y);
    }

    protected override void Update()
    {
        base.Update();

        if (isHorizontal) _knobRectTransform.anchoredPosition = new Vector2(Map(currentValue, minimumValue,maximumValue , _minValue, _maxValue), 0.0f);
        
        else _knobRectTransform.anchoredPosition = new Vector2(0.0f, Map(currentValue, minimumValue,maximumValue , _minValue, _maxValue));
        
        // Mouse Delta
        if (isActive)
        {
            var knobScreenPoint = _mainCamera.WorldToScreenPoint(_knobRectTransform.position);

            if (isHorizontal)
            {
                if (isEyeHover && !isDraggingWithMouse)
                {
                    GazePoint gazePoint = TobiiAPI.GetGazePoint();
                    knobScreenPoint.x = gazePoint.Screen.x;
                }

                else if(mouseOver)knobScreenPoint.x = Input.mousePosition.x;

                _knobRectTransform.position = _mainCamera.ScreenToWorldPoint(knobScreenPoint);

                if (_knobRectTransform.position.x < lowerLimit.position.x)
                    _knobRectTransform.position = lowerLimit.position;
                if (_knobRectTransform.position.x > upperLimit.position.x)
                    _knobRectTransform.position = upperLimit.position;


                currentValue = Map(_knobRectTransform.anchoredPosition.x, _minValue, _maxValue, minimumValue,
                    maximumValue);


                if (!Mathf.Approximately(currentValue, previousValue)) OnSliderChange?.Invoke(sliderIndex);

                previousValue = currentValue;
            }
                
            
            else
            {
                if (isEyeHover && !isDraggingWithMouse)
                {
                    GazePoint gazePoint = TobiiAPI.GetGazePoint();
                    knobScreenPoint.y = gazePoint.Screen.y;
                }

                else if(mouseOver)knobScreenPoint.y = Input.mousePosition.y;

                _knobRectTransform.position = _mainCamera.ScreenToWorldPoint(knobScreenPoint);

                if (_knobRectTransform.position.y < lowerLimit.position.y)
                    _knobRectTransform.position = lowerLimit.position;
                if (_knobRectTransform.position.y > upperLimit.position.y)
                    _knobRectTransform.position = upperLimit.position;


                currentValue = Map(_knobRectTransform.anchoredPosition.y, _minValue, _maxValue, minimumValue,
                    maximumValue);


                if (!Mathf.Approximately(currentValue, previousValue)) OnSliderChange?.Invoke(sliderIndex);

                previousValue = currentValue;
                
                FillSlider();
            }

        }
        
        
    }

    protected override void FixedUpdate()
    {
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
        
        if (isEyeHover && !isActive) {
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
        SetDefault();
    }


    private void SliderChanged(int index) {
        FillSlider();
        UpdateSliderText();
    }

    public void UpdateSliderText() {
        FillSlider();
        var value = (int) currentValue;
        _text.text = value.ToString();
    }

    protected override void OnMouseExit()
    {
    
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
        if(gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        mouseOver = false;
        _canHover = true;
        SetDefault();
    
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
