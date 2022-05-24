using Custom_Buttons.Did_Stuff_Buttons;
using DidStuffLab;
using Managers;
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

    private byte _currentValue;
    public byte CurrentValue {
        get => _currentValue;
        private set {
            _currentValue = value;
            OnSliderChange?.Invoke(sliderIndex);
        }
    }

    private Vector3 _currentInputScreenPosition = Vector3.zero;
    private Vector3 desiredY;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private byte defaultValue = 50;
    private float _x, _y, _w;
    public bool isBpmSlider;
    private float _lastScreenY, _screenY; 
    private RectTransform parentCanvas;

    [SerializeField] private float distanceToDeactivate = 10.0f;
    //Events
    public delegate void SliderChangeAction(int index);

    public event SliderChangeAction OnSliderChange;
    
    [SerializeField] private EffectSync _effectSync;
    
    


    protected override void Awake()
    {
        base.Awake();
    
        _knobRectTransform = GetComponent<RectTransform>();
        var parent = transform.parent;
        _slider = parent.GetComponent<RectTransform>();
        var rect = _slider.rect;
        _minValue = 0;
        _maxValue = rect.height;
        _knobRectTransform.anchoredPosition = new Vector2(0, rect.height/2); //Default value is 50%
        var rect1 = fillRect.rect;
        _w = rect1.width;
        _currentInputScreenPosition = _knobRectTransform.position;
    }

    public void InitialiseSlider()
    {
        if (isBpmSlider) return;
        SetCurrentValue(defaultValue);
        OnSliderChange?.Invoke(sliderIndex);
    }

    public void InitialiseBpm(byte value)
    {
        SetCurrentValue(value);
        OnSliderChange?.Invoke(sliderIndex);
    }
    
    public void SetColors(Color activeColor, Color inactiveColor)
    {
        fillRect.GetComponent<Image>().color = activeColor;
        knobBorder.color = activeColor;
        SetActiveColoursExplicit(activeColor, inactiveColor);
    }
    public void SetCurrentValue(byte value)
    {
        CurrentValue = value; 
        _knobRectTransform.anchoredPosition = new Vector2(0.0f, Map(CurrentValue, minimumValue, maximumValue, _minValue, _maxValue));
        FillSlider();
        UpdateSliderText();
    }

    

    private void FillSlider()
    {
        fillRect.sizeDelta = new Vector2( _w, _knobRectTransform.anchoredPosition.y); 
    }

    protected override void Update()
    {
        // if (!Mathf.Approximately(currentValue, previousValue)) OnSliderChange?.Invoke(sliderIndex);
        //_screenY = _currentInputScreenPosition.y;
        base.Update();

        //if(!IsHover && _isActive) DeactivateButton();
        
        //if (!IsHover || !_isActive) return;
        if (!_isActive) return;
        
        // Mouse Delta

        switch (GetInteractionMethod)
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
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_slider, _currentInputScreenPosition,
                MainCamera, out var value);
 


            _knobRectTransform.localPosition = new Vector3(0,value.y,0);
            
            if (_knobRectTransform.position.y < lowerLimit.position.y)
                _knobRectTransform.position = lowerLimit.position;
            if (_knobRectTransform.position.y > upperLimit.position.y)
                _knobRectTransform.position = upperLimit.position;
            
            CurrentValue =  (byte) Map(_knobRectTransform.anchoredPosition.y, _minValue, _maxValue, minimumValue,
                    maximumValue);
                
        
            //var dist = Vector3.Distance(value, _knobRectTransform.localPosition);
            var dist = Mathf.Abs(Mathf.Abs(value.x) - Mathf.Abs(_knobRectTransform.localPosition.x));
            //if(dist>30) DeactivateButton();
            if(dist>distanceToDeactivate && (InteractionManager.Instance.Method != InteractionMethod.Mouse || InteractionManager.Instance.Method != InteractionMethod.Touch)) DeactivateButton();
            print(InteractionManager.Instance.Method);    
            FillSlider();
            
    }

    protected override void ChangeToActiveState()
    {
        base.ChangeToActiveState();
        SetTemporaryColor(activeHoverColour);
        SetCanHover(false);
    }

    protected override void ChangeToInactiveState()
    {
        base.ChangeToInactiveState();
        SetCanHover(true);
    }

    protected override void MouseUnHover()
    {
        if (!_isActive) _mainImage.color = inactiveColour;
    }

    private void MouseInteraction()
    {
        if (Input.GetMouseButtonUp(0)) DeactivateButton();
        if (_isActive) _currentInputScreenPosition = InteractionManager.Instance.InputPosition;
    }

    protected override void MouseInput()
    {
        if (IsHover && Input.GetMouseButtonDown(0)) ChangeToActiveState();
    }


    private void MouseDwellInteraction()
    {
        if(_isActive)_currentInputScreenPosition = InteractionManager.Instance.InputPosition;
        
    }

    protected override void StartInteractionCoolDown()
    {
        
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
        if(_effectSync) _effectSync.ChangeValue(CurrentValue);
        // TODO --> check if it's not the same _effectsync
        _effectsManager.SendEffectToAudioManager(sliderIndex, CurrentValue);
    }

    private void UpdateSliderText() {
        var value = CurrentValue;
        SetText(value.ToString());
    }

    public void SetEffectSync(EffectSync effectSync) => this._effectSync = effectSync;
    public void SetValueFromServer(byte newValue) {
        SetCurrentValue(newValue);
        // TODO --> send to effect manager but dont send back to server
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
