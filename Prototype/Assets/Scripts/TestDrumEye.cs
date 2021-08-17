using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public enum InteractionMethod
{
    contextSwitch,
    dwellFeedback,
    spock
};
public class TestDrumEye : MonoBehaviour
{
    public InteractionMethod interactionMethod;
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

        //canPlay = false;
        //StartCoroutine(WaitBro());
    }

    public void PlaySnare()
    {


        //canPlay = false;
        //StartCoroutine(WaitBro());
    }
    
    IEnumerator WaitBro()
    {
        
        yield return new WaitForSeconds(bpm);
        canPlay = true;
    }
}
