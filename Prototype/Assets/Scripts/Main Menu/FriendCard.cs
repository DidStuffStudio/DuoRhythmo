using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ManageFriendsPanel;

public class FriendCard : MonoBehaviour
{
    private string _username = "";
    private string _avatarName = "";
    private FriendStatus _friendStatusUI;
    [SerializeField] private GameObject[] statusVariableElements = new GameObject[8];
    [SerializeField] private TextMeshProUGUI _usernameTMP;
    [SerializeField] private TextMeshProUGUI _inviteUsernameTMP;
    [SerializeField] private Image _avatarImg;
    [SerializeField] private Color _active = Color.green, _inactive = Color.red;
    [SerializeField] private Image statusDot;
    
    public void ChangeFriend(Friend friend, bool invite)
    {

        _usernameTMP.text = friend.Username;
        _inviteUsernameTMP.text = friend.Username;
        _avatarImg.sprite = Resources.Load<Sprite>("Avatars/" + friend.AvatarName);
        _friendStatusUI = friend.FriendStatus; //These enums must be the same order!!!

        if (!invite)
        {
            foreach (var e in statusVariableElements) e.SetActive(false);
            _inviteUsernameTMP.gameObject.SetActive(false);
            statusDot.gameObject.SetActive(false);
            _usernameTMP.gameObject.SetActive(true);
            switch (_friendStatusUI)
            {
                case FriendStatus.Confirmed:
                    statusVariableElements[0].SetActive(true);
                    break;
                case FriendStatus.Requestee:
                    statusVariableElements[1].SetActive(true);
                    break;
                case FriendStatus.Requester:
                    statusVariableElements[2].SetActive(true);
                    break;
            }
        }
        else
        {
            foreach (var e in statusVariableElements) e.SetActive(false);
            statusVariableElements[7].SetActive(true);
            _inviteUsernameTMP.gameObject.SetActive(true);
            _usernameTMP.gameObject.SetActive(false);
            statusDot.gameObject.SetActive(true);
            statusDot.color = friend.IsOnline ? _active : _inactive;
        }
    }

    public void Accept()
    {
        statusVariableElements[0].SetActive(false);
        statusVariableElements[5].SetActive(true);
        FriendsManager.Instance.AcceptFriendRequest(_username);
    }
    
    public void Decline()
    {
        statusVariableElements[0].SetActive(false);
        statusVariableElements[6].SetActive(true);
        FriendsManager.Instance.DenyFriendRequest(_username);
    }
    
    public void Remove()
    {
        statusVariableElements[0].SetActive(false);
        statusVariableElements[3].SetActive(true);
        FriendsManager.Instance.RemoveFriend(_username);
    }
    
    public void Revoke()
    {
        statusVariableElements[1].SetActive(false);
        statusVariableElements[4].SetActive(true);
        FriendsManager.Instance.RemoveFriend(_username);
    }

    public void InviteFriend()
    {
        Matchmaker.Instance.InviteFriendToMatchmaking(_username);
    }
}
