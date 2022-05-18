using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.PlayerLoop;

public class SnareSync : RealtimeComponent<DrumSyncModel>
{
    [SerializeField] private AudioSource _audioSource;
    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void OnRealtimeModelReplaced(DrumSyncModel previousModel, DrumSyncModel currentModel) {
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
    
    private void AudioDidChange(DrumSyncModel model, bool b) {
        UpdateAudioSource();
    }
    
    private void UpdateAudioSource() {
        if(model.playAudio) _audioSource.Play();
    }
    
    public void SetAudio(bool play) {
        // Set the color on the model
        // This will fire the audioChanged event on the model, which will update the sound for both the local player and all remote players.
        model.playAudio = play;
    }
}