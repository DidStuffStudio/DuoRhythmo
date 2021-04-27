using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class CanvasFollowPlayer : MonoBehaviour
{
    private Transform _cam;

    [SerializeField] private GameObject playerCanvasPrefab;
    void Start()
    {
        var gfx = Realtime.Instantiate(playerCanvasPrefab.name, true, true, true);
        gfx.GetComponent<RealtimeTransform>().RequestOwnership();
        gfx.transform.SetParent(transform, false);
    }
    
}
