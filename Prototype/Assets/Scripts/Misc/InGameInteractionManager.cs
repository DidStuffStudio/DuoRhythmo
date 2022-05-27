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
        [SerializeField]  GraphicRaycaster raycasterDrumPanel;
        private static PointerEventData _pointerEventDataDrumPanel;
        private GameObject _lastHitDrumElement;

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
            _pointerEventDataDrumPanel = new PointerEventData(_eventSystem);
            //Set the Pointer Event Position to mouse position (Change to tobii)
            _pointerEventDataDrumPanel.position = InteractionData.Instance.InputPosition;
            //Set pointer ID to 1 so we can detect that it is Tobii
            _pointerEventDataDrumPanel.pointerId = 1;
            //Create a list of Raycast Results
            List<RaycastResult> resultsDrumPanel = new List<RaycastResult>();
            //Raycast
            raycasterDrumPanel.Raycast(_pointerEventDataDrumPanel, resultsDrumPanel);
            
            if (resultsDrumPanel.Count > 0)
            {
                Debug.Log("Hit " + resultsDrumPanel[0].gameObject.name);
                var btn = resultsDrumPanel[0].gameObject;
                if (btn != null)
                {
                    _lastHitDrumElement = btn;
                    ExecuteEvents.Execute (btn.gameObject,  _pointerEventDataDrumPanel, ExecuteEvents.pointerEnterHandler);
                }
            }
            else if (_lastHitDrumElement != null)
            {
                ExecuteEvents.Execute (_lastHitDrumElement.gameObject,  _pointerEventDataDrumPanel, ExecuteEvents.pointerExitHandler);
            }
        }
    }
}
