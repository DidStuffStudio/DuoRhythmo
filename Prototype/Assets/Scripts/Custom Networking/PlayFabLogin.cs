using System;
using System.Collections.Generic;
using System.Linq;
using ctsalidis;
using DidStuffLab;
using Managers;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
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
    public GetPlayerCombinedInfoRequestParams InfoRequestParams;

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

    public static PlayFab.MultiplayerModels.EntityKey EntityKey;

    public static List<PlayFab.MultiplayerModels.EntityKey> FriendsEntityKeys =
        new List<PlayFab.MultiplayerModels.EntityKey>();

    public static List<PlayFab.MultiplayerModels.EntityKey> SelectedFriendsEntityKeys =
        new List<PlayFab.MultiplayerModels.EntityKey>();

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
            InfoRequestParameters = InfoRequestParams
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnPlayfabLoginSuccess, OnLoginError);
    }

    public void CreateAccount() {
        var request = new RegisterPlayFabUserRequest() {
            Username = Username,
            Password = PasswordPin,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnPlayfabCreatedAccountSuccess, OnLoginError);
    }

    private void OnPlayfabCreatedAccountSuccess(RegisterPlayFabUserResult result) {
        print("Created account successfully");
        ProceedWithLogin(result.SessionTicket, result.EntityToken.Entity.Id, result.EntityToken.Entity.Type, true);
    }

    public void SignIn() {
        var request = new LoginWithPlayFabRequest {
            Username = Username,
            Password = PasswordPin
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
        // Username = result.InfoResultPayload.AccountInfo.Username ?? result.PlayFabId;
        ProceedWithLogin(result.SessionTicket, result.EntityToken.Entity.Id, result.EntityToken.Entity.Type, false);
        
    }

    private void ProceedWithLogin(string resultSessionTicket, string entityId, string entityType, bool createdNewAccount) {
        // if (string.IsNullOrEmpty(Username)) Username = EntityKey.Id;
        EntityKey = new PlayFab.MultiplayerModels.EntityKey {
            Id = entityId,
            Type = entityType
        };

        if(createdNewAccount) SetUserAvatarObject(); // set the avatar object that the user has selected
        GetEntityAvatarName(new PlayFab.MultiplayerModels.EntityKey {Id = EntityKey.Id, Type = EntityKey.Type}, false);

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
        if(MainMenuManager.Instance.CurrentPanel != 4 || MainMenuManager.Instance.CurrentPanel != 19) MainMenuManager.Instance.SkipLogin();
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
            (error) => { Debug.LogError("There was an error on request trying to set the avatar object " + error.GenerateErrorReport()); });
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
                    var playerAvatarName = details["Name"].ToString(); // TODO --> Pass this to JammingSessionDetails manager
                    print("This is the retrieved avatar's name --> " + playerAvatarName + " from " + entityKey.Id);
                    if (isFriend) {
                        FriendsManager.Instance.friendAvatarNames.Add(entityKey.Id, playerAvatarName);
                        FriendsManager.Instance.AddAvatarToFriendDetails();
                    }
                }
            }
        }, (error) => { Debug.LogError("There has been a problem getting the avatar object --> " + error); });
    }
}