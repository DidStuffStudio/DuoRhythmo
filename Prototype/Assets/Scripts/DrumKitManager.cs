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

    public bool snare, kick, hiHat;
    private void Update() {
        
        // If the color has changed (via the inspector), call SetColor on the color sync component.
        if (Input.GetKeyDown(KeyCode.Alpha2) || snare)
        {
            _playAudio = true;
            // _snareSync.SetAudio(_playAudio);
            //Instantiate AudioSource
            Realtime.Instantiate(snarePrefab.name, true);
            snare = false;
        }
        else if (_playAudio) {
            _playAudio = false;
            // _snareSync.SetAudio(_playAudio);
            snare = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) || kick)
        {
            _playAudio = true;
            // _bassSync.SetAudio(_playAudio);
            Instantiate(bassPrefab);
            kick = false;
        }
        else if (_playAudio) {
            _playAudio = false;
            // _bassSync.SetAudio(_playAudio);
            kick = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) || hiHat)
        {
            _playAudio = true;
            // _highHatSync.SetAudio(_playAudio);
            Realtime.Instantiate(highHatPrefab.name, true);
            hiHat = false;
        }
        else if (_playAudio) {
            _playAudio = false;
            // _highHatSync.SetAudio(_playAudio);
            hiHat = false;
        }
    }

    public void enableKick()
    {
        kick = true;
    }
}
