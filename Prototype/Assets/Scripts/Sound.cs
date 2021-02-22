using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioClip = _audioSource.clip;
    }

    private void Update() {
        if(_audioSource.time >= _audioClip.length) Destroy(gameObject);
    }
}
