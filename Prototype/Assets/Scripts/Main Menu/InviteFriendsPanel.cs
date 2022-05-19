using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using UnityEngine;
using UnityEngine.UI;

public class InviteFriendsPanel : UiFriendsManager {
    [SerializeField] private List<Image> _onlineStatusImages = new List<Image>();
    [SerializeField] private Color _active = Color.green, _inactive = Color.red;
    
    protected override void OnEnable()
    {
        _numberOfCards = friendCards.Count;
        listToLoopUsernames = _confirmedFriendUsernames;
        listToLoopAvatars = _confirmedFriendAvatars;
        _currentUsernames = new string[_numberOfCards];
        _currentAvatars = new string[_numberOfCards];

        base.OnEnable();
    }

    protected override void ChangeGraphics() {
        base.ChangeGraphics();
        for (int i = 0; i < _displayedFriends; i++) {
            _currentOnlineStatuses[i] = AllFriendOnlineStatuses[_currentListIndex + i];
            _onlineStatusImages[i].color = _currentOnlineStatuses[i] ? _active : _inactive;
        }
    }

    public void InviteFriendToMatch(int index) => Matchmaker.Instance.InviteFriendToMatchmaking(_currentUsernames[index]);
}
