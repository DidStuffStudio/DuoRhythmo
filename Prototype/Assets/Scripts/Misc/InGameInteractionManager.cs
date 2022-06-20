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
        private AbstractDidStuffButton _lastHitDrumElement;
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
            _resultTobii.Clear();
            raycasterDrumPanel.Raycast(_pointerEventDataDrumPanel, _resultTobii);
            if (TobiiAPI.GetGazePoint().IsValid && (Time.unscaledTime - TobiiAPI.GetGazePoint().Timestamp) < 0.1f)
            {
                if (_resultTobii.Count > 0)
                {
                    _resultTobii[0].gameObject.TryGetComponent(typeof(AbstractDidStuffButton), out var isButton);
                    if (!isButton) return;
                    _lastHitDrumElement = _resultTobii[0].gameObject.GetComponent<AbstractDidStuffButton>();
                    if (_lastHitDrumElement == null) return;
                    ExecuteEvents.Execute(_lastHitDrumElement.gameObject, _pointerEventDataDrumPanel,
                        ExecuteEvents.pointerEnterHandler);
                }
                else if (_lastHitDrumElement != null)
                {
                    ExecuteEvents.Execute(_lastHitDrumElement.gameObject, _pointerEventDataDrumPanel,
                        ExecuteEvents.pointerExitHandler);
                }
            }
            else if (_lastHitDrumElement != null)
            {
                    ExecuteEvents.Execute(_lastHitDrumElement.gameObject, _pointerEventDataDrumPanel,
                        ExecuteEvents.pointerExitHandler);
            }
        }
        
    }
}
