using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class DrumModel
{
    [RealtimeProperty(1, true)] private int[] _kickNodes;
    [RealtimeProperty(2, true)] private int[] _snareNodes;
    [RealtimeProperty(3, true)] private int[] _hiHatNodes;
    [RealtimeProperty(4, true)] private int[] _tomNodes;
    [RealtimeProperty(5, true)] private int[] _cymbalNodes;
    
    [RealtimeProperty(7, true)] private float[] _kickEffects;
    [RealtimeProperty(8, true)] private float[] _snareEffects;
    [RealtimeProperty(9, true)] private float[] _hiHatEffects;
    [RealtimeProperty(10, true)] private float[] _tomEffects;
    [RealtimeProperty(11, true)] private float[] _cymbalEffects;
}