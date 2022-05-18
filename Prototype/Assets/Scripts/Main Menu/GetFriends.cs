using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DidStuffLab;
using Managers;
using UnityEngine;

public class GetFriends : MonoBehaviour
{
    
    private List<Friend> _friends = new List<Friend>();
    private List<string> _allFriendUsernames = new List<string>();
    private List<string> _confirmedFriendUsernames = new List<string>();
    private List<string> _allFriendAvatars = new List<string>();
    private List<string> _confirmedFriendAvatars = new List<string>();
    private List<bool> _confirmedFriendsOnlineStatus = new List<bool>();
    private Dictionary<string, FriendStatus> _friendStatusMap = new Dictionary<string, FriendStatus>();
    [SerializeField] private List<UiFriendsManager> managersToUpdate;
    private bool _initialised;
    private void OnEnable()
    {
        if (!_initialised) return;
        if (MainMenuManager.Instance.LoggedIn)
        {
            GetFriendDetails();
            foreach (var manager in managersToUpdate)
            {
                manager.AllFriendAvatars = _allFriendAvatars;
                manager.AllFriendUsernames = _allFriendUsernames;
                manager.ConfirmedFriendAvatars = _confirmedFriendAvatars;
                manager.ConfirmedFriendUsernames = _confirmedFriendUsernames;
                manager.FriendStatusMap = _friendStatusMap;
            }
        }
        
        
        
    }
    
       private void GetFriendDetails()
    {

            ClearLists();    
        _friends = FriendsManager.Instance.FriendsDetails.ToList();
        
        var requesterUsernames = new List<string>(); //Friend request
        var requesteeUsernames = new List<string>(); //Pending
        var confirmedUsernames = new List<string>(); //Friend

        var requesterAvatars = new List<string>(); //Friend request
        var requesteeAvatars = new List<string>(); //Pending
        var confirmedAvatars = new List<string>(); //Friend
        var confirmedOnlineStatus = new List<bool>(); //Friend
        
        foreach (var friend in _friends)
        {
            switch (friend.FriendStatus)
            {
                case FriendStatus.Requestee:
                    requesteeUsernames.Add(friend.Username);
                    requesteeAvatars.Add(friend.AvatarName);
                    break;
                
                case FriendStatus.Requester:
                    requesterUsernames.Add(friend.Username);
                    requesterAvatars.Add(friend.AvatarName);
                    break;
                
                case FriendStatus.Confirmed:
                    confirmedUsernames.Add(friend.Username);
                    confirmedAvatars.Add(friend.AvatarName);
                    confirmedOnlineStatus.Add(friend.IsOnline);
                    break;
                
                case FriendStatus.Default:
                    
                    break;
            }
        }
        
        _confirmedFriendUsernames = confirmedUsernames;
        _confirmedFriendAvatars = confirmedAvatars;
        _confirmedFriendsOnlineStatus = confirmedOnlineStatus;

        for (int i = 0; i < requesterUsernames.Count; i++)
        {
            _allFriendUsernames.Add(requesterUsernames[i]);
            _allFriendAvatars.Add( requesterAvatars[i]);
            _friendStatusMap.Add(requesterUsernames[i], FriendStatus.Requester);
        }

        for (int i = 0; i < confirmedUsernames.Count; i++)
        {
            var index = requesterUsernames.Count + i;
            _allFriendUsernames.Add(confirmedUsernames[i]);
            _allFriendAvatars.Add( confirmedAvatars[i]);
            _friendStatusMap.Add(confirmedUsernames[i], FriendStatus.Confirmed);
        }
        
        for (int i = 0; i < requesteeUsernames.Count; i++)
        {
            var index = requesterUsernames.Count + confirmedUsernames.Count + i;
            _allFriendUsernames.Add( requesteeUsernames[i]);
            _allFriendAvatars.Add( requesteeAvatars[i]);
            _friendStatusMap.Add(requesteeUsernames[i], FriendStatus.Requestee);
        }

       
    }
       
       private void ClearLists()
       {
            _allFriendUsernames.Clear();
            _allFriendAvatars.Clear();
            _friendStatusMap.Clear();
       }

       private void OnDisable() => _initialised = true;
}
