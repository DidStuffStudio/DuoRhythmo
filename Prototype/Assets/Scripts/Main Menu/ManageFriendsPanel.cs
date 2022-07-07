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
    public Dictionary<string, FriendStatus> _friendStatusDictionary = new Dictionary<string, FriendStatus>();

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
        _friendStatusDictionary = new Dictionary<string, FriendStatus>();
        for (int i = 0; i < FriendStatusMap.Count; i++)
        {
            if (FriendStatusMap[_allFriendUsernames[i]] == FriendStatus.Confirmed)
                _friendStatusDictionary.Add(_allFriendUsernames[i], FriendStatus.Confirmed);
            if (FriendStatusMap[_allFriendUsernames[i]] == FriendStatus.Requestee)
                _friendStatusDictionary.Add(_allFriendUsernames[i], FriendStatus.Requestee);
            if (FriendStatusMap[_allFriendUsernames[i]] == FriendStatus.Requester)
                _friendStatusDictionary.Add(_allFriendUsernames[i], FriendStatus.Requester);
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
            //TODO --> Update the friends cards from a list of the class friend
            //friendCards[i].ChangeFriend(friend, false);
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




    public void AddFriend() {
        var stringToPass = inputField.text;
        FriendsManager.Instance.SendFriendRequest(stringToPass);
        inputField.text = "";
    }
    

    protected override void OnDisable() {
        base.OnDisable();
        FriendsManager.Instance.GetListOfFriends(); // update the list of friends once again
    }

    private void OnApplicationQuit() {
        FriendsManager.OnReceivedFriendsDetails -= FriendsManagerOnOnReceivedFriendsDetails;
    }
}
