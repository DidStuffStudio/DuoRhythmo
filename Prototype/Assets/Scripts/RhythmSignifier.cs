using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmSignifier : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("BeatCollider")) return;
        var node = other.transform.GetComponentInParent<Node>();
        node.PlayDrum();
        if (node.isActive)
        {
            var nodeRT = node.GetComponent<RectTransform>();
            nodeRT.localPosition = new Vector3(nodeRT.localPosition.x, nodeRT.localPosition.y, -11.0f);
            StartCoroutine(LerpBack(node));
        }
        
    }

    IEnumerator LerpBack(Node node)
    {
        var nodeRT = node.GetComponent<RectTransform>();
        while (nodeRT.localPosition.z < 0.0f)
        {
            nodeRT.localPosition = Vector3.Lerp(nodeRT.localPosition,
                new Vector3(nodeRT.localPosition.x, nodeRT.localPosition.y, 0.1f), 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}