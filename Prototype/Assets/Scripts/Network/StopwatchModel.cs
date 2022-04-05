using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class StopwatchModel {
    [RealtimeProperty(1, true)] private double _startTime;
    [RealtimeProperty(3, true)] private float _animatorTime;
    [RealtimeProperty(2, true)] private bool _firstPlayer;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class StopwatchModel : RealtimeModel {
    public double startTime {
        get {
            return _startTimeProperty.value;
        }
        set {
            if (_startTimeProperty.value == value) return;
            _startTimeProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public float animatorTime {
        get {
            return _animatorTimeProperty.value;
        }
        set {
            if (_animatorTimeProperty.value == value) return;
            _animatorTimeProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public bool firstPlayer {
        get {
            return _firstPlayerProperty.value;
        }
        set {
            if (_firstPlayerProperty.value == value) return;
            _firstPlayerProperty.value = value;
            InvalidateReliableLength();
        }
    }
    
    public enum PropertyID : uint {
        StartTime = 1,
        AnimatorTime = 3,
        FirstPlayer = 2,
    }
    
    #region Properties
    
    private ReliableProperty<double> _startTimeProperty;
    
    private ReliableProperty<float> _animatorTimeProperty;
    
    private ReliableProperty<bool> _firstPlayerProperty;
    
    #endregion
    
    public StopwatchModel() : base(null) {
        _startTimeProperty = new ReliableProperty<double>(1, _startTime);
        _animatorTimeProperty = new ReliableProperty<float>(3, _animatorTime);
        _firstPlayerProperty = new ReliableProperty<bool>(2, _firstPlayer);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _startTimeProperty.UnsubscribeCallback();
        _animatorTimeProperty.UnsubscribeCallback();
        _firstPlayerProperty.UnsubscribeCallback();
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _startTimeProperty.WriteLength(context);
        length += _animatorTimeProperty.WriteLength(context);
        length += _firstPlayerProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _startTimeProperty.Write(stream, context);
        writes |= _animatorTimeProperty.Write(stream, context);
        writes |= _firstPlayerProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.StartTime: {
                    changed = _startTimeProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.AnimatorTime: {
                    changed = _animatorTimeProperty.Read(stream, context);
                    break;
                }
                case (uint) PropertyID.FirstPlayer: {
                    changed = _firstPlayerProperty.Read(stream, context);
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
        _startTime = startTime;
        _animatorTime = animatorTime;
        _firstPlayer = firstPlayer;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
