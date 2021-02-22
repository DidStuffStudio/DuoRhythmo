using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine;

public class DrumKitManager : MonoBehaviour {
    private bool _playAudio         = default;
    private bool _previousPlayAudio = default;

    public SnareSync _snareSync,_bassSync, _highHatSync;

    public GameObject snarePrefab, bassPrefab, highHatPrefab;
    private void Update() {
        // If the color has changed (via the inspector), call SetColor on the color sync component.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playAudio = true;
            // _snareSync.SetAudio(_playAudio);
            //Instantiate AudioSource
            Realtime.Instantiate(snarePrefab.name);
        }
        else if (_playAudio) {
            _playAudio = false;
            // _snareSync.SetAudio(_playAudio);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playAudio = true;
            // _bassSync.SetAudio(_playAudio);
            Realtime.Instantiate(bassPrefab.name);
        }
        else if (_playAudio) {
            _playAudio = false;
            // _bassSync.SetAudio(_playAudio);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _playAudio = true;
            // _highHatSync.SetAudio(_playAudio);
            Realtime.Instantiate(highHatPrefab.name);
        }
        else if (_playAudio) {
            _playAudio = false;
            // _highHatSync.SetAudio(_playAudio);
        }
    }
}
