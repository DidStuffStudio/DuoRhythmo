using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;


public class SliderKnob : MonoBehaviour
{
    public enum InteractionTechnique
    {
        SmoothPursuit
    }

    
    private CustomSlider _slider;
    private Image _knobImage;
    private Color _hoverColor;
    private GazeAware _gazeAware;
    private bool _mouseOver, _activated;
    private Vector2 _lastMousePosition, _lastEyePosition;
    private Camera _mainCamera;
    private RectTransform _knobRectTransform;
    private float _minValue, _maxValue;
    
    public InteractionTechnique interactionTechnique;
    [SerializeField] private Color defaultColor, activatedColor;
    [SerializeField] private RectTransform upSignifier, downSignifier; //Smooth pursuit signifiers
    [SerializeField] private float sensitivity, offset = 20.0f, threshold = 10.0f;



    public Transform upperLimit, lowerLimit;
    
    
    
    //Events
    public delegate void KnobFocusAction(bool focused);

    public event KnobFocusAction OnKnobFocus;
    
    public delegate void SliderChangeAction();

    public event SliderChangeAction OnSliderChange;

    public delegate void ActivateAction(bool activated);
    
    public event ActivateAction OnActivated;



    private void Start()
    {
        _mainCamera = Camera.main;
        _slider = GetComponentInParent<CustomSlider>();
        _knobImage = GetComponent<Image>();
        _gazeAware = GetComponent<GazeAware>();
        _knobRectTransform = GetComponent<RectTransform>();

        Color.RGBToHSV(defaultColor, out var uH, out var uS, out var uV);
        uV -= 0.3f;

        _hoverColor = Color.HSVToRGB(uH, uS, uV);
        _hoverColor.a = 1;

        switch (_slider.sliderDirection)
        {
            case CustomSlider.SliderDirection.Horizontal: //Get x delta
            {
                break;
            }
            case CustomSlider.SliderDirection.Vertical: // Get y delta
            {
                var rect = _slider.GetComponent<RectTransform>().rect;
                _minValue = 0;
                _maxValue = rect.height;
                _knobRectTransform.anchoredPosition = new Vector2(0, _minValue);
                break;
            }
        }
    }


    private void FixedUpdate()
    {
   
        
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_mainCamera, Input.mousePosition);
        // convert the screen position to the local anchored position
        Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint);
        
        print(anchoredPosition);
        
        
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        // Mouse Delta
        if (_activated)
        {
            
            var knobScreenPoint = _mainCamera.WorldToScreenPoint(_knobRectTransform.position);
            
            

                knobScreenPoint.y = gazePoint.Screen.y;
                knobScreenPoint.y = Input.mousePosition.y;
                
                _knobRectTransform.position = _mainCamera.ScreenToWorldPoint(knobScreenPoint);
                
                if (_knobRectTransform.position.y < lowerLimit.position.y) _knobRectTransform.position = lowerLimit.position;
                if (_knobRectTransform.position.y > upperLimit.position.y) _knobRectTransform.position = upperLimit.position;
                
 
            switch (_slider.sliderDirection)
            {
                case CustomSlider.SliderDirection.Horizontal: //Get x delta
                {
                    break;
                }
                case CustomSlider.SliderDirection.Vertical: // Get y delta
                {
                    //eye delta is negative, deactivate up arrow and give it a constant position below current mouse/eye position
                    if (Input.mousePosition.y > knobScreenPoint.y)
                    {
                        
                        
                    } 
                    break;
                }
            }
        }
        
        if (!TobiiAPI.IsConnected) return;
        // Eye Delta 
        if (_gazeAware.HasGazeFocus) OnKnobFocus?.Invoke(true);
        else if (!_mouseOver) OnKnobFocus?.Invoke(false);
    }

    private void OnMouseOver() //Mouse hover
    {
        _mouseOver = true;
        OnKnobFocus?.Invoke(true);
    }

    private void OnMouseExit() {
        _mouseOver = false;
        OnKnobFocus?.Invoke(false);
    }

    private void Focused(bool focus) // If the user is hovering the mouse or gazing at the knob
    {
        _knobImage.color = focus ? _hoverColor : defaultColor;
        OnActivated?.Invoke(focus); // TODO Add in interaction stuff for activation. eg. Dwell time
    }

    private void Activated(bool activate) // If the user has completed the dwell time or confirmed with other interaction technique
    {
        _knobImage.color = activate ? activatedColor : defaultColor;
        _activated = activate;
    }


    private IEnumerator FadeSignifier(Image imageToFade, bool fadeIn)
    {
        var alpha = imageToFade.color.a;
        if (fadeIn)
        {
            while (alpha < 0.96f)
            {
                alpha += 0.05f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        else
        {
            while (alpha > 0.04f)
            {
                alpha -= 0.05f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    
    private void OnEnable()
    {
        OnKnobFocus += Focused;
        OnActivated += Activated;
    }

    private void OnDisable()
    {
        OnKnobFocus -= Focused;
        OnActivated -= Activated;
    }
    
}
