using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
   public class MainMenuManager : MonoBehaviour
   {
      private List<Transform> _panels;
      private List<Vector2> _panelSizes = new List<Vector2>();
      private List<Vector2> _panelPositions = new List<Vector2>();
      [SerializeField] private GameObject _background;
      private List<RectTransform> _backgroundRTs = new List<RectTransform>();
      [SerializeField] private float speed = 2;
      [SerializeField] private TMP_InputField usernameInput;
      private Dictionary<int, int> _numberBlurPanels = new Dictionary<int, int>();
      private int _numberCurrentBlurPanels = 1;
      private int _numberDesiredBlurPanels = 1;
      private List<bool> _panelReachedTargetSizeAndPosition = new List<bool>();
      private int _currentUiPanel;
      private bool _shouldLerp = true;
      private int _lastNumberOfBlurs = 0;
      private void Start()
      {
         _panels = GetComponentsInChildren<Transform>().Where(r => r.CompareTag("MainMenuPanel")).ToList();
      
         for (int i = 0; i < _panels.Count; i++)
         {
            var blurPanels = _panels[i].GetComponentsInChildren<Transform>().Where(r => r.CompareTag("BlurPlaceholder")).ToList();
         
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
               _panelSizes.Add(new Vector2(w,h));
               _panelPositions.Add(new Vector2(x,y));
               _panelReachedTargetSizeAndPosition.Add(false);
            }
            _numberBlurPanels[i] = blurPanels.Count;
            _panels[i].gameObject.SetActive(false);
         
         }
      
         _panels[0].gameObject.SetActive(true);
         var initialBlurPnael = Instantiate(_background, transform.position, Quaternion.identity, transform);
         _backgroundRTs.Add(initialBlurPnael.GetComponent<RectTransform>());
         initialBlurPnael.transform.SetSiblingIndex(0);

      }

      private void ToggleUIPanel(int id)
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

         _shouldLerp = true;
      }

      private void Update()
      {
         if(_shouldLerp) LerpBlur();
      }

      private void LerpBlur()
      {
         for (int i = 0; i < _backgroundRTs.Count; i++)
         {
            if (_panelReachedTargetSizeAndPosition[_currentUiPanel + i + _lastNumberOfBlurs]) continue;
            var currentSize = new Vector2(_backgroundRTs[i].rect.width, _backgroundRTs[i].rect.height);
            var currentTransform = new Vector2(_backgroundRTs[i].anchoredPosition.x, _backgroundRTs[i].anchoredPosition.y);
            if (Mathf.Abs(currentSize.x - _panelSizes[_currentUiPanel + i].x) < 0.1f && Mathf.Abs(currentTransform.x - _panelPositions[_currentUiPanel + i].x) < 0.1f )
            {
               _panelReachedTargetSizeAndPosition[_currentUiPanel + i] = true;
               if (i == _backgroundRTs.Count - 1) _shouldLerp = false;
               if (i == _backgroundRTs.Count - 1) _lastNumberOfBlurs = _backgroundRTs.Count - 1;
               continue;
            }  
            var lerpedScale = Berp(currentSize, _panelSizes[_currentUiPanel + i + _lastNumberOfBlurs], Time.deltaTime * speed);
            var lerpedPos = Berp(currentTransform, _panelPositions[_currentUiPanel + i + _lastNumberOfBlurs], Time.deltaTime * speed);
            _backgroundRTs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lerpedScale.x); 
            _backgroundRTs[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lerpedScale.y);
            _backgroundRTs[i].anchoredPosition = lerpedPos;

         }
      }

      public void DeactivatePanel(int indexToDeactivate) => _panels[indexToDeactivate].gameObject.SetActive(false);
      public void ActivatePanel(int indexToActivate)
      {
         _currentUiPanel = indexToActivate;
         _numberDesiredBlurPanels = _numberBlurPanels[indexToActivate];
         ToggleUIPanel(indexToActivate);
         StartCoroutine(ActivatePanelDelayed());
      }

      IEnumerator ActivatePanelDelayed()
      {
         yield return new WaitForSeconds(0.5f);
         _panels[_currentUiPanel].gameObject.SetActive(true);
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
