using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using UnityEngine;
using UnityEngine.UI;

public class InviteFriendsPanel : UiFriendsManager {
    [SerializeField] private List<Image> _onlineStatusImages = new List<Image>();
    [SerializeField] private Color _active = Color.green, _inactive = Color.red;
    [SerializeField] private GameObject inviteFriendsMenu;
    [SerializeField] private GameObject rotatingSpinner;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        FriendsManager.OnReceivedFriendsDetails += FriendsManagerOnOnReceivedFriendsDetails;
        if(FriendsManager.Instance == null) return;
        if(FriendsManager.Instance.receivedAllFriendsDetails) FriendsManagerOnOnReceivedFriendsDetails();
        else {
            rotatingSpinner.SetActive(true);
            inviteFriendsMenu.SetActive(false);
        }
    }

    private void FriendsManagerOnOnReceivedFriendsDetails() {
        print("Hide rotating spinner in invite friends menu");
        rotatingSpinner.SetActive(false);
        inviteFriendsMenu.SetActive(true);
        
        _numberOfCards = friendCards.Count;
        listToLoopUsernames = _confirmedFriendUsernames;
        listToLoopAvatars = _confirmedFriendAvatars;
        _currentUsernames = new string[_numberOfCards];
        _currentAvatars = new string[_numberOfCards];
    }

    protected override void ChangeGraphics() {
        base.ChangeGraphics();
        for (int i = 0; i < _displayedFriends; i++) {
            _currentOnlineStatuses[i] = AllFriendOnlineStatuses[_currentListIndex + i];
            _onlineStatusImages[i].color = _currentOnlineStatuses[i] ? _active : _inactive;
        }
    }

    public void InviteFriendToMatch(int index) => Matchmaker.Instance.InviteFriendToMatchmaking(_currentUsernames[index]);

    protected override void OnDisable() {
        base.OnDisable();
        FriendsManager.OnReceivedFriendsDetails -= FriendsManagerOnOnReceivedFriendsDetails;
    }
}
