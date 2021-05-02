using Normal.Realtime;
using UnityEngine;

public class CanvasFollowPlayer : MonoBehaviour {
    private Transform _cam;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    private RealtimeView _realtimeView;
    
    private void Start() {
        _cam = Camera.main.transform;
        _realtimeView = GetComponent<RealtimeView>();
    }

    private void Update() {
        if (!_realtimeView.isOwnedLocallyInHierarchy) return;
        transform.position = _cam.position + _cam.TransformDirection(positionOffset);
        transform.rotation = _cam.rotation * Quaternion.Euler(rotationOffset);
    }

    public bool RaycastSearchForPartner() => Physics.Raycast(transform.position, transform.forward, 1000, LayerMask.NameToLayer("Player"));
}