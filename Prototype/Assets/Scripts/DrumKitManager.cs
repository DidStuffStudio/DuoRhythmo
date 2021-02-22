using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumKitManager : MonoBehaviour
{
    private bool _playAudio         = default;
    private bool _previousPlayAudio = default;

    public SnareSync _snareSync,_bassSync, _highHatSync;
    
    
    private void Update() {
        // If the color has changed (via the inspector), call SetColor on the color sync component.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playAudio = true;
            _snareSync.SetAudio(_playAudio);
        }
        else if (_playAudio) {
            _playAudio = false;
            _snareSync.SetAudio(_playAudio);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playAudio = true;
            _bassSync.SetAudio(_playAudio);
        }
        else if (_playAudio) {
            _playAudio = false;
            _bassSync.SetAudio(_playAudio);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _playAudio = true;
            _highHatSync.SetAudio(_playAudio);
        }
        else if (_playAudio) {
            _playAudio = false;
            _highHatSync.SetAudio(_playAudio);
        }
    }
}
