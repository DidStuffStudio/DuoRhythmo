using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public GazeAware buttonGaze, confirmGaze;
    public Button button, confirm;
    public float confirmWindow = 2.0f;
    [SerializeField] private bool canConfirm;
    private TestDrumEye testing;

    private bool crRunning = false;
    public bool activated;

    private void Start()
    {
        testing = GetComponent<TestDrumEye>();
    }

    private void Update()
    {
        var pointer = new PointerEventData(EventSystem.current);

        if (buttonGaze.HasGazeFocus)
        {
            //base button onHover state if looking at it
            ExecuteEvents.Execute(button.gameObject, pointer,
                ExecuteEvents.pointerEnterHandler);
            //start window of time to press confirm button
            if (crRunning) return;
            StartCoroutine(Window());
        }
        else if (!buttonGaze.HasGazeFocus && !canConfirm)
        {
            //Return both to clickable state if not looking at base button and window has passed
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
            ExecuteEvents.Execute(confirm.gameObject, pointer, ExecuteEvents.pointerExitHandler);
        }

        if (!canConfirm || !confirmGaze.HasGazeFocus) return;
        
        //confirm button OnHover state
        ExecuteEvents.Execute(confirm.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
        if (activated) DeactivateNode(pointer);
        else ActivateNode(pointer);

        if (!activated)
        {
            button.onClick.AddListener(delegate { ActivateNode(pointer); });
        }
        else
        {
            button.onClick.AddListener(delegate { DeactivateNode(pointer); });
        }
    }


    public void ActivateNode(PointerEventData pointer)
    {
        //Return both to clickable state
        ExecuteEvents.Execute(confirm.gameObject, pointer, ExecuteEvents.pointerExitHandler);
        button.GetComponent<Image>().color = Color.green;
        //Set node active
        activated = true;
    }

    public void DeactivateNode(PointerEventData pointer)
    {
        //Return both to clickable state
        button.GetComponent<Image>().color = Color.white;
        ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
        ExecuteEvents.Execute(confirm.gameObject, pointer, ExecuteEvents.pointerExitHandler);
        activated = false;
    }

    public void Play()
    {
        if (!activated) return;
        testing.play = true;
    }
    
    IEnumerator Window()
    {
        canConfirm = true;
        crRunning = true;
        yield return new WaitForSeconds(confirmWindow);
        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current),
            ExecuteEvents.pointerExitHandler);
        canConfirm = false;
        crRunning = false;
    }
}
