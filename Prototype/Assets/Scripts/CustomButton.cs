using System;
using System.Collections;
using Tobii.Gaming;
using Tobii.Gaming.Internal;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GazeAware))]
[RequireComponent(typeof(Image))]
public class CustomButton : MonoBehaviour {
    private GazeAware _gazeAware;
    private Image _image;

    private UserInput _userInput;
    /*
    struct State {
        private bool active;
        private Color color;
    }

    private State defaultState, hoverState, activeState, hintState;
    */

    public bool isDefault, isHover, isActive, isHint, isConfirmationButton;
    [SerializeField] private Color defaultColor, hoverColor, activeColor, hintColor;

    public bool mouseOver;

    private void Start() {
        _userInput = FindObjectOfType<UserInput>();
        _gazeAware = GetComponent<GazeAware>();
        _image = GetComponent<Image>();
        SetDefault();
        if (isConfirmationButton) ConfirmActivation(false);
        
    }

    private void Update() {
        if (!TobiiAPI.IsConnected) return;
        if (_gazeAware.HasGazeFocus) Hover();
        else if (!mouseOver) UnHover();
    }

    private void OnMouseOver() {
        mouseOver = true;
        Hover();
    }

    private void OnMouseExit() {
        mouseOver = false;
        UnHover();
    }


    public void SetActive()
    {
        _image.color = activeColor;
        if (isActive) return;
        isActive = true;
        isDefault = false;

    }

    private void Hover() {
        _image.color = hoverColor;
        if (isHover) return;
        isHover = true;
    }

    private void UnHover()
    {
        if (!isHover) return;
        isHover = false;
        if (isConfirmationButton) SetDefault();
        else if (isDefault) SetDefault();
        else if (isActive) SetActive();
        {
            
        }
    }

    public void SetDefault() {
        _image.color = defaultColor;
        if (isDefault) return;
        isDefault = true;
        isActive = false;
    }

    public void ConfirmActivation(bool enabled)
    {
        GetComponent<Collider>().enabled = enabled;
        GetComponent<Image>().enabled = enabled;
    }
}