using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSignifier : MonoBehaviour
{
    private bool canPlay = true;
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6 || !canPlay) yield return null;
        canPlay = false;
        var node = other.transform.GetComponentInParent<Node>();
        node.PlayDrum();
        yield return new WaitForSeconds(0.1f);
        canPlay = true;
    }
}