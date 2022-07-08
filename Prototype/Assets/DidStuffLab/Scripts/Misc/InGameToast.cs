using System.Collections;
using TMPro;
using UnityEngine;

namespace DidStuffLab.Scripts.Misc
{
    public class InGameToast : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float toastLength = 5.0f;
        private RectTransform _rt;

        public void SetText(string t)
        {
            text.text = t;
            SetToastWidth();
        }

        private void SetToastWidth()
        {
            _rt = GetComponent<RectTransform>();
            if (text.preferredWidth > _rt.rect.width - 20)
                _rt.sizeDelta = new Vector2(text.preferredWidth + 20, _rt.rect.height);
        
        }

        private void Start()
        {
            StartCoroutine(DelayedFadeOut());
        }

        IEnumerator DelayedFadeOut()
        {
            yield return new WaitForSeconds(toastLength);
            DestroyToast();
        }

        private void DestroyToast() => Destroy(gameObject);
    }
}
