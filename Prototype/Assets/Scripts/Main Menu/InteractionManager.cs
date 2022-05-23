using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using Tobii.Gaming;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private delegate void TobiiEntered();
    private event TobiiEntered OnTobiiGazeEnter;
    private delegate void TobiiExited();
    private event TobiiExited OnTobiiGazeExit;
    private static InteractionManager _instance;

    public static InteractionManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            var InteractionManagerGameObject = new GameObject();
            _instance = InteractionManagerGameObject.AddComponent<InteractionManager>();
            InteractionManagerGameObject.name = typeof(InteractionManager).ToString();
            return _instance;
        }
    }

    private void OnEnable()
    {
        OnTobiiGazeEnter += TobiiHovered;
        OnTobiiGazeExit += TobiiUnhovered;
    }


    [SerializeField] private MainMenuManager _mainMenuManager;
    private Vector2 _inputPosition = Vector2.zero;
    public InteractionMethod Method
    {
        get => (InteractionMethod)PlayerPrefs.GetInt("InteractionMethod");
        set => _interactionMethod = value;
    }

    
    public float DwellTime
    {
        get => PlayerPrefs.GetFloat("DwellTime");
        set => _dwellTime = value;
    }

    public Vector2 InputPosition => _inputPosition;

    private  InteractionMethod _interactionMethod;
    private float _dwellTime = 1.0f;
        private void Awake() {
        if (_instance == null) _instance = this;
        
        var i = PlayerPrefs.GetInt("InteractionMethod");
        Method = (InteractionMethod)i;
        DwellTime = PlayerPrefs.GetFloat("DwellTime");
        if (DwellTime < 0) DwellTime = 1.0f;
        if(_interactionMethod == InteractionMethod.Tobii && !TobiiAPI.IsConnected)
        {
            _mainMenuManager.SendToInteractionPage();
        }
        DontDestroyOnLoad(this);
        }

        public void JustInteracted(AbstractDidStuffButton btn, float coolDownTime) => StartCoroutine(CoolDownTime(btn, coolDownTime));

        private IEnumerator CoolDownTime(AbstractDidStuffButton btn, float coolDownTime)
        {
            btn.SetCanHover(false);
            btn.IsHover = false;
            yield return new WaitForSeconds(coolDownTime);
            btn.SetCanHover(true);
        }

        private void Update()
        {
            switch (Method)
            {
                case InteractionMethod.Mouse:
                    _inputPosition = Input.mousePosition;
                    break;
                case InteractionMethod.MouseDwell:
                    _inputPosition = Input.mousePosition;
                    break;
                case InteractionMethod.Tobii:
                    _inputPosition = TobiiAPI.GetGazePoint().Screen;
                    break;
            }
        }
        
        private void TobiiHovered(){}
        private void TobiiUnhovered(){}
        
        private void OnDisable()
        {
            OnTobiiGazeEnter -= TobiiHovered;
            OnTobiiGazeExit -= TobiiUnhovered;
        }
}


