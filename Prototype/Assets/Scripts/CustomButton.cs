using System;
using System.Collections;
using Tobii.Gaming;
using Tobii.Gaming.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(GazeAware))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Collider))]
public class CustomButton : MonoBehaviour {
    protected GazeAware _gazeAware;
    private Image _image;
    private Collider _collider;

    public bool isDefault = true, isHover, isActive, isHint, isConfirmationButton;
    public Color defaultColor, inactiveHoverColor, activeColor, hintColor, activeHoverColor;
    public GameObject confirmScaler;
    public bool mouseOver;
    protected RectTransform _confirmScalerRT;
    public float interactionBreakTime = 1.0f;
    private bool _canHover = true;
    public UnityEvent OnActivation, OnDeactivation;
    public bool activated;
    protected bool _usingEyeTracking;
    public float localDwellTimeSpeed = 100.0f;
    [SerializeField] private bool isDwellTimeSetter; 
    

    protected virtual void Start()
    {
        
        _confirmScalerRT = confirmScaler.GetComponent<RectTransform>();
        _gazeAware = GetComponent<GazeAware>();
        _image = GetComponent<Image>();
        _collider = GetComponent<Collider>();
        _image.color = defaultColor;
        confirmScaler.GetComponent<Image>().color = activeColor;
        if (isConfirmationButton) ConfirmActivation(false);
    }

    protected virtual void Update() {
        if (TobiiAPI.IsConnected)
        {
            _usingEyeTracking = true;
            if (_gazeAware.HasGazeFocus) Hover();
            else if (!mouseOver) UnHover();
        }
        
    }

    protected virtual void FixedUpdate()
    {
        if (isHover) {
            if (_confirmScalerRT.localScale.x < 1.0f)
                if (!isDwellTimeSetter)
                    _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
                else
                    _confirmScalerRT.localScale += Vector3.one / localDwellTimeSpeed;
                
            else {
                _confirmScalerRT.localScale = Vector3.zero;
             
                if (!isActive) { 
                    StartCoroutine(InteractionBreakTime());
                    SetActive();
                    confirmScaler.GetComponent<Image>().color = defaultColor;
                    OnActivation?.Invoke();
                  
                }
                else {
                    StartCoroutine(InteractionBreakTime());
                    SetDefault();
                    confirmScaler.GetComponent<Image>().color = activeColor;
                    OnDeactivation?.Invoke();
               
                }
            }
        }

        else {
            if (_confirmScalerRT.localScale.x < 0.0f) return;
            if (!isDwellTimeSetter)
                _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            else
                _confirmScalerRT.localScale -= Vector3.one / localDwellTimeSpeed;
        }
    }
    protected virtual void OnMouseOver()
    {
        if(gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        mouseOver = true;
        Hover();
    }

    protected virtual void OnMouseExit() {
        if(gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        mouseOver = false;
        UnHover();
    }

    protected virtual void SetActive() {
        _image.color = activeColor;
        if (isActive) return;
        isActive = true;
        isDefault = false;
        if (isHint) isHint = false;
    }

    protected virtual void Hover() {
        if (!_canHover || gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        if (isActive) _image.color = activeHoverColor;
        else _image.color = inactiveHoverColor;
        if (isHover) return;
        isHover = true;
    }

    protected virtual void UnHover() {
        if (!isHover) return;
        isHover = false;
        if (isConfirmationButton) SetDefault();
        else if (isDefault) SetDefault();
        else if (isActive) SetActive();
    }

    protected virtual void SetDefault() {
        _image.color = defaultColor;
        confirmScaler.GetComponent<Image>().color = activeColor;
        if (isDefault) return;
        isDefault = true;
        isActive = false;
    }

    protected virtual void ConfirmActivation(bool enabled) {
        _collider.enabled = enabled;
        _image.enabled = enabled;
    }

    protected virtual void SetHint() {
        _image.color = hintColor;
        isHint = true;
    }

    protected virtual IEnumerator InteractionBreakTime() {
        _canHover = false;
        isHover = false;
        yield return new WaitForSeconds(interactionBreakTime);
        _canHover = true;
    }
}