using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PinManager : MonoBehaviour
    {
        private String _pin;
        private List<int> pinIntegers = new List<int>();
        private int _currentIndex = 0;
        private Vector2 _lastPos;
        [SerializeField] private float lineWidth;
        [SerializeField] private Color lineColor;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject linePrefab;

        private void CreateLine(Vector2 endPos)
        {
            /*var line = new GameObject("Line" + _currentIndex);
            line.transform.SetParent(parent);
            line.transform.SetSiblingIndex(0);
            //var rt = line.gameObject.AddComponent<RectTransform>();
            var img = line.gameObject.AddComponent<Image>();
            var rt = line.gameObject.GetComponent<RectTransform>();
            img.color = lineColor;
            var vals = new Vector2(0.5f, 0);
            rt.offsetMin = vals;
            rt.offsetMax = vals;
            rt.pivot = vals;
            TransformLine(endPos, rt);*/

            var line = Instantiate(linePrefab, parent);
            line.transform.SetSiblingIndex(0);
            line.GetComponent<Image>().color = lineColor;
            var rt = line.GetComponent<RectTransform>();
            TransformLine(endPos, rt);
        }

        private void TransformLine(Vector2 end, RectTransform line)
        {
            line.anchoredPosition = _lastPos;
            var height = Vector2.Distance(_lastPos, end);
            line.sizeDelta = new Vector2(lineWidth, height);

            /*var dot = _lastPos.x * end.x + _lastPos.y * end.y;      // dot product
            var det = _lastPos.x * end.y - _lastPos.y * end.x;      // determinant
            var angle = Mathf.Atan2(det, dot);  */

            var angle = Mathf.Acos(Vector2.Dot(_lastPos, end) / (_lastPos.magnitude * end.magnitude));
            print("Just clicked on position " + end);
            print("Last clicked on position " + _lastPos);
            //var angle = Vector2.Angle(_lastPos, end);
            var rot = Quaternion.Euler(new Vector3(0,0,SignedAngleBetween(_lastPos,end,true)));
            line.rotation = rot;
            //line.Rotate(new Vector3(0,0,angle), Space.World);
            /*var worldEnd = new Vector3(end.x, end.y, 0);
            line.transform.LookAt(worldEnd);*/
            //line.Rotate(new Vector3(0,0,angle));
            
           
        }

       


        private float SignedAngleBetween(Vector3 a, Vector3 b, bool clockwise) {
            float angle = Vector3.Angle(a, b);

            //clockwise
            if( Mathf.Sign(angle) == -1 && clockwise )
                angle = 360 + angle;

            //counter clockwise
            else if (Mathf.Sign(angle) == 1 && !clockwise)
                angle = -angle;
            return angle;
        }
        
        
        public void SetPinCharacter(int value, Vector2 position)
        {
            if (_currentIndex > 0) CreateLine(position);
            _currentIndex++;
            pinIntegers.Add(value);
            _lastPos = position;
        }

        public void SetPin()
        {
            _pin = pinIntegers.Select(i => i.ToString()).Aggregate((i, j) => i + j);
            PlayFabLogin.Instance.PasswordPin = _pin;
            print(_pin);
        }
    }
}

 