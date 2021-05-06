using System;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;

public class RadialSlider : CustomButton {
    private Vector2 _targetPosition = Vector2.zero;
    private float _minValue, _maxValue;
    public int maximumValue = 100, minimumValue = 0;
    [SerializeField] private float angleConstraint;
    [SerializeField] private float startingValue = 0.0f;
    [SerializeField] private int sliderIndex; // Pass to euclidean manager to tell which slider corresponds to which effect 

    private Camera _mainCamera;
    [SerializeField] private Text _text;
    public float currentValue, previousValue;
    private float _angle = 0;
    private bool isDraggingWithMouse;

    public RectTransform[] quadrants = new RectTransform[4];
    public Image knobBorder;

    //Events
    public delegate void SliderChangeAction(int index);

    public event SliderChangeAction OnSliderChange;

    protected override void GetImageComponent()
    {
        
    }


    void FillSlider()
    {

        
        if (currentValue <= 20)
        {
            var x = MasterManager.Instance.Map(_angle, 180, 90, 0, 100);
            quadrants[0].sizeDelta = new Vector2(x,x);
            for (int i = 1; i < 3; i++) quadrants[i].sizeDelta = Vector2.zero;
        }
        
        else if (currentValue <= 50 && currentValue > 20)
        {
            var x = MasterManager.Instance.Map(_angle, 90, 0, 0, 100);
            quadrants[1].sizeDelta = new Vector2(x,x);
            for (int i = 2; i < 3; i++) quadrants[i].sizeDelta = Vector2.zero;
            quadrants[0].sizeDelta = new Vector2(100, 100);
        }
        
        else if (currentValue <= 80 && currentValue > 50)
        {
            var x = MasterManager.Instance.Map(_angle, 0, -90, 0, 100);
            quadrants[2].sizeDelta = new Vector2(x,x);
            for (int i = 0; i < 1; i++)quadrants[i].sizeDelta = new Vector2(100, 100);
            quadrants[3].sizeDelta = Vector2.zero;
        }
        
        else if (currentValue <= 99 && currentValue > 80)
        {
            var x = MasterManager.Instance.Map(_angle, -90, -180, 0, 100);
            for (int i = 0; i < 2; i++)quadrants[i].sizeDelta = new Vector2(100, 100);
            quadrants[3].sizeDelta = new Vector2(x,x);
        }
    }

    protected override void Start() {
        base.Start();
        _mainCamera = Camera.main;
        SetCurrentValue(startingValue);
        UpdateSliderText();
        FillSlider();
    }

    public void SetCurrentValue(float value)
    {
        if (Mathf.Abs(value - currentValue) < 0.1f) return;
        currentValue = value;
        _angle = MasterManager.Instance.Map(currentValue, minimumValue, maximumValue, angleConstraint,
            -angleConstraint);
        transform.parent.localRotation = Quaternion.Euler(0, 0, _angle);
        OnSliderChange?.Invoke(sliderIndex);
        
    }

    public void ForceDefault() => UnHover();
    
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
        
    }
    protected override void Update() {
        base.Update();
        if(!isActive) return;
        if (isEyeHover && !isDraggingWithMouse) {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            _targetPosition = gazePoint.Screen;
        }

        else if(mouseOver) _targetPosition = Input.mousePosition;

        Vector3 targetVector = new Vector3(_targetPosition.x, _targetPosition.y, 0) -
                               _mainCamera.WorldToScreenPoint(transform.parent.position);

        // project targetVector to 2D plane formed by the local axes of x and y
        Vector3 projectedVector = Vector3.ProjectOnPlane(targetVector, Vector3.Cross(Vector3.up, Vector3.right));
        // Debug.DrawRay(transform.parent.position, projectedVector, Color.red);
        // Debug.DrawRay(transform.parent.position, Vector3.up * 100, Color.green);
        // Vector2.up (y axis) --> (x: 0, y: 1) 
        var dotProduct = 0 * projectedVector.x + 1 * projectedVector.y;
        var determinant = 0 * projectedVector.y - 1 * projectedVector.x;
        _angle = Mathf.Atan2(determinant, dotProduct) * Mathf.Rad2Deg; // angle will be within 180 to -180 (full 360)
        // if (_angle < 0) _angle = 180 -_angle; // turn it to full 360 instead of from 180 to -180
        // only rotate if within the angle constraints
        if(_angle <= angleConstraint && _angle >= -angleConstraint) {
            transform.parent.localRotation = Quaternion.Euler(0, 0, _angle);
            currentValue = MasterManager.Instance.Map(_angle, angleConstraint, -angleConstraint, minimumValue, maximumValue);
            if (!Mathf.Approximately(currentValue, previousValue)) OnSliderChange?.Invoke(sliderIndex);
            previousValue = currentValue;
            FillSlider();
        }

    }
    
    protected override void FixedUpdate() {
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
       
        if (isEyeHover && !isActive) {
            if (_confirmScalerRT.localScale.x < 1.0f)
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            else {
                SetActive();
                _confirmScalerRT.localScale = Vector3.zero;
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

    private void SliderChanged(int index) => UpdateSliderText();

    public void UpdateSliderText() {
        var value = (int) currentValue;
        _text.text = value.ToString();
    }

    private void OnEnable() => OnSliderChange += SliderChanged;
    

    private void OnDisable() => OnSliderChange -= SliderChanged;
}