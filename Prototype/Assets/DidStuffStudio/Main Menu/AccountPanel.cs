using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
