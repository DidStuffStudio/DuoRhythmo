using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine;

public class DrumKitManager : MonoBehaviour {
    private bool _playAudio = default;
    private bool _previousPlayAudio = default;

    public SnareSync _snareSync, _bassSync, _highHatSync;
    

    private void Update() {
        
  
        
        /*// If the color has changed (via the inspector), call SetColor on the color sync component.
        if (Input.GetKeyDown(KeyCode.Alpha2) || snare) {
            _playAudio = true;
            Instantiate(snarePrefab);
            snare = false;
        }
        else if (_playAudio) {
            _playAudio = false;
            snare = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || kick) {
            _playAudio = true;
            kickEvent.Post(gameObject);
            //Instantiate(bassPrefab);
            kick = false;
        }
        else if (_playAudio) {
            _playAudio = false;
            kick = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || hiHat) {
            _playAudio = true;
            Instantiate(highHatPrefab);
            hiHat = false;
        }
        else if (_playAudio) {
            _playAudio = false;
            hiHat = false;
        }*/
    }
}