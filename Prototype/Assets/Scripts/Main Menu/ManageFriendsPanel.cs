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

    public enum friendStatus
    {
        requester,
        requestee,
        confirmed,
        revoked,
        removed,
        accepted,
        declined
    }
    [Header("Add friend Card")] [SerializeField]
    private GameObject addFriendCard;
    [SerializeField] private DidStuffTextField inputField;
    
    [Header("Friend Interface")]
    [SerializeField] private List<GameObject> confirmedInteraction = new List<GameObject>();
    [SerializeField] private List<GameObject> requestInteraction = new List<GameObject>();
    [SerializeField] private List<GameObject> pendingInteraction = new List<GameObject>();
    [SerializeField] private List<GameObject> revokedText = new List<GameObject>();
    [SerializeField] private List<GameObject> removedText = new List<GameObject>();
    [SerializeField] private List<GameObject> acceptedText = new List<GameObject>();
    [SerializeField] private List<GameObject> declinedText = new List<GameObject>();
    public Dictionary<string, friendStatus> _friendStatusDictionary = new Dictionary<string, friendStatus>();

    [Header("FriendsMenu/Spinner")]
    [SerializeField] private GameObject friendsMenuList;
    [SerializeField] private GameObject rotatingSpinner;
    private static bool isUpdated = false;

    public static void SubscribeToEvents(bool subscribe) {
        if (subscribe) {
            // subscribe to the event when we've received all friends details including avatars
            UiFriendsDetailsInstance.OnInitializedFriendsDetails += FriendsUpdated;
        }
        else {
            // unsubscribe to the event when we've received all friends details including avatars
            UiFriendsDetailsInstance.OnInitializedFriendsDetails -= FriendsUpdated;
        }
    }

    private static void FriendsUpdated() => isUpdated = true;

    
   

    protected override void OnEnable() {
        _numberOfCards = friendCards.Count;

        if (!isUpdated) {
            print("It's NOT updated, so show the rotating spinner");
            friendsMenuList.SetActive(false);
            rotatingSpinner.SetActive(true);
        }
        else {
            print("It's updated, so show the friends list");
            listToLoopUsernames = _allFriendUsernames;
            listToLoopAvatars = _allFriendAvatars;
            friendsMenuList.SetActive(true);
            rotatingSpinner.SetActive(false);
        }
        base.OnEnable();
        if (!PlayFabLogin.Instance.IsLoggedInAsGuest)
        {
            EnableAddFriend();
            
        }
    }

    protected override void Initialise()
    {
        base.Initialise();
        _friendStatusDictionary = new Dictionary<string, friendStatus>();
        for (int i = 0; i < FriendStatusMap.Count; i++)
        {
            if (FriendStatusMap[_allFriendUsernames[i]] == FriendStatus.Confirmed)
                _friendStatusDictionary.Add(_allFriendUsernames[i], friendStatus.confirmed);
            if (FriendStatusMap[_allFriendUsernames[i]] == FriendStatus.Requestee)
                _friendStatusDictionary.Add(_allFriendUsernames[i], friendStatus.requestee);
            if (FriendStatusMap[_allFriendUsernames[i]] == FriendStatus.Requester)
                _friendStatusDictionary.Add(_allFriendUsernames[i], friendStatus.requester);
        }
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
            switch (_friendStatusDictionary[_currentUsernames[i]])
            {
                case friendStatus.confirmed:
                    SwitchFriendStatusButtons(i, friendStatus.confirmed);
                    break;
                case friendStatus.requestee:
                    SwitchFriendStatusButtons(i, friendStatus.requestee);
                    break;
                case friendStatus.requester:
                    SwitchFriendStatusButtons(i, friendStatus.requester);
                    break;
                case friendStatus.removed:
                    SwitchFriendStatusButtons(i, friendStatus.removed);
                    break;
                case friendStatus.revoked:
                    SwitchFriendStatusButtons(i, friendStatus.revoked);
                    break;
                case friendStatus.accepted:
                    SwitchFriendStatusButtons(i, friendStatus.accepted);
                    break;
                case friendStatus.declined:
                    SwitchFriendStatusButtons(i, friendStatus.declined);
                    break;
            }
        }
    }

    private void SwitchFriendStatusButtons(int i, friendStatus status)
    {
        switch (status)
        {
            case friendStatus.confirmed:
                confirmedInteraction[i].SetActive(true);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(false);
                revokedText[i].SetActive(false);
                removedText[i].SetActive(false);
                acceptedText[i].SetActive(false);
                declinedText[i].SetActive(false);
                break;
            case friendStatus.requester:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(true);
                pendingInteraction[i].SetActive(false);
                revokedText[i].SetActive(false);
                removedText[i].SetActive(false);
                acceptedText[i].SetActive(false);
                declinedText[i].SetActive(false);
                break;
            case friendStatus.requestee:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(true);
                revokedText[i].SetActive(false);
                removedText[i].SetActive(false);
                acceptedText[i].SetActive(false);
                declinedText[i].SetActive(false);
                break;
            
            case friendStatus.revoked:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(false);
                revokedText[i].SetActive(true);
                removedText[i].SetActive(false);
                acceptedText[i].SetActive(false);
                declinedText[i].SetActive(false);
                break;
            case friendStatus.removed:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(false);
                revokedText[i].SetActive(false);
                removedText[i].SetActive(true);
                acceptedText[i].SetActive(false);
                declinedText[i].SetActive(false);
                break;
            case friendStatus.accepted:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(false);
                revokedText[i].SetActive(false);
                removedText[i].SetActive(false);
                acceptedText[i].SetActive(true);
                declinedText[i].SetActive(false);
                break;
            case friendStatus.declined:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(false);
                revokedText[i].SetActive(false);
                removedText[i].SetActive(false);
                acceptedText[i].SetActive(false);
                declinedText[i].SetActive(true);
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
        _friendStatusDictionary[_currentUsernames[index]] = friendStatus.accepted; 
        RemoveButtons(index, requestInteraction[index], acceptedText[index]);
    }

    public void DeclineFriendRequest(int index)
    {
        FriendsManager.Instance.DenyFriendRequest(_currentUsernames[index]);
        _friendStatusDictionary[_currentUsernames[index]] = friendStatus.declined; 
        RemoveButtons(index, requestInteraction[index], declinedText[index]);
    }

    public void RemoveFriend(int index)
    {
        FriendsManager.Instance.RemoveFriend(_currentUsernames[index]);
        _friendStatusDictionary[_currentUsernames[index]] = friendStatus.removed; 
        RemoveButtons(index, confirmedInteraction[index], removedText[index]);
    }

    public void RevokeFriendRequest(int index)
    {
        FriendsManager.Instance.RemoveFriend(_currentUsernames[index]);
        _friendStatusDictionary[_currentUsernames[index]] = friendStatus.revoked; 
        RemoveButtons(index, pendingInteraction[index], revokedText[index]);
    }

    public void AddFriend() {
        var stringToPass = inputField.text;
        FriendsManager.Instance.SendFriendRequest(stringToPass);
        inputField.text = "";
    }

    private void RemoveButtons(int index, GameObject deactivate, GameObject activate)
    {
        deactivate.SetActive(false);
        activate.SetActive(true);
    }

    protected override void OnDisable() {
        base.OnDisable();
        FriendsManager.Instance.GetListOfFriends(); // update the list of friends once again
    }

    private void OnApplicationQuit() {
        FriendsManager.OnReceivedFriendsDetails -= FriendsManagerOnOnReceivedFriendsDetails;
    }
}
