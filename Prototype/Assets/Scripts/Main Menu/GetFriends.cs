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
        [SerializeField] private List<UiFriendsManager> managersToUpdate;

        private TextMeshProUGUI _notificationText;
        [SerializeField] private Image friendRequestNotification;

        private void OnEnable() => PopulateFriendsMenu();

        private void PopulateFriendsMenu() {
            if (PlayFabLogin.Instance.IsLoggedInToAccount) {
                foreach (var manager in managersToUpdate) {
                    manager.AllFriendAvatars = UiFriendsDetailsInstance.Instance.AllFriendAvatars;
                    manager.AllFriendUsernames = UiFriendsDetailsInstance.Instance.AllFriendUsernames;
                    manager.ConfirmedFriendAvatars = UiFriendsDetailsInstance.Instance.ConfirmedFriendAvatars;
                    manager.ConfirmedFriendUsernames = UiFriendsDetailsInstance.Instance.ConfirmedFriendUsernames;
                    manager.FriendStatusMap = UiFriendsDetailsInstance.Instance.FriendStatusMap;
                    manager.AllFriendOnlineStatuses = UiFriendsDetailsInstance.Instance.AllFriendOnlineStatuses;
                }

               
            }

            _notificationText = friendRequestNotification.GetComponentInChildren<TextMeshProUGUI>();
            friendRequestNotification.gameObject.SetActive(false);
            UpdateFriendRequests();
        }

        private void UpdateFriendRequests() {
            if (UiFriendsDetailsInstance.Instance.FriendRequestCount < 1) friendRequestNotification.gameObject.SetActive(false);
            else {
                var num = UiFriendsDetailsInstance.Instance.FriendRequestCount.ToString(CultureInfo.CurrentCulture);
                if (UiFriendsDetailsInstance.Instance.FriendRequestCount > 9) num = "9+";
                _notificationText.text = num;
                friendRequestNotification.gameObject.SetActive(true);
            }
        }
    }
}