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
    }

    private void Update() {
        if (!TobiiAPI.IsConnected) return;
        if (_gazeAware.HasGazeFocus) Hover();
        else if (!mouseOver) SetDefault();
        //StartCoroutine(WaitForEndOfFrameCoroutine());
    }

    private void OnMouseOver() {
        mouseOver = true;
        Hover();
    }

    private void OnMouseExit() {
        mouseOver = false;
        SetDefault();
    }


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

    private IEnumerator WaitForEndOfFrameCoroutine() {
        yield return new WaitForEndOfFrame();
        if (!isHover) SetDefault();
    }
}