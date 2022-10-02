using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using Tobii.Gaming;
using UnityEngine;

namespace DidStuffLab.Scripts.Managers
{
    public class MainMenuInteractionManager : InteractionManager
    {
        [SerializeField] private MainMenuManager mainMenuManager;

        
        public static MainMenuInteractionManager Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, kill myself.
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }

        public void RemoveButtonFromList(AbstractDidStuffButton btn)
        {
            _lastHitButtons.Remove(btn);
        }
        
        private void Start()
        {
            if(InteractionData.Instance.interactionMethod == InteractionMethod.Tobii && !TobiiAPI.IsConnected) SendToInteractionPage();
            InteractionData.Instance.CheckInteractionMethod(InteractionData.Instance.interactionMethod);
        }

        private void SendToInteractionPage()
        {
            mainMenuManager.SendToInteractionPage();
            //InteractionData.Instance.interactionMethod = InteractionMethod.MouseDwell;
        }
        
    }
}
