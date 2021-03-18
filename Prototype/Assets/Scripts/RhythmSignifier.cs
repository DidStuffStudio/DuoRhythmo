using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSignifier : MonoBehaviour {
    private IEnumerator OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != 6) yield return null;
        var node = other.transform.GetComponentInParent<Node>();
        node.PlayDrum();
        other.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        other.GetComponent<Collider>().enabled = true;
    }
}