using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncTest : MonoBehaviour {
    [SerializeField]
    private bool _playAudio         = default;
    private bool _previousPlayAudio = default;

    private AudioSync _colorSync;

    private void Awake() {
        // Get a reference to the color sync component
        _colorSync = GetComponent<AudioSync>();
    }

    private void Update() {
        // If the color has changed (via the inspector), call SetColor on the color sync component.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playAudio = true;
            _colorSync.SetAudio(_playAudio);
        }
        else if (_playAudio) {
            _playAudio = false;
            _colorSync.SetAudio(_playAudio);
        }
    }
}