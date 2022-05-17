using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using Tobii.Gaming;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
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
    
    
    [SerializeField] private MainMenuManager _mainMenuManager;
    public InteractionMethod Method
    {
        get => _interactionMethod;
        set => _interactionMethod = value;
    }

    
    public float DwellTime
    {
        get => _dwellTime;
        set => _dwellTime = value;
    }

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
}


