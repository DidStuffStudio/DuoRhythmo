using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{

    private List<RaycastResult> result = new List<RaycastResult>();
    [SerializeField]  GraphicRaycaster m_Raycaster;
    private static PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    private static GraphicRaycaster tobiiRay;
    private AbstractDidStuffButton _lastHitButton;
    [SerializeField] private RectTransform gazeSignifier;
    private bool _activateTobiiRay = true;
    private bool _interact;
    [SerializeField] private GameObject inGamePersistantUi, mainMenuPersistantUi;
    
    private static InteractionManager _instance;

    public static InteractionManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            return _instance;
        }
    }
    


    [SerializeField] private MainMenuManager _mainMenuManager;
    private Vector2 _inputPosition = Vector2.zero;
    public InteractionMethod Method
    {
        get => (InteractionMethod)PlayerPrefs.GetInt("InteractionMethod");
        set
        {
            _interactionMethod = value;
            if(value == InteractionMethod.Tobii || value == InteractionMethod.MouseDwell) gazeSignifier.gameObject.SetActive(true);
            else gazeSignifier.gameObject.SetActive(false);
            PlayerPrefs.SetInt("InteractionMethod", (int)value);
        }
    }


    public float DwellTime
    {
        get => PlayerPrefs.GetFloat("DwellTime");
        set => _dwellTime = value;
    }

    public Vector2 InputPosition => _inputPosition;

    public bool ActivateTobiiRay
    {
        get => _activateTobiiRay;
        set => _activateTobiiRay = value;
    }

    public bool Interact
    {
        get => _interact;
        set => _interact = value;
    }

    private  InteractionMethod _interactionMethod;
    private float _dwellTime = 1.0f;
    [SerializeField] private float signifierSpeed = 1.0f;

    private void Awake() {
        if (_instance == null) _instance = this;
        
        var i = PlayerPrefs.GetInt("InteractionMethod");
        Method = (InteractionMethod)i;
        DwellTime = PlayerPrefs.GetFloat("DwellTime");
        if (DwellTime < 0) DwellTime = 1.0f;
        if(_interactionMethod == InteractionMethod.Tobii && !TobiiAPI.IsConnected)
        {
            _mainMenuManager.SendToInteractionPage();
            _interactionMethod = InteractionMethod.MouseDwell;
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
            if (!_interact) return;
            switch (Method)
            {
                case InteractionMethod.Mouse:
                    _inputPosition = Input.mousePosition;
                    break;
                case InteractionMethod.MouseDwell:
                    _inputPosition = Input.mousePosition;
                    signifierFollowInputPosition();
                    break;
                case InteractionMethod.Tobii:
                    if (TobiiAPI.IsConnected)
                    {
                        _inputPosition = TobiiAPI.GetGazePoint().Screen;
                        TobiiGraphicRaycast(_inputPosition);
                    } 
                    break;
            }
            if(_activateTobiiRay) TobiiGraphicRaycast(_inputPosition);
        }

        private void TobiiGraphicRaycast(Vector3 pos)
        {
            //if (!TobiiAPI.IsConnected) return;
            
            
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to mouse position (Change to tobii)
            m_PointerEventData.position = pos;
            m_PointerEventData.pointerId = 1;
            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            if (results.Count > 0)
            {
                Debug.Log("Hit " + results[0].gameObject.name);
                var btn = result[0].gameObject.GetComponent<AbstractDidStuffButton>();
                if (btn != null)
                {
                    _lastHitButton = btn;
                    ExecuteEvents.Execute (btn.gameObject,  m_PointerEventData, ExecuteEvents.pointerEnterHandler);
                }
            }
            else if (_lastHitButton != null)
            {
                ExecuteEvents.Execute (_lastHitButton.gameObject,  m_PointerEventData, ExecuteEvents.pointerExitHandler);
            }
        } // Raycast to the main menu in the scene //settings and back button need a raycast too

        public void SetNewGraphicsRaycaster(GraphicRaycaster raycaster)
        {
            m_Raycaster = raycaster;
            _interact = true;
        }

        public void SwitchSceneInteraction(int sceneLoading)
        {
            if (sceneLoading == 0)
            {
                _interact = false;
                ToggleInGamePersistantUI(false);
                ToggleMainMenuPersistantUI(true);
            }
            else
            {
                _interact = false;
                ToggleInGamePersistantUI(true);
                ToggleMainMenuPersistantUI(false);
            }
        }
        private void ToggleInGamePersistantUI(bool active) => inGamePersistantUi.SetActive(active);
        private void ToggleMainMenuPersistantUI(bool active) => mainMenuPersistantUi.SetActive(active);


        private void signifierFollowInputPosition()
        {
            var pos = Vector3.Lerp(gazeSignifier.position, _inputPosition, Time.deltaTime * signifierSpeed);

            gazeSignifier.position = pos;
        }
       
 
}


