using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {

        private List<RaycastResult> result = new List<RaycastResult>();
        [SerializeField]  GraphicRaycaster raycasterOverlay;
        private static PointerEventData _pointerEventDataOverlay;
        [SerializeField] internal EventSystem _eventSystem;
        private static GraphicRaycaster tobiiRay;
        private GameObject _lastHitButton;
        [SerializeField] private RectTransform gazeSignifier;
        private bool _activateTobiiRay = false;
        private bool _interact = true;
        //public static InteractionManager Instance { get; private set; }
        
        public bool ActivateTobiiRay
        {
            get => _activateTobiiRay;
            set => _activateTobiiRay = value;
        }

        public bool Interact
        {
            get => _interact;
            set => _interact = value;
        }
        
        [SerializeField] private float signifierSpeed = 1.0f;


     
        private void Update()
        {
            if (!_interact) return;
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
                    /*if (TobiiAPI.IsConnected)
                    {
                        InteractionData.Instance.InputPosition = TobiiAPI.GetGazePoint().Screen;
                        TobiiGraphicRaycast();
                        SignifierFollowInputPosition();
                    } */
                    InteractionData.Instance.InputPosition = Input.mousePosition;
                    TobiiGraphicRaycast();
                    break;
            }
            if(_activateTobiiRay) TobiiGraphicRaycast();
        }

        protected virtual void TobiiGraphicRaycast()
        {
            //if (!TobiiAPI.IsConnected) return;
            
            
            //Set up the new Pointer Event
            _pointerEventDataOverlay = new PointerEventData(_eventSystem);
            //Set the Pointer Event Position to mouse position (Change to tobii)
            _pointerEventDataOverlay.position = InteractionData.Instance.InputPosition;
            _pointerEventDataOverlay.pointerId = 1;
            //Create a list of Raycast Results
            List<RaycastResult> resultsOverlay = new List<RaycastResult>();
            
            //Raycast using the Graphics Raycaster and mouse click position
            raycasterOverlay.Raycast(_pointerEventDataOverlay, resultsOverlay);
            
            if (resultsOverlay.Count > 0) //Raycast for overlay canvas
            {
                Debug.Log("Hit " + resultsOverlay[0].gameObject.name);
                var btn = result[0].gameObject;
                if (btn != null)
                {
                    _lastHitButton = btn;
                    ExecuteEvents.Execute (btn.gameObject,  _pointerEventDataOverlay, ExecuteEvents.pointerEnterHandler);
                }
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


