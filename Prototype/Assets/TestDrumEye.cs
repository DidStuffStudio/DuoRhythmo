using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class TestDrumEye : MonoBehaviour
{
    public DrumKitManager drumKit;
    private bool canPlay = true;
    public bool play = false;
    public float bpm;
    private void Start()
    {
        drumKit = Transform.FindObjectOfType<DrumKitManager>();
        
    }

    private void Update()
    {
        if (!play || !canPlay) return;
        drumKit.kick = true;
        canPlay = false;
        StartCoroutine(WaitBro());
    }

    IEnumerator WaitBro()
    {
        play = false;
        yield return new WaitForSeconds(bpm);
        canPlay = true;
    }
}
