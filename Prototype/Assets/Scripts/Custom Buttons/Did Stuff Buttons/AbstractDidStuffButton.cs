using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AK.Wwise;
using TMPro;
using Tobii.Gaming;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static UnityEngine.Vector3;
using Image = UnityEngine.UI.Image;

namespace Custom_Buttons.Did_Stuff_Buttons
{
#if UNITY_EDITOR

	[CustomEditor(typeof(AbstractDidStuffButton), true)]
	public class DidStuffButtonEditor : UnityEditor.Editor
	{
		private static Sprite SpriteField(string name, Sprite sprite)
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
				abstractDidStuffButton.activeTextOrIconColour = EditorGUILayout.ColorField("Active Text or Icon Colour",
					abstractDidStuffButton.activeTextOrIconColour);
				abstractDidStuffButton.inactiveTextOrIconColour =
					EditorGUILayout.ColorField("Inactive Text or Icon Colour",
						abstractDidStuffButton.inactiveTextOrIconColour);
			}

			if (abstractDidStuffButton.useIcon)
				abstractDidStuffButton.iconImg = SpriteField("Icon Image", abstractDidStuffButton.iconImg);
			//else button.useIcon = EditorGUILayout.TextField("Button Text", button.text);
			if (abstractDidStuffButton.useText)
				abstractDidStuffButton.text = EditorGUILayout.TextField("Button Text", abstractDidStuffButton.text);
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

		private delegate void Clicked();

		private event Clicked OnClick;

		private delegate void Hovered();

		private event Hovered OnHover;

		private delegate void UnHovered();

		private event UnHovered OnUnHover;

		private delegate void Deactivated();

		private event Deactivated OnDeactivate;

		private delegate void Activated();

		private event Activated OnActivate;
		

		[SerializeField] private UnityEvent onClicked;
		public bool interactionSetting;
		[SerializeField]
		private Color activeColour = Color.green, inactiveColour = Color.red, disabledColour = Color.grey;
		private RectTransform _dwellGfx;
		
		[HideInInspector] public string text;
		[HideInInspector] public bool useInteractableLayer;
		[HideInInspector] public LayerMask interactableLayer;
		[HideInInspector] public bool customHoverColours = false;

		[HideInInspector, Header("Custom Hover Colours")]
		public Color inactiveHoverColour, activeHoverColour;

		[HideInInspector] public bool changeTextOrIconColour = false;
		[HideInInspector] public Color activeTextOrIconColour = Color.white, inactiveTextOrIconColour = Color.black;
		[HideInInspector] public bool useIcon, useText;
		[HideInInspector] public Sprite iconImg;

		private bool _mouseHover = false, _canHover = true;
		protected bool _isActive, _isInactive = true, _isHover, _isDisabled;
		private Image _mainImage;
		private Image _iconImage;
		private Image _dwellGfxImg;
		private bool _provideDwellFeedback = false;
		private TextMeshProUGUI _text;
		private Animator _dwellAnimator;
		private float _interactionBreakTime = 1.0f;
		private GazeAware _gazeAware;
		private Camera _mainCamera;
		private static float _dwellTime = 100.0f;
		private static readonly int DwellSpeed = Animator.StringToHash("DwellSpeed");
		private static InteractionMethod _interactionMethod = InteractionMethod.Mouse;
		[SerializeField] protected InteractionMethod localInteractionMethod; // Used for buttons which switch interaction methods
		//private List<SpriteRenderer> _spriteRenderers;
		
		
		#endregion
		
		public float DwellTime
		{
			get => _dwellTime;
			set => _dwellTime = value;
		}

		protected void SetInteractionMethod(InteractionMethod method) => _interactionMethod = method;
		
		protected virtual void OnEnable()
		{
			OnClick += ButtonClicked;
			OnHover += ButtonHovered;
			OnUnHover += ButtonUnHovered;
			OnActivate += ActivateButton;
			OnDeactivate += DeactivateButton;
			
			DeactivateButton();
		}
		public static float dwellTime = 1.0f;
		private float _currentDwellTime = dwellTime;

		protected void SetNewDwellTime()
		{
			PlayerPrefs.SetFloat("DwellTime", _dwellTime);
		}
		
		private void Awake()
		{
			//_spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
			_mainImage = GetComponent<Image>();
			//_dwellFeedback = _spriteRenderers[0];
			_text = GetComponentInChildren<TextMeshProUGUI>();
			_dwellAnimator = GetComponentInChildren<Animator>();
			_gazeAware = GetComponent<GazeAware>();
			_mainCamera = Camera.main;
			if (!useInteractableLayer) interactableLayer = ~0;
			//if (useIcon) _spriteRenderers[1].sprite = iconImg;
			_iconImage = GetComponentsInChildren<Image>().Where(r => r.CompareTag("ButtonIcon")).ToArray()[0];
			if (useIcon) _iconImage.sprite = iconImg;
			else _iconImage.gameObject.SetActive(false);
			if (useText) _text.text = text;
			else _text.transform.gameObject.SetActive(false);
			_dwellGfx = GetComponentsInChildren<RectTransform>().Where(r => r.CompareTag("DwellGfx")).ToArray()[0];
			_dwellGfxImg = _dwellGfx.GetComponent<Image>();
			if (!customHoverColours) SetAutomaticColours();
			else SetColours();
			ToggleDwellGfx(false);
			if (localInteractionMethod == InteractionMethod.Tobii ||
			    localInteractionMethod == InteractionMethod.MouseDwell) _provideDwellFeedback = true;
			
			if (_interactionMethod == InteractionMethod.Tobii ||
			    _interactionMethod == InteractionMethod.MouseDwell) _provideDwellFeedback = true;
		}
		
		protected virtual void Update()
		{
			if (!interactionSetting)
			{
				switch (_interactionMethod)
				{
					case InteractionMethod.MouseDwell:
						break;
					case InteractionMethod.Mouse:
						MouseInput();
						break;
					case InteractionMethod.Tobii:
						TobiiInput();
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
						break;
					case InteractionMethod.Mouse:
						MouseInput();
						break;
					case InteractionMethod.Tobii:
						TobiiInput();
						break;
					case InteractionMethod.Touch:
						TouchInput();
						break;
				}
			}
			
			if(_canHover && _provideDwellFeedback)DwellScale();
		}

		protected virtual void ButtonClicked()
		{
			ToggleButton(!_isActive);
			onClicked?.Invoke();
			StartInteractionCoolDown();
		}


		private void DwellScale()
		{
			if(!_dwellGfx.gameObject.activeInHierarchy) ToggleDwellGfx(true);
			if (_isHover && _currentDwellTime > 0) _currentDwellTime -= Time.deltaTime;
			else if(_isHover &&_currentDwellTime <= 0) DwellActivated();
			else if (!_isHover && _currentDwellTime < dwellTime) _currentDwellTime += Time.deltaTime;
			if(_isHover||(_currentDwellTime < 1 && _currentDwellTime > 0))
				_dwellGfx.localScale = one - new Vector3(_currentDwellTime, _currentDwellTime, _currentDwellTime);
		}

		private void DwellActivated()
		{
		
			StartCoroutine(CoolDownTime());
			_currentDwellTime = dwellTime;
			_dwellGfx.localScale = zero;
			if (!_isActive) _dwellGfxImg.color = inactiveColour;
			else _dwellGfxImg.color = activeColour;
			ToggleDwellGfx(false);
			OnClick?.Invoke();
		}

		private void ToggleDwellGfx(bool activate) => _dwellGfx.transform.gameObject.SetActive(activate);
		
		
		
		protected void InvokeOnClickUnityEvent() => onClicked?.Invoke();

		protected virtual void StartInteractionCoolDown()
		{
			if (_interactionMethod == InteractionMethod.Tobii || _interactionMethod == InteractionMethod.MouseDwell)
				StartCoroutine(CoolDownTime());
		}

		protected void PlayDwellBackwards()
		{
			_dwellAnimator.playbackTime = 1.0f;
			_dwellAnimator.SetFloat(DwellSpeed, -1.0f/_dwellTime);
			_dwellAnimator.Play("NodeDwell");
		}

		private void ButtonHovered()
		{
			if (!_canHover) return;
			if (!interactionSetting)
			{
				switch (_interactionMethod)
				{
					case InteractionMethod.MouseDwell:
						//FillDwellFeedback(1 / _dwellTime);
						_currentDwellTime = dwellTime;
						ToggleDwellGfx(true);
						break;
					case InteractionMethod.Mouse:
						MouseHover();
						break;
					case InteractionMethod.Tobii:
						//FillDwellFeedback(1 / _dwellTime);
						_currentDwellTime = dwellTime;
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
						//FillDwellFeedback(1 / _dwellTime);
						_currentDwellTime = dwellTime;
						ToggleDwellGfx(true);
						break;
					case InteractionMethod.Mouse:
						MouseHover();
						break;
					case InteractionMethod.Tobii:
						_currentDwellTime = dwellTime;
						ToggleDwellGfx(true);
						//FillDwellFeedback(1 / _dwellTime);
						break;
					case InteractionMethod.Touch:
						break;
				}

			}
		}

		private void ButtonUnHovered()
		{
			if(!interactionSetting){switch (_interactionMethod)
			{
				case InteractionMethod.MouseDwell:
					//FillDwellFeedback(-1/_dwellTime);
					break;
				case InteractionMethod.Mouse:
					MouseUnHover();
					break;
				case InteractionMethod.Tobii:
					//FillDwellFeedback(-1/_dwellTime);
					break;
				case InteractionMethod.Touch:
					break;
			}}
			else
			{
				switch (localInteractionMethod)
				{
					case InteractionMethod.MouseDwell:
						DwellScale();
						//FillDwellFeedback(-1/_dwellTime);
						break;
					case InteractionMethod.Mouse:
						MouseUnHover();
						break;
					case InteractionMethod.Tobii:
						DwellScale();
						//FillDwellFeedback(-1/_dwellTime);
						break;
					case InteractionMethod.Touch:
						break;
				}
			}
		}

		protected void SetCanHover(bool canHover) => _canHover = canHover;
		protected void ActivateButton() => ToggleButton(true);

		protected void DeactivateButton() => ToggleButton(false);

		private void ToggleButton(bool activate)
		{
			if (activate)
			{
				_isActive = true;
				_isInactive = false;
				_mainImage.color = activeColour;
				_dwellGfxImg.color = inactiveColour;
				if (useIcon && changeTextOrIconColour) _iconImage.color = activeTextOrIconColour;
				if (useText && changeTextOrIconColour) _text.color = activeTextOrIconColour;
			}

			else
			{
				_isActive = false;
				_isInactive = true;
				_mainImage.color = inactiveColour;
				_dwellGfxImg.color = activeColour;
				if (useIcon && changeTextOrIconColour) _iconImage.color = inactiveTextOrIconColour;
				if (useText && changeTextOrIconColour) _text.color = inactiveTextOrIconColour;
			}
		}

		private void SetColours()
		{
			_mainImage.color = inactiveColour;
			_dwellGfxImg.color = activeColour;
			if (useText) _text.color = inactiveColour;
			if (useIcon) _iconImage.color = inactiveColour;
		}

		public void SetActiveColoursExplicit(Color color)
		{
			activeColour = color;
			SetAutomaticColours();
		}

		public void SetText(string t)
		{
			_text.text = t;
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
			_dwellGfxImg.color = activeColour;
		}

		protected void ToggleHoverable(bool canHover) => _canHover = canHover;


		#region MouseInteraction

		private void MouseInput()
		{
			if (_isHover && Input.GetMouseButtonDown(0)) OnClick?.Invoke();
		}

		private void MouseHover()
		{
			_mainImage.color = _isActive ? activeHoverColour : inactiveHoverColour;
		}

		private void MouseUnHover()
		{
			_mainImage.color = _isActive ? activeColour : inactiveColour;
		}

		#endregion

		#region MouseDwellInteraction

		protected virtual void FillDwellFeedback(float speed)
		{
			_dwellAnimator.SetFloat(DwellSpeed, speed);
			_dwellAnimator.Play("NodeDwell");
		}

		public void DwellFilled() => OnClick?.Invoke();

		#endregion

		#region TobiiInteraction

		private void TobiiInput()
		{
			//if (!TobiiAPI.IsConnected) return;
			if (_gazeAware.HasGazeFocus)
			{
				_isHover = true;
				OnHover?.Invoke();
			}
			else
			{
				_isHover = false;
				OnUnHover?.Invoke();
			}
		}

		#endregion

		#region TouchInteraction

		private void TouchInput()
		{
			if (Input.touchCount <= 0) return;
			var touch = Input.GetTouch(0);
			if (!Physics.Raycast(_mainCamera.ScreenToWorldPoint(touch.position), forward, out var hit,
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
			if(!_canHover) return;
			_isHover = true;
			OnHover?.Invoke();
		}

		private void OnMouseExit()
		{
			_isHover = false;
			OnUnHover?.Invoke();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if(!_canHover) return;
			_isHover = true;
			OnHover?.Invoke();
		}
  
		public void OnPointerExit(PointerEventData eventData)
		{
			_isHover = false;
			OnUnHover?.Invoke();
		}
		#endregion


		private IEnumerator CoolDownTime()
		{
			_canHover = false;
			yield return new WaitForSeconds(_interactionBreakTime);
			_canHover = true;

		}

		private void OnDisable()
		{
			OnClick -= ButtonClicked;
			OnHover -= ButtonHovered;
			OnUnHover -= ButtonUnHovered;
			OnActivate -= ActivateButton;
			OnDeactivate -= DeactivateButton;
		}
	}
}