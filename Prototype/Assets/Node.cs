using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{

    public GazeAware buttonGaze, confirmGaze;
    public CustomButton button, confirm;
    public float confirmWindow = 2.0f;
    [SerializeField] private bool canConfirm;
    private TestDrumEye testing;

    private bool crRunning = false;
    public bool activated;


    // if it's confirmation button --> invisible, visible, onHover
    
    // default, onHover, active, hint, isConfirmationButton

    private void Start()
    {
        testing = GetComponent<TestDrumEye>();
    }

    private void Update()
    {
        

        // Confirm button OnHover state

        if (buttonGaze.HasGazeFocus)
        {
            // base button onHover state if looking at it
            
            // Start window of time to press confirm button
            if (crRunning) return;
            StartCoroutine(Window());
        }
        else if (!buttonGaze.HasGazeFocus && !canConfirm)
        {
            // Return both to clickable state if not looking at base button and window has passed
            
        }

        if (!canConfirm || !confirmGaze.HasGazeFocus) return;
    }


    public void ActivateNode(PointerEventData pointer)
    {
        // Return both to clickable state
        
        button.GetComponent<Image>().color = Color.green;
        // Set node active
        activated = true;
    }

    public void DeactivateNode(PointerEventData pointer)
    {
        // Return both to clickable state
        button.GetComponent<Image>().color = Color.white;
        
        activated = false;
    }

    public void Play()
    {
        if (!activated) return;
        testing.play = true;
    }
    
    private IEnumerator Window()
    {
        canConfirm = true;
        crRunning = true;
        yield return new WaitForSeconds(confirmWindow);
        
        canConfirm = false;
        crRunning = false;
    }
}

