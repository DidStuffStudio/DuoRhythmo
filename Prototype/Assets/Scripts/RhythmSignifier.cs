using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSignifier : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        var node = other.transform.GetComponentInParent<Node>();
        node.Play();
    }
}