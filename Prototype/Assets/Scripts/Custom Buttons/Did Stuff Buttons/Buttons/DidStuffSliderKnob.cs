using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using TMPro;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;

public class DidStuffSliderKnob : AbstractDidStuffButton
{
     

    private RectTransform _slider;
    private RectTransform _knobRectTransform;
    private float _minValue, _maxValue;
    public int maximumValue = 100, minimumValue = 0;
    [SerializeField] private int sliderIndex; // Pass to euclidean manager to tell which slider corresponds to which effect
    public Transform upperLimit, lowerLimit;
    [SerializeField] private RectTransform fillRect;
    [SerializeField] private Image knobBorder;
    public float currentValue,previousValue;
    private Vector3 _currentInputScreenPosition = Vector3.zero;
    private Vector3 desiredY;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private float defaultValue = 50;
    private float _x, _y, _w;
    public bool isBpmSlider;
    private float _lastScreenY, _screenY; 
    private RectTransform parentCanvas;

    //Events
    public delegate void SliderChangeAction(int index);

    public event SliderChangeAction OnSliderChange;


    private void Start()
    {
        _knobRectTransform = GetComponent<RectTransform>();
        _slider = transform.parent.GetComponent<RectTransform>();
        var rect = _slider.rect;
        _minValue = 0;
        _maxValue = rect.height;
        _knobRectTransform.anchoredPosition = new Vector2(0, rect.height/2);
        var rect1 = fillRect.rect;
        _x = rect1.x;
        _y = rect1.y;
        _w = rect1.width;
        _currentInputScreenPosition = _knobRectTransform.position;
        parentCanvas = _effectsManager.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        SetCurrentValue(defaultValue);
    }

    public void SetColors(Color activeColor, Color inactiveColor)
    {
        fillRect.GetComponent<Image>().color = activeColor;
        knobBorder.color = activeColor;
        SetActiveColoursExplicit(activeColor, inactiveColor);
    }
    public void SetCurrentValue(float value)
    {
        currentValue = value;
        OnSliderChange?.Invoke(sliderIndex);
        //_knobRectTransform.anchoredPosition = new Vector2(0.0f, Map(currentValue, minimumValue, maximumValue, _minValue, _maxValue));
        FillSlider();
    }

    

    private void FillSlider()
    {
        fillRect.rect.Set(_x,_y,_w,  _knobRectTransform.anchoredPosition.y);
    }

    protected override void Update()
    {
        _screenY = _currentInputScreenPosition.y;
        base.Update();

        if (!_isHover || !_isActive) return;
        
        
        
        // Mouse Delta
        if (_isActive)
        {
            

            switch (GetInteractionMethod())
            {
                case InteractionMethod.Mouse:
                    MouseInteraction();
                    break;
                case InteractionMethod.MouseDwell:
                    MouseDwellInteraction();
                    break;
                case InteractionMethod.Tobii:
                    EyeInteraction();
                    break;
                case InteractionMethod.Touch:
                    TouchInteraction();
                    break;
            }

            RectTransformUtility.ScreenPointToWorldPointInRectangle(parentCanvas as RectTransform, _currentInputScreenPosition,
                _mainCamera, out desiredY);
 
            //Vector2 normalizedPoint = Rect.PointToNormalized(_knobRectTransform.rect, localPoint);
            
            //_currentKnobScreenPosition = _mainCamera.WorldToScreenPoint(_knobRectTransform.position);
            var point = new Vector3(0, desiredY.y + 125, _knobRectTransform.position.z);
            _knobRectTransform.position = point;
            /* _knobRectTransform.anchoredPosition = point;
             //_knobRectTransform.anchoredPosition = new Vector2(0.0f, Map(currentValue, minimumValue,maximumValue , _minValue, _maxValue));
                 if (_knobRectTransform.position.y < lowerLimit.position.y)
                     _knobRectTransform.position = lowerLimit.position;
                 if (_knobRectTransform.position.y > upperLimit.position.y)
                     _knobRectTransform.position = upperLimit.position;*/

            
                currentValue = Map(_knobRectTransform.anchoredPosition.y, _minValue, _maxValue, minimumValue,
                    maximumValue);
                
                if (!Mathf.Approximately(currentValue, previousValue)) OnSliderChange?.Invoke(sliderIndex);

                previousValue = currentValue;
                
                FillSlider();
                _lastScreenY = _currentInputScreenPosition.y;
        }
       
    }

    protected override void ChangeToActiveState()
    {
        _isActive = true;
        _isInactive = false;
        
        switch (GetInteractionMethod())
        {
            case InteractionMethod.Mouse:
                break;
            case InteractionMethod.MouseDwell:
                break;
            case InteractionMethod.Tobii:
                break;
            case InteractionMethod.Touch:
                break;
        }
    }
    

    private void MouseInteraction()
    {
        if (Input.GetMouseButtonUp(0)) DeactivateButton();
        if(_isHover)_currentInputScreenPosition = Input.mousePosition;
    }
    

    private void MouseDwellInteraction()
    {
        if(_isHover)_currentInputScreenPosition = Input.mousePosition;
    }
    
    private void EyeInteraction()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        _currentInputScreenPosition = gazePoint.Screen;
    }

    private void TouchInteraction()
    {
        //TODO Write touch interaction for slider knobs
    }
    
    private void SliderChanged(int index) {
        UpdateSliderText();
        _effectsManager.SendEffectToAudioManager(sliderIndex, currentValue);
    }

    private void UpdateSliderText() {
        var value = (int) currentValue;
        SetText(value.ToString());
    }
    

    private float Map(float value, float min1, float max1, float min2, float max2) {
        return min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        OnSliderChange += SliderChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnSliderChange -= SliderChanged;
    }
    
  
}
