using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
   public class MainMenuManager : MonoBehaviour
   {
      private Dictionary<int, UIPanel> _panelDictionary = new Dictionary<int, UIPanel>();
      private List<Vector2> _currentPanelSizes = new List<Vector2>();
      private List<Vector2> _currentPanelPositions = new List<Vector2>();
      [SerializeField] private GameObject _background;
      private List<RectTransform> _backgroundRTs = new List<RectTransform>();
      [SerializeField] private float speed = 2;
      [SerializeField] private TMP_InputField usernameInputLogin;
      [SerializeField] private TMP_InputField usernameInputSignup;
      private Dictionary<int, int> _numberBlurPanels = new Dictionary<int, int>();
      private int _numberCurrentBlurPanels = 1;
      private int _numberDesiredBlurPanels = 1;
      private List<bool> _panelReachedTarget = new List<bool>();
      private bool _shouldLerp = false;
      [SerializeField] private GameObject toast;
      private bool _okayToSwitch = true;
      private int _activatedSettingsFrom = 0, _currentPanel = 0;
      [SerializeField] private GameObject backButton, settingsButton;
      private Dictionary<int, Vector2[]> blurPosDictionary = new Dictionary<int, Vector2[]>();
      private Dictionary<int, Vector2[]> blursizeDictionary = new Dictionary<int, Vector2[]>();
      private string _currentUsernameInput = "";
      private string _currentPinInput = "";
      [SerializeField] private List<int> panelsThatDontshowSettings = new List<int>();
      [SerializeField] private List<int> panelsThatDontshowBack = new List<int>();
      private void Start()
      {
         backButton.SetActive(false);
         var uiPanels = GetComponentsInChildren<UIPanel>();
         foreach (var p in uiPanels) _panelDictionary.Add(p.panelId,p);
         
         
         for (int i = 0; i < _panelDictionary.Count; i++)
         {
            var blurPanels = _panelDictionary[i].transform.GetComponentsInChildren<Transform>().Where(r => r.CompareTag("BlurPlaceholder")).ToList();
            List<Vector2> panelSizes = new List<Vector2>();
            List<Vector2> panelPositions = new List<Vector2>();
            
            for (int j = 0; j < blurPanels.Count; j++)
            {
               var blurParent = blurPanels[j].parent.GetComponent<RectTransform>();
               var rect = blurPanels[j].GetComponent<RectTransform>();
               var rect1 = rect.rect;
               var w = rect1.width;
               var h = rect1.height;
               var anchoredPosition = blurParent.anchoredPosition;
               var x = anchoredPosition.x;
               var y = anchoredPosition.y;
               panelSizes.Add(new Vector2(w,h));
               panelPositions.Add(new Vector2(x,y));
            }
            _panelDictionary[i].gameObject.SetActive(false);
            blurPosDictionary.Add(_panelDictionary[i].panelId, panelPositions.ToArray());
            blursizeDictionary.Add(_panelDictionary[i].panelId, panelSizes.ToArray());
            _numberBlurPanels.Add(_panelDictionary[i].panelId, blurPanels.Count);
         }
         var initialBlurPanel = Instantiate(_background, transform.position, Quaternion.identity, transform);
         _backgroundRTs.Add(initialBlurPanel.GetComponent<RectTransform>());
         initialBlurPanel.transform.SetSiblingIndex(0);
         
         ActivatePanel(0);
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

         _currentPanelPositions = blurPosDictionary[_currentPanel].ToList();
         _currentPanelSizes = blursizeDictionary[_currentPanel].ToList();
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
            _backgroundRTs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lerpedScale.x); 
            _backgroundRTs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lerpedScale.y);
            _backgroundRTs[i].anchoredPosition = lerpedPos;

         }
      }

      public void Back()
      {
         var p = _panelDictionary[_currentPanel].panelToReturnTo;
         if (_currentPanel == 13) p = _activatedSettingsFrom;
         DeactivatePanel(_currentPanel);
         ActivatePanel(p);
      }

      public void Settings()
      {
         _activatedSettingsFrom = _currentPanel;
         DeactivatePanel(_currentPanel);
         ActivatePanel(13);
      }

      public void DeactivatePanel(int indexToDeactivate)
      {
         if (!_okayToSwitch) _okayToSwitch = true;
         else
         {
            _panelDictionary[indexToDeactivate].gameObject.SetActive(false);
         }
      }

      public void ActivatePanel(int indexToActivate)
      {
         _currentPanel = indexToActivate;
         backButton.SetActive(!panelsThatDontshowBack.Contains(_currentPanel));
         settingsButton.SetActive(!panelsThatDontshowSettings.Contains(_currentPanel));
         _numberDesiredBlurPanels = _numberBlurPanels[_currentPanel];
         ToggleUIPanel();
         StartCoroutine(ActivatePanelDelayed());
      }

      IEnumerator ActivatePanelDelayed()
      {
         yield return new WaitForSeconds(0.5f);
         _panelDictionary[_currentPanel].gameObject.SetActive(true);
      }

      public void NextFromInteractionPage()
      {
         if (InteractionManager.Instance.Method == InteractionMethod.Tobii ||
             InteractionManager.Instance.Method == InteractionMethod.MouseDwell)
         {
            DeactivatePanel(_currentPanel);
            if (_currentPanel == 0) ActivatePanel(1);
            else ActivatePanel(22);
         }
         else
         {
            if(_currentPanel==0) ActivatePanel(2);
            else ActivatePanel(13);
         }
      }

      public void SendToInteractionPage()
      {
         DeactivatePanel(_currentPanel);
         ActivatePanel(21);
         InstantiateToast("No Tobii eye tracker found!");
      }
      public void SubmitUsernameLogIn(int indexToActivate)
      {
         if (usernameInputLogin.text.Length < 3)
         {
            _okayToSwitch = false;
            InstantiateToast("Please add a minimum of 3 characters");
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
         //TODO Failed login in send to sign in
      }

      public void SubmitUsernameSignUp(int indexToActivate)
      {
         
         if (usernameInputSignup.text.Length < 3)
         {
            _okayToSwitch = false;
            InstantiateToast("Please add a minimum of 3 characters");
         }
         else
         {
            ActivatePanel(indexToActivate);
            _currentUsernameInput = usernameInputLogin.text;
         }
         
      }

      public void SignUp() {
         PlayFabLogin.Instance.Username = _currentUsernameInput;
         PlayFabLogin.Instance.PasswordPin = _currentPinInput;
         PlayFabLogin.Instance.CreateAccount();
         //TODO Failed login in send to sign in username taken
      }

      /// <summary>
      /// Call this if the login rememberMeId is cached/saved to player prefs (if it's successful)
      /// </summary>
      public void SkipLogin() {
         DeactivatePanel(_currentPanel);
         ActivatePanel(4);
      }

      public void LoginAsGuest() => PlayFabLogin.Instance.LoginWithDeviceUniqueIdentifier();

      public void SetPin(string pin) => _currentPinInput = pin;

      private void InstantiateToast(string toastText)
      {
         var toastPlaceholder = GameObject.FindWithTag("Toast Placeholder");
         if(toastPlaceholder.transform.childCount > 0) Destroy(toastPlaceholder.transform.GetChild(0).gameObject);
         var t = Instantiate(toast, toastPlaceholder.transform);
         t.GetComponent<Toast>().SetText(toastText);
      }
      public void SetDrumType(int i) => JamSessionDetails.Instance.DrumTypeIndex = i;
   
      public void LoadJamSession() => SceneManager.LoadScene(1);

      private Vector2 Berp(Vector2 start, Vector2 end, float value)
      {
         value = Mathf.Clamp01(value);
         value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
         return start + (end - start) * value;
      }
   }
}
