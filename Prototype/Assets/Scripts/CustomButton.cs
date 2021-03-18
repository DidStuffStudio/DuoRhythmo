using System;
using System.Collections;
using Tobii.Gaming;
using Tobii.Gaming.Internal;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(GazeAware))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Collider))]
public class CustomButton : MonoBehaviour {
    private GazeAware _gazeAware;
    private Image _image;
    private Collider _collider;

    public bool isDefault, isHover, isActive, isHint, isConfirmationButton;
    [SerializeField] private Color defaultColor, hoverColor, activeColor, hintColor;
    public GameObject confirmScaler;
    public bool mouseOver;
    public RectTransform confirmScalerRT;
    public float interactionBreakTime = 1.0f;
    private bool _canHover = true;

    private void Start() {
        confirmScalerRT = confirmScaler.GetComponent<RectTransform>();
        _gazeAware = GetComponent<GazeAware>();
        _image = GetComponent<Image>();
        _collider = GetComponent<Collider>();
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

    public void SetActive() {
        _image.color = activeColor;
        if (isActive) return;
        isActive = true;
        isDefault = false;
        if (isHint) isHint = false;
    }

    private void Hover() {
        if (!_canHover) return;
        _image.color = hoverColor;
        if (isHover) return;
        isHover = true;
    }

    private void UnHover() {
        if (!isHover) return;
        isHover = false;
        if (isConfirmationButton) SetDefault();
        else if (isDefault) SetDefault();
        else if (isActive) SetActive();
    }

    public void SetDefault() {
        _image.color = defaultColor;
        if (isDefault) return;
        isDefault = true;
        isActive = false;
    }

    public void ConfirmActivation(bool enabled) {
        _collider.enabled = enabled;
        _image.enabled = enabled;
    }

    public void SetHint() {
        _image.color = hintColor;
        isHint = true;
    }

    public IEnumerator InteractionBreakTime() {
        _canHover = false;
        isHover = false;
        yield return new WaitForSeconds(interactionBreakTime);
        _canHover = true;
    }
}