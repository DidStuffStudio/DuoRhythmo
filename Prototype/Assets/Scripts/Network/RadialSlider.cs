using System;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;

public class RadialSlider : CustomButton {
    private Transform _knob;
    private Vector2 _targetPosition = Vector2.zero;
    private float _minValue, _maxValue;
    public int maximumValue = 100, minimumValue = 0;

    [SerializeField]
    private int sliderIndex; // Pass to euclidean manager to tell which slider corresponds to which effect 

    private Vector2 refPosition;
    private Camera _mainCamera;
    private Text _text;
    public Transform upperLimit, lowerLimit;
    public float currentValue, previousValue;

    //Events
    public delegate void SliderChangeAction(int index);

    public event SliderChangeAction OnSliderChange;

    protected override void Start() {
        base.Start();
        _mainCamera = Camera.main;
    }

    protected override void Update() {
        base.Update();
        if (_usingEyeTracking) {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            _targetPosition = gazePoint.Screen;
        }

        else _targetPosition = Input.mousePosition;

        float angle = 0;
        Vector3 targetVector = new Vector3(_targetPosition.x, _targetPosition.y, 0) -
                               _mainCamera.WorldToScreenPoint(transform.parent.position);
        print(targetVector);

        // project targetVector to 2D plane formed by the local axes of x and y
        Vector3 projectedVector = Vector3.ProjectOnPlane(targetVector, Vector3.Cross(Vector3.up, Vector3.right));
        Debug.DrawRay(transform.parent.position, projectedVector, Color.red);
        Debug.DrawRay(transform.parent.position, Vector3.up * 100, Color.green);
        // var cosAngle = Vector3.Dot(Vector3.up, projectedVector) / (projectedVector.magnitude * Vector3.up.magnitude);
        // print(cosAngle);
        // if(cosAngle <= 0) angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        // else angle = -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        // angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        // Vector3.up (y axis) --> (0,1,0) 
        /*
         Full angle between two vectors:
            dot = x1*x2 + y1*y2 --> dot product
            det = x1*y2 - y1*x2 --> determinant
            angle = atan2(det, dot) --> atan2(y, x) or atan2(sin, cos)
         */
        var dotProduct = 0 * projectedVector.x + 1 * projectedVector.y;
        var determinant = 0 * projectedVector.y - 1 * projectedVector.x;
        angle = Mathf.Atan2(determinant, dotProduct) * Mathf.Rad2Deg;
        print(angle);
        transform.parent.rotation = Quaternion.Euler(0, 0, angle);
    }

    private float SignedAngleBetween(Vector3 a, Vector3 b, bool clockwise) {
        float angle = Vector3.Angle(a, b);

        //clockwise
        if (Mathf.Sign(angle) == -1 && clockwise)
            angle = 360 + angle;

        //counter clockwise
        else if (Mathf.Sign(angle) == 1 && !clockwise)
            angle = -angle;
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

    protected override void UnHover() {
        base.UnHover();
        mouseOver = false;
        SetDefault();
    }

    protected override void SetActive() {
        base.SetActive();
        if (_usingEyeTracking) {
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

    private void OnEnable() {
        OnSliderChange += SliderChanged;
    }

    private void OnDisable() {
        OnSliderChange -= SliderChanged;
    }
}