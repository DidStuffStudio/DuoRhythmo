using UnityEngine;

namespace DidStuffLab.Scripts.Main_Menu
{
    [ExecuteInEditMode]

    public class PositionRooms : MonoBehaviour
    {
        [SerializeField] private float radius;


        void Update()
        {
            if (!Application.isPlaying)
            {
            
                for (var index = 0; index < transform.childCount; index++)
                {
                    var angleInDegrees = index * (-360 / transform.childCount) + (-360 / transform.childCount/2);
                    float x = (float) (radius * Mathf.Cos((angleInDegrees + 90) * Mathf.PI / 180F));
                    float y = (float) (radius * Mathf.Sin((angleInDegrees + 90) * Mathf.PI / 180F));
                    transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                }
            }

        }

        void Awake()
        {
            for (var index = 0; index < transform.childCount; index++)
            {
                var angleInDegrees = index * (-360 / transform.childCount) + (-360 / transform.childCount/2);
                float x = (float)(radius * Mathf.Cos((angleInDegrees+90) * Mathf.PI / 180F));
                float y = (float)(radius * Mathf.Sin((angleInDegrees+90) * Mathf.PI / 180F));
                transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
            }
        }
    }
}

