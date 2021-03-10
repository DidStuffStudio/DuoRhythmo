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


    private void Start()
    {
        testing = GetComponent<TestDrumEye>();
    }

    private void Update()
    {
        var pointer = new PointerEventData(EventSystem.current);

        if (buttonGaze.HasGazeFocus)
        {
            ExecuteEvents.Execute(button.gameObject, pointer,
                ExecuteEvents.pointerEnterHandler);
            
            canConfirm = true;
            if (crRunning) return;
            StartCoroutine(Window());
        }
        else if (!buttonGaze.HasGazeFocus && !canConfirm)
        {
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
            ExecuteEvents.Execute(confirm.gameObject, pointer, ExecuteEvents.pointerExitHandler);
        }

        if (canConfirm && confirmGaze.HasGazeFocus)
        {
            print("confirmed");
            ExecuteEvents.Execute(confirm.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
                   
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
            ExecuteEvents.Execute(confirm.gameObject, pointer, ExecuteEvents.pointerExitHandler);
            testing.play = true;
        }
       

    }

    IEnumerator Window()
    {
        crRunning = true;
        yield return new WaitForSeconds(confirmWindow);
        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current),
            ExecuteEvents.pointerExitHandler);
        canConfirm = false;
        crRunning = false;
    }
}
