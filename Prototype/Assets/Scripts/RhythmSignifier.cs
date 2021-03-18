using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSignifier : MonoBehaviour
{
    private bool canPlay = true;
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (!canPlay) yield break;
        canPlay = false;
        var node = other.transform.GetComponentInParent<Node>();
        node.PlayDrum();
        yield return new WaitForSeconds(0.2f);
        canPlay = true;
    }
}