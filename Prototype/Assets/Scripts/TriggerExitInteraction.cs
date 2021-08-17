using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class TriggerExitInteraction : MonoBehaviour
{
    [SerializeField] private RadialSlider _radialSlider;
    private GazeAware _gazeAware;
    private void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    private void Update()
    {
        if (TobiiAPI.IsConnected && _gazeAware.HasGazeFocus) Hover();
    }

    private void Hover() => _radialSlider.ForceDefault();
    

    private void OnMouseEnter() => _radialSlider.ForceDefault();
}
  
