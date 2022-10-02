using System.Collections;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DidStuffLab.Scripts.Managers
{
    public class InteractionData : MonoBehaviour
    {
        private float _dwellTime = 1f;
        private InteractionMethod _interactionMethod;
        private static float DwellTimeFromPlayerPrefs
        {
            get => PlayerPrefs.GetFloat("DwellTime");
            set => PlayerPrefs.SetFloat("DwellTime", value);
        }

        private static InteractionMethod MethodFromPlayerPrefs
        {
            get => (InteractionMethod)PlayerPrefs.GetInt("InteractionMethod");
            set => PlayerPrefs.SetInt("InteractionMethod", (int)value);
        }
        
        public float DwellTime
        {
            get => _dwellTime;
            set
            {
                _dwellTime = value;
                DwellTimeFromPlayerPrefs = value;
            }
        }

        public InteractionMethod interactionMethod
        {
            get => _interactionMethod;
            set
            {
                _interactionMethod = value;
                MethodFromPlayerPrefs = value;
                CheckInteractionMethod(value);
            }
        }
        

        public void CheckInteractionMethod(InteractionMethod method)
        {
            if (method == InteractionMethod.MouseDwell || method == InteractionMethod.Tobii)
            {
                FindObjectOfType<InteractionManager>().SetSignifierActive(true);
            }
            else
            {
                FindObjectOfType<InteractionManager>().SetSignifierActive(false);
            }
        }

        public Vector2 InputPosition { get; set; } = Vector2.zero;

        public static InteractionData Instance { get; private set; }
        private void Awake()
        {
            // If there is an instance, and it's not me, kill myself.
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
            _dwellTime = DwellTimeFromPlayerPrefs;
            _dwellTime = _dwellTime <= 0 ? 1 : _dwellTime;
            interactionMethod = MethodFromPlayerPrefs;
            DontDestroyOnLoad(this);
        }
        
        public void JustInteracted(AbstractDidStuffButton btn, float coolDownTime, PointerEventData p) => StartCoroutine(CoolDownTime(btn, coolDownTime, p));

        private IEnumerator CoolDownTime(AbstractDidStuffButton btn, float coolDownTime, PointerEventData p)
        {
            btn.SetCanHover(false);
            btn.IsHover = false;
            yield return new WaitForSeconds(coolDownTime);
            ExecuteEvents.Execute (btn.gameObject, p, ExecuteEvents.pointerExitHandler);
            p.pointerEnter = null;
            btn.SetCanHover(true);
        }

        
    }
}
