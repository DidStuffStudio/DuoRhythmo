using System.Collections;
using System.Collections.Generic;
using LeTai.Asset.TranslucentImage.Demo;
using TMPro;
using UnityEngine;

public class Toast : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string t) => text.text = t;
}
