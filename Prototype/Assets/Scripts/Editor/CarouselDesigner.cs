#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class CarouselDesigner : EditorWindow {
    private int numberOfInstruments = 5, numberOfNodes = 16;
    private static GameObject effects, nodes;
    [MenuItem("Custom/Carousel Designer")]

    private void OnGUI() {
        // EditorGUILayout.Toggle("Filter Inactive",filterInactive);

        numberOfInstruments = EditorGUI.IntSlider(new Rect(0, 0, position.width, 20), "Number of Instruments", 5, 1, 10);
        numberOfNodes = EditorGUI.IntSlider(new Rect(0, 30, position.width, 20), "Number of Nodes", 16, 1, 16);

        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("Spawn Carousel")) {
            Debug.Log("Spawning carousel");
            SpawnCarousel();
        }
    }

    private void OnInspectorUpdate() { }

    private void SpawnCarousel() {
        nodes = Resources.Load("NodesPanel") as GameObject;
        effects = Resources.Load("EffectsPanel") as GameObject;
        Vector3 rotationValue = Vector3.zero;
        var nodesPanelsGo = new GameObject("Nodes panels");
        var effectsPanelsGo = new GameObject("Effects panels");
        
        for (int i = 0; i < numberOfInstruments; i++) {
            var nodePanel = Instantiate(nodes, Vector3.zero, Quaternion.Euler(rotationValue)) as GameObject;
            nodePanel.transform.SetParent(nodesPanelsGo.transform);
            rotationValue += new Vector3(0, 360.0f / (numberOfInstruments * 2) * -1, 0);
            var effectPanel = Instantiate(effects, Vector3.zero, Quaternion.Euler(rotationValue)) as GameObject;
            effectPanel.transform.SetParent(effectsPanelsGo.transform);
            
            rotationValue += new Vector3(0, 360.0f / (numberOfInstruments * 2) * -1, 0);
        }
    }

    private void SpawnNodes() {
        
    }
}

#endif