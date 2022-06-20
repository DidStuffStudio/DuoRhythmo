using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using Tobii.Gaming;
using UnityEngine;

namespace Main_Menu
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
