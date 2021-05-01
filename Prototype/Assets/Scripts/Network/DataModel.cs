using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel()]
public class DataModel : MonoBehaviour {
    [RealtimeProperty(1, true, true)] private int _timer;
    [RealtimeProperty(1, true, true)] private float _currentAnimatorTime;
    [RealtimeProperty(1, true, true)] private int _owner;
}