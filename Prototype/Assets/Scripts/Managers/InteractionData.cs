using System;
using System.Collections;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InteractionData : MonoBehaviour
    {
        private Vector2 _inputPosition = Vector2.zero;
        private InteractionMethod _interactionMethod = 0;
        private float _dwellTime = 1f;
        private float DwellTimeFromPlayerPrefs
        {
            get => PlayerPrefs.GetFloat("DwellTime");
            set => PlayerPrefs.SetFloat("DwellTime", value);
        }

        private InteractionMethod MethodFromPlayerPrefs
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

        public InteractionMethod Method
        {
            get => _interactionMethod;
            set
            {
                _interactionMethod = value;
                MethodFromPlayerPrefs = value;
                if (value == InteractionMethod.MouseDwell || value == InteractionMethod.Tobii)
                    FindObjectOfType<InteractionManager>().SetSignifierActive(true);
                else
                    FindObjectOfType<InteractionManager>().SetSignifierActive(false);
            }
        }

        public void CheckInteractionMethod()
        {
            if (_interactionMethod == InteractionMethod.MouseDwell || _interactionMethod == InteractionMethod.Tobii)
                FindObjectOfType<InteractionManager>().SetSignifierActive(true);
            else
                FindObjectOfType<InteractionManager>().SetSignifierActive(false);
        }

        public Vector2 InputPosition
        {
            get => _inputPosition;
            set => _inputPosition = value;
        }
        public static InteractionData Instance { get; private set; }
        private void Awake()
        {
            // If there is an instance, and it's not me, kill myself.
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
            _dwellTime = DwellTimeFromPlayerPrefs;
            _dwellTime = _dwellTime <= 0 ? 1 : _dwellTime;
            _interactionMethod = MethodFromPlayerPrefs;
            DontDestroyOnLoad(this);
        }
        
        public void JustInteracted(AbstractDidStuffButton btn, float coolDownTime) => StartCoroutine(CoolDownTime(btn, coolDownTime));

        private IEnumerator CoolDownTime(AbstractDidStuffButton btn, float coolDownTime)
        {
            btn.SetCanHover(false);
            btn.IsHover = false;
            yield return new WaitForSeconds(coolDownTime);
            ExecuteEvents.Execute (btn.gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerExitHandler);
            btn.SetCanHover(true);
        }

        
    }
}
