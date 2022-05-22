using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI accountNameText;
    [SerializeField] private Image avatar;

    private void OnEnable()
    {
        avatar.sprite = Resources.Load<Sprite>("Avatars/" + PlayFabLogin.Instance.UserAvatar);
        accountNameText.text = PlayFabLogin.Instance.Username;
    }
}
