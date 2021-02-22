using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.PlayerLoop;

public class AudioSync : RealtimeComponent<AudioSyncModel> {
    [SerializeField] private AudioSource _audioSource;
    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void OnRealtimeModelReplaced(AudioSyncModel previousModel, AudioSyncModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.playAudioDidChange -= AudioDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.playAudio = _audioSource.isPlaying;
        
            // Update the mesh render to match the new model
            UpdateAudioSource();

            // Register for events so we'll know if the color changes later
            currentModel.playAudioDidChange += AudioDidChange;
        }
    }
    
    private void AudioDidChange(AudioSyncModel model, bool b) {
        // Update the mesh renderer
        UpdateAudioSource();
    }
    
    private void UpdateAudioSource() {
        if(model.playAudio) _audioSource.Play();
        print(_audioSource.isPlaying);
        // if(model.playAudio) _audioSource.Play();
        // else _audioSource.Stop();
    }
    
    public void SetAudio(bool play) {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.playAudio = play;
        // print(play);
    }
}
