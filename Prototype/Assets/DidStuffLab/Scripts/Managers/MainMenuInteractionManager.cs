using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using Tobii.Gaming;
using UnityEngine;

namespace DidStuffLab.Scripts.Managers
{
    public class MainMenuInteractionManager : InteractionManager
    {
        [SerializeField] private MainMenuManager mainMenuManager;

        private void Awake()
        {
            if(InteractionData.Instance.Method == InteractionMethod.Tobii && !TobiiAPI.IsConnected) SendToInteractionPage();
        }

        private void SendToInteractionPage()
        {
            mainMenuManager.SendToInteractionPage();
            //InteractionData.Instance.Method = InteractionMethod.MouseDwell;
        }
    }
}
