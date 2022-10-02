using System.Collections.Generic;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DidStuffLab.Scripts.Managers
{
    public class InGameInteractionManager : InteractionManager
    {
        [SerializeField] private InGameMenuManager inGameMenuManager;
        [SerializeField] private GraphicRaycaster raycasterDrumPanel;
        private static PointerEventData _pointerEventDataDrumPanel;
        private AbstractDidStuffButton _currentlyHitDrumElement;
        public List<AbstractDidStuffButton> _lastHitDrumElements = new List<AbstractDidStuffButton>();
        private List<RaycastResult> _resultTobii = new List<RaycastResult>();

        public GraphicRaycaster RaycasterDrumPanel
        {
            get => raycasterDrumPanel;
            set => raycasterDrumPanel = value;
        }
        
        public static InGameInteractionManager Instance { get; private set; }
        
        private void Awake()
        {
            // If there is an instance, and it's not me, kill myself.
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
#if  !UNITY_SERVER
            if(InteractionData.Instance.interactionMethod == InteractionMethod.Tobii && !TobiiAPI.IsConnected) SendToInteractionPage();
            InteractionData.Instance.CheckInteractionMethod(InteractionData.Instance.interactionMethod);
#endif
        }
    
        private void SendToInteractionPage()
        {
            inGameMenuManager.SendToInteractionPanel();
            //InteractionData.Instance.interactionMethod = InteractionMethod.MouseDwell;
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
                    if (_resultTobii[0].gameObject.TryGetComponent<AbstractDidStuffButton>(out var isButton))
                    {
                        _currentlyHitDrumElement = isButton;
                        if (!_lastHitDrumElements.Contains(_currentlyHitDrumElement)) _lastHitDrumElements.Add(_currentlyHitDrumElement);
                        if (isButton == null) return;
                        ExecuteEvents.Execute(isButton.gameObject, _pointerEventDataDrumPanel,
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
            foreach (var b in _lastHitDrumElements)
            {
                b.IsHover = false;
                ExecuteEvents.Execute(b.gameObject, _pointerEventDataDrumPanel, ExecuteEvents.pointerExitHandler);
            }
        }
        
        public void RemoveButtonFromList(AbstractDidStuffButton btn)
        {
            _lastHitDrumElements.Remove(btn);
        }

    }
}
