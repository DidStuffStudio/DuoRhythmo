using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using PlayFab;
using PlayFab.MultiplayerModels;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour {
    // TODO: Finish implementing all these lobby methods accordingly - possibly use them with matchmaking if it makes sense

    // TODO: Look more into creating/joining lobbies                                                              
    // ref --> https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/lobby/unity-multiplayer-api-reference/

    public static LobbyManager Instance { get; private set; }

    [SerializeField] private string connectionString;
    [SerializeField] private string _lobbyId;

    [SerializeField] private Text friendsLobbiesListText;

    private void Awake() => Instance = this;

    public void CreateLobby() {
        // TODO: Note --> When I create a lobby and add other members, those members will automatically be added to the lobby
        var request = new CreateLobbyRequest {
            Owner = PlayFabLogin.EntityKey,
            MaxPlayers = 3,
            Members = new List<Member>() {new Member() {MemberEntity = PlayFabLogin.EntityKey}}
            // Members = FriendsManager.Instance.GetFriendMembers(),
        };
        PlayFabMultiplayerAPI.CreateLobby(request, OnCreatedLobbySuccess, OnCreatedLobyError);
        // PlayFabClientAPI.GetAccountInfo();
        // PlayFabClientAPI.GetUserData();
        
    }

    private void OnCreatedLobyError(PlayFabError error) {
        Debug.LogError("Error creating lobby: " + error.GenerateErrorReport());
    }

    private void OnCreatedLobbySuccess(CreateLobbyResult response) {
        print("Lobby created successfully: " + response.ConnectionString + " - " + response.LobbyId);
        connectionString = response.ConnectionString;
        _lobbyId = response.LobbyId;
    }

    public void JoinLobby() {
        var request = new JoinLobbyRequest {
            ConnectionString = connectionString,
            MemberEntity = PlayFabLogin.EntityKey,
        };
        PlayFabMultiplayerAPI.JoinLobby(request, OnJoinedLobbySuccess, OnJoinedLobbyError);
    }

    private void OnJoinedLobbyError(PlayFabError error) {
        Debug.LogError("Error joining the lobby: " + error.GenerateErrorReport());
    }

    private void OnJoinedLobbySuccess(JoinLobbyResult response) {
        print("Joined lobby successfully: Lobby Id: " + response.LobbyId);
        _lobbyId = response.LobbyId;
    }

    public void LeaveLobby() {
        var request = new LeaveLobbyRequest {
            LobbyId = _lobbyId,
            MemberEntity = PlayFabLogin.EntityKey,
        };
        PlayFabMultiplayerAPI.LeaveLobby(request, OnLobbyLeftSuccess, OnLobbyLeftError);
    }

    private void OnLobbyLeftError(PlayFabError error) {
        Debug.LogError("Error leaving the lobby: " + error.GenerateErrorReport());
    }

    private void OnLobbyLeftSuccess(LobbyEmptyResult response) {
        print("Left lobby successfully");
    }

    public void InviteToLobby() {
        // new SubscribeToLobbyResourceRequest()
        var request = new InviteToLobbyRequest {
            InviteeEntity =
                FriendsManager.Instance.GetFriendMembers()[0]
                    .MemberEntity, // TODO: Invite selected friend, not simply the first one
            LobbyId = _lobbyId,
            MemberEntity = PlayFabLogin.EntityKey,
        };
        PlayFabMultiplayerAPI.InviteToLobby(request, OnInviteToLobbySuccess, OnInviteTolobbyError);
    }

    private void OnInviteTolobbyError(PlayFabError error) {
        Debug.LogError("Error inviting to lobby: " + error.GenerateErrorReport());
    }

    private void OnInviteToLobbySuccess(LobbyEmptyResult response) {
        print("Invitation to lobby successful");
    }

    public void FindLobbies() {
        var request = new FindLobbiesRequest() {
            Filter = ""
        };
        PlayFabMultiplayerAPI.FindLobbies(request, OnFindLobbiesSuccess, OnFindLobbiesError);
    }

    private void OnFindLobbiesError(PlayFabError error) {
        Debug.LogError("Error finding lobbies: " + error.GenerateErrorReport());
    }

    private void OnFindLobbiesSuccess(FindLobbiesResult response) {
        print("Found lobbies");
        foreach (var lobby in response.Lobbies) {
            print("Owner: " + lobby.Owner + "\n Connection String: " + lobby.ConnectionString + "\n Current Players: " +
                  lobby.CurrentPlayers + "\n Lobby Id:  " + lobby.LobbyId);
        }
    }

    public void FindFriendsLobbies() {
        var request = new FindFriendLobbiesRequest {
            Filter = ""
        };
        PlayFabMultiplayerAPI.FindFriendLobbies(request, OnFindFriendLobbiesSuccess, OnFindFriendLobbiesError);
    }

    private void OnFindFriendLobbiesError(PlayFabError error) {
        Debug.LogError("Error finding friends lobbies: " + error.GenerateErrorReport());
    }

    private void OnFindFriendLobbiesSuccess(FindFriendLobbiesResult result) {
        print("Found friends lobbies");
        foreach (var lobby in result.Lobbies) {
            print("Owner: " + lobby.Owner + "\n Connection String: " + lobby.ConnectionString + "\n Current Players: " +
                  lobby.CurrentPlayers + "\n Lobby Id:  " + lobby.LobbyId);
            friendsLobbiesListText.text += "\n Owner: " + lobby.Owner + "\n Connection String: " +
                                           lobby.ConnectionString + "\n Current Players: " +
                                           lobby.CurrentPlayers + "\n Lobby Id:  " + lobby.LobbyId;
        }
    }

    public void GetLobby() {
        var request = new GetLobbyRequest {
            LobbyId = _lobbyId,
        };
        PlayFabMultiplayerAPI.GetLobby(request, OnGetLobbySuccess, OnGetLobbyError);
        
        // PlayFabMultiplayerAPI.i
    }

    private void OnGetLobbyError(PlayFabError error) {
        Debug.LogError("Error getting lobby " + error.GenerateErrorReport());
    }

    private void OnGetLobbySuccess(GetLobbyResult result) {
        print("Got lobby successfully " + result.Lobby);
    }

    public void DeleteLobby() {
        var request = new DeleteLobbyRequest() { };
        PlayFabMultiplayerAPI.DeleteLobby(request, OnDeleteLobbySuccess, OnDeleteLobbyError);
    }

    private void OnDeleteLobbyError(PlayFabError error) {
        Debug.LogError("Error deleting lobby: " + error.GenerateErrorReport());
    }

    private void OnDeleteLobbySuccess(LobbyEmptyResult result) {
        print("Successfully deleted lobby");
    }

    /*
    private void OnDisable() {
        DeleteLobby();
    }
    */


    private void OnApplicationQuit() {
        LeaveLobby();
    }
}