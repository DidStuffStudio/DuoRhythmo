using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class TestDrumEye : MonoBehaviour
{
    public DrumKitManager drumKit;
    private bool canPlay = true;
    public bool playKick, playSnare = false;
    public float bpm;
    private void Start()
    {
        drumKit = Transform.FindObjectOfType<DrumKitManager>();
        
    }

    public void PlayKick()
    {

        drumKit.kick = true;
        //canPlay = false;
        //StartCoroutine(WaitBro());
    }

    public void PlaySnare()
    {

        drumKit.snare = true;
        //canPlay = false;
        //StartCoroutine(WaitBro());
    }
    
    IEnumerator WaitBro()
    {
        
        yield return new WaitForSeconds(bpm);
        canPlay = true;
    }
}
