using System;
using System.Collections;
using System.Collections.Generic;
using LeTai.Asset.TranslucentImage.Demo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameToast : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float toastLength = 5.0f;
    private RectTransform _rt;
    public void SetText(string t) => text.text = t;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        if (text.GetRenderedValues(true).x > _rt.rect.width)
            _rt.sizeDelta = text.GetRenderedValues(true) + new Vector2(10, 10);
        StartCoroutine(DelayedFadeOut());
    }

    IEnumerator DelayedFadeOut()
    {
        yield return new WaitForSeconds(toastLength);
        DestroyToast();
    }

    private void DestroyToast() => Destroy(gameObject);
}
