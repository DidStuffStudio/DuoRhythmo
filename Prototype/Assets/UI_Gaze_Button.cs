using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.VFX;


public class UI_Gaze_Button : MonoBehaviour {
    public InteractionMethod interactionMethod;
    public CustomButton button, confirm;
    public float confirmWindow = 2.0f;
    [SerializeField] private bool canConfirm;
    private bool crRunning = false;
    public bool activated;
    [SerializeField] private Text _text;
    [SerializeField] private String _string;

    public bool cameFromButton;
    private void Start() {
        switch (interactionMethod) {
            case InteractionMethod.contextSwitch: {
                break;
            }

            case InteractionMethod.dwellFeedback: {
                confirm.gameObject.SetActive(false);
                break;
            }
        }

        _text.text = _string;
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
                    MasterManager.Instance.userInterfaceManager.Solo(true);
                    activated = true;
                }
                else {
                    button.SetDefault();
                    MasterManager.Instance.userInterfaceManager.Solo(false);
                    activated = false;
                }


                break;
            }
            case InteractionMethod.dwellFeedback: {
                if (button.isHover) {
                    if (button.confirmScalerRT.localScale.x < 1.0f)
                        button.confirmScalerRT.localScale += Vector3.one / 100;
                    else {
                        button.confirmScalerRT.localScale = Vector3.zero;
                        if (!activated) {
                            StartCoroutine(button.InteractionBreakTime());
                            button.SetActive();
                            MasterManager.Instance.userInterfaceManager.Solo(true);
                            activated = true;
                        }
                        else {
                            StartCoroutine(button.InteractionBreakTime());
                            button.SetDefault();
                            MasterManager.Instance.userInterfaceManager.Solo(false);
                            activated = false;
                            print("Deactivated");
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

    private IEnumerator Window() {
        crRunning = true;
        yield return new WaitForSeconds(confirmWindow);
        canConfirm = false;
        confirm.ConfirmActivation(false);
        crRunning = false;
    }
}