using System;
using System.Collections.Generic;
using ctsalidis;
using DidStuffLab;
using Managers;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.MultiplayerModels;
using PlayFab.ProfilesModels;
using UnityEngine.UI;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;
using ExecuteCloudScriptResult = PlayFab.ClientModels.ExecuteCloudScriptResult;

// ref --> https://docs.microsoft.com/en-us/gaming/playfab/features/social/friends/quickstart

namespace DidStuffLab {
    public enum FriendIdType {
        PlayFabId,
        Username,
        Email,
        DisplayName
    };

    public class FriendsManager : MonoBehaviour {
        private static FriendsManager _instance;

        public static FriendsManager Instance {
            get {
                if (_instance != null) return _instance;
                var friendsManagerGameObject = new GameObject();
                _instance = friendsManagerGameObject.AddComponent<FriendsManager>();
                friendsManagerGameObject.name = typeof(FriendsManager).ToString();
                return _instance;
            }
        }

        [SerializeField] private FriendPanelManager friendsPanel;
        // functionality variables
        private List<FriendInfo> _friends = null;
        public List<FriendInfo> Friends => _friends;

        private List<string> friendPlayfabIds = new List<string>();

        public List<EntityKey> friendsEntities = new List<EntityKey>();

        public List<Friend> FriendsDetails = new List<Friend>();
        public Dictionary<string, string> friendAvatarNames = new Dictionary<string, string>();

        public string AddFriendInput { get; set; }

        // ui variables
        /*
        [SerializeField] private Button addFriendsButton;
        [SerializeField] private InputField friendId;
        [SerializeField] private Text friendsListText;
        [SerializeField] private GameObject friendsPanel;
        */

        private void Awake() {
            if (_instance == null) _instance = this;
            DontDestroyOnLoad(this);
        }

        public void EnableFriendsManager() {
            if (!PlayFabMultiplayerAPI.IsEntityLoggedIn()) return;
            // friendsPanel.SetActive(true);
            GetFriends(); // call this on start because it's deactivated by default, and then activated once user logs in
            // LobbyManager.Instance.FindFriendsLobbies();
        }

        private void DisplayFriends(List<FriendInfo> friendsCache) {
            foreach (var friend in friendsCache) {
                print(friend.Username);
                print(friend.FriendPlayFabId);
                // friendsListText.text += "\n" + friend.Username + "\n";
                friendPlayfabIds.Add(friend.FriendPlayFabId);
                
                
            }
        }

        private void DisplayPlayFabError(PlayFabError error) {
            Debug.Log(error.GenerateErrorReport());
        }

        private void GetFriends() {
            PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest {
                IncludeSteamFriends = false,
                IncludeFacebookFriends = false,
                XboxToken = null
            }, result => {
                _friends = result.Friends;
                // TODO: get entityIds for friends (maybe use cloudscript/Azure functions for it?)
                DisplayFriends(_friends); // triggers your UI
                GetEntityIdsFromPlayfabIds();
                // GetFriendsDetails();
            }, DisplayPlayFabError);
        }

        public void AddFriend(FriendIdType idType, string friendId) {
            // TODO --> Look into making it a two way way process --> https://community.playfab.com/questions/46961/add-friend-using-friend-request-with-acceptdecline.html
            var request = new AddFriendRequest();
            switch (idType) {
                case FriendIdType.PlayFabId:
                    request.FriendPlayFabId = friendId;
                    break;
                case FriendIdType.Username:
                    request.FriendUsername = friendId;
                    break;
                case FriendIdType.Email:
                    request.FriendEmail = friendId;
                    break;
                case FriendIdType.DisplayName:
                    request.FriendTitleDisplayName = friendId;
                    break;
            }

            // Execute request and update friends when we are done
            PlayFabClientAPI.AddFriend(request, result => { Debug.Log("Friend added successfully!"); },
                DisplayPlayFabError);
        }

        // unlike AddFriend, RemoveFriend only takes a PlayFab ID
        // you can get this from the FriendInfo object under FriendPlayFabId
        public void RemoveFriend(FriendInfo friendInfo) {
            PlayFabClientAPI.RemoveFriend(new RemoveFriendRequest {
                FriendPlayFabId = friendInfo.FriendPlayFabId
            }, result => { _friends.Remove(friendInfo); }, DisplayPlayFabError);
        }

        // this REPLACES the list of tags on the server
        // for updates, make sure this includes the original tag list
        private void SetFriendTags(FriendInfo friend, List<string> newTags) {
            // update the tags with the edited list
            PlayFabClientAPI.SetFriendTags(new SetFriendTagsRequest {
                FriendPlayFabId = friend.FriendPlayFabId,
                Tags = newTags
            }, tagresult => {
                // Make sure to save new tags locally. That way you do not have to hard-update friendlist
                friend.Tags = newTags;
            }, DisplayPlayFabError);
        }

        // TODO: look into Cloudscript / Azure functions
        private void GetEntityIdsFromPlayfabIds() {
            // StartCloudHelloWorld();
            var request = new GetTitlePlayersFromMasterPlayerAccountIdsRequest {
                MasterPlayerAccountIds = friendPlayfabIds,
            };
            PlayFabProfilesAPI.GetTitlePlayersFromMasterPlayerAccountIds(request, OnReceivedPlayerIdsSuccess,
                OnReceivedPlayerIdsError);
            // PlayFabGroupsAPI.AcceptGroupApplication();
            // PlayFabGroupsAPI.ListGroupInvitations();
        }

        private void OnReceivedPlayerIdsError(PlayFabError error) {
            Debug.LogError("Error receiving the players ids: " + error.GenerateErrorReport());
        }

        private void OnReceivedPlayerIdsSuccess(GetTitlePlayersFromMasterPlayerAccountIdsResponse response) {
            print("Successfully received player ids");
            foreach (var player in response.TitlePlayerAccounts.Values) {
                print("Player entity key: " + player + " - Id: " + player.Id);
                friendsEntities.Add(new EntityKey() {
                    Id = player.Id,
                    Type = player.Type
                });
            }

            PlayFabLogin.FriendsEntityKeys = friendsEntities;
            if (_friends.Count == friendsEntities.Count) GetFriendAvatarNames();
            // if (friendsEntities.Count > 0) PlayFabLogin.SelectedFriendsEntityKeys.Add(PlayFabLogin.FriendsEntityKeys[0]);
            Matchmaker.Instance.Initialize();
        }

        private void GetFriendAvatarNames() {
            foreach (var f in friendsEntities) {
                PlayFabLogin.Instance.GetEntityAvatarName(f, true);
            }
        }

        public void SelectFriend(string friend) {
            if (friendsEntities.Count > 0) {
                PlayFabLogin.SelectedFriendsEntityKeys.Add(PlayFabLogin.FriendsEntityKeys[0]);
                print("Selected to play with " + PlayFabLogin.SelectedFriendsEntityKeys[0].Id);
            }
        }

        public void SendFriendRequest(string username) {
            PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
                FunctionName = "SendFriendRequest",
                FunctionParameter = new {FriendUsername = username},
                GeneratePlayStreamEvent = true,
            }, OnSendFriendRequestSuccess, OnSendFriendRequestError);
        }

        private void OnSendFriendRequestError(PlayFabError error) {
            print("Error sending friend request --> " + error.GenerateErrorReport());
            MainMenuManager.Instance.SpawnErrorToast(error.GenerateErrorReport(), 0.1f);
        }

        private void OnSendFriendRequestSuccess(PlayFab.CloudScriptModels.ExecuteCloudScriptResult response) {
            print("Sent friend request successfully " + response);
            MainMenuManager.Instance.SpawnSuccessToast("Friend request sent!", 0.1f);
            GetFriends(); // update  the friends
        }

        public void AcceptFriendRequest(string friendPlayfabId) {
            PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
                FunctionName = "AcceptFriendRequest",
                FunctionParameter = new {FriendPlayFabId = friendPlayfabId},
                GeneratePlayStreamEvent = true,
            }, OnAcceptFriendSuccess, OnAcceptFriendError);
        }

        private void OnAcceptFriendError(PlayFabError error) {
            print("Error accepting friend request --> " + error.GenerateErrorReport());
        }

        private void OnAcceptFriendSuccess(PlayFab.CloudScriptModels.ExecuteCloudScriptResult result) {
            print("Accepted friend request success --> " + result);
        }

        public void DenyFriendRequest(string friendPlayfabId) {
            PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
                FunctionName = "DenyFriendRequest",
                FunctionParameter = new {FriendPlayFabId = friendPlayfabId},
                GeneratePlayStreamEvent = true,
            }, OnDenyFriendSuccess, OnDenyFriendError);
        }

        private void OnDenyFriendError(PlayFabError error) {
            print("Error denying friend request --> " + error.GenerateErrorReport());
        }

        private void OnDenyFriendSuccess(PlayFab.CloudScriptModels.ExecuteCloudScriptResult result) {
            print("Successfully rejected friend request");
        }

        public List<Member> GetFriendMembers() {
            var members = new List<Member>();
            // add the creator of lobby as a member for lobby
            // members.Add(new Member() {MemberEntity = PlayFabLogin.EntityKey});
            // add this user's friends (TODO: Only add to lobby the friends that the user selects)
            foreach (var friend in PlayFabLogin.FriendsEntityKeys) {
                members.Add(new Member {
                    MemberEntity = friend,
                });
            }

            return members;
        }

        // TODO: Look more into groups of friends
        // https://docs.microsoft.com/en-us/gaming/playfab/features/social/groups/quickstart


        public void GetFriendsDetails() {
            for (var i = 0; i < _friends.Count; i++) {
                var f = _friends[i];
                var friend = new Friend {
                    Username = f.Username,
                    FriendInfo = f,
                    EntityKey = friendsEntities[i],
                };
                if (f.Tags != null && f.Tags.Count > 0) {
                    var friendTag = f.Tags[0];
                    if (friendTag == FriendStatus.Confirmed.ToString() || friendTag == "confirmed")
                        friend.FriendStatus = FriendStatus.Confirmed;
                    else if (friendTag == FriendStatus.Requester.ToString() || friendTag == "requester")
                        friend.FriendStatus = FriendStatus.Requester;
                    else if (friendTag == FriendStatus.Requestee.ToString() || friendTag == "requestee")
                        friend.FriendStatus = FriendStatus.Requestee;
                    else friend.FriendStatus = FriendStatus.Default;
                }

                // friend.AvatarName = friendAvatarNames[friend.EntityKey.Id];
                if(friendAvatarNames.ContainsKey(friend.EntityKey.Id)) friend.AvatarName = friendAvatarNames[friend.EntityKey.Id];
                // friend.AvatarName = PlayFabLogin.Instance.GetEntityAvatarName(friend.EntityKey);
                FriendsDetails.Add(friend);
                print(friend.Username);
            }
        }


        #region UI

        //  public void AddFriendAction() => AddFriend(FriendIdType.Username, friendId.text);

        #endregion

        public void AddAvatarToFriendDetails() {
            if(friendAvatarNames.Count == friendsEntities.Count) GetFriendsDetails();
            /*
            for (var i = 0; i < FriendsDetails.Count; i++) {
                var f = FriendsDetails[i];
                if(friendAvatarNames.ContainsKey(f.EntityKey.Id)) f.AvatarName = friendAvatarNames[f.EntityKey.Id];
            }
            */
        }
    }

    public class Friend {
        public string Username { get; set; }
        public string AvatarName { get; set; }
        public FriendInfo FriendInfo { get; set; }
        public EntityKey EntityKey { get; set; }
        public FriendStatus FriendStatus { get; set; }
    }

    public enum FriendStatus {
        Default,
        Confirmed,
        Requester,
        Requestee
    }
}