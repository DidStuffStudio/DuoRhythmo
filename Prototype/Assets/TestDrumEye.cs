using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class TestDrumEye : MonoBehaviour
{
    private GazeAware gazeAware;
    public DrumKitManager drumKit;
    private bool canPlay = true;
    public float bpm;
    private void Start()
    {
        gazeAware = GetComponent<GazeAware>();
    }

    private void Update()
    {
        if (!gazeAware.HasGazeFocus || !canPlay) return;
        drumKit.kick = true;
        canPlay = false;
        StartCoroutine(WaitBro());
    }

    IEnumerator WaitBro()
    {
        yield return new WaitForSeconds(bpm);
        canPlay = true;
    }
}
