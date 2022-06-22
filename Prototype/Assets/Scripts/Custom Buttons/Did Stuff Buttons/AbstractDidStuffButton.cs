using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Managers;
using TMPro;
using Tobii.Gaming;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.Vector3;
using Image = UnityEngine.UI.Image;

namespace Custom_Buttons.Did_Stuff_Buttons
{
#if UNITY_EDITOR

	[CustomEditor(typeof(AbstractDidStuffButton), true)]
	public class DidStuffButtonEditor : Editor
	{
		private Sprite SpriteField(string name, Sprite sprite)
		{
			GUILayout.BeginVertical();
			var style = new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.UpperCenter,
				fixedWidth = 70
			};
			GUILayout.Label(name, style);
			var result = (Sprite) EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(70),
				GUILayout.Height(70));
			GUILayout.EndVertical();
			return result;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var abstractDidStuffButton = (AbstractDidStuffButton) target;
			abstractDidStuffButton.customHoverColours = GUILayout.Toggle(abstractDidStuffButton.customHoverColours,
				"Use Custom Hover Colours");
			abstractDidStuffButton.useInteractableLayer = GUILayout.Toggle(abstractDidStuffButton.useInteractableLayer,
				"Use Interactable Layer");
			abstractDidStuffButton.changeTextOrIconColour = GUILayout.Toggle(
				abstractDidStuffButton.changeTextOrIconColour, "Change the color of the text or icon on activation");
			abstractDidStuffButton.useIcon =
				GUILayout.Toggle(abstractDidStuffButton.useIcon, "Use an icon on the button");
			abstractDidStuffButton.useText = GUILayout.Toggle(abstractDidStuffButton.useText, "Use text on the button");
			abstractDidStuffButton.useSecondaryText = GUILayout.Toggle(abstractDidStuffButton.useSecondaryText, "Use secondary text on the button");
			abstractDidStuffButton.interactionSetting = GUILayout.Toggle(abstractDidStuffButton.interactionSetting, "Does this button set the interaction method");
			abstractDidStuffButton.dwellTimeSetting = GUILayout.Toggle(abstractDidStuffButton.dwellTimeSetting, "Does this button set dwell time");
			abstractDidStuffButton.changeTextToSameAsButton = GUILayout.Toggle(abstractDidStuffButton.changeTextToSameAsButton, "Do you want the buttons colours?");
			if (abstractDidStuffButton.useInteractableLayer)
			{
				abstractDidStuffButton.interactableLayer = EditorGUILayout.LayerField("Interactable Layer",
					abstractDidStuffButton.interactableLayer);
			}

			if (abstractDidStuffButton.customHoverColours)
			{
				abstractDidStuffButton.activeHoverColour = EditorGUILayout.ColorField("Active Hover Colour",
					abstractDidStuffButton.activeHoverColour);
				abstractDidStuffButton.inactiveHoverColour = EditorGUILayout.ColorField("Inactive Hover Colour",
					abstractDidStuffButton.inactiveHoverColour);
			}

			if (abstractDidStuffButton.changeTextOrIconColour)
			{
				if (!abstractDidStuffButton.changeTextToSameAsButton)
				{
				abstractDidStuffButton.activeTextOrIconColour = EditorGUILayout.ColorField("Active Text or Icon Colour",
					abstractDidStuffButton.activeTextOrIconColour);
				abstractDidStuffButton.inactiveTextOrIconColour =
					EditorGUILayout.ColorField("Inactive Text or Icon Colour",
						abstractDidStuffButton.inactiveTextOrIconColour);
				}
			}

			if (abstractDidStuffButton.useIcon)
				abstractDidStuffButton.iconImg = SpriteField("Icon Image", abstractDidStuffButton.iconImg);
			//else button.useIcon = EditorGUILayout.TextField("Button Text", button.text);
			if (abstractDidStuffButton.useText)
				abstractDidStuffButton.primaryText = EditorGUILayout.TextField("Button Text", abstractDidStuffButton.primaryText);
			if (abstractDidStuffButton.useSecondaryText)
				abstractDidStuffButton.secondaryText = EditorGUILayout.TextField("Button secondary Text", abstractDidStuffButton.secondaryText,  GUILayout.Height(100));
			if (abstractDidStuffButton.interactionSetting)
				abstractDidStuffButton.localInteractionMethod = (InteractionMethod)EditorGUILayout.EnumPopup("Local interaction method",
					abstractDidStuffButton.localInteractionMethod);
			if(abstractDidStuffButton.dwellTimeSetting)
				abstractDidStuffButton.localDwellTime = EditorGUILayout.FloatField("Local dwell time to set",
				abstractDidStuffButton.localDwellTime);






		}
	}

#endif

	public enum InteractionMethod
	{
		Mouse,
		MouseDwell,
		Tobii,
		Touch
	}
	
	public abstract class AbstractDidStuffButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		#region Fields

		public delegate void Clicked();

		public event Clicked OnClick;

		private delegate void Hovered();

		private event Hovered OnHover;

		private delegate void UnHovered();

		private event UnHovered OnUnHover;

		private delegate void Deactivated();

		private event Deactivated OnDeactivate;

		private delegate void Activated();

		private event Activated OnActivate;
		
		private delegate void RunInteractionMethod();
		private RunInteractionMethod RunInteraction;
		

		[SerializeField] private UnityEvent onClicked;
		
		[SerializeField]
		protected Color activeColour = Color.green, inactiveColour = Color.red, disabledColour = Color.grey;
		protected RectTransform _dwellGfx;
		[SerializeField] private bool startDisabled;
		
		[HideInInspector] public string primaryText, secondaryText;
		[HideInInspector] public bool useInteractableLayer;
		[HideInInspector] public LayerMask interactableLayer;
		[HideInInspector] public bool customHoverColours = false;
		

		[HideInInspector, Header("Custom Hover Colours")]
		public Color inactiveHoverColour, activeHoverColour;

		[HideInInspector] public bool changeTextOrIconColour = false;
		[HideInInspector] public bool changeTextToSameAsButton = false;
		[HideInInspector] public Color activeTextOrIconColour = Color.white, inactiveTextOrIconColour = Color.black;
		[HideInInspector] public bool useIcon, useText;
		[HideInInspector] public bool useSecondaryText = false;
		[HideInInspector] public Sprite iconImg;
		[HideInInspector] public bool interactionSetting;
		[HideInInspector] public bool dwellTimeSetting;
		[HideInInspector] public InteractionMethod localInteractionMethod;
		[HideInInspector] public float localDwellTime = 1.0f;

		private bool _mouseHover = false, _canHover = true;
		public bool _isActive = false;
		protected bool _isHover, _isDisabled;
		internal Image _mainImage;
		private Image _iconImage;
		private Image _dwellGfxImg;
		private bool _provideDwellFeedbackLocal = false;
		private static bool _provideDwellFeedbackGlobal;
		private TextMeshProUGUI _primaryText, _secondaryText;
		protected Camera MainCamera;
		internal static float _dwellTime = 1.0f;
		private static InteractionMethod _interactionMethod = InteractionMethod.Mouse;
		internal float _currentDwellTime = 0.0f;
		private bool _initialised;
		private bool _playActivatedScale;
		private bool _isSquare;
		[SerializeField] internal bool dwellScaleX = false;
		private bool _dwelling;
		internal float _originaldwellScaleY = 0;
		private float _targetX;
		private float _coolDownTime = 0.5f;
		private PointerEventData _pointerEventData;

		private Dictionary<InteractionMethod, float> interactionCooldownDictionary =
			new Dictionary<InteractionMethod, float>();
		#endregion

		protected float DwellTime
		{
			get => _dwellTime;
			set => _dwellTime = value;
		}

		public bool Initialised => _initialised;

		public static InteractionMethod GetInteractionMethod => _interactionMethod;

		public bool IsHover
		{
			get => _isHover;
			set => _isHover = value;
		}

		public bool IsDisabled => _isDisabled;

		public Image DwellGfxImg
		{
			get => _dwellGfxImg;
			set => _dwellGfxImg = value;
		}

		public float CoolDownTime
		{
			get => _coolDownTime;
			set => _coolDownTime = value;
		}

		protected void SetInteractionMethod(InteractionMethod method)
		{
			//DelegateInteractionMethod(false);
			
			InteractionData.Instance.Method = method;
			_interactionMethod = method;
			if (GetInteractionMethod ==InteractionMethod.Tobii ||
			    GetInteractionMethod == InteractionMethod.MouseDwell)
			{
				_provideDwellFeedbackGlobal = true;
			}
			else _provideDwellFeedbackGlobal = false;

			//DelegateInteractionMethod(true);
		}
		
		
		protected virtual void OnEnable()
		{
			OnClick += ButtonClicked;
			OnHover += ButtonHovered;
			OnUnHover += ButtonUnHovered;
			OnActivate += ActivateButton;
			OnDeactivate += DeactivateButton;
			
			if (DelegateInteractionMethod(true)) return;

			_initialised = false;

		}

		private bool DelegateInteractionMethod(bool enable) {
			
			return false;
		}

		protected void SetNewDwellTime()
		{
			PlayerPrefs.SetFloat("DwellTime", _dwellTime);
			InteractionData.Instance.DwellTime = _dwellTime;
		}
		
		protected virtual void Awake()
		{
			interactionCooldownDictionary.Add(InteractionMethod.Mouse, 0.1f);
			interactionCooldownDictionary.Add(InteractionMethod.MouseDwell, 1.0f);
			interactionCooldownDictionary.Add(InteractionMethod.Tobii, 1.0f);
			interactionCooldownDictionary.Add(InteractionMethod.Touch, 0.1f);
			_mainImage = GetComponent<Image>();
			MainCamera = Camera.main;
			if (!useInteractableLayer) interactableLayer = ~0;
			GetTheChildren();
			_originaldwellScaleY = _dwellGfx.localScale.y;
			if (!customHoverColours) SetAutomaticColours();
			else SetColours();
			
			SetScaleOfChildren();
			ToggleDwellGfx(false);
			ChangeToInactiveState();
			if(startDisabled) DisableButton();
		}
		
		protected virtual void Start()
		{
#if !UNITY_SERVER
			SetInteractionMethod(InteractionData.Instance.Method);
			if (GetInteractionMethod == InteractionMethod.Tobii ||
			    GetInteractionMethod == InteractionMethod.MouseDwell) _provideDwellFeedbackGlobal = true;
			else _provideDwellFeedbackGlobal = false;

			if (interactionSetting && (localInteractionMethod == InteractionMethod.Tobii ||
			                           localInteractionMethod == InteractionMethod.MouseDwell))
				_provideDwellFeedbackLocal = true;
			else _provideDwellFeedbackLocal = false;

			if (!interactionSetting) localInteractionMethod = _interactionMethod;
			DwellTime = InteractionData.Instance.DwellTime != 0.0f ? InteractionData.Instance.DwellTime : 1.0f;
			_currentDwellTime = dwellTimeSetting ? localDwellTime : _dwellTime;
			if (dwellScaleX) _dwellGfx.localScale = new Vector3(0.0f, _originaldwellScaleY, 1);
			else _dwellGfx.localScale = zero;
#endif
		}

		protected virtual void SetScaleOfChildren()
		{
			var rt = GetComponent<RectTransform>().rect;
			var w = rt.width;
			var h = rt.height;
			_dwellGfx.sizeDelta = new Vector2(w, h);
		}
		
		protected virtual void GetTheChildren()
		{
			
			if(GetComponentsInChildren<Image>().Where(r => r.CompareTag("ButtonIcon")).ToArray()[0] != null)
				_iconImage = GetComponentsInChildren<Image>().Where(r => r.CompareTag("ButtonIcon")).ToArray()[0];
			if(GetComponentsInChildren<RectTransform>().Where(r => r.CompareTag("DwellGfx")).ToArray()[0] != null)
				_dwellGfx = GetComponentsInChildren<RectTransform>().Where(r => r.CompareTag("DwellGfx")).ToArray()[0];
			if (GetComponentsInChildren<TextMeshProUGUI>().Where(r => r.CompareTag("ButtonPrimaryText")).ToArray()[0] !=
			    null)
				_primaryText = GetComponentsInChildren<TextMeshProUGUI>().Where(r => r.CompareTag("ButtonPrimaryText"))
					.ToArray()[0];
			if (GetComponentsInChildren<TextMeshProUGUI>().Where(r => r.CompareTag("ButtonSecondaryText")).ToArray()[0] !=
			    null)
				_secondaryText = GetComponentsInChildren<TextMeshProUGUI>().Where(r => r.CompareTag("ButtonSecondaryText"))
					.ToArray()[0];
			
			DwellGfxImg = _dwellGfx.GetComponent<Image>();
			
			if (useIcon) _iconImage.sprite = iconImg;
			else _iconImage.gameObject.SetActive(false);
			if (useText) _primaryText.text = primaryText;
			else _primaryText.transform.gameObject.SetActive(false);
			if (useSecondaryText) _secondaryText.text = secondaryText;
			else _secondaryText.transform.gameObject.SetActive(false);
		}

		
		protected virtual void Update() {
#if !UNITY_SERVER
			if(!interactionSetting)
			{
				
				switch (_interactionMethod)
						{
							case InteractionMethod.MouseDwell:
								DwellScale();
								break;
							case InteractionMethod.Mouse:
								MouseInput();
								break;
							case InteractionMethod.Tobii:
								//TobiiInput();
								DwellScale();
								break;
							case InteractionMethod.Touch:
								TouchInput();
								break;
					}
			}
			else
			{
				switch (localInteractionMethod)
				{
					case InteractionMethod.MouseDwell:
						DwellScale();
						break;
					case InteractionMethod.Mouse:
						MouseInput();
						break;
					case InteractionMethod.Tobii:
						//TobiiInput();
						DwellScale();
						break;
					case InteractionMethod.Touch:
						TouchInput();
						break;
				}
			}

			if (_playActivatedScale)
			{
				if (_dwellGfx.localScale.x > 0.0f && dwellScaleX)
					_dwellGfx.localScale -= new Vector3(0.01f, 0.0f, 0.0f);
				else if (_dwellGfx.localScale.x > 0.0f) _dwellGfx.localScale -= one * 0.01f;
				else ToggleDwellGfx(false);
			}
#endif
		}

		protected virtual void ButtonClicked()
		{
			ToggleButton(!_isActive);
			StartInteractionCoolDown();
			onClicked?.Invoke();
		}

		protected void ActivatedScaleFeedback()
		{
			// print("called");
			ToggleDwellGfx(true);
			_dwellGfx.localScale = dwellScaleX ? new Vector3(1,_originaldwellScaleY,1) : one;
			_playActivatedScale = true;
		}

		protected void ToggleDwellGfx(bool activate) {
			var color = DwellGfxImg.color;
			if (!activate) _playActivatedScale = false;
			DwellGfxImg.color = new Color(color.r, color.g, color.b,  activate ? 1 : 0);
			// _dwellGfx.transform.gameObject.SetActive(activate);
		}
		

		protected void ActivateText(bool activate) => _primaryText.gameObject.SetActive(activate);
		
		protected void InvokeOnClickUnityEvent() => onClicked?.Invoke();

		protected virtual void StartInteractionCoolDown()
		{
			IsHover = false;
			InteractionData.Instance.JustInteracted(this, interactionCooldownDictionary[_interactionMethod]);
		}

		private void ButtonHovered()
		{
			if (!_canHover) return;
			if (!Initialised) _initialised = true;
			if (!interactionSetting)
			{
				switch (GetInteractionMethod)
				{
					case InteractionMethod.MouseDwell:
						if(!_dwelling)_currentDwellTime = dwellTimeSetting ? localDwellTime : _dwellTime;
						ToggleDwellGfx(true);
						break;
					case InteractionMethod.Mouse:
						MouseHover();
						break;
					case InteractionMethod.Tobii:
						if(!_dwelling)_currentDwellTime = dwellTimeSetting ? localDwellTime : _dwellTime;
						ToggleDwellGfx(true);
						break;
					case InteractionMethod.Touch:
						break;
				}
			}
			else
				
			{
				switch (localInteractionMethod)
				{
					case InteractionMethod.MouseDwell:
						_currentDwellTime = dwellTimeSetting ? localDwellTime : _dwellTime;
						ToggleDwellGfx(true);
						break;
					case InteractionMethod.Mouse:
						MouseHover();
						break;
					case InteractionMethod.Tobii:
						_currentDwellTime = dwellTimeSetting ? localDwellTime : _dwellTime;
						ToggleDwellGfx(true);
						break;
					case InteractionMethod.Touch:
						break;
				}

			}
		}

		private void ButtonUnHovered()
		{
			if(!interactionSetting){switch (GetInteractionMethod)
			{
				case InteractionMethod.MouseDwell:
					break;
				case InteractionMethod.Mouse:
					MouseUnHover();
					break;
				case InteractionMethod.Tobii:
					break;
				case InteractionMethod.Touch:
					break;
			}}
			else
			{
				switch (localInteractionMethod)
				{
					case InteractionMethod.MouseDwell:
						break;
					case InteractionMethod.Mouse:
						MouseUnHover();
						break;
					case InteractionMethod.Tobii:
						break;
					case InteractionMethod.Touch:
						break;
				}
			}
		}

		public void SetCanHover(bool canHover)
		{
			_canHover = canHover;
		} 
		//Call this if you want to change the state of the button with no events being called. Like if you want to activate a DuoRhythmo drum node from the server.
		public virtual void ActivateButton() => ToggleButton(true);

		public void ClickAndCallEvents() => OnClick?.Invoke();

		public virtual void DeactivateButton() => ToggleButton(false);

		protected virtual void ToggleButton(bool activate)
		{
			if (activate) ChangeToActiveState();
			else ChangeToInactiveState();
		}

		protected virtual void ChangeToActiveState()
		{
			_isActive = true; 
			_mainImage.color = activeColour;
			DwellGfxImg.color = inactiveColour;
			if (useIcon && changeTextOrIconColour) _iconImage.color = changeTextToSameAsButton ? activeColour : activeTextOrIconColour;
			if (useText && changeTextOrIconColour) _primaryText.color = changeTextToSameAsButton ? activeColour : activeTextOrIconColour;
			if (useSecondaryText && changeTextOrIconColour) _secondaryText.color =changeTextToSameAsButton ? activeColour : activeTextOrIconColour;
			if (_pointerEventData == null) _pointerEventData = new PointerEventData(EventSystem.current);
			ExecuteEvents.Execute(gameObject, _pointerEventData, ExecuteEvents.pointerExitHandler);
		}

		protected virtual void ChangeToInactiveState()
		{
			_isActive = false;
			_mainImage.color = inactiveColour;
			DwellGfxImg.color = activeColour;
			if (useIcon && changeTextOrIconColour) _iconImage.color = changeTextToSameAsButton ? inactiveColour : inactiveTextOrIconColour;
			if (useText && changeTextOrIconColour) _primaryText.color = changeTextToSameAsButton ? inactiveColour : inactiveTextOrIconColour;
			if (useSecondaryText && changeTextOrIconColour) _secondaryText.color = changeTextToSameAsButton ? inactiveColour : inactiveTextOrIconColour;
		}
		
		protected virtual void ChangeToDisabledState()
		{
			_isActive = false;
			_mainImage.color = disabledColour;
			DwellGfxImg.color = activeColour;
			if (useIcon && changeTextOrIconColour) _iconImage.color = changeTextToSameAsButton ? inactiveColour : inactiveTextOrIconColour;
			if (useText && changeTextOrIconColour) _primaryText.color = changeTextToSameAsButton ? inactiveColour : inactiveTextOrIconColour;
			if (useSecondaryText && changeTextOrIconColour) _secondaryText.color = changeTextToSameAsButton ? inactiveColour : inactiveTextOrIconColour;
		}

		private void SetColours()
		{
			_mainImage.color = inactiveColour;
			DwellGfxImg.color = activeColour;
			if (useText) _primaryText.color = _secondaryText.color = inactiveColour;
			if (useIcon) _iconImage.color = inactiveColour;
		}

		public void SetActiveColoursExplicit(Color newActiveColor, Color newInactiveColor)
		{
			activeColour = newActiveColor;
			inactiveColour = newInactiveColor;
			SetAutomaticColours();
		}

		protected void SetTemporaryColor(Color col)
		{
			_mainImage.color = col;
		}

		public void SetText(string t)
		{
			_primaryText.text = t;
		}

		private void SetAutomaticColours()
		{
			Color.RGBToHSV(inactiveColour, out var uH, out var uS, out var uV);
			uV -= 0.3f;
			Color.RGBToHSV(activeColour, out var aH, out var aS, out var aV);
			aV -= 0.3f;

			inactiveHoverColour = Color.HSVToRGB(uH, uS, uV);
			activeHoverColour = Color.HSVToRGB(aH, aS, aV);

			inactiveHoverColour.a = 1;
			activeHoverColour.a = 1;
			_mainImage.color = inactiveColour;
			DwellGfxImg.color = activeColour;
		}
		

		#region MouseInteraction

		protected virtual void MouseInput()
		{
			if (IsHover && Input.GetMouseButtonUp(0)) OnClick?.Invoke();
		}

		private void MouseHover()
		{
			if (IsDisabled) return;
			_mainImage.color = _isActive ? activeHoverColour : inactiveHoverColour;
		}

		protected virtual void MouseUnHover()
		{
			_mainImage.color = _isActive ? activeColour : inactiveColour;
		}

		public void DisableButton()
		{
			_isDisabled = true;
			ChangeToDisabledState();
		}

		public void EnableButton()
		{
			_isDisabled = false;
			ChangeToInactiveState();
		}

		#endregion
		
		#region Dwell
		
		protected virtual void DwellScale()
		{
			if (IsDisabled) return;
			if ((!_provideDwellFeedbackGlobal && !interactionSetting) || !_canHover) return;
			_dwelling = true;
			var d = dwellTimeSetting ? localDwellTime : _dwellTime;
			if(!_dwellGfx.gameObject.activeInHierarchy) ToggleDwellGfx(true);
			if (IsHover && _currentDwellTime > 0) _currentDwellTime -= Time.deltaTime;
			else if(IsHover &&_currentDwellTime <= 0) DwellActivated();
			else if (!IsHover && _currentDwellTime < d) _currentDwellTime += Time.deltaTime;
			if ((_currentDwellTime < d && _currentDwellTime > 0))
			{
				var size = Map(_currentDwellTime, 0f, d, 0f, 1f);
				if(dwellScaleX) _dwellGfx.localScale = one - new Vector3(size,0,0);
				else _dwellGfx.localScale = one - new Vector3(size,size,size);
			}
			if (_currentDwellTime > d)
			{
				_dwelling = false;
				ToggleDwellGfx(false);
			}
		}

		private float Map(float value, float min1, float max1, float min2, float max2) {
			return min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
		}

		
		protected virtual void DwellActivated()
		{
			StartInteractionCoolDown();
			_currentDwellTime = dwellTimeSetting ? localDwellTime : _dwellTime;
			_dwellGfx.localScale = dwellScaleX ? new Vector3(0, _originaldwellScaleY, 1) : zero;
			DwellGfxImg.color = !_isActive ? inactiveColour : activeColour;
			ToggleDwellGfx(false);
			OnClick?.Invoke();
		}
		
			
		#endregion
		

		#region TouchInteraction

		private void TouchInput()
		{
			if (IsDisabled) return;
			if (Input.touchCount <= 0) return;
			var touch = Input.GetTouch(0);
			if (!Physics.Raycast(MainCamera.ScreenToWorldPoint(touch.position), forward, out var hit,
				interactableLayer)) return;
			if (hit.transform == this.transform)
			{
				OnClick?.Invoke();
			}
		}

		#endregion

		#region MouseOverEvents

		private void OnMouseOver()
		{
			if (IsDisabled) return;
			if(!_canHover) return;
			IsHover = true;
			OnHover?.Invoke();
		}

		private void OnMouseExit()
		{
			if (IsDisabled) return;
			IsHover = false;
			OnUnHover?.Invoke();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log("Entered on " + name + " with pointer id "+ eventData.pointerId);
			_pointerEventData = eventData;
			if(IsHover) return;
			if (interactionSetting)
			{
				if ((eventData.pointerId == 1 && localInteractionMethod != InteractionMethod.Tobii) ||
				    (eventData.pointerId < 0 && localInteractionMethod == InteractionMethod.Tobii))
					return;
			}
			
			if (_isDisabled) return;
			if(!_canHover) return;
			_isHover = true;
			OnHover?.Invoke();
		}
  
		public void OnPointerExit(PointerEventData eventData)
		{
			Debug.Log("Exited from " + name + " with pointer id "+ eventData.pointerId);
			_pointerEventData = eventData;
			if (interactionSetting)
			{
				if ((eventData.pointerId == 1 && localInteractionMethod != InteractionMethod.Tobii) ||
				    (eventData.pointerId < 0 && localInteractionMethod == InteractionMethod.Tobii))
					return;
			}

			if (IsDisabled) return;
			IsHover = false;
			
			OnUnHover?.Invoke();
		}
		#endregion
		
		
		protected virtual void OnDisable()
		{
			_isHover = false;
			OnClick -= ButtonClicked;
			OnHover -= ButtonHovered;
			OnUnHover -= ButtonUnHovered;
			OnActivate -= ActivateButton;
			OnDeactivate -= DeactivateButton;

			DelegateInteractionMethod(false);
		}
		
		static void ShowMessage(string message,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string caller = null)
		{
			Debug.Log(message + " at line " + lineNumber + " (" + caller + ")");
		}
	}
}