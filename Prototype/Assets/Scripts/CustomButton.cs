using System;
using Tobii.Gaming;
using Tobii.Gaming.Internal;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GazeAware))]
[RequireComponent(typeof(Image))]
public class CustomButton : MonoBehaviour, IGazeFocusable {
    private GazeAware _gazeAware;
    private Image _image;

    /*
    struct State {
        private bool active;
        private Color color;
    }

    private State defaultState, hoverState, activeState, hintState;
    */

    public bool isDefault, isHover, isActive, isHint, isConfirmationButton;
    [SerializeField] private Color defaultColor, hoverColor, activeColor, hintColor;

    private void Start() {
        _gazeAware = GetComponent<GazeAware>();
        _image = GetComponent<Image>();
        SetDefault();
    }

    private void OnMouseOver() => Hover();

    private void OnMouseExit() => SetDefault();


    public void SetActive() {
        isActive = true;
        _image.color = activeColor;
        isHint = false;
    }

    private void Hover() {
        if (isHover) return;
        print("isHover");
        isHover = true;
        isDefault = false;
        _image.color = hoverColor;
    }

    private void SetDefault() {
        if (isDefault) return;
        print("isDefault");
        isHover = false;
        isDefault = true;
        _image.color = defaultColor;
    }

    public void UpdateGazeFocus(bool hasFocus) {
        if(hasFocus) Hover();
        else SetDefault();
    }
}