#if UNITY_EDITOR

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
            CarouselDesigner carouselDesigner = (CarouselDesigner)EditorWindow.GetWindow (typeof (CarouselDesigner));
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
            var rotationValue = Vector3.zero;
            var carouselGo = new GameObject("Carousel");
            var nodesPanelsGo = new GameObject("Nodes panels");
            var effectsPanelsGo = new GameObject("Effects panels");
            nodesPanelsGo.transform.SetParent(carouselGo.transform);
            effectsPanelsGo.transform.SetParent(carouselGo.transform);

            for (var i = 0; i < _numberOfInstruments; i++)
            {
                var nodePanel = Instantiate(_nodes, Vector3.zero, Quaternion.Euler(rotationValue));
                nodePanel.transform.SetParent(nodesPanelsGo.transform);
                Debug.Log(nodePanel.transform.childCount);
                SpawnNodes(nodePanel.transform.GetChild(0).transform.GetChild(0));
                rotationValue += new Vector3(0, 360.0f / (_numberOfInstruments * 2) * -1, 0);
                var effectPanel = Instantiate(_effects, Vector3.zero, Quaternion.Euler(rotationValue));
                effectPanel.transform.SetParent(effectsPanelsGo.transform);

                rotationValue += new Vector3(0, 360.0f / (_numberOfInstruments * 2) * -1, 0);
            }

            _spawned = true;
            _carousel = carouselGo;
        }

        private void SpawnNodes(Transform transform)
        {
            // position all the nodes

            for (var i = 0; i < _numberOfNodes; i++)
            {
                var radians =
                    (i * 2 * Mathf.PI) / (-_numberOfNodes) +
                    (Mathf.PI / 2); // set them starting from 90 degrees = PI / 2 radians
                var y = Mathf.Sin(radians);
                var x = Mathf.Cos(radians);
                var spawnPos = new Vector2(x, y) * _radiusOfNodeRing;
                var node = Instantiate(_drumNode, transform.position,Quaternion.identity,transform);
                var rt = node.GetComponent<RectTransform>();
                rt.localRotation = Quaternion.Euler(0, 0, i * (360.0f / -_numberOfNodes));
                rt.anchoredPosition = spawnPos;
            }
        }
    }
}

#endif