using System.Collections;
using System.Collections.Generic;
using ctsalidis;
using JetBrains.Annotations;
using UnityEngine;

public class EyeFollow : MonoBehaviour {
    [SerializeField] private Camera Camera;
    [SerializeField] private Player player;

    private Vector3 followPosition;

    void Update() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.nearClipPlane;
        Vector3 mouseWorld = Camera.ScreenToWorldPoint(mousePos);
        followPosition = mouseWorld;
        transform.LookAt(mouseWorld);
    }

    public void SetFollowSync(Player player) =>
        this.player = player;
    public void SetFollowPositionFromServer(Vector3 newValue) {
        
    }
}