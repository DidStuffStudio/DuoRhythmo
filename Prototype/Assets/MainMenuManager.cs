using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
   private List<Transform> _panels;
   private List<Vector2> _panelSizes = new List<Vector2>();
   [SerializeField] private GameObject _background;
   private List<RectTransform> _backgroundRTs = new List<RectTransform>();
   [SerializeField] private float speed = 2;
   [SerializeField] private TMP_InputField usernameInput;
   private Dictionary<int, int> _numberBlurPanels = new Dictionary<int, int>();
   private int _numberCurrentBlurPanels = 1;
   private int _numberDesiredBlurPanels = 1;
   private List<bool> _panelReachedTargetSize = new List<bool>();
   private int _currentUiPanel;
   private void Start()
   {
      _panels = GetComponentsInChildren<Transform>().Where(r => r.CompareTag("MainMenuPanel")).ToList();
      
      for (int i = 0; i < _panels.Count; i++)
      {
         var blurPanels = _panels[i].GetComponentsInChildren<Transform>().Where(r => r.CompareTag("BlurPlaceholder")).ToList().Count;
         for (int j = 0; j < blurPanels; j++)
         {
            var rect = _panels[j].GetComponentInChildren<RectTransform>().rect;
            var w = rect.width;
            var h = rect.height;
            _panelSizes.Add(new Vector2(w,h));
         }
         _numberBlurPanels[i] = blurPanels;
         _panels[i].gameObject.SetActive(false);
         _panelReachedTargetSize.Add(false);
      }
      
      _panels[0].gameObject.SetActive(true);
      var initialBlurPnael = Instantiate(_background, transform.position, Quaternion.identity, transform);
      _backgroundRTs.Add(initialBlurPnael.GetComponent<RectTransform>());
      
   }

   private void ToggleUIPanel(int id)
   {
      if (_numberCurrentBlurPanels < _numberDesiredBlurPanels)
      {
         for (int i = _numberCurrentBlurPanels; i <  _numberDesiredBlurPanels; i++)
         {
            var blurpanel = Instantiate(_background, _panels[_currentUiPanel]);
            _backgroundRTs.Add(blurpanel.GetComponent<RectTransform>());
         }
      }
      else if (_numberCurrentBlurPanels > _numberDesiredBlurPanels)
      {
         for (int i = _numberCurrentBlurPanels; i > _numberDesiredBlurPanels; i--)
         {
            var d = _backgroundRTs[i];
            _backgroundRTs.Remove(_backgroundRTs[i]);
            Destroy(d.gameObject);
         }
      }
   }

   private void Update()
   {

      for (int i = 0; i < _backgroundRTs.Count; i++)
      {
        
         if (_panelReachedTargetSize[_currentUiPanel + i]) break;
         var currentSize = new Vector2(_backgroundRTs[i].rect.width, _backgroundRTs[i].rect.height);
         if (Mathf.Abs(currentSize.x - _panelSizes[_currentUiPanel + i].x) < 0.1f)
         {
            _panelReachedTargetSize[i] = true;
            break;
         }  
         var lerpedScale = Vector2.Lerp(currentSize, _panelSizes[_currentUiPanel + i], Time.deltaTime * speed); 
         _backgroundRTs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lerpedScale.x); 
         _backgroundRTs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lerpedScale.y);   
         
      }
   }

   public void DeactivatePanel(int indexToDeactivate) => _panels[indexToDeactivate].gameObject.SetActive(false);
   public void ActivatePanel(int indexToActivate)
   {
      _currentUiPanel = indexToActivate;
      _numberDesiredBlurPanels = _numberBlurPanels[indexToActivate];
      _panels[indexToActivate].gameObject.SetActive(true);
      ToggleUIPanel(indexToActivate);
   }
}
