using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DidStuffLab {
    public class UiFriendsDetailsInstance : MonoBehaviour {
        public static UiFriendsDetailsInstance Instance { get; private set; }

        // friends details variables
        public List<string> AllFriendUsernames { get; private set; } = new List<string>();
        public List<string> ConfirmedFriendUsernames { get; private set; } = new List<string>();
        public List<string> AllFriendAvatars { get; private set; } = new List<string>();
        public List<string> ConfirmedFriendAvatars { get; private set; } = new List<string>();

        public Dictionary<string, FriendStatus> FriendStatusMap { get; private set; } =
            new Dictionary<string, FriendStatus>();

        public List<bool> AllFriendOnlineStatuses { get; private set; } = new List<bool>();
        public float FriendRequestCount { get; private set; } = 0;

        private List<Friend> _friends = new List<Friend>();
        
        public delegate void InitializedFriendsDetails();
        public static event InitializedFriendsDetails OnInitializedFriendsDetails;

        private void Awake() {
            if (Instance == null) Instance = this;
        }

        private void OnEnable() => FriendsManager.OnReceivedFriendsDetails += SetFriendsDetails;
        private void OnDisable() => FriendsManager.OnReceivedFriendsDetails -= SetFriendsDetails;

        private void SetFriendsDetails() {
            print("friends updated, so update all friends details");
            ClearLists();
            _friends = FriendsManager.Instance.FriendsDetails.ToList();

            var requesterUsernames = new List<string>(); // Friend request
            var requesteeUsernames = new List<string>(); // Pending
            var confirmedUsernames = new List<string>(); // Friend

            var requesterAvatars = new List<string>(); // Friend request
            var requesteeAvatars = new List<string>(); // Pending
            var confirmedAvatars = new List<string>(); // Friend
            var confirmedOnlineStatus = new List<bool>(); // Friend

            foreach (var friend in _friends) {
                switch (friend.FriendStatus) {
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

                    case FriendStatus.Default: break;
                }
            }

            ConfirmedFriendUsernames = confirmedUsernames;
            ConfirmedFriendAvatars = confirmedAvatars;
            AllFriendOnlineStatuses = confirmedOnlineStatus;

            for (var i = 0; i < requesterUsernames.Count; i++) {
                FriendRequestCount++;
                AllFriendUsernames.Add(requesterUsernames[i]);
                AllFriendAvatars.Add(requesterAvatars[i]);
                FriendStatusMap.Add(requesterUsernames[i], FriendStatus.Requester);
            }

            for (var i = 0; i < confirmedUsernames.Count; i++) {
                AllFriendUsernames.Add(confirmedUsernames[i]);
                AllFriendAvatars.Add(confirmedAvatars[i]);
                FriendStatusMap.Add(confirmedUsernames[i], FriendStatus.Confirmed);
            }

            for (var i = 0; i < requesteeUsernames.Count; i++) {
                AllFriendUsernames.Add(requesteeUsernames[i]);
                AllFriendAvatars.Add(requesteeAvatars[i]);
                FriendStatusMap.Add(requesteeUsernames[i], FriendStatus.Requestee);
            }
            
            OnInitializedFriendsDetails?.Invoke();
        }

        private void ClearLists() {
            FriendRequestCount = 0;
            AllFriendUsernames.Clear();
            AllFriendAvatars.Clear();
            ConfirmedFriendAvatars.Clear();
            FriendStatusMap.Clear();
            AllFriendOnlineStatuses.Clear();
            _friends.Clear();
        }
    }
}