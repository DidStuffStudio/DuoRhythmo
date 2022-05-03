using System;
using System.Collections.Generic;
using ctsalidis;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using PlayFab.ProfilesModels;
using UnityEngine.UI;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;

// ref --> https://docs.microsoft.com/en-us/gaming/playfab/features/social/friends/quickstart

enum FriendIdType {
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

    // functionality variables
    private List<FriendInfo> _friends = null;
    public List<FriendInfo> Friends => _friends;

    private List<string> friendPlayfabIds = new List<string>();

    public List<EntityKey> friendsEntities = new List<EntityKey>();

    // ui variables
    /*
    [SerializeField] private Button addFriendsButton;
    [SerializeField] private InputField friendId;
    [SerializeField] private Text friendsListText;
    [SerializeField] private GameObject friendsPanel;
    */

    private void Awake() {
        if (_instance == null) _instance = this;
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
        }, DisplayPlayFabError);
    }

    private void AddFriend(FriendIdType idType, string friendId) {
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
    private void RemoveFriend(FriendInfo friendInfo) {
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
        print("Error receiving the players ids: " + error.GenerateErrorReport());
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
        // if (friendsEntities.Count > 0) PlayFabLogin.SelectedFriendsEntityKeys.Add(PlayFabLogin.FriendsEntityKeys[0]);
        Matchmaker.Instance.Initialize();
    }

    public void SelectFriend(string friend) {
        if (friendsEntities.Count > 0) {
            PlayFabLogin.SelectedFriendsEntityKeys.Add(PlayFabLogin.FriendsEntityKeys[0]);
            print("Selected to play with " + PlayFabLogin.SelectedFriendsEntityKeys[0].Id);
        }
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

    
    
    
    
    
    #region UI

    //  public void AddFriendAction() => AddFriend(FriendIdType.Username, friendId.text);

    #endregion
}