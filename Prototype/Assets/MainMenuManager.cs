using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
   private List<Transform> _panels;
   private List<Vector2> _panelSizes = new List<Vector2>();
   private Vector2 _targetSize;
   [SerializeField] private RectTransform _background;
   [SerializeField] private float speed = 2;
   private void Start()
   {
      _panels = GetComponentsInChildren<Transform>().Where(r => r.CompareTag("MainMenuPanel")).ToList();
      foreach (var p in _panels)
      {
         var rect = p.GetComponentInChildren<RectTransform>().rect;
         var w = rect.width;
         var h = rect.height;
         _panelSizes.Add(new Vector2(w,h));
         p.gameObject.SetActive(false);
      }
      _panels[0].gameObject.SetActive(true);
      _targetSize = _panelSizes[0];
   }

   public void ToggleUIPanel(int id)
   {
      //_panels[id].SetActive(!_panels[id].activeSelf);
      _targetSize = _panelSizes[id];
   }

   private void Update()
   {
      var currentSize = new Vector2(_background.rect.width, _background.rect.height);
      if(Mathf.Abs(currentSize.x - _targetSize.x) < 0.1f) return;
      var lerpedScale = Vector2.Lerp(currentSize, _targetSize, Time.deltaTime * speed);
      _background.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lerpedScale.x);
      _background.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lerpedScale.y);
      
      
   }

   private void LerpUIBox()
   {
      
   }
}
