using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanelManager : MonoBehaviour
{
    [SerializeField] private Image friend1img, friend2img;
    [SerializeField] private TextMeshProUGUI friend1Username, friend2Username;

    [SerializeField] private GameObject rightArrow, leftArrow, secondFriendBtn;
    


    public void ChangeImages(Sprite img1, string username1, Sprite img2, string username2)
    {
        friend1img.sprite = img1;
        friend2img.sprite = img2;
        friend1Username.text = username1;
        friend2Username.text = username2;
    }

    private void ActivateSecondFriend(bool activate)
    {
        friend2img.gameObject.SetActive(false);
        friend2Username.gameObject.SetActive(false);
        secondFriendBtn.gameObject.SetActive(false);
    }
}
