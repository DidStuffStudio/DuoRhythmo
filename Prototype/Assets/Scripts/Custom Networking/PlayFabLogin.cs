using System;
using System.Collections.Generic;
using ctsalidis;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using EntityKey = PlayFab.ProfilesModels.EntityKey;

// ref --> https://github.com/DapperDino/PlayFab-Tutorials/blob/main/Assets/Mirror/Examples/Pong/Scripts/PlayFabLogin.cs
public class PlayFabLogin : MonoBehaviour {
    public static PlayFabLogin Instance { get; private set; }

    [SerializeField] private Text playerIdText;
    [SerializeField] private GameObject signInDisplay = default;
    [SerializeField] private InputField usernameInputField = default;
    [SerializeField] private InputField emailInputField = default;
    [SerializeField] private InputField passwordInputField = default;
    [SerializeField] private GameObject matchCanvas;

    [Header("Managers")] 
    [SerializeField] private FriendsManager _friendsManager;
    
    private const string _PlayFabRememberMeIdKey = "PlayFabIdPassDeviceUniqueIdentifier";
    public GetPlayerCombinedInfoRequestParams InfoRequestParams;
    /// <summary>
    /// Generated Remember Me ID
    /// Pass Null for a value to have one auto-generated.
    /// </summary>
    private string RememberMeId
    {
        get => PlayerPrefs.GetString(_PlayFabRememberMeIdKey, "");
        set {
            var guid = value ?? Guid.NewGuid().ToString();
            PlayerPrefs.SetString(_PlayFabRememberMeIdKey, guid);
        }
    }

    public static string SessionTicket;
    public static string EntityId;
    public static string EntityType;
    public static string EntityToken;
    public static PlayFab.MultiplayerModels.EntityKey EntityKey; 
    public static List<PlayFab.MultiplayerModels.EntityKey> FriendsEntityKeys = new List<PlayFab.MultiplayerModels.EntityKey>();
    public static List<PlayFab.MultiplayerModels.EntityKey> SelectedFriendsEntityKeys = new List<PlayFab.MultiplayerModels.EntityKey>();
    // public static string Username;

    private void Awake() => Instance = this;

#if !UNITY_SERVER
    private void Start() {
        // NOTE --> Make sure that RememberMeId is initialised as the SystemInfo.deviceUniqueIdentifier
        LoginWithRememberMeId();
    }
#endif

    private void LoginWithRememberMeId() {
        if(string.IsNullOrEmpty(RememberMeId)) return;
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
            Username = usernameInputField.text,
            Email = emailInputField.text,
            Password = passwordInputField.text
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnPlayfabCreatedAccountSuccess, OnLoginError);
    }

    private void OnPlayfabCreatedAccountSuccess(RegisterPlayFabUserResult result) {
        print("Created account successfully");
        ProceedWithLogin(result.SessionTicket, result.EntityToken.Entity.Id, result.EntityToken.Entity.Type);
    }

    public void SignIn() {
        var request = new LoginWithPlayFabRequest {
            Username = usernameInputField.text,
            Password = passwordInputField.text
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
        ProceedWithLogin(result.SessionTicket, result.EntityToken.Entity.Id, result.EntityToken.Entity.Type);
    }

    private void ProceedWithLogin(string resultSessionTicket, string entityId, string entityType) {
        // if (string.IsNullOrEmpty(Username)) Username = EntityKey.Id;
        SessionTicket = resultSessionTicket;
        EntityKey = new PlayFab.MultiplayerModels.EntityKey {
            Id = entityId,
            Type = entityType
        };
        EntityId = entityId;
        EntityType = entityType;
        signInDisplay.SetActive(false);
        playerIdText.text = entityId;
        _friendsManager.EnableFriendsManager();
        if(matchCanvas) matchCanvas.gameObject.SetActive(true);

        if (string.IsNullOrEmpty(RememberMeId)) {
            RememberMeId = Guid.NewGuid().ToString();
            // Fire and forget, but link the custom ID to this PlayFab Account.
            PlayFabClientAPI.LinkCustomID(
                new LinkCustomIDRequest()
                {
                    CustomId = RememberMeId,
                    ForceLink = false
                },
                OnLinkedSuccess,
                OnLinkedError
            );
        }
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
    }
    
    // TODO --> Add clearing saved user info functionality
    public void ClearRememberMe() {
        PlayerPrefs.DeleteKey(_PlayFabRememberMeIdKey);
    }
}