using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {

        private List<RaycastResult> _resultMouse = new List<RaycastResult>();
        private List<RaycastResult> _resultTobii = new List<RaycastResult>();
        [SerializeField]  GraphicRaycaster raycasterOverlay;
        private static PointerEventData _pointerEventDataOverlay;
        [SerializeField] internal EventSystem eventSystem;
        private static GraphicRaycaster _tobiiRay;
        private GameObject _lastHitButton;
        [SerializeField] private RectTransform gazeSignifier;

        //public static InteractionManager Instance { get; private set; }
        
        public bool ActivateTobiiRay { get; set; } = false;

        public bool Interact { get; set; } = true;

        [SerializeField] private float signifierSpeed = 1.0f;


     
        private void Update()
        {
            if (!Interact) return;
            switch (InteractionData.Instance.Method)
            {
                case InteractionMethod.Mouse:
                    InteractionData.Instance.InputPosition = Input.mousePosition;
                    break;
                case InteractionMethod.MouseDwell:
                    InteractionData.Instance.InputPosition = Input.mousePosition;
                    SignifierFollowInputPosition();
                    break;
                case InteractionMethod.Tobii:
                    if (TobiiAPI.IsConnected)
                    {
                        InteractionData.Instance.InputPosition = TobiiAPI.GetGazePoint().Screen;
                        TobiiGraphicRaycast();
                        SignifierFollowInputPosition();
                    }
                    break;
                case InteractionMethod.Touch:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if(ActivateTobiiRay) TobiiGraphicRaycast();
            //MouseGraphicRaycast();
        }

        protected virtual void TobiiGraphicRaycast()
        {
            if (!TobiiAPI.IsConnected) return;
            //Set up the new Pointer Event
            _pointerEventDataOverlay = new PointerEventData(eventSystem)
            {
                //Set the Pointer Event Position to mouse position (Change to tobii)
                position = TobiiAPI.GetGazePoint().Screen,
                pointerId = 1
            };
            
            //Raycast using the Graphics Raycaster and mouse click position
            raycasterOverlay.Raycast(_pointerEventDataOverlay, _resultTobii);
            
            if (_resultTobii.Count > 0) //Raycast for overlay canvas
            {
                var btn = _resultTobii[0].gameObject;
                if (btn == null) return;
                _lastHitButton = btn;
                ExecuteEvents.Execute (btn.gameObject,  _pointerEventDataOverlay, ExecuteEvents.pointerEnterHandler);
            }
            else if (_lastHitButton != null)
            {
                ExecuteEvents.Execute (_lastHitButton.gameObject,  _pointerEventDataOverlay, ExecuteEvents.pointerExitHandler);
            }
        }
        
        protected virtual void MouseGraphicRaycast()
        {
            if (!TobiiAPI.IsConnected) return;
            //Set up the new Pointer Event
            _pointerEventDataOverlay = new PointerEventData(eventSystem)
            {
                //Set the Pointer Event Position to mouse position (Change to tobii)
                position = Input.mousePosition,
                pointerId = 1
            };
            
            //Raycast using the Graphics Raycaster and mouse click position
            raycasterOverlay.Raycast(_pointerEventDataOverlay, _resultMouse);
            
            if (_resultMouse.Count > 0) //Raycast for overlay canvas
            {
                var btn = _resultMouse[0].gameObject;
                if (btn == null) return;
                _lastHitButton = btn;
                ExecuteEvents.Execute (btn.gameObject,  _pointerEventDataOverlay, ExecuteEvents.pointerEnterHandler);
            }
            else if (_lastHitButton != null)
            {
                ExecuteEvents.Execute (_lastHitButton.gameObject,  _pointerEventDataOverlay, ExecuteEvents.pointerExitHandler);
            }
        }

        public void SetSignifierActive(bool active)
        {
            gazeSignifier.gameObject.SetActive(active);
            Cursor.visible = !active;
        }
        
        private void SignifierFollowInputPosition()
        {
            var pos = Vector3.Lerp(gazeSignifier.position, InteractionData.Instance.InputPosition, Time.deltaTime * signifierSpeed);
            gazeSignifier.position = pos;
        }
       
 
    }
}


