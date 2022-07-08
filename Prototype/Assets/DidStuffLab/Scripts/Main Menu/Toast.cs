using TMPro;
using UnityEngine;

namespace DidStuffLab.Scripts.Main_Menu
{
    public class Toast : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetText(string t) => text.text = t;
    }
}
