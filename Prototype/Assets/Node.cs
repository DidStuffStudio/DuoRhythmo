using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {
    public CustomButton button, confirm;
    public float confirmWindow = 2.0f;
    [SerializeField] private bool canConfirm;
    private TestDrumEye testing;

    private bool crRunning = false;
    public bool activated;

    public bool cameFromButton;

    public bool isKick, isSnare;

    private void Start() {
        testing = GetComponent<TestDrumEye>();
    }

    private void Update() {
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
            activated = true;
        }
        else {
            button.SetDefault();
            activated = false;
        }
    }

    public void Play() {
        if (isKick) PlayKick();
        else PlaySnare();
    }

    public void PlayKick() {
        if (!activated) return;
        testing.PlayKick();
    }

    public void PlaySnare() {
        if (!activated) return;
        testing.PlaySnare();
    }

    private IEnumerator Window() {
        crRunning = true;
        yield return new WaitForSeconds(confirmWindow);
        canConfirm = false;
        confirm.ConfirmActivation(false);
        crRunning = false;
    }
}