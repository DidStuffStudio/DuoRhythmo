using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using DidStuffLab;
using PlayFab;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
   public class MainMenuManager : MonoBehaviour
   {
      
      private static MainMenuManager _instance;

      public static MainMenuManager Instance => _instance;

      private Dictionary<int, UIPanel> _panelDictionary = new Dictionary<int, UIPanel>();
      private List<Vector2> _currentPanelSizes = new List<Vector2>();
      private List<Vector2> _currentPanelPositions = new List<Vector2>();
      [SerializeField] private GameObject _background;
      private List<RectTransform> _backgroundRTs = new List<RectTransform>();
      [SerializeField] private float speed = 2;
      [SerializeField] private DidStuffTextField usernameInputLogin;
      [SerializeField] private DidStuffTextField usernameInputSignup;
      private Dictionary<int, int> _numberBlurPanels = new Dictionary<int, int>();
      private int _numberCurrentBlurPanels = 1;
      private int _numberDesiredBlurPanels = 1;
      private List<bool> _panelReachedTarget = new List<bool>();
      private bool _shouldLerp = false;
      [SerializeField] private GameObject errorToast, successToast;
      [SerializeField] private GameObject inviteToPlay;
      private bool _okayToSwitch = true;
      private int _activatedSettingsFrom = 0, _currentPanel = 0;
      [SerializeField] private GameObject backButton, settingsButton;
      private Dictionary<int, Vector2[]> blurPosDictionary = new Dictionary<int, Vector2[]>();
      private Dictionary<int, Vector2[]> blursizeDictionary = new Dictionary<int, Vector2[]>();
      private string _currentUsernameInput = "";
      private string _currentPinInput = "", _referencePin ="";
      [SerializeField] private List<int> panelsThatDontshowSettings = new List<int>();
      [SerializeField] private List<int> panelsThatDontshowBack = new List<int>();
      private DidStuffInvite _currentInvitePopUp;
      [SerializeField] private Transform defaultToastPlaceholder;
      [SerializeField] private TextMeshProUGUI matchmakingStatusText;


      public string ReferencePin
      {
         set => _referencePin = value;
      }

      public int CurrentPanel => _currentPanel;
      
      private void Awake()
      {
         if (_instance == null) _instance = this;
      
         
         backButton.SetActive(false);
         var uiPanels = GetComponentsInChildren<UIPanel>();
         foreach (var p in uiPanels) _panelDictionary.Add(p.panelId,p);
         
         
         for (int i = 0; i < _panelDictionary.Count; i++)
         {
            var panelPositions = uiPanels[i].BlurPosition();
            var panelSizes = uiPanels[i].BlurScales();
            var numberOfPanels = uiPanels[i].NumberOfPanels();
            uiPanels[i].DeactivatePlaceholder();
            _panelDictionary[i].gameObject.SetActive(false);
            blurPosDictionary.Add(_panelDictionary[i].panelId, panelPositions.ToArray());
            blursizeDictionary.Add(_panelDictionary[i].panelId, panelSizes.ToArray());
            _numberBlurPanels.Add(_panelDictionary[i].panelId, numberOfPanels);
         }
         var initialBlurPanel = Instantiate(_background, transform.position, Quaternion.identity, transform);
         _backgroundRTs.Add(initialBlurPanel.GetComponent<RectTransform>());
         initialBlurPanel.transform.SetSiblingIndex(0);
         uiPanels[6].ExecuteSpecificChanges();
         ActivatePanel(0);
         InteractionData.Instance.CheckInteractionMethod();
      }

      private void Start()
      {
         if(!PlayFabClientAPI.IsClientLoggedIn()) return;
         if(CurrentPanel != 4 && CurrentPanel != 19) MainMenuManager.Instance.SkipLogin();
      }

      private void ToggleUIPanel()
      {
         if (_numberCurrentBlurPanels < _numberDesiredBlurPanels)
         {
            for (int i = _numberCurrentBlurPanels; i <  _numberDesiredBlurPanels; i++)
            {
               var blurpanel = Instantiate(_background, transform);
               blurpanel.transform.SetSiblingIndex(0);
               _backgroundRTs.Add(blurpanel.GetComponent<RectTransform>());
               _numberCurrentBlurPanels++;
            }
         }
         else if (_numberCurrentBlurPanels > _numberDesiredBlurPanels)
         {
            for (int i = _numberCurrentBlurPanels-1; i > _numberDesiredBlurPanels-1; i--)
            {
               var d = _backgroundRTs[i];
               _backgroundRTs.Remove(_backgroundRTs[i]);
               Destroy(d.gameObject);
               _numberCurrentBlurPanels--;
               print("Tried to destroy");
            }
         }

         _currentPanelPositions = blurPosDictionary[CurrentPanel].ToList();
         _currentPanelSizes = blursizeDictionary[CurrentPanel].ToList();
         var reachedTarget = new bool[_currentPanelPositions.Count];
         if (reachedTarget == null) throw new ArgumentNullException(nameof(reachedTarget));
         _panelReachedTarget.Clear();
         _panelReachedTarget.AddRange(reachedTarget);
         _shouldLerp = true;
      }

      private void Update()
      {
         if(_shouldLerp) LerpBlur();
      }

      private void LerpBlur()
      {
         for (int i = 0; i < _panelReachedTarget.Count; i++)
         {
            if (_panelReachedTarget[i]) continue;
            var currentSize = new Vector2(_backgroundRTs[i].rect.width, _backgroundRTs[i].rect.height);
            var currentTransform = new Vector2(_backgroundRTs[i].anchoredPosition.x, _backgroundRTs[i].anchoredPosition.y);
            if (Mathf.Abs(currentSize.x - _currentPanelSizes[i].x) < 0.1f && Mathf.Abs(currentTransform.x - _currentPanelPositions[i].x) < 0.1f )
            {
               _panelReachedTarget[i] = true;
               if (i == _backgroundRTs.Count - 1) _shouldLerp = false;
               continue;
            }  
            var lerpedScale = Berp(currentSize, _currentPanelSizes[i], Time.deltaTime * speed);
            var lerpedPos = Berp(currentTransform, _currentPanelPositions[i], Time.deltaTime * speed);
            _backgroundRTs[i].sizeDelta = lerpedScale;
            _backgroundRTs[i].anchoredPosition = lerpedPos;

         }
      }

      public void Back()
      {
         var p = _panelDictionary[CurrentPanel].panelToReturnTo;
         
         switch (CurrentPanel)
         {
            case 13:
               p = _activatedSettingsFrom;
               break;
            
            case 2 when InteractionData.Instance.Method == InteractionMethod.Tobii ||
                        InteractionData.Instance.Method == InteractionMethod.MouseDwell:
               p = 1;
               break;
            case 2:
               p = 0;
               break;
         }

         DeactivatePanel(CurrentPanel);
         ActivatePanel(p);
      }

      public void Settings()
      {
         _activatedSettingsFrom = _panelDictionary[CurrentPanel].panelId;
         if (_activatedSettingsFrom == 13) _activatedSettingsFrom = 4;
         DeactivatePanel(CurrentPanel);
         ActivatePanel(13);
      }

      public void DeactivatePanel(int indexToDeactivate)
      {
         if (!_okayToSwitch) _okayToSwitch = true;
         else
         {
            var toasts = GameObject.FindGameObjectsWithTag("Toast");
            foreach (var toast in toasts) DestroyImmediate(toast);
            _panelDictionary[indexToDeactivate].gameObject.SetActive(false);
         }
      }

      public void ActivatePanel(int indexToActivate)
      {
         _currentPanel = indexToActivate;
         backButton.SetActive(!panelsThatDontshowBack.Contains(CurrentPanel));
         settingsButton.SetActive(!panelsThatDontshowSettings.Contains(CurrentPanel));
         _numberDesiredBlurPanels = _numberBlurPanels[CurrentPanel];
         ToggleUIPanel();
         StartCoroutine(ActivatePanelDelayed());
      }

      IEnumerator ActivatePanelDelayed()
      {
         yield return new WaitForSeconds(0.5f);
         _panelDictionary[CurrentPanel].gameObject.SetActive(true);
      }

      public void NextFromInteractionPage()
      {
         if (InteractionData.Instance.Method == InteractionMethod.Tobii ||
             InteractionData.Instance.Method == InteractionMethod.MouseDwell)
         {
            DeactivatePanel(CurrentPanel);
            if (CurrentPanel == 0) ActivatePanel(1);
            else ActivatePanel(22);
         }
         else
         {
            DeactivatePanel(CurrentPanel);
            if(CurrentPanel==0) ActivatePanel(2);
            else ActivatePanel(13);
         }
      }

      public void SendToInteractionPage()
      {
         DeactivatePanel(CurrentPanel);
         ActivatePanel(21);
         SpawnErrorToast("No Tobii eye tracker found!",0.5f);
      }
      public void SubmitUsernameLogIn(int indexToActivate)
      {
         if (usernameInputLogin.text.Length < 3)
         {
            _okayToSwitch = false;
            SpawnErrorToast("Please add a minimum of 3 characters",0.5f);
         }
         else
         {
            ActivatePanel(indexToActivate);
            _currentUsernameInput = usernameInputLogin.text;
         }

         usernameInputSignup.text = usernameInputLogin.text;
      }

      public void LogIn() {
         PlayFabLogin.Instance.Username = _currentUsernameInput;
         PlayFabLogin.Instance.PasswordPin = _currentPinInput;
         PlayFabLogin.Instance.SignIn();
      }

      public void SubmitUsernameSignUp(int indexToActivate)
      {
         
         if (usernameInputSignup.text.Length < 3)
         {
            _okayToSwitch = false;
            SpawnErrorToast("Please add a minimum of 3 characters",0.5f);
         }
         else
         {
            ActivatePanel(indexToActivate);
            _currentUsernameInput = usernameInputSignup.text;
         }
         
      }
      
      public void SubmitPin(int indexToActivate)
      {
         if (_currentPinInput.Length < 6)
         {
            _okayToSwitch = false;
            SpawnErrorToast("Please use a minimum of 6 digits",0.5f);
         }
         else
         {
            DeactivatePanel(_currentPanel);
            ActivatePanel(indexToActivate);
         }
         
      }

      public void CheckPinFromSignUp(int indexToActivate)
      {
         if (_currentPinInput != _referencePin)
         {
            _okayToSwitch = false;
            SpawnErrorToast("Pins do not match", 0.1f);
         }
         else
         {
            ActivatePanel(indexToActivate);
         }
      }

      public void SendBackToLogin(string errorMsg)
      {
         DeactivatePanel(CurrentPanel);
         ActivatePanel(2);
         SpawnErrorToast(errorMsg, 0.5f);
      }
      
      public void SpawnSuccessToast(string msg, float delay) => StartCoroutine(InstantiateSuccessToast(msg,delay));
      public void SpawnErrorToast(string msg, float delay) => StartCoroutine(InstantiateErrorToast(msg,delay));
   
      public void SignUp() {
         PlayFabLogin.Instance.Username = _currentUsernameInput;
         PlayFabLogin.Instance.PasswordPin = _currentPinInput;
         PlayFabLogin.Instance.CreateAccount();
      }

      /// <summary>
      /// Call this if the login rememberMeId is cached/saved to player prefs (if it's successful)
      /// </summary>
      public void SkipLogin() {
         DeactivatePanel(0);
         ActivatePanel(4);
      }

      public void LoginAsGuest()
      {
         PlayFabLogin.Instance.LoginAsGuest();
         panelsThatDontshowBack.Add(19);
         _panelDictionary[13].ExecuteSpecificChanges();
      }

      public void LogOut()
      {
         PlayFabLogin.Instance.Logout();
         DeactivatePanel(_currentPanel);
         ActivatePanel(0);
      }

      public void DeleteAccount()
      {
         PlayFabLogin.Instance.DeleteAccount();
         DeactivatePanel(_currentPanel);
         ActivatePanel(0);
      }

      public void ReceiveInviteToPlay(string username)
      {
         if(_currentInvitePopUp != null)_currentInvitePopUp.GetComponent<DidStuffInvite>().DestroyForNew();
         var popUp = Instantiate(inviteToPlay, transform);
         popUp.transform.SetSiblingIndex(transform.childCount-2);
         _currentInvitePopUp = popUp.GetComponent<DidStuffInvite>();
         _currentInvitePopUp.SetInviter(username);
      }

      public void AcceptInvite()
      {
         print("Accepted");
         Matchmaker.Instance.JoinMatchmaking();
         Matchmaker.Instance.isRandom = false;
         // Matchmaker.Instance.SelectDrumAndStartMatch("0"); // TODO --> Fix this (get drum type in getmatchmakingticketObject method )
         DeactivatePanel(_currentPanel);
         ActivatePanel(18);
      }
      public void DeclineInvite(string username)
      {
         print("Declined");
         Matchmaker.Instance.DeclineInvite(username);
      }
      
      public void SetPin(string pin) => _currentPinInput = pin;
      public void SetAvatarName(string avatarName) => PlayFabLogin.Instance.UserAvatar = avatarName;

      IEnumerator InstantiateErrorToast(string toastText, float delay)
      {
         yield return new WaitForSeconds(delay);
         var toastPlaceholder = GameObject.FindWithTag("Toast Placeholder");
         if (toastPlaceholder == null) toastPlaceholder = defaultToastPlaceholder.gameObject;
         if(toastPlaceholder.transform.childCount > 0) Destroy(toastPlaceholder.transform.GetChild(0).gameObject);
         var t = Instantiate(errorToast, toastPlaceholder.transform);
         t.GetComponent<Toast>().SetText(toastText);
      }
      
      IEnumerator InstantiateSuccessToast(string toastText, float delay)
      {
         yield return new WaitForSeconds(delay);
         var toastPlaceholder = GameObject.FindWithTag("Toast Placeholder").transform;
         if (toastPlaceholder == null) toastPlaceholder = defaultToastPlaceholder;
         if(toastPlaceholder.transform.childCount > 0) Destroy(toastPlaceholder.GetChild(0).gameObject);
         var t = Instantiate(successToast, toastPlaceholder);
         t.GetComponent<Toast>().SetText(toastText);
      }
      public void SetDrumType(int i) => JamSessionDetails.Instance.DrumTypeIndex = i;

      public void LoadJamSession()
      {
         SceneManager.LoadScene(1); // change to single player scene
      }
      
      public void OpenSurvey() => Application.OpenURL("https://t.maze.co/84786499");
      public void OpenFeedback() => Application.OpenURL("http://duorhythmo.frill.co/");

      private Vector2 Berp(Vector2 start, Vector2 end, float value)
      {
         value = Mathf.Clamp01(value);
         value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
         return start + (end - start) * value;
      }
      
      public void SetMatchmakingStatusText(string text) => matchmakingStatusText.text = text;
      
   }
   
}
