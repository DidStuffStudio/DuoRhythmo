using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public int panelId = 0;
    public int panelToReturnTo = 0;
    public List<Vector2> sizes = new List<Vector2>();
    public List<Vector2> positions = new List<Vector2>();
    public int numberOfPanels;
    private void Awake()
    {
        var blurPanels = transform.GetComponentsInChildren<Transform>().Where(r => r.CompareTag("BlurPlaceholder")).ToList();
        foreach (var blurPanel in blurPanels)
        {
            var size = blurPanel.GetComponent<RectTransform>().sizeDelta;
            size += new Vector2(242.1f, 210.71f);
            var position = blurPanel.GetComponent<RectTransform>().anchoredPosition;
            numberOfPanels = blurPanels.Count;
            sizes.Add(size);
            positions.Add(position);
            blurPanel.gameObject.SetActive(false);
        }
    }

    public virtual void ExecuteSpecificChanges()
    {
        
    }

    private void DestroyAllToasts()
    {
        var toasts = GameObject.FindGameObjectsWithTag("Toast");
        foreach (var toast in toasts) DestroyImmediate(toast);
    }

    private void OnDisable()
    {
        DestroyAllToasts();
    }
}
