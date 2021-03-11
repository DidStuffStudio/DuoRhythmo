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

    private void Start() {
        testing = GetComponent<TestDrumEye>();
    }

    private void Update() {
        if(button.isHover) {
            confirm.gameObject.SetActive(true);
            StartCoroutine(Window());
        }
        if (!canConfirm) return;
        if(confirm.isHover) {
            button.SetActive();
            activated = true;
            confirm.gameObject.SetActive(false);
        }
    }

    public void Play() {
        if (!activated) return;
        testing.play = true;
    }

    private IEnumerator Window() {
        canConfirm = true;
        yield return new WaitForSeconds(confirmWindow);
        canConfirm = false;
        confirm.gameObject.SetActive(false);
    }
}