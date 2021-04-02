using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSignifier : MonoBehaviour {
    private IEnumerator OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != 6) yield return null;
        var node = other.transform.GetComponentInParent<Node>();
        node.PlayDrum();
        var nodeRT = node.GetComponent<RectTransform>();
        nodeRT.position = Vector3.Lerp( nodeRT.position,new Vector3(nodeRT.position.x,nodeRT.position.y, nodeRT.position.z+10.0f), 0.1f);
        node.canPlay = false;
        yield return new WaitForSeconds(0.01f);
        node.canPlay = true;
    }

    IEnumerator  OnTriggerExit(Collider other)
    {
        var node = other.GetComponentInParent<Node>();
        var nodeRT = node.GetComponent<RectTransform>();
        nodeRT.position = Vector3.Lerp( nodeRT.position,new Vector3(nodeRT.position.x,nodeRT.position.y, nodeRT.position.z-10.0f), 0.1f);
        yield return new WaitForSeconds(0.01f);
    }
}