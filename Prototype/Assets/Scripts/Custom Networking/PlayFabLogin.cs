using System;
using System.Collections.Generic;
using System.Linq;
using ctsalidis;
using DidStuffLab;
using Managers;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.DataModels;
using PlayFab.Json;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;

// ref --> https://github.com/DapperDino/PlayFab-Tutorials/blob/main/Assets/Mirror/Examples/Pong/Scripts/PlayFabLogin.cs
public class PlayFabLogin : MonoBehaviour {
    private static PlayFabLogin _instance;

    public static PlayFabLogin Instance {
        get {
            if (_instance != null) return _instance;
            var playfabLoginGameObject = new GameObject();
            _instance = playfabLoginGameObject.AddComponent<PlayFabLogin>();
            playfabLoginGameObject.name = typeof(PlayFabLogin).ToString();
            return _instance;
        }
    }

    public string Username { get; set; }
    public string PasswordPin { get; set; }
    public string UserAvatar { get; set; }

    private const string _PlayFabRememberMeIdKey = "PlayFabIdPassDeviceUniqueIdentifier";
    private GetPlayerCombinedInfoRequestParams _infoRequestParameters = new GetPlayerCombinedInfoRequestParams {
        // GetUserData = true,
        GetUserAccountInfo = true,
        // GetUserReadOnlyData = true,
        // GetCharacterList = true,
        // GetPlayerProfile = true,
        // GetTitleData = true
    };

    /// <summary>
    /// Generated Remember Me ID
    /// Pass Null for a value to have one auto-generated.
    /// </summary>
    private string RememberMeId {
        get => PlayerPrefs.GetString(_PlayFabRememberMeIdKey, "");
        set {
            var guid = value ?? Guid.NewGuid().ToString();
            PlayerPrefs.SetString(_PlayFabRememberMeIdKey, guid);
        }
    }
    
    public static string MasterPlayfabId;
    public static EntityKey EntityKey;

    public static List<EntityKey> FriendsEntityKeys =
        new List<EntityKey>();

    public static List<EntityKey> SelectedFriendsEntityKeys =
        new List<EntityKey>();

    private static PlayFabAuthenticationContext AuthenticationContext;

    private void Awake() {
        if (_instance == null) _instance = this;
        DontDestroyOnLoad(this);
    }

#if !UNITY_SERVER
    private void Start() {
        // NOTE --> Make sure that RememberMeId is initialised as the SystemInfo.deviceUniqueIdentifier
        LoginWithRememberMeId();
    }
#endif

    private void LoginWithRememberMeId() {
        if (string.IsNullOrEmpty(RememberMeId)) return;
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest() {
            TitleId = PlayFabSettings.TitleId,
            CreateAccount = true,
            CustomId = RememberMeId,
            InfoRequestParameters = _infoRequestParameters
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnPlayfabLoginSuccess, OnLoginError);
    }

    public void CreateAccount() {
        var request = new RegisterPlayFabUserRequest() {
            Username = Username,
            Password = PasswordPin,
            RequireBothUsernameAndEmail = false,
            InfoRequestParameters = _infoRequestParameters
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnPlayfabCreatedAccountSuccess, OnLoginError);
    }

    private void OnPlayfabCreatedAccountSuccess(RegisterPlayFabUserResult result) {
        print("Created account successfully");
        ProceedWithLogin(result.AuthenticationContext, result.Username, true);
    }

    public void SignIn() {
        var request = new LoginWithPlayFabRequest {
            Username = Username,
            Password = PasswordPin,
            InfoRequestParameters = _infoRequestParameters
        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnPlayfabLoginSuccess, OnLoginError);
    }

    public void LoginWithDeviceUniqueIdentifier() {
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest() {
            TitleId = PlayFabSettings.TitleId,
            CreateAccount = true,
            CustomId = SystemInfo.deviceUniqueIdentifier
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnPlayfabLoginSuccess, OnLoginError);
    }

    private void OnPlayfabLoginSuccess(LoginResult result) {
        print("Logged in successfully");
        ProceedWithLogin(result.AuthenticationContext, result.InfoResultPayload.AccountInfo.Username, false);
    }

    private void ProceedWithLogin(PlayFabAuthenticationContext authenticationContext, string username,  bool createdNewAccount) {
        AuthenticationContext = authenticationContext;
        MasterPlayfabId = AuthenticationContext.PlayFabId;
        Username = username ?? authenticationContext.EntityId;
        print("My username: " + username);
        EntityKey = new EntityKey {
            Id = AuthenticationContext.EntityId,
            Type = AuthenticationContext.EntityType
        };

        if (createdNewAccount) SetUserAvatarObject(); // set the avatar object that the user has selected
        GetEntityAvatarName(new EntityKey {Id = EntityKey.Id, Type = EntityKey.Type}, false);

        FriendsManager.Instance.EnableFriendsManager();

        if (string.IsNullOrEmpty(RememberMeId)) {
            RememberMeId = Guid.NewGuid().ToString();
            // Fire and forget, but link the custom ID to this PlayFab Account.
            PlayFabClientAPI.LinkCustomID(
                new LinkCustomIDRequest() {
                    CustomId = RememberMeId,
                    ForceLink = false
                },
                OnLinkedSuccess,
                OnLinkedError
            );
        }

        if (MainMenuManager.Instance.CurrentPanel != 4 || MainMenuManager.Instance.CurrentPanel != 19)
            MainMenuManager.Instance.SkipLogin();
    }

    private void OnLinkedError(PlayFabError error) {
        print(error.GenerateErrorReport());
    }

    private void OnLinkedSuccess(LinkCustomIDResult result) {
        print("Successfully linked device identifier/rememberId: " + RememberMeId);
    }

    private void OnLoginError(PlayFabError error) {
        Debug.LogError("Error logging in: " + error.GenerateErrorReport());
        RememberMeId = ""; // reset the remembermeId
        MainMenuManager.Instance.SendBackToLogin(error.GenerateErrorReport());
    }


    // TODO --> Add clearing saved user info functionality
    public void ClearRememberMe() {
        PlayerPrefs.DeleteKey(_PlayFabRememberMeIdKey);
        RememberMeId = "";
    }

    // Logout from this device - should unlink custom id from user's account, and reset the instance variables
    public void Logout() {
        // request to unlink this RememberMeId from the account
        PlayFabClientAPI.UnlinkCustomID(new UnlinkCustomIDRequest {CustomId = RememberMeId},
            (result) => { print("Successfully unlinked custom id from the user's account"); },
            (error) => { Debug.LogError(error.GenerateErrorReport()); });

        AuthenticationContext.ForgetAllCredentials();
        MasterPlayfabId = string.Empty;
        Username = string.Empty;
        PasswordPin = string.Empty;
        UserAvatar = string.Empty;
        ClearRememberMe();
        EntityKey = new EntityKey();
        FriendsEntityKeys = new List<EntityKey>();
        SelectedFriendsEntityKeys = new List<EntityKey>();
    }

    public void DeleteAccount() {
        // DeletePlayerAccount
        PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
            FunctionName = "DeletePlayerAccount",
            FunctionParameter = new {PlayFabId = MasterPlayfabId},
            GeneratePlayStreamEvent = true,
        }, (result) => {
            // TODO --> Check if this actually happened (return bool for success from cloudscript)
            print("Successfully sent request to delete player's account");
            Logout();
        }, (error) => { Debug.LogError(error.GenerateErrorReport()); });
    }

    private void SetUserAvatarObject() {
        print("Setting avatar name to --> " + UserAvatar);
        var data = new Dictionary<string, object>() {
            {"Name", UserAvatar},
        };
        var dataList = new List<SetObject>() {
            new SetObject() {
                ObjectName = "AvatarName",
                DataObject = data,
            },
        };
        PlayFabDataAPI.SetObjects(new SetObjectsRequest() {
                Entity = new PlayFab.DataModels.EntityKey {
                    Id = PlayFabLogin.EntityKey.Id,
                    Type = PlayFabLogin.EntityKey.Type,
                },
                Objects = dataList,
            }, (setResult) => { Debug.Log("Successfully set AvatarName object --> " + setResult.ProfileVersion); },
            (error) => {
                Debug.LogError("There was an error on request trying to set the avatar object " +
                               error.GenerateErrorReport());
            });
    }

    public void GetEntityAvatarName(EntityKey entityKey, bool isFriend) {
        print("Getting the user's avatar name of " + entityKey.Id);
        var getRequest = new GetObjectsRequest {
            Entity = new PlayFab.DataModels.EntityKey {
                Id = entityKey.Id,
                Type = entityKey.Type,
            },
        };
        PlayFabDataAPI.GetObjects(getRequest, result => {
            var objs = result.Objects;
            foreach (var o in objs.Where(o => o.Key == "AvatarName")) {
                if (o.Value.DataObject is JsonObject details) {
                    var playerAvatarName =
                        details["Name"].ToString(); // TODO --> Pass this to JammingSessionDetails manager
                    print("This is the retrieved avatar's name --> " + playerAvatarName + " from " + entityKey.Id);
                    if (isFriend) {
                        // FriendsManager.Instance.friendAvatarNames.Add(entityKey.Id, playerAvatarName);
                        FriendsManager.Instance.AddAvatarToFriendDetails(entityKey, playerAvatarName);
                    }
                }
            }
        }, (error) => { Debug.LogError("There has been a problem getting the avatar object --> " + error); });
    }
}