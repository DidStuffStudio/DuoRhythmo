using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


public class Node : MonoBehaviour {
    public NodesVisualizer _nodesVisualizer;
    public NodeManager nodeManager;
    public int drumIndex;
    public InteractionMethod interactionMethod;
    public DrumType drumType;
    public CustomButton button, confirm;
    public float confirmWindow = 2.0f;
    [SerializeField] private bool canConfirm;
    private bool _crRunning = false;
    public bool activated, canPlay=true;

    public int indexValue;

    private ScreenSync _screenSync;
    
    public bool cameFromButton;
    public AK.Wwise.Event kickEvent, snareEvent, hiHatEvent, tomTomEvent, cymbalEvent;
    private VisualEffect _vfx;
    private void Start() {
        _screenSync = GetComponentInParent<ScreenSync>();
        _vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
        
        switch (interactionMethod) {
            case InteractionMethod.contextSwitch: {
                break;
            }

            case InteractionMethod.dwellFeedback: {
                confirm.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void Update() {
        switch (interactionMethod) {
            case InteractionMethod.contextSwitch: {
                if (button.isHover) {
                    canConfirm = true;
                    confirm.ConfirmActivation(true);
                    cameFromButton = true;
                }
                else if (!_crRunning) StartCoroutine(Window());

                if (!confirm.isHover || !cameFromButton) return;

                cameFromButton = false;
                if (!activated) {
                    button.SetActive();
                    _screenSync.SetIndexValue(indexValue);
                    activated = true;
                }
                else {
                    button.SetDefault();
                    _screenSync.SetIndexValue(indexValue);
                    activated = false;
                }


                break;
            }
            case InteractionMethod.dwellFeedback: {
                if (button.isHover)
                {

                    if (button.isActive) button.confirmScaler.GetComponent<Image>().color = button.defaultColor;
                    else button.confirmScaler.GetComponent<Image>().color = button.activeColor;
                    
                    if (button.confirmScalerRT.localScale.x < 1.0f)
                        button.confirmScalerRT.localScale += Vector3.one / 100;
                    else {
                        button.confirmScalerRT.localScale = Vector3.zero;
                        if (!activated) {
                            StartCoroutine(button.InteractionBreakTime());
                            button.SetActive();
                            _screenSync.SetIndexValue(indexValue);
                            activated = true;
                        }
                        else {
                            StartCoroutine(button.InteractionBreakTime());
                            button.SetDefault();
                            _screenSync.SetIndexValue(indexValue);
                            activated = false;
                        }
                    }
                }

                else {
                    if (button.confirmScalerRT.localScale.x < 0.0f) return;
                    button.confirmScalerRT.localScale -= Vector3.one / 100;
                }

                break;
            }
        }
    }

    public void Activate(bool setActive) {
        if (!activated) {
            button.SetActive();
            activated = true;
        }
        else {
            button.SetDefault();
            activated = false;
        }
        //_nodesVisualizer.UpdateNode(drumIndex, indexValue, activated);
    }

    public void PlayDrum()
    {
        if (!activated || !canPlay) return;
        
        StartCoroutine(AudioVFX());
        
        
        
        switch (drumType)
        {
            case DrumType.Kick:
                kickEvent.Post(gameObject);
                break;
            case DrumType.Snare:
                snareEvent.Post(gameObject);
                break;
            case DrumType.HiHat:
                hiHatEvent.Post(gameObject);
                break;
            case DrumType.TomTom:
                tomTomEvent.Post(gameObject);
                break;
            case DrumType.Cymbal:
                cymbalEvent.Post(gameObject);
                break;
        }
    }
    private IEnumerator AudioVFX()
    {
        _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") + 1.0f);
        bool run = true;
        while (run)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            if (_vfx.GetFloat("SphereSize") > 1.1f) _vfx.SetFloat("SphereSize", _vfx.GetFloat("SphereSize") - 0.1f);
            else run = false;
        }
    }


    private IEnumerator Window() {
        _crRunning = true;
        yield return new WaitForSeconds(confirmWindow);
        canConfirm = false;
        confirm.ConfirmActivation(false);
        _crRunning = false;
    }
}