using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


public class Node : MonoBehaviour {
    public InteractionMethod interactionMethod;
    public DrumType drumType;
    public CustomButton button, confirm;
    public float confirmWindow = 2.0f;
    [SerializeField] private bool canConfirm;
    private bool crRunning = false;
    public bool activated, canPlay=true;

    public int indexValue;

    private ScreenSync _screenSync;
    
    public bool cameFromButton;
    public AK.Wwise.Event kickEvent, snareEvent, hiHatEvent, tomTomEvent, cymbalEvent;
    private VisualEffect vfx;
    private void Start() {
        _screenSync = GetComponentInParent<ScreenSync>();
        vfx = GameObject.FindWithTag("AudioVFX").GetComponent<VisualEffect>();
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
                else if (!crRunning) StartCoroutine(Window());

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
                if (button.isHover) {
                    if (button.confirmScalerRT.localScale.x < 1.0f)
                        button.confirmScalerRT.localScale += Vector3.one / 200;
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
    }

    public void PlayDrum()
    {
        if (!activated || !canPlay) return;
        vfx.SetFloat("SphereSize", vfx.GetFloat("SphereSize") + 2.0f);
        StartCoroutine(SetVFXBack());
        switch (drumType)
        {
            case DrumType.kick:
                kickEvent.Post(gameObject);
                break;
            case DrumType.snare:
                snareEvent.Post(gameObject);
                break;
            case DrumType.hiHat:
                hiHatEvent.Post(gameObject);
                break;
            case DrumType.tomTom:
                tomTomEvent.Post(gameObject);
                break;
            case DrumType.cymbal:
                cymbalEvent.Post(gameObject);
                break;
        }
    }
    private IEnumerator SetVFXBack() {
        while (vfx.GetFloat("SphereSize") > 0.7)
        {
            yield return new WaitForSeconds(0.01f);
            vfx.SetFloat("SphereSize", vfx.GetFloat("SphereSize") - 2.0f);
        }
    }


    private IEnumerator Window() {
        crRunning = true;
        yield return new WaitForSeconds(confirmWindow);
        canConfirm = false;
        confirm.ConfirmActivation(false);
        crRunning = false;
    }
}