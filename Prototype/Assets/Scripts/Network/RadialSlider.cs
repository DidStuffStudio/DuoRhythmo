using System;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;

public class RadialSlider : CustomButton {
    
    private Transform _knob;
    private Vector2 _targetPosition = Vector2.zero;
    private float _minValue, _maxValue;
    public int maximumValue = 100, minimumValue = 0;
    [SerializeField] private int sliderIndex; // Pass to euclidean manager to tell which slider corresponds to which effect 

    private Vector2 refPosition;
    private Camera _mainCamera;
    private Text _text;
    public Transform upperLimit, lowerLimit;
    public float currentValue,previousValue;
    
    //Events
    public delegate void SliderChangeAction(int index);

    public event SliderChangeAction OnSliderChange;

    protected override void Start() {
        base.Start();
        _mainCamera = Camera.main;
    }

    protected override void Update() {
        base.Update();
        if (_usingEyeTracking)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            _targetPosition = gazePoint.Screen;
        }

        else _targetPosition = Input.mousePosition;

        // float _rotation = 0;
        // _rotation = transform.parent.localRotation.eulerAngles.x;
        // var mousePosition = Input.mousePosition - new Vector3(refPosition.x, refPosition.y, 0);
        // _rotation = Mathf.Abs(Vector3.Magnitude(mousePosition));
        // transform.parent.RotateAround(transform.parent.position, transform.parent.forward, angle: _rotation);
        
        // transform.parent.Rotate(_mainCamera.ScreenToWorldPoint(_targetPosition));
        
        // transform.parent.LookAt(new Vector3(_targetPosition.x, _targetPosition.y, transform.parent.position.z), Vector3.forward);
        
        // print(_mainCamera.ScreenToWorldPoint(_targetPosition));
        // transform.parent.LookAt(_mainCamera.ScreenToWorldPoint(_targetPosition));
        
        float angle = 0;
        // Vector3 relative = transform.TransformPoint(_targetPosition);
        // Vector3 relative = transform.InverseTransformPoint(_targetPosition);
        // angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        // transform.parent.Rotate(0,0, -angle);

        // Vector3 targetVector = _targetPosition;
        // Vector3 targetVector = new Vector3(_targetPosition.x - transform.parent.position.x, _targetPosition.y - transform.parent.position.y, 0);
        // Vector3 targetVector = new Vector3(_targetPosition.x, _targetPosition.y, 0);
        // Debug.DrawRay(transform.parent.position, _targetPosition, Color.red);
        // Vector3 horizontalVector = Vector3.right;
        // Debug.DrawRay(transform.parent.position, horizontalVector, Color.green);
        // angle = Vector2.Angle(_targetPosition, Vector2.right);
        // angle = Vector3.Angle(targetVector, horizontalVector);
        // transform.parent.Rotate(0, 0, angle);
        
        // transform.parent.Rotate(0, 0, );
        
        print(_targetPosition);
        // Vector3 targetVector = _mainCamera.ScreenToWorldPoint(_targetPosition) - transform.parent.position;
        Vector3 targetVector = new Vector3(_targetPosition.x, _targetPosition.y, 0) - _mainCamera.WorldToScreenPoint(transform.parent.position);
        print(targetVector);
        
        // project targetVector to 2D plane formed by the local axes of x and y
        Vector3 projectedVector = Vector3.ProjectOnPlane(targetVector, Vector3.Cross(Vector3.up, Vector3.right));
        Debug.DrawRay(transform.parent.position, projectedVector, Color.red);
        // angle = Vector3.Angle(projectedVector, transform.parent.parent.right);
        // angle = SignedAngleBetween(projectedVector, Vector3.up, clockwise: true);
        var cosAngle = Vector3.Dot(Vector3.up, projectedVector) / (projectedVector.magnitude * Vector3.up.magnitude);
        print(cosAngle);
        // if(cosAngle <= 0) angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        // else angle = -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
            // if (_targetPosition.x < transform.parent.position.x) angle *= -1;
        print(angle);
        // transform.GetComponent<RectTransform>().anchoredPosition = (projectedVector - transform.parent.position);
        // Debug.DrawRay(transform.parent.parent.position, Vector3.up * 100, Color.green);
        transform.parent.rotation = Quaternion.Euler(0, 0, angle);
        // transform.parent.Rotate(0, 0, angle);
        // transform.parent.Rotate(0, 0, angle);
    }
    
    private float SignedAngleBetween (Vector3 a, Vector3 b, bool clockwise) {
        float angle = Vector3.Angle(a, b);

        //clockwise
        if( Mathf.Sign(angle) == -1 && clockwise )
            angle = 360 + angle;

        //counter clockwise
        else if( Mathf.Sign(angle) == 1 && !clockwise)
            angle = - angle;
        return angle;
    }

    protected override void FixedUpdate() {
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
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

    protected override void SetActive() {
        base.SetActive();
        if (_usingEyeTracking)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            refPosition = gazePoint.Screen;
        }
        else refPosition = Input.mousePosition;
    }


    private void SliderChanged(int index) {
        UpdateSliderText();
    }

    public void UpdateSliderText() {
        var value = (int) currentValue;
        _text.text = value.ToString();
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