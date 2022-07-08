using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DidStuffLab.Scripts.Main_Menu {
    public class GetFriends : MonoBehaviour {
        [SerializeField] private List<UiFriendsManager> managersToUpdate;

        private TextMeshProUGUI _notificationText;
        [SerializeField] private Image friendRequestNotification;

        private void OnEnable() => PopulateFriendsMenu();

        private void PopulateFriendsMenu() {
            if (PlayFabLogin.Instance.IsLoggedInToAccount) {
                foreach (var manager in managersToUpdate)
                {
                    manager.AllFriends = UiFriendsDetailsInstance.Instance.AllFriends;
                    manager.ConfirmedFriends = UiFriendsDetailsInstance.Instance.ConfirmedFriends;
                    manager.FriendStatusMap = UiFriendsDetailsInstance.Instance.FriendStatusMap;
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