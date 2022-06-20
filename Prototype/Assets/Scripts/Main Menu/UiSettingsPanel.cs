using UnityEngine;

namespace Main_Menu
{
    public class UiSettingsPanel : UIPanel
    {
        public GameObject logInOrSignUp;
        public GameObject logOut;

        private void OnEnable() {
            if (PlayFabLogin.Instance.IsLoggedInToAccount) {
                logInOrSignUp.SetActive(false);
                logOut.SetActive(true);
            }
            else if(PlayFabLogin.Instance.IsLoggedInAsGuest) {
                logInOrSignUp.SetActive(true);
                logOut.SetActive(false);
            }
            else {
                logInOrSignUp.SetActive(true);
                logOut.SetActive(false);
            }
        }
    }
}
