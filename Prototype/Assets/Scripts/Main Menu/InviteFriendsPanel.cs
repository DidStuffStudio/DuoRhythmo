using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using UnityEngine;
using UnityEngine.UI;

public class InviteFriendsPanel : UiFriendsManager {

    [SerializeField] private GameObject inviteFriendsMenu;
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
    
    /*
    protected override void OnEnable() {
        FriendsManager.OnReceivedFriendsDetails += FriendsManagerOnOnReceivedFriendsDetails;
        if (FriendsManager.Instance == null) return;
        if (FriendsManager.Instance.receivedAllFriendsDetails) FriendsManagerOnOnReceivedFriendsDetails();
        else {
            rotatingSpinner.SetActive(true);
            inviteFriendsMenu.SetActive(false);
        }

        base.OnEnable();
    }
    */

    protected override void OnEnable() {
        EnablePanel();
        base.OnEnable();
    }

    private void EnablePanel() {
        print("Hide rotating spinner in invite friends menu");
        _numberOfCards = friendCards.Count;


        if (isUpdated) {
            print("It's updated, so hide the rotating spinner in invite friends panel");
            rotatingSpinner.SetActive(false);
            inviteFriendsMenu.SetActive(true);
            friendListToLoop = confirmedFriends;
        }
        else {
            print("It's NOT updated, so show the rotating spinner in invite friends panel");
            rotatingSpinner.SetActive(true);
            inviteFriendsMenu.SetActive(false);
        }
    }

    protected override void ChangeGraphics() {
        base.ChangeGraphics();
        for (int i = 0; i < _displayedFriends; i++) {
            friendCards[i].ChangeFriend(friendListToLoop[_currentListIndex+i], true);
        }
    }

    /*
    protected override void OnDisable() {
        base.OnDisable();
        FriendsManager.OnReceivedFriendsDetails -= FriendsManagerOnOnReceivedFriendsDetails;
    }
    */
}