using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.MultiplayerModels;
using PlayFab.ProfilesModels;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;

// ref --> https://docs.microsoft.com/en-us/gaming/playfab/features/social/friends/quickstart

namespace DidStuffLab {
    public class FriendsManager : MonoBehaviour {
        public static FriendsManager Instance { get; private set; }

        private Dictionary<string, Friend> _friendsDictionary = new Dictionary<string, Friend>();
        public IEnumerable<Friend> FriendsDetails => updatingFriends ? _auxTempFriendsDetails : _friendsDictionary.Values.ToList();
        private List<Friend> _auxTempFriendsDetails = new List<Friend>(); // in case friends dictionary has been cleared, use this as the aux
        private int _friendAvatarsReceivedCounter = 0;
        public bool receivedAllFriendsDetails;
        private bool updatingFriends;
        private bool _changeInFriends = true;

        public delegate void ReceivedFriendsDetails();
        public static event ReceivedFriendsDetails OnReceivedFriendsDetails;
        
        public string IdFromUsername(string username) {
            foreach (var f in _friendsDictionary.Where(f => f.Value.Username == username)) {
                return f.Key;
            }

            Debug.LogError("Error getting the specified username from the friends dictionary");
            return string.Empty;
        }

        private void Awake() {
            if (Instance == null) Instance = this;
            ManageFriendsPanel.SubscribeToEvents(true);
            InviteFriendsPanel.SubscribeToEvents(true);
        }

        public void EnableFriendsManager() {
            if (!PlayFabMultiplayerAPI.IsEntityLoggedIn()) return;
            // StartCoroutine(WaitToRequestFriends());
            GetListOfFriends(); // call this on start because it's deactivated by default, and then activated once user logs in
        }

        private IEnumerator WaitForTimeoutFriendsRequest() {
            // if after 10 seconds we still haven't received all the friends details,
            // just populate it with the friends that we got and default values
            yield return new WaitForSeconds(10);
            if(!receivedAllFriendsDetails) OnReceivedFriendsDetails?.Invoke();
        }

        private void DisplayPlayFabError(PlayFabError error) {
            Debug.Log(error.GenerateErrorReport());
        }

        public void GetListOfFriends() {
            // if (!_finishedWaitingToRequestFriends) return;
            if (!_changeInFriends) return;
            _changeInFriends = false;
            updatingFriends = true;
            StartCoroutine(WaitForTimeoutFriendsRequest());
            receivedAllFriendsDetails = false;
            _friendsDictionary.Clear();
            PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest {
                IncludeSteamFriends = false,
                IncludeFacebookFriends = false,
                XboxToken = null,
                ProfileConstraints = new PlayerProfileViewConstraints {
                    ShowLastLogin = true,
                }
            }, result => { InitializeFriends(result.Friends); }, DisplayPlayFabError);
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

        private void InitializeFriends(List<FriendInfo> friends) {
            if (friends.Count <= 0) return;
            foreach (var f in friends) {
                _friendsDictionary.Add(
                    f.FriendPlayFabId,
                    new Friend {
                        MasterPlayfabId = f.FriendPlayFabId,
                        Username = f.Username,
                        FriendInfo = f,
                        AvatarName = "Avatar1",
                        LastLogin = f.Profile.LastLogin,
                        FriendStatus = GetFriendTag(f)
                    });
                _friendsDictionary[f.FriendPlayFabId].IsOnline = GetFriendOnlineStatus(_friendsDictionary[f.FriendPlayFabId]);
            }
            var friendPlayfabIds = friends.Select(f => f.FriendPlayFabId).ToList();
            var request = new GetTitlePlayersFromMasterPlayerAccountIdsRequest {
                MasterPlayerAccountIds = friendPlayfabIds,
            };
            PlayFabProfilesAPI.GetTitlePlayersFromMasterPlayerAccountIds(request, OnReceivedPlayerIdsSuccess,
                OnReceivedPlayerIdsError);
        }

        private FriendStatus GetFriendTag(FriendInfo f) {
            if (f.Tags != null && f.Tags.Count > 0) {
                var friendTag = f.Tags[0];
                if (friendTag == FriendStatus.Confirmed.ToString() || friendTag == "confirmed")
                    return FriendStatus.Confirmed;
                if (friendTag == FriendStatus.Requester.ToString() || friendTag == "requester")
                    return FriendStatus.Requester;
                if (friendTag == FriendStatus.Requestee.ToString() || friendTag == "requestee")
                    return FriendStatus.Requestee;
            }

            return FriendStatus.Default;
        }

        private bool GetFriendOnlineStatus(Friend friend) {
            // if (friend.FriendStatus != FriendStatus.Confirmed || friend.LastLogin == null) return false;
            var breakDuration = TimeSpan.FromMinutes(10.0f); // threshold for how long ago they were online
            var isOnline = (DateTime.UtcNow - friend.LastLogin) < breakDuration;
            return isOnline;
        }

        private void OnReceivedPlayerIdsError(PlayFabError error) {
            Debug.LogError("Error receiving the players ids: " + error.GenerateErrorReport());
        }

        private void OnReceivedPlayerIdsSuccess(GetTitlePlayersFromMasterPlayerAccountIdsResponse response) {
            print("Successfully received player ids");
            foreach (var player in response.TitlePlayerAccounts) {
                var entityKey = new EntityKey {
                    Id = player.Value.Id,
                    Type = player.Value.Type
                };
                _friendsDictionary[player.Key].TitleEntityKey = entityKey;
                print("Player entity key: " + player.Key + " - Id: " + player.Value.Id);
                PlayFabLogin.FriendsEntityKeys.Add(entityKey);
            }

            GetFriendAvatarNames();
            Matchmaker.Instance.Initialize();
        }

        private void GetFriendAvatarNames() {
            foreach (var f in _friendsDictionary.Values) {
                PlayFabLogin.Instance.GetEntityAvatarName(f.TitleEntityKey, true);
            }
        }

        public void SendFriendRequest(string username) {
            // first check if the user is already a friend - if so, tell the user - otherwise, send the friend request
            if (_friendsDictionary.Count > 0) {
                var id = IdFromUsername(username);
                if (!string.IsNullOrEmpty(id)) {
                    var message = "";
                    switch (_friendsDictionary[id].FriendStatus) {
                        case FriendStatus.Default:
                        case FriendStatus.Confirmed: message = username + " is already a friend";
                            break;
                        case FriendStatus.Requestee: message = username + " friend request already sent";
                            break;
                        case FriendStatus.Requester: AcceptFriendRequest(username); // if invitation was pending from this player automatically accept
                            break;
                    }

                    print(message);
                    MainMenuManager.Instance.SpawnErrorToast(message, 0.1f);
                    return;
                }
            }
            PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
                FunctionName = "SendFriendRequest",
                FunctionParameter = new {FriendUsername = username},
                GeneratePlayStreamEvent = true,
            }, OnSendFriendRequestSuccess, OnSendFriendRequestError);
        }

        public void RemoveFriend(string username) => DenyFriendRequest(username); // automatically delete friendship via Cloudscript

        private void OnSendFriendRequestError(PlayFabError error) {
            print("Error sending friend request --> " + error.GenerateErrorReport());
            MainMenuManager.Instance.SpawnErrorToast(error.GenerateErrorReport(), 0.1f);
        }

        private void OnSendFriendRequestSuccess(PlayFab.CloudScriptModels.ExecuteCloudScriptResult response) {
            print("Sent friend request successfully " + response.FunctionResult);
            if(response.FunctionResult != null) print(response.FunctionResult);
            if (response.Error != null) {
                MainMenuManager.Instance.SpawnErrorToast("username not found", 0.1f);
                Debug.LogError("Error send friend request" + response.Error.Error);
            }
            else {
                Debug.Log("Seems like sending the friend request happened successfully");
                MainMenuManager.Instance.SpawnSuccessToast("Friend request sent!", 0.1f);
                _changeInFriends = true;
            }
        }

        public void AcceptFriendRequest(string username) {
            var friendPlayfabId = IdFromUsername(username);
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
            _changeInFriends = true;
        }

        public void DenyFriendRequest(string username) {
            var friendPlayfabId = IdFromUsername(username);
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
            _changeInFriends = true;
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
        
        public void AddAvatarToFriendDetails(EntityKey entityKey, string playerAvatarName) {
            foreach (var f in _friendsDictionary.Where(f => f.Value.TitleEntityKey.Id == entityKey.Id)) {
                f.Value.AvatarName = playerAvatarName;
                _friendAvatarsReceivedCounter++;
            }

            if (_friendAvatarsReceivedCounter == _friendsDictionary.Count) {
                print("Invoking the received all friends details event");
                _auxTempFriendsDetails = _friendsDictionary.Values.ToList();
                _friendAvatarsReceivedCounter = 0;
                receivedAllFriendsDetails = true;
                updatingFriends = false;
                OnReceivedFriendsDetails?.Invoke();
            }
        }

        public void ClearAllFriendsDetails() {
            _friendsDictionary.Clear();
            receivedAllFriendsDetails = false;
            _changeInFriends = true;
            updatingFriends = true;
            _auxTempFriendsDetails.Clear();
            OnReceivedFriendsDetails?.Invoke(); // now that it's empty, invoke it again to clear friends everywhere
        }

        private void OnApplicationQuit() {
            ManageFriendsPanel.SubscribeToEvents(false);
            InviteFriendsPanel.SubscribeToEvents(false);
        }
    }

    public class Friend {
        public string MasterPlayfabId { get; set; }
        public string Username { get; set; }
        public string AvatarName { get; set; }
        public FriendInfo FriendInfo { get; set; }
        public EntityKey TitleEntityKey { get; set; }
        public FriendStatus FriendStatus { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsOnline { get; set; }
    }

    public enum FriendStatus {
        Default,
        Confirmed,
        Requester,
        Requestee
    }
}