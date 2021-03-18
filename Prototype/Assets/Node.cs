using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;


public class Node : MonoBehaviour {
    public InteractionMethod interactionMethod;
    public DrumType drumType;
    public CustomButton button, confirm;
    public float confirmWindow = 2.0f;
    [SerializeField] private bool canConfirm;
    private bool crRunning = false;
    public bool activated;
    
    private NodeSync _nodeSync;
    
    public bool cameFromButton;
    public AK.Wwise.Event kickEvent, snareEvent, hiHatEvent, tomTomEvent;

    private void Start() {
        _nodeSync = GetComponent<NodeSync>();
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
        if (_nodeSync.IsActivated && !activated) {
            activated = true;
            button.SetActive();
        }
        else if (!_nodeSync.IsActivated && activated) {
            activated = false;
            button.SetDefault();
        }
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
                    _nodeSync.SetActive(true);
                    activated = true;
                }
                else {
                    button.SetDefault();
                    _nodeSync.SetActive(false);
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
                            activated = true;
                        }
                        else {
                            StartCoroutine(button.InteractionBreakTime());
                            button.SetDefault();
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

    public void PlayDrum() {
        if (!activated) return;
        switch (drumType) {
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