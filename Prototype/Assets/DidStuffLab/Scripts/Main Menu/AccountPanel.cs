using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DidStuffLab.Scripts.Main_Menu
{
    public class AccountPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI accountNameText;
        [SerializeField] private Image avatar;

        private void OnEnable()
        {
            if (PlayFabLogin.Instance.IsLoggedInToAccount)
            {
                avatar.sprite = Resources.Load<Sprite>("Avatars/" + PlayFabLogin.Instance.UserAvatar);
                accountNameText.text = PlayFabLogin.Instance.Username;
            }
            else
            {
                avatar.sprite = Resources.Load<Sprite>("Avatars/" + "Avatar1");
                accountNameText.text = "Guest: " + PlayFabLogin.Instance.Username;
            }
        }
    }
}
