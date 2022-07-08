using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DidStuffLab.Scripts.Main_Menu {
    public class UiFriendsDetailsInstance : MonoBehaviour {
        public static UiFriendsDetailsInstance Instance { get; private set; }
        public Dictionary<Friend, FriendStatus> FriendStatusMap { get; private set; } =
            new Dictionary<Friend, FriendStatus>();

        public List<bool> AllFriendOnlineStatuses { get; private set; } = new List<bool>();
        public float FriendRequestCount { get; private set; } = 0;



        private List<Friend> _friends = new List<Friend>();

        public List<Friend> ConfirmedFriends { get; private set; } = new List<Friend>();

        public List<Friend> AllFriends { get; private set;  } = new List<Friend>();

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
            
            var requesteeFriends = new List<Friend>();
            var requesterFriends = new List<Friend>();
            var confirmedFriends = new List<Friend>();
            
            foreach (var friend in _friends) {
                switch (friend.FriendStatus) {
                    case FriendStatus.Requestee:
                        requesteeFriends.Add(friend);
                        break;

                    case FriendStatus.Requester:
                        requesterFriends.Add(friend);
                        break;

                    case FriendStatus.Confirmed:
                        confirmedFriends.Add(friend);
                        break;

                    case FriendStatus.Default: break;
                }
            }

            ConfirmedFriends = confirmedFriends;

            foreach (var friend in requesterFriends)
            {
                FriendRequestCount++;
                AllFriends.Add(friend);
                FriendStatusMap.Add(friend, FriendStatus.Requester);
            }

            foreach (var friend in confirmedFriends)
            {
                AllFriends.Add(friend);
                FriendStatusMap.Add(friend, FriendStatus.Confirmed);
            }

            foreach (var friend in requesteeFriends)
            {
                AllFriends.Add(friend);
                FriendStatusMap.Add(friend, FriendStatus.Requestee);
            }
            
            OnInitializedFriendsDetails?.Invoke();
        }

        private void ClearLists() {
            FriendRequestCount = 0;
            FriendStatusMap.Clear();
            _friends.Clear();
            ConfirmedFriends.Clear();
            AllFriends.Clear();
        }
    }
}