#if UNITY_EDITOR

using System.Collections.Generic;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CarouselDesigner : EditorWindow
    {
        private int _numberOfInstruments = 5, _numberOfNodes = 12, _radiusOfNodeRing = 35;
        private bool _isSoloMode = true;
        private static GameObject _effects, _nodes, _drumNode;
       

        [MenuItem("Did Stuff/Carousel Designer")]
        static void Init()
        {
            CarouselDesigner carouselDesigner = (CarouselDesigner)GetWindow (typeof (CarouselDesigner));
            
            carouselDesigner.Show();
        }
        private void OnGUI()
        {
            
            _numberOfInstruments =
                EditorGUI.IntSlider(new Rect(0, 0, position.width, 20), "Number of Instruments", _numberOfInstruments, 1, 10);
            _numberOfNodes = EditorGUI.IntSlider(new Rect(0, 30, position.width, 20), "Number of Nodes", _numberOfNodes, 2, 16);
            _radiusOfNodeRing =
                EditorGUI.IntSlider(new Rect(0, 60, position.width, 20), "Radius of the node ring", _radiusOfNodeRing, 10, 50);
            _isSoloMode =  EditorGUI.Toggle(new Rect(0, 90, position.width, 20),"Solo Mode Ordering" ,_isSoloMode);

            GUILayout.FlexibleSpace();

            if (!GUILayout.Button("Spawn Carousel")) return;
            SpawnCarousel();
        }

        private void SpawnCarousel()
        {
            var carouselManager = Resources.FindObjectsOfTypeAll<CarouselManager>()[0];
            
            if(carouselManager.transform.childCount > 0) DestroyImmediate(carouselManager.transform.GetChild(0).gameObject);
            
            _nodes = Resources.Load("NodesPanelParent") as GameObject;
            _effects = Resources.Load("EffectsPanel") as GameObject;
            _drumNode = Resources.Load("Node") as GameObject;
            var carouselGo = new GameObject("Carousel");
            carouselGo.transform.SetParent(carouselManager.transform);
            var nodesPanelsGo = new GameObject("Nodes panels");
            var effectsPanelsGo = new GameObject("Effects panels");
            nodesPanelsGo.transform.SetParent(carouselGo.transform);
            effectsPanelsGo.transform.SetParent(carouselGo.transform);
            var masterManager = Resources.FindObjectsOfTypeAll<MasterManager>()[0];
            
            masterManager.nodePanels.Clear();
            masterManager.effectPanels.Clear();
            masterManager.numberInstruments = _numberOfInstruments;
            masterManager.numberOfNodes = _numberOfNodes;

            var rotationValueSolo = Vector3.zero;
            var counter = 0;
            for (var i = 0; i < _numberOfInstruments; i++)
            {
                var nodePanel = Instantiate(_nodes, Vector3.zero, Quaternion.Euler(_isSoloMode ? rotationValueSolo:(rotationValueSolo*2)));
                masterManager.nodePanels.Add(nodePanel);

                Debug.Log(masterManager);
                nodePanel.transform.SetParent(nodesPanelsGo.transform);
                Debug.Log(nodePanel.transform.childCount);
                SpawnNodes(nodePanel.transform.GetChild(0), i);
                if(_isSoloMode)rotationValueSolo += new Vector3(0, 360.0f / (_numberOfInstruments * 2) * -1, 0);
                var effectPanel = Instantiate(_effects, Vector3.zero, Quaternion.Euler(_isSoloMode ? rotationValueSolo:(rotationValueSolo*2+new Vector3(0,180,0))));
                masterManager.effectPanels.Add(effectPanel);

                effectPanel.transform.SetParent(effectsPanelsGo.transform);
                rotationValueSolo += new Vector3(0, 360.0f / (_numberOfInstruments * 2) * -1, 0);
            }

        }

        private void SpawnNodes(Transform transform, int index)
        {
            // position all the nodes

            var nodesParent = new GameObject("Nodes");
            nodesParent.transform.SetParent(transform);
       
            
            for (var i = 0; i < _numberOfNodes; i++)
            {
                var angleDelta = 2.0f * Mathf.PI / -_numberOfNodes;
                var radians = i * angleDelta + angleDelta / 2.0f;
                radians += Mathf.PI/2.0f;
                var y = Mathf.Sin(radians);
                var x = Mathf.Cos(radians);
                var spawnPos = new Vector2(x, y) * _radiusOfNodeRing;
                
                
                var node = Instantiate(_drumNode, transform.position,Quaternion.identity, transform);
                var rt = node.GetComponent<RectTransform>();
                var z = i * angleDelta + angleDelta / 2;
                z *= Mathf.Rad2Deg;
                rt.localRotation = Quaternion.Euler(0, 0, z);
                rt.anchoredPosition = spawnPos;
                node.transform.SetParent(nodesParent.transform);
                transform.GetComponent<NodeManager>().nodes.Add(node.GetComponentInChildren<DidStuffNode>());    
            }
        }
    }
}

#endif