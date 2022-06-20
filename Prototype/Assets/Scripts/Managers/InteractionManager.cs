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
        private List<RaycastResult> _resultTobii = new List<RaycastResult>();
        [SerializeField]  GraphicRaycaster raycasterOverlay;
        private static PointerEventData _pointerEventDataOverlay;
        [SerializeField] internal EventSystem eventSystem;
        private static GraphicRaycaster _tobiiRay;
        private AbstractDidStuffButton _lastHitButton;
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
                    if(ActivateTobiiRay) TobiiGraphicRaycast();
                    break;
                case InteractionMethod.MouseDwell:
                    InteractionData.Instance.InputPosition = Input.mousePosition;
                    SignifierFollowInputPosition();
                    if(ActivateTobiiRay) TobiiGraphicRaycast();
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
            _resultTobii.Clear();
            raycasterOverlay.Raycast(_pointerEventDataOverlay, _resultTobii);
            if (TobiiAPI.GetGazePoint().IsValid && (Time.unscaledTime - TobiiAPI.GetGazePoint().Timestamp) < 0.1f)
            {
                if (_resultTobii.Count > 0) //Raycast for overlay canvas
                {
                    if (_resultTobii[0].gameObject.TryGetComponent<AbstractDidStuffButton>(out var isButton))
                    {
                        _lastHitButton = isButton;
                        if (_lastHitButton == null) return;
                        ExecuteEvents.Execute(_lastHitButton.gameObject, _pointerEventDataOverlay,
                            ExecuteEvents.pointerEnterHandler);
                    }
                }
                else if (_lastHitButton != null)
                {
                    _lastHitButton.IsHover = false;
                    ExecuteEvents.Execute(_lastHitButton.gameObject, _pointerEventDataOverlay,
                        ExecuteEvents.pointerExitHandler);
                }
            }
            else if (_lastHitButton != null)
            {
                _lastHitButton.IsHover = false;
                ExecuteEvents.Execute(_lastHitButton.gameObject, _pointerEventDataOverlay,
                    ExecuteEvents.pointerExitHandler);
            }
        }
        

        public void SetSignifierActive(bool active)
        {
            gazeSignifier.gameObject.SetActive(active);
            //Cursor.visible = !active;
        }
        
        private void SignifierFollowInputPosition()
        {
            var pos = Vector3.Lerp(gazeSignifier.position, InteractionData.Instance.InputPosition, Time.deltaTime * signifierSpeed);
            gazeSignifier.position = pos;
        }
       
 
    }
}


