using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class AudioSyncModel {
    [RealtimeProperty(1, true, true)] private bool _playAudio;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class AudioSyncModel : RealtimeModel {
    public bool playAudio {
        get {
            return _playAudioProperty.value;
        }
        set {
            if (_playAudioProperty.value == value) return;
            _playAudioProperty.value = value;
            InvalidateReliableLength();
            FirePlayAudioDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(AudioSyncModel model, T value);
    public event PropertyChangedHandler<bool> playAudioDidChange;
    
    public enum PropertyID : uint {
        PlayAudio = 1,
    }
    
    #region Properties
    
    private ReliableProperty<bool> _playAudioProperty;
    
    #endregion
    
    public AudioSyncModel() : base(null) {
        _playAudioProperty = new ReliableProperty<bool>(1, _playAudio);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _playAudioProperty.UnsubscribeCallback();
    }
    
    private void FirePlayAudioDidChange(bool value) {
        try {
            playAudioDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _playAudioProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _playAudioProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.PlayAudio: {
                    changed = _playAudioProperty.Read(stream, context);
                    if (changed) FirePlayAudioDidChange(playAudio);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _playAudio = playAudio;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
