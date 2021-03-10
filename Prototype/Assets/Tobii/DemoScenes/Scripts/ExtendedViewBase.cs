//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

using System;
using UnityEngine;
#if (UNITY_5 || UNITY_4_6_OR_NEWER)
using UnityEngine.UI;
#endif
using Tobii.Gaming;
using System.Collections;

/*
 * Extended View
 * 
 * Extended view decouples your look direction from your movement direction. 
 * This lets you both look around a bit more without conciously have to move 
 * the mouse, but also lets you inspect objects that are locked to the camera's 
 * reference frame.
 * 
 * This implementation works in an absolute reference frame, essentially 
 * mapping each possible gaze point directly to a camera target orientation and
 * then interpolating the camera to that orientation. It does this using two 
 * levels of indirection, in this order:  Gaze point -> View target -> Camera 
 * rotation. The main reason is to smooth out the action when the camera 
 * rotation is close to the gaze point, but having the intermediate view target
 * is also useful when we want to pause the system, letting it smoothly come to
 * a stop instead of instantly halting it.
 */
public abstract class ExtendedViewBase : MonoBehaviour
{
	private const float MaxTimeWithoutData = 2f;

	private const float HeadPoseRotationalRangeRadians = Mathf.PI;
	private const float ReferenceFrequency = 60;
	private const float HeadTrackingFrequency = 30;

	public ExtendedViewType ExtendedViewType = ExtendedViewType.Direct;

	/* This is an additional scale value complementing Sensitivity, but is only 
	* applied when lerping the camera towards the view target. */

	public float GazeViewResponsiveness = 0.5f;

	public float GazeViewSensitivityScale = 0.5f;
	public float GazeViewSensitivityScaleGazeOnly = 1f;

	public float GazeViewExtensionAngle = 5f;
	public float GazeViewExtensionAngleGazeOnly = 20f;

	public float GazeSensitivityExponent = 2.25f;
	public float GazeSensitivityInflectionPoint = 0.8f;
	public float GazeSensitivityStartPoint = 0;
	public float GazeSensitivityEndPoint = 1;

	public float HeadViewResponsiveness = 1.0f;

	public float HeadViewSensitivityScale = 0.65f;
	public float HeadViewSensitivityScaleHeadOnly = 0.65f;

	public float HeadSensitivityExponent = 1.25f;
	public float HeadSensitivityInflectionPoint = 0.5f;
	public float HeadSensitivityStartPoint = 0;
	public float HeadSensitivityEndPoint = 1;

	public float ResetResponsiveness = 5;

	public Vector2 MaximumRotationLocalAngles = new Vector3(90f, 180f);
	public Vector2 MinimumRotationLocalAngles = new Vector3(-90f, -180f);

	public float MaximumRotationWorldAnglesX = 90f;
	public float MinimumRotationWorldAnglesX = -90f;

	//-------------------------------------------------------------------------
	// Public members
	//-------------------------------------------------------------------------
#if (UNITY_5 || UNITY_4_6_OR_NEWER)
	/* If you are at an extreme view angle and quickly want to return to the center, 
	 * it is nice to be able to look at a given stimulus (usually the crosshair).*/
	public Image OptionalReturnToCenterStimuli;
#endif
	/* Should we remove extended view when aiming to allow more precise cursor 
	 * movements? */
	public bool RemoveExtendedViewWhenAiming = true;
	/* Should we pause the system when the user is looking at UI? */
	public bool PauseInCleanUiDeadzones = true;
	/* We want to make sure to completely center the view when the user is 
	 * looking straight forward. We also want to speed up the centering process if
	 * the user is at an extreme view angle and wants back to the middle. */
	public float ViewCenteringDeadzoneSize = 0.1f;

	/* This lets you scale the amount of Extended View effect depending on how 
	 * much change in pitch your have. This might be useful since we get strange 
	 * effects when using Extended View in extreme pitch angles. */
	public AnimationCurve AmountOfScreenShiftDependingOnCameraPitch = AnimationCurve.EaseInOut(0.0f, 1.0f, 1.0f, 1.0f);
	public bool ScaleScreenShiftByBasePitch = false;
	public bool IsEnabled = true;
	public bool IsLocked = false;

	public bool IsAiming { get; set; }

	public bool IsPaused { get { return Time.deltaTime < 1E-08f; } }

	public float Yaw { get; private set; }
    public float Pitch { get; private set; }

    public float HeadRotationScalar { get; set; }

    //-------------------------------------------------------------------------
    // Protected members
    //-------------------------------------------------------------------------

    protected Vector2 AimTargetScreen;
	protected Ray AimTargetRay;


	//-------------------------------------------------------------------------
	// Private members
	//-------------------------------------------------------------------------

	private Camera _cameraWithoutExtendedView;
	private Camera _cameraWithExtendedView;

	private bool _lastIsAiming;

	/* This variable are used for main extended view algorithm */
	private Quaternion _currentHeadPose = Quaternion.identity;
	private float _gazeViewExtensionAngleDegrees;

	private Vector3 _filteredHeadPose;
	private Vector2 _lerpedFilteredHeadPose;

	private GazePoint _lastValidGazePoint = GazePoint.Invalid;
	private Vector2 _gazeViewTarget;

	private float _accumulatedTimeDeltaForHeadPoseLerp;
	private Vector3 _previousFilteredHeadPose;

	private float _clampedPitch;
	private float _clampedYaw;

	private float _currentGazeViewSensitivityScale;
	private float _currentHeadViewSensitivityScale;
	private float _currentGazeViewExtensionAngle;

    private float _gazeYawOffset;
    private float _gazePitchOffset;
    private float _headYawOffset;
    private float _headPitchOffset;

    //-------------------------------------------------------------------------
    // Public properties
    //-------------------------------------------------------------------------

    public virtual Camera CameraWithoutExtendedView
	{
		get
		{
			if (_cameraWithoutExtendedView != null && _cameraWithoutExtendedView.gameObject != null) return _cameraWithoutExtendedView;

			var cameraGo = new GameObject("CameraTransformWithoutExtendedView");

            cameraGo.transform.parent = null;

			_cameraWithoutExtendedView = cameraGo.AddComponent<Camera>();
			_cameraWithoutExtendedView.enabled = false;
			return _cameraWithoutExtendedView;
		}
	}


	public virtual Camera CameraWithExtendedView
	{
		get
		{
			if (_cameraWithExtendedView != null && _cameraWithExtendedView.gameObject != null) return _cameraWithExtendedView;

			var cameraGo = new GameObject("CameraTransformWithExtendedView");

            cameraGo.transform.parent = null;

			_cameraWithExtendedView = cameraGo.AddComponent<Camera>();
			_cameraWithExtendedView.enabled = false;
			TobiiAPI.SetCurrentUserViewPointCamera(CameraWithExtendedView);
			return _cameraWithExtendedView;
		}
	}

	protected virtual void UpdateSettings()
	{
	}

	protected virtual void UpdateTransform()
	{
	}

    protected virtual void Start()
    {
        
    }

    protected virtual void InitAimAtGazeOffset(float yaw, float pitch)
    {
        _gazeYawOffset = yaw;
        _gazePitchOffset = pitch;
    }

    //--------------------------------------------------------------------
    // MonoBehaviour event functions (messages)
    //--------------------------------------------------------------------
    protected virtual void LateUpdate()
	{
		UpdateSettings();

		UpdateHeadPose();

		var currentGazePoint = TobiiAPI.GetGazePoint();
		if (!IsEnabled || !currentGazePoint.IsRecent() || (IsAiming && RemoveExtendedViewWhenAiming))
		{
			_gazeViewTarget = new Vector2();
        }
        else
		{
			_lastValidGazePoint = currentGazePoint;

			if (!ShouldPauseExtendedViewOnCleanUi())
			{
				if (!HasRecentGazePointData())
				{
					if (!IsLocked)
					{
						_gazeViewTarget = new Vector2();
					}
				}
				else if (_lastValidGazePoint.IsValid)
				{
					var normalizedCenteredGazeCoordinates = CreateNormalizedCenteredGazeCoordinates(_lastValidGazePoint);
					if (!IsLocked)
					{
						UpdateViewTarget(normalizedCenteredGazeCoordinates);
					}
				}

			}
		}
        UpdateExtendedViewAngles();
        Pitch = _gazePitchOffset + (_headPitchOffset * HeadRotationScalar);
        Yaw = _gazeYawOffset + (_headYawOffset * HeadRotationScalar);
        UpdateTransform();
	}

	private void UpdateHeadPose()
	{
		if (!IsEnabled || !HasRecentHeadPoseData())
		{
            HeadRotationScalar = Mathf.Lerp(HeadRotationScalar, 0, Time.unscaledDeltaTime * ResetResponsiveness);
		}
		else
		{
            HeadRotationScalar = !IsAiming ? Mathf.Lerp(HeadRotationScalar, 1, Time.unscaledDeltaTime * ResetResponsiveness) : 0;
		}

		var headPose = TobiiAPI.GetHeadPose();
		var currentFilterredHeadPose = _filteredHeadPose;
		if (headPose.IsRecent())
		{
			_currentHeadPose = headPose.Rotation;
			UpdateFilteredHeadPose(_currentHeadPose, Time.unscaledDeltaTime);
			_previousFilteredHeadPose = currentFilterredHeadPose;
			_accumulatedTimeDeltaForHeadPoseLerp -= (1.0f / HeadTrackingFrequency);
		}

		_accumulatedTimeDeltaForHeadPoseLerp += Time.unscaledDeltaTime;
		_accumulatedTimeDeltaForHeadPoseLerp = Mathf.Clamp(_accumulatedTimeDeltaForHeadPoseLerp, 0.0f, 1.0f / HeadTrackingFrequency);

		var lerpStep = _accumulatedTimeDeltaForHeadPoseLerp * HeadTrackingFrequency;

		_lerpedFilteredHeadPose.x = Mathf.Lerp(_previousFilteredHeadPose.x, _filteredHeadPose.x, lerpStep);
		_lerpedFilteredHeadPose.y = Mathf.Lerp(_previousFilteredHeadPose.y, _filteredHeadPose.y, lerpStep);

		/* Update current HeadViewSensitivityScale */
		if (!HasRecentGazePointData())
		{
			_currentHeadViewSensitivityScale = Mathf.Lerp(_currentHeadViewSensitivityScale, HeadViewSensitivityScaleHeadOnly,
				10 * Time.unscaledDeltaTime);
		}
		else
		{
			_currentHeadViewSensitivityScale = Mathf.Lerp(_currentHeadViewSensitivityScale, HeadViewSensitivityScale,
				10 * Time.unscaledDeltaTime);
		}

		var centeredNormalizedHeadPose = CreateCenterNormalizedHeadPose(_lerpedFilteredHeadPose);

		var headViewAngles = GetHeadViewAngles(centeredNormalizedHeadPose);

		_headYawOffset = headViewAngles.y;
		_headPitchOffset = headViewAngles.x * 1.5f;
	}

	//-------------------------------------------------------------------------
	// Protected/public virtual functions
	//-------------------------------------------------------------------------

	public virtual void AimAtWorldPosition(Vector3 worldPosition)
	{
        /* empty default implementation */
    }

    protected virtual bool ShouldPauseExtendedViewOnCleanUi()
    {
        return false;
    }

	//-------------------------------------------------------------------------
	// Protected functions
	//-------------------------------------------------------------------------

	protected void ProcessAimAtGaze(Camera mainCamera)
	{
		if (!_lastIsAiming && IsAiming && HasRecentGazePointData())
		{
			AimTargetScreen = TobiiAPI.GetGazePoint().Screen;
			var aimTargetWorld = mainCamera.ScreenToWorldPoint(new Vector3(AimTargetScreen.x, AimTargetScreen.y, 10));
			AimTargetRay = mainCamera.ScreenPointToRay(new Vector3(AimTargetScreen.x, AimTargetScreen.y, 10));

			AimAtWorldPosition(aimTargetWorld);
		}

        _lastIsAiming = IsAiming;
    }

    public void UpdateCameraWithoutExtendedView(Camera mainCamera)
    {
        UpdateCamera(CameraWithoutExtendedView, mainCamera);
    }

    public void UpdateCameraWithExtendedView(Camera mainCamera)
    {
        UpdateCamera(CameraWithExtendedView, mainCamera);
    }

	protected void UpdateCamera(Camera cameraToUpdate, Camera mainCamera)
	{
		cameraToUpdate.transform.position = mainCamera.transform.position;
		cameraToUpdate.transform.rotation = mainCamera.transform.rotation;
		cameraToUpdate.fieldOfView = mainCamera.fieldOfView;
	}

    public void Rotate(Component componentToRotate, float fovScalar = 1f, Vector3 up = new Vector3(), bool calculateClampedValues = true)
    {
        var componenetAsTransform = componentToRotate as Transform;
        var transformToRotate = componenetAsTransform != null ? componenetAsTransform : componentToRotate.transform;

        if (calculateClampedValues)
        {
			_clampedPitch = Pitch;
			_clampedYaw = Yaw;

			_clampedPitch = _clampedPitch < MaximumRotationLocalAngles.x ? _clampedPitch : MaximumRotationLocalAngles.x;
			_clampedPitch = _clampedPitch > MinimumRotationLocalAngles.x ? _clampedPitch : MinimumRotationLocalAngles.x;

			_clampedYaw = _clampedYaw < MaximumRotationLocalAngles.y ? _clampedYaw : MaximumRotationLocalAngles.y;
			_clampedYaw = _clampedYaw > MinimumRotationLocalAngles.y ? _clampedYaw : MinimumRotationLocalAngles.y;

			_clampedPitch = Mathf.DeltaAngle(0, _clampedPitch + transformToRotate.rotation.eulerAngles.x) < MaximumRotationWorldAnglesX ? _clampedPitch : MaximumRotationWorldAnglesX - transformToRotate.rotation.eulerAngles.x;
			_clampedPitch = Mathf.DeltaAngle(0, _clampedPitch + transformToRotate.rotation.eulerAngles.x) > MinimumRotationWorldAnglesX ? _clampedPitch : MinimumRotationWorldAnglesX - transformToRotate.rotation.eulerAngles.x;
		}

		transformToRotate.Rotate(_clampedPitch * fovScalar, 0.0f, 0.0f, Space.Self);

		if (up == new Vector3())
		{
			transformToRotate.Rotate(0.0f, _clampedYaw * fovScalar, 0.0f, Space.World);
		}
		else
		{
			transformToRotate.Rotate(up, _clampedYaw * fovScalar, Space.World);
		}
	}

    public Quaternion GetRotation(Component componentToRotate, float fovScalar = 1f, Vector3 up = new Vector3(), bool calculateClampedValues = true)
    {
        var componentAsTransform = componentToRotate as Transform;
        var transformToRotate = componentAsTransform != null ? componentAsTransform : componentToRotate.transform;
        var oldRotation = transformToRotate.rotation;

        Rotate(transformToRotate, fovScalar, up, calculateClampedValues);

		var rotation = transformToRotate.rotation;

		transformToRotate.rotation = oldRotation;

        return rotation;
    }

    protected void RotateAndGetCrosshairScreenPosition(Camera cameraToRotate, out Vector2 crosshairScreenPosition, float fovScalar = 1f, Vector3 up = new Vector3())
    {
        Vector3 vector;
        RotateAndGetCrosshairScreenPosition(cameraToRotate, out vector, fovScalar, up);
        crosshairScreenPosition = vector;
    }

    protected void RotateAndGetCrosshairScreenPosition(Camera cameraToRotate, out Vector3 crosshairScreenPosition, float fovScalar = 1f, Vector3 up = new Vector3())
    {
        var crosshairWorldPosition = cameraToRotate.transform.position + cameraToRotate.transform.forward;
        Rotate(cameraToRotate.transform, fovScalar, up);
        crosshairScreenPosition = cameraToRotate.WorldToScreenPoint(crosshairWorldPosition);
    }

    protected void RotateAndGetCrosshairViewportPosition(Camera cameraToRotate, out Vector2 crosshairScreenPosition, float fovScalar = 1f, Vector3 up = new Vector3())
    {
        Vector3 vector;
        RotateAndGetCrosshairViewportPosition(cameraToRotate, out vector, fovScalar, up);
        crosshairScreenPosition = vector;
    }

    protected void RotateAndGetCrosshairViewportPosition(Camera cameraToRotate, out Vector3 crosshairScreenPosition, float fovScalar = 1f, Vector3 up = new Vector3())
    {
        var crosshairWorldPosition = cameraToRotate.transform.position + cameraToRotate.transform.forward;
        Rotate(cameraToRotate.transform, fovScalar, up);
        crosshairScreenPosition = cameraToRotate.WorldToViewportPoint(crosshairWorldPosition);
    }

    protected IEnumerator ResetCameraWorld(Quaternion rotation, Transform cameraTransform = null)
    {
        cameraTransform = cameraTransform ? cameraTransform : transform;

        yield return new WaitForEndOfFrame();
        cameraTransform.rotation = rotation;
        PostResetCamera();
    }

    protected IEnumerator ResetCameraLocal(Quaternion? rotation = null, Transform camTransform = null)
    {
        camTransform = camTransform ? camTransform : transform;
        rotation = rotation.HasValue ? rotation : Quaternion.identity;

        yield return new WaitForEndOfFrame();
        camTransform.localRotation = rotation.Value;
        PostResetCamera();
    }

	protected IEnumerator ResetTransformPosition(Transform aTransform, Vector3 position)
	{
		yield return new WaitForEndOfFrame();
		aTransform.position = position;
	}

	protected IEnumerator ResetCameraPosition(Vector3 position)
	{
		yield return ResetTransformPosition(transform, position);
	}

	protected virtual void PostResetCamera()
	{

	}

	/// <summary>
	/// Lerps the view target closer to the gaze point.
	/// </summary>
	private void UpdateViewTarget(Vector2 normalizedCenteredGazeCoordinates)
	{
		var responsiveness = Mathf.Pow(GazeViewResponsiveness, 2.5f);
		var scale = Mathf.Clamp01(Time.unscaledDeltaTime * ReferenceFrequency);
		_gazeViewTarget.x = Mathf.Lerp(_gazeViewTarget.x, normalizedCenteredGazeCoordinates.x, responsiveness * scale);
		_gazeViewTarget.y = Mathf.Lerp(_gazeViewTarget.y, normalizedCenteredGazeCoordinates.y, responsiveness * scale);

		/* If you are at an extreme view angle and quickly want to return to the center, 
		 * it is nice to be able to look at a given stimulus (usually the crosshair). */
#if (UNITY_5 || UNITY_4_6_OR_NEWER)
		var returnToCenterStimuliTransform = OptionalReturnToCenterStimuli == null ? null : OptionalReturnToCenterStimuli.GetComponent<RectTransform>();

		bool userLookingInCenteringDeadzone = returnToCenterStimuliTransform == null || !_lastValidGazePoint.IsValid ? normalizedCenteredGazeCoordinates.magnitude < ViewCenteringDeadzoneSize
			 : (Vector2.Distance(_lastValidGazePoint.GUI, returnToCenterStimuliTransform.anchoredPosition) / new Vector2(Screen.width, Screen.height).magnitude) < ViewCenteringDeadzoneSize;
#else
		bool userLookingInCenteringDeadzone = normalizedCenteredGazeCoordinates.magnitude < ViewCenteringDeadzoneSize;
#endif

		if (userLookingInCenteringDeadzone)
		{
			_gazeViewTarget.x = Mathf.Lerp(_gazeViewTarget.x, 0.0f, 10 * Time.unscaledDeltaTime);
			_gazeViewTarget.y = Mathf.Lerp(_gazeViewTarget.y, 0.0f, 10 * Time.unscaledDeltaTime);
		}
	}

	//private void OnGUI()
	//{
	//	GUI.backgroundColor = Color.blue;
	//	GUI.Box(new Rect((_gazeViewTarget.x + 1) * 0.5f * Screen.width - 5, Screen.height - ((_gazeViewTarget.y + 1) * 0.5f * Screen.height) - 5, 10, 10), " ");
	//}

	/// <summary>
	/// Translates the current view target to target orientation angles and lerp 
	/// the camera orientation towards it.
	/// </summary>
    private void UpdateExtendedViewAngles()
	{
		/* Update current GazeViewSensitivityScale */
		if (!HasRecentHeadPoseData())
		{
			_currentGazeViewSensitivityScale = Mathf.Lerp(_currentGazeViewSensitivityScale, GazeViewSensitivityScaleGazeOnly, 10 * Time.unscaledDeltaTime);
			_currentGazeViewExtensionAngle = Mathf.Lerp(_currentGazeViewExtensionAngle, GazeViewExtensionAngleGazeOnly, 10 * Time.unscaledDeltaTime);
		}
		else
		{
			_currentGazeViewSensitivityScale = Mathf.Lerp(_currentGazeViewSensitivityScale, GazeViewSensitivityScale, 10 * Time.unscaledDeltaTime);
			_currentGazeViewExtensionAngle = Mathf.Lerp(_currentGazeViewExtensionAngle, GazeViewExtensionAngle, 10 * Time.unscaledDeltaTime);
		}

		//Update angles
		var displayAspectRatio = (float)Screen.width / Screen.height;

		_gazeViewExtensionAngleDegrees = GetGazeViewExtensionAngleDegrees(_lerpedFilteredHeadPose);

		/* Translate gaze offset to angles along our curve */
		var yawLimit = _gazeViewExtensionAngleDegrees * displayAspectRatio;
		float targetYaw = _gazeViewTarget.x * yawLimit;

		var pitchLimit = _gazeViewExtensionAngleDegrees;
		float targetPitch = -_gazeViewTarget.y * pitchLimit;

		if (_gazeViewTarget.y > 0)
		{
			targetPitch *= 2;
		}

		if (ScaleScreenShiftByBasePitch)
		{
			float cameraPitchWithin90Range = transform.localRotation.eulerAngles.x > 90
				? transform.localRotation.eulerAngles.x - 360
				: transform.localRotation.eulerAngles.x;
			float pitchShiftMinus1To1 = cameraPitchWithin90Range / 90.0f;
			float pitchShift01 = (pitchShiftMinus1To1 + 1) / 2.0f;
			float pitchShiftScale = AmountOfScreenShiftDependingOnCameraPitch.Evaluate(pitchShift01);
			targetYaw *= pitchShiftScale;
		}



		/* Rotate current angles toward our target angles.
		 * Please note that depending on preference, a slerp here might be a better 
		 * fit because of angle spacing errors when using lerp with angles. */

		var deltaVector = new Vector2((targetYaw - _gazeYawOffset) / yawLimit, (targetPitch - _gazePitchOffset) / pitchLimit);
		var normalizedDistance = Mathf.Clamp01(deltaVector.magnitude);
		var viewTargetStepSize = _currentGazeViewSensitivityScale * TobiiSensitivityGradient(normalizedDistance, GazeSensitivityExponent,
			GazeSensitivityInflectionPoint, GazeSensitivityStartPoint, GazeSensitivityEndPoint);

		_gazeYawOffset = Mathf.LerpAngle(_gazeYawOffset, targetYaw,
			(IsAiming || !IsEnabled || !HasRecentGazePointData() ? ResetResponsiveness : viewTargetStepSize) * Time.unscaledDeltaTime);
		_gazePitchOffset = Mathf.LerpAngle(_gazePitchOffset, targetPitch,
			(IsAiming || !IsEnabled || !HasRecentGazePointData() ? ResetResponsiveness : viewTargetStepSize) * Time.unscaledDeltaTime);
	}

	private bool HasRecentHeadPoseData()
	{
		var headPose = TobiiAPI.GetHeadPose();
		return headPose.IsRecent(MaxTimeWithoutData);
	}

	private bool HasRecentGazePointData()
	{
		var gazePoint = TobiiAPI.GetGazePoint();
		return gazePoint.IsRecent(MaxTimeWithoutData);
	}

	//-------------------------------------------------------------------------
	// Static utility functions
	//-------------------------------------------------------------------------

	/// <summary>
	/// Creates normalized centered gaze coordinates from the provided Gaze Point.
	/// Centered normalized means bottom left is (-1,-1) and top right is (1,1).
	/// </summary>
	/// <param name="gazePoint">Gaze point</param>
	/// <returns>Normalized centered gaze coordinates if the gaze point is not 
	/// null, otherwise a zero-valued Vector2.</returns>
	private static Vector2 CreateNormalizedCenteredGazeCoordinates(GazePoint gazePoint)
	{
		var normalizedCenteredGazeCoordinates = (gazePoint.Viewport - new Vector2(0.5f, 0.5f)) * 2;
		//normalizedCenteredGazeCoordinates.y = -normalizedCenteredGazeCoordinates.y;

		normalizedCenteredGazeCoordinates.x = Mathf.Clamp(normalizedCenteredGazeCoordinates.x, -1.0f, 1.0f);
		normalizedCenteredGazeCoordinates.y = Mathf.Clamp(normalizedCenteredGazeCoordinates.y, -1.0f, 1.0f);
		return normalizedCenteredGazeCoordinates;
	}

	private static Vector3 CreateCenterNormalizedHeadPose(Vector3 headRotation)
	{
		var halfHeadPoseRotationalRangeRadians = HeadPoseRotationalRangeRadians / 2.0f;

		return headRotation / halfHeadPoseRotationalRangeRadians;
	}

	private void UpdateFilteredHeadPose(Quaternion headPose, float timeDeltaInSeconds)
	{
		float responsiveNess = Mathf.Pow(HeadViewResponsiveness, 2.5f);
		float timeDeltaFactor = Mathf.Clamp01(timeDeltaInSeconds * ReferenceFrequency);

		float halfHeadPoseRotationalRangeRadians = HeadPoseRotationalRangeRadians / 2.0f;

		var pitch = headPose.eulerAngles.x;
		if (pitch > 180) pitch -= 360;
		var yaw = headPose.eulerAngles.y;
		if (yaw > 180) yaw -= 360;
		var roll = headPose.eulerAngles.z;
		if (roll > 180) roll -= 360;

		_filteredHeadPose.y += DampenDelta(DampenHeadPose, yaw * Mathf.Deg2Rad - _filteredHeadPose.y, -halfHeadPoseRotationalRangeRadians, halfHeadPoseRotationalRangeRadians) * responsiveNess * timeDeltaFactor;
		_filteredHeadPose.x += DampenDelta(DampenHeadPose, pitch * Mathf.Deg2Rad - _filteredHeadPose.x, -halfHeadPoseRotationalRangeRadians, halfHeadPoseRotationalRangeRadians) * responsiveNess * timeDeltaFactor;
		_filteredHeadPose.z += DampenDelta(DampenHeadPose, roll * Mathf.Deg2Rad - _filteredHeadPose.z, -halfHeadPoseRotationalRangeRadians, halfHeadPoseRotationalRangeRadians) * responsiveNess * timeDeltaFactor;
	}

	private static float DampenDelta(Func<float, float> dampeningFunction, float delta, float minValue, float maxValue)
	{
		float valueRangeLength = maxValue - minValue;
		float deltaSign = Mathf.Sign(delta);
		float normalizedDelta = Mathf.Clamp01(Mathf.Abs(delta) / valueRangeLength);
		float transformedDelta = deltaSign * dampeningFunction(normalizedDelta) * valueRangeLength;
		return transformedDelta;
	}

	private static float DampenHeadPose(float value)
	{
		return Mathf.Pow(value, 1.75f);
	}

	private static float TobiiSensitivityGradient(float normalizedValue, float exponent, float inflectionPoint, float startPoint, float endPoint)
	{
		if (startPoint >= 1.0f)
		{
			return 0.0f;
		}

		if (endPoint <= 0.0f)
		{
			return 1.0f;
		}

		exponent = Mathf.Max(exponent, 1.0f);
		inflectionPoint = Mathf.Clamp(inflectionPoint, float.Epsilon, (1.0f - float.Epsilon));

		float x = (normalizedValue - startPoint) / (endPoint - startPoint);
		x = Mathf.Clamp01(x);

		float a = 1.0f / inflectionPoint;
		float b = a * x;
		float c = a / (a - 1.0f);
		float d = Mathf.Min(Mathf.Floor(b), 1.0f);

		return ((1.0f - d) * (Mathf.Pow(b, exponent) / a)) + (d * (1.0f - (Mathf.Pow(c * (1.0f - x), exponent) / c)));
	}

	private static float GetSensititivyGradientValue(float centerNormalizedValue, float exponent, float inflectionPoint, float startPoint, float endPoint)
	{
		var sign = Mathf.Sign(centerNormalizedValue);
		return sign * TobiiSensitivityGradient(Mathf.Clamp01(Mathf.Abs(centerNormalizedValue)), exponent, inflectionPoint, startPoint, endPoint);
	}

	private float GetGazeViewExtensionAngleDegrees(Vector3 centerNormalizedHeadPose)
	{
		switch (ExtendedViewType)
		{
			case ExtendedViewType.Direct:
				{
					return _currentGazeViewExtensionAngle;
				}

			case ExtendedViewType.Dynamic:
				{
					var headPoseLength = Mathf.Clamp01(Mathf.Sqrt(centerNormalizedHeadPose.y * centerNormalizedHeadPose.y + centerNormalizedHeadPose.x * centerNormalizedHeadPose.x));
					return _currentGazeViewExtensionAngle + (headPoseLength * 90.0f);
				}

			case ExtendedViewType.None:
			default:
				return 0.0f;
		}
	}

	private Vector3 GetHeadViewAngles(Vector3 centerNormalizedHeadPose)
	{
		var angles = new Vector3();

		angles.y = GetSensititivyGradientValue(centerNormalizedHeadPose.y, HeadSensitivityExponent, HeadSensitivityInflectionPoint, HeadSensitivityStartPoint, HeadSensitivityEndPoint) * _currentHeadViewSensitivityScale * 180.0f;
		angles.x = GetSensititivyGradientValue(centerNormalizedHeadPose.x, HeadSensitivityExponent, HeadSensitivityInflectionPoint, HeadSensitivityStartPoint, HeadSensitivityEndPoint) * _currentHeadViewSensitivityScale * 180.0f;

		return angles;
	}
}

public enum ExtendedViewType
{
	Direct,
	Dynamic,
	None
}
