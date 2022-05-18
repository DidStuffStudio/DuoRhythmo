using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteFriendsPanel : UiFriendsManager
{
    protected override void OnEnable()
    {
        _numberOfCards = friendCards.Count;
        listToLoopUsernames = _confirmedFriendUsernames;
        listToLoopAvatars = _confirmedFriendAvatars;
        _currentUsernames = new string[_numberOfCards];
        _currentAvatars = new string[_numberOfCards];
        
        base.OnEnable();
    }
}
