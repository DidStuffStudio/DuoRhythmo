using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Tobii.Gaming.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestRaycastScript : MonoBehaviour

{
    private Image updateImage;
    private List<RaycastResult> result = new List<RaycastResult>();
    [SerializeField]  GraphicRaycaster m_Raycaster;
    private static PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    private static GraphicRaycaster tobiiRay;
    private AbstractDidStuffButton _lastHitButton;
    private void Update()
    {
        
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to mouse position (Change to tobii)
        m_PointerEventData.position = Input.mousePosition;
        
        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);
        if (results.Count > 0)
        {
            Debug.Log("Hit " + results[0].gameObject.name);
            var btn = result[0].gameObject.GetComponent<AbstractDidStuffButton>();
            if (btn != null)
            {
                _lastHitButton = btn;
                ExecuteEvents.Execute (btn.gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }
        else if (_lastHitButton != null)
        {
            ExecuteEvents.Execute (_lastHitButton.gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerExitHandler);
        }
        
    }
}
