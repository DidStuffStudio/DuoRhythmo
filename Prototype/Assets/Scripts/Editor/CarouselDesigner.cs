#if UNITY_EDITOR

using System.Collections.Generic;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CarouselDesigner : EditorWindow
    {
        private int _numberOfInstruments = 5, _numberOfNodes = 16, _radiusOfNodeRing = 250;
        private static GameObject _effects, _nodes, _drumNode;
        private static bool _spawned = false;
        private static GameObject _carousel;
        
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
                EditorGUI.IntSlider(new Rect(0, 60, position.width, 20), "Radius of the node ring", _radiusOfNodeRing, 50, 250);

            GUILayout.FlexibleSpace();

            if (!GUILayout.Button("Spawn Carousel")) return;
            SpawnCarousel();
        }
    
        private void SpawnCarousel()
        {
            if (_spawned)
            {
                _spawned = false;
                Destroy(_carousel);
            }
            _nodes = Resources.Load("NodesPanel") as GameObject;
            _effects = Resources.Load("EffectsPanel") as GameObject;
            _drumNode = Resources.Load("Node") as GameObject;
            var carouselGo = new GameObject("Carousel");
            var carouselManager = Resources.FindObjectsOfTypeAll<CarouselManager>()[0];
            carouselGo.transform.SetParent(carouselManager.transform);
            var nodesPanelsGo = new GameObject("Nodes panels");
            var effectsPanelsGo = new GameObject("Effects panels");
            nodesPanelsGo.transform.SetParent(carouselGo.transform);
            effectsPanelsGo.transform.SetParent(carouselGo.transform);
            var masterManager = Resources.FindObjectsOfTypeAll<MasterManager>()[0];
            masterManager.numberInstruments = _numberOfInstruments;
            masterManager.numberOfNodes = _numberOfNodes;

            var rotationValue = Vector3.zero;
            
            for (var i = 0; i < _numberOfInstruments; i++)
            {
                var nodePanel = Instantiate(_nodes, Vector3.zero, Quaternion.Euler(rotationValue));
                masterManager.nodePanels.Add(nodePanel);
                Debug.Log(masterManager);
                nodePanel.transform.SetParent(nodesPanelsGo.transform);
                Debug.Log(nodePanel.transform.childCount);
                SpawnNodes(nodePanel.transform.GetChild(0).transform.GetChild(0), i);
                rotationValue += new Vector3(0, 360.0f / (_numberOfInstruments * 2) * -1, 0);
                var effectPanel = Instantiate(_effects, Vector3.zero, Quaternion.Euler(rotationValue));
                masterManager.effectPanels.Add(effectPanel);
                effectPanel.transform.SetParent(effectsPanelsGo.transform);
                rotationValue += new Vector3(0, 360.0f / (_numberOfInstruments * 2) * -1, 0);
            }

            _spawned = true;
            _carousel = carouselGo;
        }

        private void SpawnNodes(Transform transform, int index)
        {
            // position all the nodes

            var nodesParent = new GameObject("Nodes");
            nodesParent.transform.SetParent(transform);
       
            
            for (var i = 0; i < _numberOfNodes; i++)
            {
                var angleDelta = 360 / -_numberOfNodes;
                var degrees = i * angleDelta + angleDelta / 2;
                degrees += 90;
                var radians = degrees * Mathf.Deg2Rad;
                var y = Mathf.Sin(radians);
                var x = Mathf.Cos(radians);
                var spawnPos = new Vector2(x, y) * _radiusOfNodeRing;
                
                
                var node = Instantiate(_drumNode, transform.position,Quaternion.identity, transform);
                var rt = node.GetComponent<RectTransform>();
                var z = (i * angleDelta) + angleDelta / 2;
                //z += 180;
                rt.localRotation = Quaternion.Euler(0, 0, z);
                rt.anchoredPosition = spawnPos;
                node.transform.SetParent(nodesParent.transform);
                transform.GetComponent<NodeManager>()._nodes.Add(node.GetComponentInChildren<DidStuffNode>());    
            }
        }
    }
}

#endif