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
    private Collider _collider;
    public bool canInteractBeforeStart = false;
    public bool isDefault = true, isEyeHover, isMouseHover, isActive, isConfirmationButton;
    public Color defaultColor, activeColor, defaultTextColor = new Color(33,33,33,1), activeTextColor = new Color(238,238,238,1);
    protected Color inactiveHoverColor;
    protected Color activeHoverColor;
    public GameObject confirmScaler;
    public bool mouseOver;
    protected RectTransform _confirmScalerRT;
    private float _interactionBreakTime = 3.0f;
    protected bool _canHover = true;
    public UnityEvent OnActivation, OnDeactivation;
    public bool activated;
    protected bool _usingEyeTracking;

    [SerializeField] protected Image mainButtonImage;
    protected bool colorsSet = false;
    protected Text buttonText;
    public bool changeTextColor;

    protected virtual void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        _confirmScalerRT = confirmScaler.GetComponent<RectTransform>();
        _gazeAware = GetComponent<GazeAware>();
        _collider = GetComponent<Collider>();
        GetImageComponent();
        mainButtonImage.color = defaultColor;
        isDefault = true;
        isActive = false;
        if (isConfirmationButton) ConfirmActivation(false);
        ToggleConfirmScaler(false);
    }
    
    protected virtual void GetImageComponent()=> mainButtonImage = GetComponent<Image>();

    protected virtual void Update() {
        
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
        if (TobiiAPI.IsConnected)
        {
            _usingEyeTracking = true;
            if (_gazeAware.HasGazeFocus) Hover();
            else if(!mouseOver)UnHover();
        }

        MouseInteraction();
        
    }

    protected virtual void FixedUpdate()
    {
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
        
        if (isEyeHover) {
            if (_confirmScalerRT.localScale.x < 1.0f)
            {
                ToggleConfirmScaler(true);
                _confirmScalerRT.localScale += Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            }
            else if(_canHover) {
                _confirmScalerRT.localScale = Vector3.zero;
                ToggleConfirmScaler(false);
             
                if (!isActive) { 
                    StartCoroutine(InteractionBreakTime());
                    SetActive(true);
                    
                    OnActivation?.Invoke();
                  
                }
                else {
                    StartCoroutine(InteractionBreakTime());
                    SetDefault(true);
                    confirmScaler.GetComponent<Image>().color = activeColor;
                    OnDeactivation?.Invoke();
               
                }
            }
        }

        else {
            if (_confirmScalerRT.localScale.x < 0.0f)
            {
                ToggleConfirmScaler(false);
                return;
            }
            
                _confirmScalerRT.localScale -= Vector3.one / MasterManager.Instance.dwellTimeSpeed;
            
        }

       
        
    }

    protected virtual void ToggleConfirmScaler(bool activate)
    {
        if (confirmScaler.activeInHierarchy == activate) return;
        _confirmScalerRT.transform.gameObject.SetActive(activate);
    }
    
    protected virtual void MouseInteraction()
    {
        if(!mouseOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (isDefault)
            {
                OnActivation?.Invoke();
                SetActive(true);
                
            }
            else
            {
                OnDeactivation?.Invoke();
                SetDefault(true);
                
            }
        }

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector3.forward, out var hit, LayerMask.GetMask("RenderPanel"))) {
                if (hit.transform == this.transform) {
                    if (isDefault) {
                        OnActivation?.Invoke();
                        SetActive(true);
                    }
                    else {
                        OnDeactivation?.Invoke();
                        SetDefault(true);
                
                    }
                }
            }
        }
    }
    protected virtual void OnMouseOver()
    {
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
        if(gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        mouseOver = true;
        if (!colorsSet)
        {
            Color.RGBToHSV(defaultColor, out var uH, out var uS, out var uV);
            uV -= 0.3f;
            Color.RGBToHSV(activeColor, out var aH, out var aS, out var aV);
            aV -= 0.3f;

            inactiveHoverColor = Color.HSVToRGB(uH, uS, uV);
            activeHoverColor = Color.HSVToRGB(aH, aS, aV);

            inactiveHoverColor.a = 1;
            activeHoverColor.a = 1;
            confirmScaler.GetComponent<Image>().color = activeColor;
            colorsSet = true;
        }

        if (!_canHover || gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        if (isActive) mainButtonImage.color = activeHoverColor;
        else mainButtonImage.color = inactiveHoverColor;
        
    }

    protected virtual void OnMouseExit() {
        if (!MasterManager.Instance.isInPosition && !canInteractBeforeStart) return;
        if(gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        mouseOver = false;
        _canHover = true;
        if (isDefault) SetDefault(false);
        else if (isActive) SetActive(false);
    }

    protected virtual void SetActive(bool sendToServer)
    {
        if(changeTextColor) buttonText.color = activeTextColor;
        mainButtonImage.color = activeColor;
        confirmScaler.GetComponent<Image>().color = defaultColor;
        if (isActive) return;
        isActive = true;
        isDefault = false;
    }

    protected virtual void Hover() {
        if (!colorsSet)
        {
            Color.RGBToHSV(defaultColor, out var uH, out var uS, out var uV);
            uV -= 0.3f;
            Color.RGBToHSV(activeColor, out var aH, out var aS, out var aV);
            aV -= 0.3f;

            inactiveHoverColor = Color.HSVToRGB(uH, uS, uV);
            activeHoverColor = Color.HSVToRGB(aH, aS, aV);

            inactiveHoverColor.a = 1;
            activeHoverColor.a = 1;
            confirmScaler.GetComponent<Image>().color = activeColor;
            colorsSet = true;
        }

        if (!_canHover || gameObject.layer != LayerMask.NameToLayer("RenderPanel")) return;
        if (isActive) mainButtonImage.color = activeHoverColor;
        else mainButtonImage.color = inactiveHoverColor;
        isEyeHover = true;
    }

    protected virtual void UnHover() {
        _canHover = true;
        if (!isEyeHover) return;
        isEyeHover = false;
        if (isDefault) SetDefault(false);
        else if (isActive) SetActive(false);
    }

    protected virtual void SetDefault(bool sendToServer) {
         mainButtonImage.color = defaultColor;
        if(changeTextColor)buttonText.color = defaultTextColor;
        confirmScaler.GetComponent<Image>().color = activeColor;
        if (isDefault) return;
        isDefault = true;
        isActive = false;
    }

    protected virtual void ConfirmActivation(bool enabled) {
        _collider.enabled = enabled;
        mainButtonImage.enabled = enabled;
    }
    
    

    protected virtual IEnumerator InteractionBreakTime() {
        _canHover = false;
        isEyeHover = false;
        yield return new WaitForSeconds(_interactionBreakTime);
        _canHover = true;
    }
}