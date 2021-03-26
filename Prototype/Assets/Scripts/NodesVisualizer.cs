using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class NodesVisualizer: MonoBehaviour {
    [SerializeField] private List<EuclideanManager> _euclideanManagers = new List<EuclideanManager>();
    private List<EuclideanManager> _euclideanManagersClones = new List<EuclideanManager>();
    private int radiusIndex = 10;
    private List<List<GameObject>> rings = new List<List<GameObject>>();
    [SerializeField] private GameObject nodeVisualization;
    
    // I need to keep track of only:
    // the position of the nodes of each EuclideanManager - and activating  or deactivating the nodes of each one
    
    public void InitializeCircle(Vector2[] nodesPositions) {
        var parentRing = new GameObject("Ring " + (rings.Count + 1));
        parentRing.transform.SetParent(transform);
        var nodesList = new List<GameObject>();
        // instantiate a new circle on the appropriate position and with the appropriate rotation
        for (int i = 0; i < nodesPositions.Length; i++) {
            var node = Instantiate(nodeVisualization, parentRing.transform);
            var rt = node.GetComponent<RectTransform>();
            rt.localRotation = Quaternion.Euler(0, 0, i * (360 / -nodesPositions.Length));
            rt.anchoredPosition = nodesPositions[i];
            rt.localScale = Vector3.one;
            nodesList.Add(node);
        }
        rings.Add(nodesList);
        radiusIndex++;
    }

    // call this function to update the appropriate node whenever there has been a bool that has been activated / deactivated
    public void UpdateNode(int drumIndex, int indexValue, bool activated) {
        var nodeImage = rings[drumIndex][indexValue].GetComponent<Image>();
        if(activated) nodeImage.color = CustomButton.activeColor;
        else nodeImage.color = CustomButton.defaultColor;
    }
}
