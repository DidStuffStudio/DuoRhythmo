using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DidStuffLab;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace DidStuffLab {
    public class GetFriends : MonoBehaviour {
        private static List<Friend> _friends = new List<Friend>();
        private static List<string> _allFriendUsernames = new List<string>();
        private static List<string> _confirmedFriendUsernames = new List<string>();
        private static List<string> _allFriendAvatars = new List<string>();
        private static List<string> _confirmedFriendAvatars = new List<string>();
        private static List<bool> _confirmedFriendsOnlineStatus = new List<bool>();
        private static Dictionary<string, FriendStatus> _friendStatusMap = new Dictionary<string, FriendStatus>();
        [SerializeField] private List<UiFriendsManager> managersToUpdate;

        private TextMeshProUGUI _notificationText;
        [SerializeField] private Image friendRequestNotification;
        private static float _friendRequestCount = 0;

        public static void SubscribeToEvents(bool subscribe) {
            if (subscribe) {
                // subscribe to the event when we've received all friends details including avatars
                FriendsManager.OnReceivedFriendsDetails += GetFriendDetails;
            }
            else {
                // unsubscribe to the event when we've received all friends details including avatars
                FriendsManager.OnReceivedFriendsDetails -= GetFriendDetails;
            }
        }
        
        private void OnEnable() {
            // if(FriendsManager.Instance.receivedAllFriendsDetails) FriendsManagerOnOnReceivedFriendsDetails();
            PopulateFriendsMenu();
        }
        

        private void PopulateFriendsMenu() {
            if (PlayFabLogin.Instance.IsLoggedInToAccount) {
                // GetFriendDetails();
                foreach (var manager in managersToUpdate) {
                    manager.AllFriendAvatars = _allFriendAvatars;
                    manager.AllFriendUsernames = _allFriendUsernames;
                    manager.ConfirmedFriendAvatars = _confirmedFriendAvatars;
                    manager.ConfirmedFriendUsernames = _confirmedFriendUsernames;
                    manager.FriendStatusMap = _friendStatusMap;
                    manager.AllFriendOnlineStatuses = _confirmedFriendsOnlineStatus;
                }
            }

            _notificationText = friendRequestNotification.GetComponentInChildren<TextMeshProUGUI>();
            friendRequestNotification.gameObject.SetActive(false);
            UpdateFriendRequests();
        }

        private static void GetFriendDetails() {
            print("Update friends menu list");
            ClearLists();
            _friends = FriendsManager.Instance.FriendsDetails.ToList();

            var requesterUsernames = new List<string>(); //Friend request
            var requesteeUsernames = new List<string>(); //Pending
            var confirmedUsernames = new List<string>(); //Friend

            var requesterAvatars = new List<string>(); //Friend request
            var requesteeAvatars = new List<string>(); //Pending
            var confirmedAvatars = new List<string>(); //Friend
            var confirmedOnlineStatus = new List<bool>(); //Friend

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

                    case FriendStatus.Default:

                        break;
                }
            }

            _confirmedFriendUsernames = confirmedUsernames;
            _confirmedFriendAvatars = confirmedAvatars;
            _confirmedFriendsOnlineStatus = confirmedOnlineStatus;

            for (int i = 0; i < requesterUsernames.Count; i++) {
                _friendRequestCount++;
                _allFriendUsernames.Add(requesterUsernames[i]);
                _allFriendAvatars.Add(requesterAvatars[i]);
                _friendStatusMap.Add(requesterUsernames[i], FriendStatus.Requester);
            }

            for (int i = 0; i < confirmedUsernames.Count; i++) {
                var index = requesterUsernames.Count + i;
                _allFriendUsernames.Add(confirmedUsernames[i]);
                _allFriendAvatars.Add(confirmedAvatars[i]);
                _friendStatusMap.Add(confirmedUsernames[i], FriendStatus.Confirmed);
            }

            for (int i = 0; i < requesteeUsernames.Count; i++) {
                var index = requesterUsernames.Count + confirmedUsernames.Count + i;
                _allFriendUsernames.Add(requesteeUsernames[i]);
                _allFriendAvatars.Add(requesteeAvatars[i]);
                _friendStatusMap.Add(requesteeUsernames[i], FriendStatus.Requestee);
            }
        }

        private void UpdateFriendRequests() {
            if (_friendRequestCount < 1) friendRequestNotification.gameObject.SetActive(false);
            else {
                var num = _friendRequestCount.ToString(CultureInfo.CurrentCulture);
                if (_friendRequestCount > 9) num = "9+";
                _notificationText.text = num;
                friendRequestNotification.gameObject.SetActive(true);
            }
        }

        private static void ClearLists() {
            _friendRequestCount = 0;
            _allFriendUsernames.Clear();
            _allFriendAvatars.Clear();
            _friendStatusMap.Clear();
        }

        // private void OnApplicationQuit() => FriendsManager.OnReceivedFriendsDetails -= FriendsManagerOnOnReceivedFriendsDetails;
    }
}