using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using LeTai.Asset.TranslucentImage;
using Managers;
using Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
   
   private static InGameMenuManager _instance;

   public static InGameMenuManager Instance => _instance;
   
    [SerializeField] private GameObject _background;
    private Dictionary<int, UIPanel> _panelDictionary = new Dictionary<int, UIPanel>();
    private List<Vector2> _currentPanelSizes = new List<Vector2>();
    private List<Vector2> _currentPanelPositions = new List<Vector2>();
    private List<RectTransform> _backgroundRTs = new List<RectTransform>();
    [SerializeField] private float speed = 2;
    private Dictionary<int, int> _numberBlurPanels = new Dictionary<int, int>();
    private int _numberCurrentBlurPanels = 1;
    private int _numberDesiredBlurPanels = 1;
    private List<bool> _panelReachedTarget = new List<bool>();
    private bool _shouldLerp = false;
    [SerializeField] private GameObject errorToast, successToast, infoToast;
    private Dictionary<int, Vector2[]> blurPosDictionary = new Dictionary<int, Vector2[]>();
    private Dictionary<int, Vector2[]> blursizeDictionary = new Dictionary<int, Vector2[]>();
    [SerializeField] private List<int> panelsThatDontshowBack = new List<int>();
    [SerializeField] private Transform defaultToastPlaceholder;
    [SerializeField] private GameObject exitButton, backButton, settingsButton;
    private int _currentPanel = 0;
    
    [SerializeField] private TranslucentImageSource blurBackground;
    [SerializeField] private GameObject raycastBlock;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private InGameInteractionManager interactionManager;
    private GraphicRaycaster drumGraphicRaycaster;
   

    private void Awake()
    {
       if (_instance == null) _instance = this;
       
        var uiPanels = GetComponentsInChildren<UIPanel>();
        foreach (var p in uiPanels) _panelDictionary.Add(p.panelId, p);
       
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

        var initialBlurPanel = Instantiate(_background, transform.position, Quaternion.identity, settingsMenu.transform);
        _backgroundRTs.Add(initialBlurPanel.GetComponent<RectTransform>());
        initialBlurPanel.transform.SetSiblingIndex(0);
        CloseSettings();
        
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
            _backgroundRTs[i].sizeDelta = lerpedScale;
            _backgroundRTs[i].anchoredPosition = lerpedPos;

         }
      }
      
      private Vector2 Berp(Vector2 start, Vector2 end, float value)
      {
         value = Mathf.Clamp01(value);
         value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
         return start + (end - start) * value;
      }
      
      public void Back()
      {
         var p = _panelDictionary[_currentPanel].panelToReturnTo;
         DeactivatePanel(_currentPanel);
         if(_currentPanel == 0) CloseSettings();
         else ActivatePanel(p);
      }

      public void DeactivatePanel(int indexToDeactivate)
      {
         _panelDictionary[indexToDeactivate].gameObject.SetActive(false);
      }

      public void ActivatePanel(int indexToActivate)
      {
         _currentPanel = indexToActivate;
         backButton.SetActive(!panelsThatDontshowBack.Contains(_currentPanel));
         _numberDesiredBlurPanels = _numberBlurPanels[_currentPanel];
         ToggleUIPanel();
         StartCoroutine(ActivatePanelDelayed());
      }

      public void SendToInteractionPanel()
      {
         //TODO send to interaction page if tobii not detected.
      }


      IEnumerator ActivatePanelDelayed()
      {
         yield return new WaitForSeconds(0.5f);
         _panelDictionary[_currentPanel].gameObject.SetActive(true);
      }

      public void NextFromInteractionPage()
      {
         if (InteractionData.Instance.Method == InteractionMethod.Tobii ||
             InteractionData.Instance.Method  == InteractionMethod.MouseDwell)
         {
            DeactivatePanel(_currentPanel);
            ActivatePanel(2);
         }
         else
         {
            DeactivatePanel(_currentPanel);
            ActivatePanel(0);
         }
      }

     
    private void BlurBackground(bool blur) => blurBackground.enabled = blur;
    

    public void OpenSettings()
    {
       MuteDrums(true);
       drumGraphicRaycaster = interactionManager.RaycasterDrumPanel;
       interactionManager.RaycasterDrumPanel = graphicRaycaster;
       settingsButton.SetActive(false);
       for (var i = 0; i < _panelDictionary.Count; i++) DeactivatePanel(i); 
       exitButton.SetActive(false);
       backButton.SetActive(true);
       BlurBackground(true);
       settingsMenu.SetActive(true);
       ActivatePanel(0);
    }

    public void CloseSettings()
    {
       DeactivatePanel(0);
       settingsButton.SetActive(true);
       for (var i = 0; i < _panelDictionary.Count; i++) DeactivatePanel(i); 
       exitButton.SetActive(true);
       backButton.SetActive(false);
       BlurBackground(false);
       settingsMenu.SetActive(false);
       MuteDrums(false);
       if(drumGraphicRaycaster != null) interactionManager.RaycasterDrumPanel = drumGraphicRaycaster;
    }
    
    public void OpenSaveBeats()
    {
       MuteDrums(true);
       drumGraphicRaycaster = interactionManager.RaycasterDrumPanel;
       interactionManager.RaycasterDrumPanel = graphicRaycaster;
       settingsButton.SetActive(false);
       backButton.SetActive(false);
       exitButton.SetActive(false);
       BlurBackground(true);
       settingsMenu.SetActive(true);
       ActivatePanel(5);
    }

    private void MuteDrums(bool mute)
    {
       if(MasterManager.Instance != null) MasterManager.Instance.audioManager.MuteAll(mute);
    }

    public void SpawnSuccessToast(string msg, float delay) => StartCoroutine(InstantiateSuccessToast(msg,delay));
    public void SpawnErrorToast(string msg, float delay) => StartCoroutine(InstantiateErrorToast(msg,delay));
    public void SpawnInfoToast(string msg, float delay) => StartCoroutine(InstantiateInfoToast(msg,delay));
    
    IEnumerator InstantiateErrorToast(string toastText, float delay)
    {
       yield return new WaitForSeconds(delay);
       var toastPlaceholder = GameObject.FindWithTag("DefaultToastHolder").transform;
       if(toastPlaceholder.transform.childCount > 0) Destroy(toastPlaceholder.transform.GetChild(0).gameObject);
       var t = Instantiate(errorToast, toastPlaceholder);
       t.GetComponent<InGameToast>().SetText(toastText);
    }
      
    IEnumerator InstantiateSuccessToast(string toastText, float delay)
    {
       yield return new WaitForSeconds(delay);
       var toastPlaceholder =  GameObject.FindWithTag("DefaultToastHolder").transform;
       if(toastPlaceholder.transform.childCount > 0) Destroy(toastPlaceholder.GetChild(0).gameObject);
       var t = Instantiate(successToast, toastPlaceholder);
       t.GetComponent<InGameToast>().SetText(toastText);
    }
    
    IEnumerator InstantiateInfoToast(string toastText, float delay)
    {
       yield return new WaitForSeconds(delay);
       var toastPlaceholder = GameObject.FindWithTag("DefaultToastHolder").transform;
       if(toastPlaceholder.transform.childCount > 0) Destroy(toastPlaceholder.GetChild(0).gameObject);
       var t = Instantiate(infoToast, toastPlaceholder);
       t.GetComponent<InGameToast>().SetText(toastText);
    }

    public void OpenSurvey() => Application.OpenURL("https://t.maze.co/84786499");
    public void OpenFeedback() => Application.OpenURL("http://duorhythmo.frill.co/");

    public void Quit()
    {
       StopAllCoroutines();
       JamSessionDetails.Instance.quitFromGame = true;
       SceneManager.LoadScene(0);
    }
    
}
