using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Misc
{
    public class InGameInteractionManager : InteractionManager
    {
        [SerializeField] private InGameMenuManager inGameMenuManager;
        [SerializeField] private GraphicRaycaster raycasterDrumPanel;
        private static PointerEventData _pointerEventDataDrumPanel;
        private GameObject _lastHitDrumElement;
        private List<RaycastResult> _resultMouse = new List<RaycastResult>();
        private List<RaycastResult> _resultTobii = new List<RaycastResult>();

        public GraphicRaycaster RaycasterDrumPanel
        {
            get => raycasterDrumPanel;
            set => raycasterDrumPanel = value;
        }

        private void Awake()
        {
            if(InteractionData.Instance.Method == InteractionMethod.Tobii && !TobiiAPI.IsConnected) SendToInteractionPage();
            InteractionData.Instance.CheckInteractionMethod();
        }
    
        private void SendToInteractionPage()
        {
            inGameMenuManager.SendToInteractionPanel();
            //InteractionData.Instance.Method = InteractionMethod.MouseDwell;
        }

        public void SwitchDrumPanel(GraphicRaycaster newCanvas) => raycasterDrumPanel = newCanvas;
        
        protected override void TobiiGraphicRaycast()
        {
            base.TobiiGraphicRaycast();
            
            //Set up the new Pointer Event
            _pointerEventDataDrumPanel = new PointerEventData(eventSystem)
            {
                //Set the Pointer Event Position to mouse position (Change to tobii)
                position = TobiiAPI.GetGazePoint().Screen,
                //Set pointer ID to 1 so we can detect that it is Tobii
                pointerId = 1
            };

            //Raycast
            raycasterDrumPanel.Raycast(_pointerEventDataDrumPanel, _resultTobii);
            
            if (_resultTobii.Count > 0)
            {
                var btn = _resultTobii[0].gameObject;
                if (btn == null) return;
                _lastHitDrumElement = btn;
                ExecuteEvents.Execute (btn.gameObject,  _pointerEventDataDrumPanel, ExecuteEvents.pointerEnterHandler);
            }
            else if (_lastHitDrumElement != null)
            {
                ExecuteEvents.Execute (_lastHitDrumElement.gameObject,  _pointerEventDataDrumPanel, ExecuteEvents.pointerExitHandler);
            }
        }

        protected override void MouseGraphicRaycast()
        {
            base.MouseGraphicRaycast();
            
            //Set up the new Pointer Event
            _pointerEventDataDrumPanel = new PointerEventData(eventSystem)
            {
                //Set the Pointer Event Position to mouse position (Change to tobii)
                position = Input.mousePosition,
                //Set pointer ID to 1 so we can detect that it is Tobii
                pointerId = 1
            };
            //Raycast
            raycasterDrumPanel.Raycast(_pointerEventDataDrumPanel, _resultMouse);
            
            if (_resultMouse.Count > 0)
            {
                var btn = _resultMouse[0].gameObject;
                if (btn == null) return;
                _lastHitDrumElement = btn;
                ExecuteEvents.Execute (btn.gameObject,  _pointerEventDataDrumPanel, ExecuteEvents.pointerEnterHandler);
            }
            else if (_lastHitDrumElement != null)
            {
                ExecuteEvents.Execute (_lastHitDrumElement.gameObject,  _pointerEventDataDrumPanel, ExecuteEvents.pointerExitHandler);
            }
        }
    }
}
