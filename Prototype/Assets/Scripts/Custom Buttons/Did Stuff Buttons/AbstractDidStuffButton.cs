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


	public abstract class AbstractDidStuffButton : MonoBehaviour, IDidStuffButton
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
		[SerializeField] private bool interactionSetting;
		[SerializeField]
		private Color activeColour = Color.green, inactiveColour = Color.red, disabledColour = Color.grey;

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
		private SpriteRenderer _dwellFeedback;
		private TextMeshPro _text;
		private Animator _dwellAnimator;
		private float _interactionBreakTime = 1.0f;
		private GazeAware _gazeAware;
		private Camera _mainCamera;
		private static float _dwellTime = 1.0f;
		private static readonly int DwellSpeed = Animator.StringToHash("DwellSpeed");
		private static InteractionMethod _interactionMethod;
		[SerializeField] protected InteractionMethod localInteractionMethod; // Used for buttons which switch interaction methods
		private List<SpriteRenderer> _spriteRenderers;

		#endregion
		
		public float DwellTime
		{
			get => _dwellTime;
			set => _dwellTime = value;
		}
		
		protected void SetInteractionMethod(InteractionMethod method) => _interactionMethod = method;

		private void OnEnable()
		{
			OnClick += ButtonClicked;
			OnHover += ButtonHovered;
			OnUnHover += ButtonUnHovered;
			OnActivate += ActivateButton;
			OnDeactivate += DeactivateButton;
		}

		protected void SetNewDwellTime()
		{
			PlayerPrefs.SetFloat("DwellTime", _dwellTime);
		}
		
		private void Awake()
		{
			_spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
			_mainImage = GetComponent<Image>();
			_dwellFeedback = _spriteRenderers[0];
			_text = GetComponentInChildren<TextMeshPro>();
			_dwellAnimator = GetComponentInChildren<Animator>();
			_gazeAware = GetComponent<GazeAware>();
			_mainCamera = Camera.main;
			if (!useInteractableLayer) interactableLayer = ~0;
			if (useIcon) _spriteRenderers[1].sprite = iconImg;
			else  _spriteRenderers[1].transform.gameObject.SetActive(false);
			if (useText) _text.text = text;
			else _text.transform.gameObject.SetActive(false);
			if (!customHoverColours) SetAutomaticColours();
			else SetColours();

			_interactionMethod = InteractionMethod.MouseDwell;
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
		}

		protected virtual void ButtonClicked()
		{
			ToggleButton(!_isActive);
			onClicked?.Invoke();
			StartInteractionCoolDown();
		}

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
						FillDwellFeedback(1 / _dwellTime);
						break;
					case InteractionMethod.Mouse:
						MouseHover();
						break;
					case InteractionMethod.Tobii:
						FillDwellFeedback(1 / _dwellTime);
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
						FillDwellFeedback(1 / _dwellTime);
						break;
					case InteractionMethod.Mouse:
						MouseHover();
						break;
					case InteractionMethod.Tobii:
						FillDwellFeedback(1 / _dwellTime);
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
					FillDwellFeedback(-1/_dwellTime);
					break;
				case InteractionMethod.Mouse:
					MouseUnHover();
					break;
				case InteractionMethod.Tobii:
					FillDwellFeedback(-1/_dwellTime);
					break;
				case InteractionMethod.Touch:
					break;
			}}
			else
			{
				switch (localInteractionMethod)
				{
					case InteractionMethod.MouseDwell:
						FillDwellFeedback(-1/_dwellTime);
						break;
					case InteractionMethod.Mouse:
						MouseUnHover();
						break;
					case InteractionMethod.Tobii:
						FillDwellFeedback(-1/_dwellTime);
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
				_dwellFeedback.color = inactiveColour;
				if (useIcon && changeTextOrIconColour) _spriteRenderers[1].color = activeTextOrIconColour;
				if (useText && changeTextOrIconColour) _text.color = activeTextOrIconColour;
			}

			else
			{
				_isActive = false;
				_isInactive = true;
				_mainImage.color = inactiveColour;
				_dwellFeedback.color = activeColour;
				if (useIcon && changeTextOrIconColour) _spriteRenderers[1].color = inactiveTextOrIconColour;
				if (useText && changeTextOrIconColour) _text.color = inactiveTextOrIconColour;
			}
		}

		private void SetColours()
		{
			_mainImage.color = inactiveColour;
			_dwellFeedback.color = activeColour;
			if (useText) _text.color = inactiveColour;
			if (useIcon) _spriteRenderers[1].color = inactiveColour;
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
			_dwellFeedback.color = activeColour;
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