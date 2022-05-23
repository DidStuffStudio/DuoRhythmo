using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using DidStuffLab;
using Managers;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageFriendsPanel : UiFriendsManager
{

    [Header("Add friend Card")] [SerializeField]
    private GameObject addFriendCard;
    [SerializeField] private DidStuffTextField inputField;
    
    [Header("Friend Interface")]
    [SerializeField] private List<GameObject> confirmedInteraction = new List<GameObject>();
    [SerializeField] private List<GameObject> requestInteraction = new List<GameObject>();
    [SerializeField] private List<GameObject> pendingInteraction = new List<GameObject>();
    [SerializeField] private List<TextMeshProUGUI> confirmInteractionTexts = new List<TextMeshProUGUI>();
    private readonly string[] _confirmInteractionTexts = {"Accepted", "Declined", "Removed", "Revoked"};
    
    [Header("FriendsMenu/Spinner")]
    [SerializeField] private GameObject friendsMenuList;
    [SerializeField] private GameObject rotatingSpinner;
    private static bool isUpdated = false;

    public static void SubscribeToEvents(bool subscribe) {
        if (subscribe) {
            // subscribe to the event when we've received all friends details including avatars
            FriendsManager.OnReceivedFriendsDetails += FriendsUpdated;
        }
        else {
            // unsubscribe to the event when we've received all friends details including avatars
            FriendsManager.OnReceivedFriendsDetails -= FriendsUpdated;
        }
    }

    private static void FriendsUpdated() => isUpdated = true;

    /*
    private void Start() {
        // subscribe to the event when we've received all friends details including avatars
        FriendsManager.OnReceivedFriendsDetails += FriendsManagerOnOnReceivedFriendsDetails;
    }
    */

    protected override void OnEnable() {
        _numberOfCards = friendCards.Count;
        listToLoopUsernames = _allFriendUsernames;
        listToLoopAvatars = _allFriendAvatars;
        foreach (var t in confirmInteractionTexts)
        {
            t.gameObject.SetActive(false);
        }

        if (!isUpdated) {
            print("It's NOT updated, so show the rotating spinner");
            friendsMenuList.SetActive(false);
            rotatingSpinner.SetActive(true);
        }
        else {
            print("It's updated, so show the friends list");
            friendsMenuList.SetActive(true);
            rotatingSpinner.SetActive(false);
        }
        base.OnEnable();
        if (!PlayFabLogin.Instance.IsLoggedInAsGuest) EnableAddFriend();
    }

    private void FriendsManagerOnOnReceivedFriendsDetails() {
        print("Friends updated, so hide the rotating spinner");
        friendsMenuList.SetActive(true);
        rotatingSpinner.SetActive(false);
    }
    
    protected override void ChangeGraphics()
    {
        base.ChangeGraphics();
        for (int i = 0; i < _displayedFriends; i++)
        {
            if (FriendStatusMap[_currentUsernames[i]] == FriendStatus.Confirmed)
                SwitchFriendStatusButtons(i, FriendStatus.Confirmed);
            else if (FriendStatusMap[_currentUsernames[i]] == FriendStatus.Requestee)
                SwitchFriendStatusButtons(i, FriendStatus.Requestee);
            if (FriendStatusMap[_currentUsernames[i]] == FriendStatus.Requester)
                SwitchFriendStatusButtons(i, FriendStatus.Requester);
        }
    }

    private void SwitchFriendStatusButtons(int i, FriendStatus status)
    {
        switch (status)
        {
            case FriendStatus.Confirmed:
                confirmedInteraction[i].SetActive(true);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(false);
                break;
            case FriendStatus.Requester:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(true);
                pendingInteraction[i].SetActive(false);
                break;
            case FriendStatus.Requestee:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(true);
                break;
        }
    }


    protected override void DisableAllInteraction()
    {
        addFriendCard.SetActive(false);
        base.DisableAllInteraction();
    }

    private void EnableAddFriend()
    {
        addFriendCard.SetActive(true);
    }


    public void AcceptFriendRequest(int index)
    {
        FriendsManager.Instance.AcceptFriendRequest(_currentUsernames[index]);
        RemoveButtons(index, requestInteraction[index], _confirmInteractionTexts[0]);
    }

    public void DeclineFriendRequest(int index)
    {
        FriendsManager.Instance.DenyFriendRequest(_currentUsernames[index]);
        RemoveButtons(index, requestInteraction[index], _confirmInteractionTexts[1]);
    }

    public void RemoveFriend(int index)
    {
        FriendsManager.Instance.RemoveFriend(_currentUsernames[index]);
        RemoveButtons(index, confirmedInteraction[index], _confirmInteractionTexts[2]);
    }

    public void RevokeFriendRequest(int index)
    {
        FriendsManager.Instance.RemoveFriend(_currentUsernames[index]);
        RemoveButtons(index, pendingInteraction[index], _confirmInteractionTexts[3]);
    }

    public void AddFriend() {
        var stringToPass = inputField.text;
        FriendsManager.Instance.SendFriendRequest(stringToPass);
        inputField.text = "";
    }
    
    public void RemoveButtons(int index, GameObject buttons,string text)
    {
        buttons.SetActive(false);
        confirmInteractionTexts[index].text = text;
        confirmInteractionTexts[index].gameObject.SetActive(true);
    }

    protected override void OnDisable() {
        base.OnDisable();
        FriendsManager.Instance.GetListOfFriends(); // update the list of friends once again
    }

    private void OnApplicationQuit() {
        FriendsManager.OnReceivedFriendsDetails -= FriendsManagerOnOnReceivedFriendsDetails;
    }
}
