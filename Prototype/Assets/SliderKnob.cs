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
                _minValue = -rect.height / 2;
                _maxValue = rect.height / 2;
                _knobRectTransform.anchoredPosition = new Vector2(0, _minValue);
                break;
            }
        }
    }


    private void Update()
    {
        // Mouse Delta
        if (_activated)
        {
            switch (_slider.sliderDirection)
            {
                case CustomSlider.SliderDirection.Horizontal: //Get x delta
                {
                    break;
                }
                case CustomSlider.SliderDirection.Vertical: // Get y delta
                {
                    var mouseDeltaY = Input.mousePosition.y - _lastMousePosition.y;
                    _lastMousePosition = Input.mousePosition;
                    
                    //eye delta is negative, deactivate up arrow and give it a constant position below current mouse/eye position
                    if (Input.mousePosition.y > _knobRectTransform.position.y)
                    {
                        StartCoroutine(FadeSignifier(upSignifier.GetComponent<Image>(), true));
                        StartCoroutine(FadeSignifier(downSignifier.GetComponent<Image>(), false));
                        
                        upSignifier.anchoredPosition = new Vector2(0.0f,
                            Input.mousePosition.y + offset);
                        
                        // If the mouse has travelled to a point above the knob that is greater than the threshold to start moving the knob
                        if (_knobRectTransform.anchoredPosition.y < _maxValue)
                        {
                            _knobRectTransform.anchoredPosition = new Vector2(0.0f, Input.mousePosition.y - offset);
                        }
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
