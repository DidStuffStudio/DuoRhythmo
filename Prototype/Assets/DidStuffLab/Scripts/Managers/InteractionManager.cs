using System;
using System.Collections.Generic;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DidStuffLab.Scripts.Managers
{
    public class InteractionManager : MonoBehaviour
    {
        private List<RaycastResult> _resultTobii = new List<RaycastResult>();
        [SerializeField] GraphicRaycaster raycasterOverlay;
        private static PointerEventData _pointerEventDataOverlay;
        [SerializeField] internal EventSystem eventSystem;
        private static GraphicRaycaster _tobiiRay;
        public List<AbstractDidStuffButton> _lastHitButtons = new List<AbstractDidStuffButton>();
        private AbstractDidStuffButton _currentlyHitButton;
        [SerializeField] private RectTransform gazeSignifier;

        //public static InteractionManager Instance { get; private set; }

        public bool ActivateTobiiRay { get; set; } = false;

        public bool Interact { get; set; } = true;

        [SerializeField] private float signifierSpeed = 1.0f;





        private void Update()
        {
#if !UNITY_SERVER
            if (!Interact) return;
            switch (InteractionData.Instance.interactionMethod)
            {
                case InteractionMethod.Mouse:
                    InteractionData.Instance.InputPosition = Input.mousePosition;
                    if (ActivateTobiiRay) TobiiGraphicRaycast();
                    break;
                case InteractionMethod.MouseDwell:
                    InteractionData.Instance.InputPosition = Input.mousePosition;
                    SignifierFollowInputPosition();
                    if (ActivateTobiiRay) TobiiGraphicRaycast();
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
#endif
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
                        _currentlyHitButton = isButton;
                        if (!_lastHitButtons.Contains(_currentlyHitButton)) _lastHitButtons.Add(_currentlyHitButton);
                        if (_lastHitButtons == null) return;
                        ExecuteEvents.Execute(_currentlyHitButton.gameObject, _pointerEventDataOverlay,
                            ExecuteEvents.pointerEnterHandler);
                    }
                    else UnHoverPreviousButtons();
                }
                else UnHoverPreviousButtons();
            }
            else UnHoverPreviousButtons();
        }


        private void UnHoverPreviousButtons()
        {
            foreach (var b in _lastHitButtons)
            {
                b.IsHover = false;
                ExecuteEvents.Execute(b.gameObject, _pointerEventDataOverlay, ExecuteEvents.pointerExitHandler);
            }
        }


        public void SetSignifierActive(bool active)
        {
            gazeSignifier.gameObject.SetActive(active);
            //Cursor.visible = !active;
        }

        private void SignifierFollowInputPosition()
        {
            if (!float.IsNaN(InteractionData.Instance.InputPosition.x) &&
                !float.IsNaN(InteractionData.Instance.InputPosition.y))
            {
                var pos = Vector2.Lerp(gazeSignifier.position, InteractionData.Instance.InputPosition,
                    Time.deltaTime * signifierSpeed);
                gazeSignifier.position = pos;
            }
        }
    }
}


