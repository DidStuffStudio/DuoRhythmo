using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine;


[RealtimeModel]
    public partial class NodeSyncModel {
        [RealtimeProperty(1, true, true)] private bool _kickNode1;
        [RealtimeProperty(2, true, true)] private bool _kickNode2;
        [RealtimeProperty(3, true, true)] private bool _kickNode3;
        [RealtimeProperty(4, true, true)] private bool _kickNode4;
        [RealtimeProperty(5, true, true)] private bool _kickNode5;
        [RealtimeProperty(6, true, true)] private bool _kickNode6;
        [RealtimeProperty(7, true, true)] private bool _kickNode7;
        [RealtimeProperty(8, true, true)] private bool _kickNode8;
        [RealtimeProperty(9, true, true)] private bool _kickNode9;
        [RealtimeProperty(10, true, true)] private bool _kickNode10;
        [RealtimeProperty(11, true, true)] private bool _kickNode11;
        [RealtimeProperty(12, true, true)] private bool _kickNode12;
        [RealtimeProperty(13, true, true)] private bool _kickNode13;
        [RealtimeProperty(14, true, true)] private bool _kickNode14;
        [RealtimeProperty(15, true, true)] private bool _kickNode15;
        [RealtimeProperty(16, true, true)] private bool _kickNode16;
        
        [RealtimeProperty(17, true, true)] private bool _hiHatNode1;
        [RealtimeProperty(18, true, true)] private bool _hiHatNode2;
        [RealtimeProperty(19, true, true)] private bool _hiHatNode3;
        [RealtimeProperty(20, true, true)] private bool _hiHatNode4;
        [RealtimeProperty(21, true, true)] private bool _hiHatNode5;
        [RealtimeProperty(22, true, true)] private bool _hiHatNode6;
        [RealtimeProperty(23, true, true)] private bool _hiHatNode7;
        [RealtimeProperty(24, true, true)] private bool _hiHatNode8;
        [RealtimeProperty(25, true, true)] private bool _hiHatNode9;
        [RealtimeProperty(26, true, true)] private bool _hiHatNode10;
        [RealtimeProperty(27, true, true)] private bool _hiHatNode11;
        [RealtimeProperty(28, true, true)] private bool _hiHatNode12;
        [RealtimeProperty(29, true, true)] private bool _hiHatNode13;
        [RealtimeProperty(30, true, true)] private bool _hiHatNode14;
        [RealtimeProperty(31, true, true)] private bool _hiHatNode15;
        [RealtimeProperty(32, true, true)] private bool _hiHatNode16;
        
        [RealtimeProperty(33, true, true)] private bool _snareNode1;
        [RealtimeProperty(34, true, true)] private bool _snareNode2;
        [RealtimeProperty(35, true, true)] private bool _snareNode3;
        [RealtimeProperty(36, true, true)] private bool _snareNode4;
        [RealtimeProperty(37, true, true)] private bool _snareNode5;
        [RealtimeProperty(38, true, true)] private bool _snareNode6;
        [RealtimeProperty(39, true, true)] private bool _snareNode7;
        [RealtimeProperty(40, true, true)] private bool _snareNode8;
        [RealtimeProperty(41, true, true)] private bool _snareNode9;
        [RealtimeProperty(42, true, true)] private bool _snareNode10;
        [RealtimeProperty(43, true, true)] private bool _snareNode11;
        [RealtimeProperty(44, true, true)] private bool _snareNode12;
        [RealtimeProperty(45, true, true)] private bool _snareNode13;
        [RealtimeProperty(46, true, true)] private bool _snareNode14;
        [RealtimeProperty(47, true, true)] private bool _snareNode15;
        [RealtimeProperty(48, true, true)] private bool _snareNode16;
        
        [RealtimeProperty(49, true, true)] private bool _tomNode1;
        [RealtimeProperty(50, true, true)] private bool _tomNode2;
        [RealtimeProperty(51, true, true)] private bool _tomNode3;
        [RealtimeProperty(52, true, true)] private bool _tomNode4;
        [RealtimeProperty(53, true, true)] private bool _tomNode5;
        [RealtimeProperty(54, true, true)] private bool _tomNode6;
        [RealtimeProperty(55, true, true)] private bool _tomNode7;
        [RealtimeProperty(56, true, true)] private bool _tomNode8;
        [RealtimeProperty(57, true, true)] private bool _tomNode9;
        [RealtimeProperty(58, true, true)] private bool _tomNode10;
        [RealtimeProperty(59, true, true)] private bool _tomNode11;
        [RealtimeProperty(60, true, true)] private bool _tomNode12;
        [RealtimeProperty(61, true, true)] private bool _tomNode13;
        [RealtimeProperty(62, true, true)] private bool _tomNode14;
        [RealtimeProperty(63, true, true)] private bool _tomNode15;
        [RealtimeProperty(64, true, true)] private bool _tomNode16;
        
        [RealtimeProperty(65, true, true)] private bool _cymbalNode1;
        [RealtimeProperty(66, true, true)] private bool _cymbalNode2;
        [RealtimeProperty(67, true, true)] private bool _cymbalNode3;
        [RealtimeProperty(68, true, true)] private bool _cymbalNode4;
        [RealtimeProperty(69, true, true)] private bool _cymbalNode5;
        [RealtimeProperty(70, true, true)] private bool _cymbalNode6;
        [RealtimeProperty(71, true, true)] private bool _cymbalNode7;
        [RealtimeProperty(72, true, true)] private bool _cymbalNode8;
        [RealtimeProperty(73, true, true)] private bool _cymbalNode9;
        [RealtimeProperty(74, true, true)] private bool _cymbalNode10;
        [RealtimeProperty(75, true, true)] private bool _cymbalNode11;
        [RealtimeProperty(76, true, true)] private bool _cymbalNode12;
        [RealtimeProperty(77, true, true)] private bool _cymbalNode13;
        [RealtimeProperty(78, true, true)] private bool _cymbalNode14;
        [RealtimeProperty(79, true, true)] private bool _cymbalNode15;
        [RealtimeProperty(80, true, true)] private bool _cymbalNode16;
    }


/* ----- Begin Normal Autogenerated Code ----- */
public partial class NodeSyncModel : RealtimeModel {
    public bool kickNode1 {
        get {
            return _kickNode1Property.value;
        }
        set {
            if (_kickNode1Property.value == value) return;
            _kickNode1Property.value = value;
            InvalidateReliableLength();
            FireKickNode1DidChange(value);
        }
    }
    
    public bool kickNode2 {
        get {
            return _kickNode2Property.value;
        }
        set {
            if (_kickNode2Property.value == value) return;
            _kickNode2Property.value = value;
            InvalidateReliableLength();
            FireKickNode2DidChange(value);
        }
    }
    
    public bool kickNode3 {
        get {
            return _kickNode3Property.value;
        }
        set {
            if (_kickNode3Property.value == value) return;
            _kickNode3Property.value = value;
            InvalidateReliableLength();
            FireKickNode3DidChange(value);
        }
    }
    
    public bool kickNode4 {
        get {
            return _kickNode4Property.value;
        }
        set {
            if (_kickNode4Property.value == value) return;
            _kickNode4Property.value = value;
            InvalidateReliableLength();
            FireKickNode4DidChange(value);
        }
    }
    
    public bool kickNode5 {
        get {
            return _kickNode5Property.value;
        }
        set {
            if (_kickNode5Property.value == value) return;
            _kickNode5Property.value = value;
            InvalidateReliableLength();
            FireKickNode5DidChange(value);
        }
    }
    
    public bool kickNode6 {
        get {
            return _kickNode6Property.value;
        }
        set {
            if (_kickNode6Property.value == value) return;
            _kickNode6Property.value = value;
            InvalidateReliableLength();
            FireKickNode6DidChange(value);
        }
    }
    
    public bool kickNode7 {
        get {
            return _kickNode7Property.value;
        }
        set {
            if (_kickNode7Property.value == value) return;
            _kickNode7Property.value = value;
            InvalidateReliableLength();
            FireKickNode7DidChange(value);
        }
    }
    
    public bool kickNode8 {
        get {
            return _kickNode8Property.value;
        }
        set {
            if (_kickNode8Property.value == value) return;
            _kickNode8Property.value = value;
            InvalidateReliableLength();
            FireKickNode8DidChange(value);
        }
    }
    
    public bool kickNode9 {
        get {
            return _kickNode9Property.value;
        }
        set {
            if (_kickNode9Property.value == value) return;
            _kickNode9Property.value = value;
            InvalidateReliableLength();
            FireKickNode9DidChange(value);
        }
    }
    
    public bool kickNode10 {
        get {
            return _kickNode10Property.value;
        }
        set {
            if (_kickNode10Property.value == value) return;
            _kickNode10Property.value = value;
            InvalidateReliableLength();
            FireKickNode10DidChange(value);
        }
    }
    
    public bool kickNode11 {
        get {
            return _kickNode11Property.value;
        }
        set {
            if (_kickNode11Property.value == value) return;
            _kickNode11Property.value = value;
            InvalidateReliableLength();
            FireKickNode11DidChange(value);
        }
    }
    
    public bool kickNode12 {
        get {
            return _kickNode12Property.value;
        }
        set {
            if (_kickNode12Property.value == value) return;
            _kickNode12Property.value = value;
            InvalidateReliableLength();
            FireKickNode12DidChange(value);
        }
    }
    
    public bool kickNode13 {
        get {
            return _kickNode13Property.value;
        }
        set {
            if (_kickNode13Property.value == value) return;
            _kickNode13Property.value = value;
            InvalidateReliableLength();
            FireKickNode13DidChange(value);
        }
    }
    
    public bool kickNode14 {
        get {
            return _kickNode14Property.value;
        }
        set {
            if (_kickNode14Property.value == value) return;
            _kickNode14Property.value = value;
            InvalidateReliableLength();
            FireKickNode14DidChange(value);
        }
    }
    
    public bool kickNode15 {
        get {
            return _kickNode15Property.value;
        }
        set {
            if (_kickNode15Property.value == value) return;
            _kickNode15Property.value = value;
            InvalidateReliableLength();
            FireKickNode15DidChange(value);
        }
    }
    
    public bool kickNode16 {
        get {
            return _kickNode16Property.value;
        }
        set {
            if (_kickNode16Property.value == value) return;
            _kickNode16Property.value = value;
            InvalidateReliableLength();
            FireKickNode16DidChange(value);
        }
    }
    
    public bool hiHatNode1 {
        get {
            return _hiHatNode1Property.value;
        }
        set {
            if (_hiHatNode1Property.value == value) return;
            _hiHatNode1Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode1DidChange(value);
        }
    }
    
    public bool hiHatNode2 {
        get {
            return _hiHatNode2Property.value;
        }
        set {
            if (_hiHatNode2Property.value == value) return;
            _hiHatNode2Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode2DidChange(value);
        }
    }
    
    public bool hiHatNode3 {
        get {
            return _hiHatNode3Property.value;
        }
        set {
            if (_hiHatNode3Property.value == value) return;
            _hiHatNode3Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode3DidChange(value);
        }
    }
    
    public bool hiHatNode4 {
        get {
            return _hiHatNode4Property.value;
        }
        set {
            if (_hiHatNode4Property.value == value) return;
            _hiHatNode4Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode4DidChange(value);
        }
    }
    
    public bool hiHatNode5 {
        get {
            return _hiHatNode5Property.value;
        }
        set {
            if (_hiHatNode5Property.value == value) return;
            _hiHatNode5Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode5DidChange(value);
        }
    }
    
    public bool hiHatNode6 {
        get {
            return _hiHatNode6Property.value;
        }
        set {
            if (_hiHatNode6Property.value == value) return;
            _hiHatNode6Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode6DidChange(value);
        }
    }
    
    public bool hiHatNode7 {
        get {
            return _hiHatNode7Property.value;
        }
        set {
            if (_hiHatNode7Property.value == value) return;
            _hiHatNode7Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode7DidChange(value);
        }
    }
    
    public bool hiHatNode8 {
        get {
            return _hiHatNode8Property.value;
        }
        set {
            if (_hiHatNode8Property.value == value) return;
            _hiHatNode8Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode8DidChange(value);
        }
    }
    
    public bool hiHatNode9 {
        get {
            return _hiHatNode9Property.value;
        }
        set {
            if (_hiHatNode9Property.value == value) return;
            _hiHatNode9Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode9DidChange(value);
        }
    }
    
    public bool hiHatNode10 {
        get {
            return _hiHatNode10Property.value;
        }
        set {
            if (_hiHatNode10Property.value == value) return;
            _hiHatNode10Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode10DidChange(value);
        }
    }
    
    public bool hiHatNode11 {
        get {
            return _hiHatNode11Property.value;
        }
        set {
            if (_hiHatNode11Property.value == value) return;
            _hiHatNode11Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode11DidChange(value);
        }
    }
    
    public bool hiHatNode12 {
        get {
            return _hiHatNode12Property.value;
        }
        set {
            if (_hiHatNode12Property.value == value) return;
            _hiHatNode12Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode12DidChange(value);
        }
    }
    
    public bool hiHatNode13 {
        get {
            return _hiHatNode13Property.value;
        }
        set {
            if (_hiHatNode13Property.value == value) return;
            _hiHatNode13Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode13DidChange(value);
        }
    }
    
    public bool hiHatNode14 {
        get {
            return _hiHatNode14Property.value;
        }
        set {
            if (_hiHatNode14Property.value == value) return;
            _hiHatNode14Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode14DidChange(value);
        }
    }
    
    public bool hiHatNode15 {
        get {
            return _hiHatNode15Property.value;
        }
        set {
            if (_hiHatNode15Property.value == value) return;
            _hiHatNode15Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode15DidChange(value);
        }
    }
    
    public bool hiHatNode16 {
        get {
            return _hiHatNode16Property.value;
        }
        set {
            if (_hiHatNode16Property.value == value) return;
            _hiHatNode16Property.value = value;
            InvalidateReliableLength();
            FireHiHatNode16DidChange(value);
        }
    }
    
    public bool snareNode1 {
        get {
            return _snareNode1Property.value;
        }
        set {
            if (_snareNode1Property.value == value) return;
            _snareNode1Property.value = value;
            InvalidateReliableLength();
            FireSnareNode1DidChange(value);
        }
    }
    
    public bool snareNode2 {
        get {
            return _snareNode2Property.value;
        }
        set {
            if (_snareNode2Property.value == value) return;
            _snareNode2Property.value = value;
            InvalidateReliableLength();
            FireSnareNode2DidChange(value);
        }
    }
    
    public bool snareNode3 {
        get {
            return _snareNode3Property.value;
        }
        set {
            if (_snareNode3Property.value == value) return;
            _snareNode3Property.value = value;
            InvalidateReliableLength();
            FireSnareNode3DidChange(value);
        }
    }
    
    public bool snareNode4 {
        get {
            return _snareNode4Property.value;
        }
        set {
            if (_snareNode4Property.value == value) return;
            _snareNode4Property.value = value;
            InvalidateReliableLength();
            FireSnareNode4DidChange(value);
        }
    }
    
    public bool snareNode5 {
        get {
            return _snareNode5Property.value;
        }
        set {
            if (_snareNode5Property.value == value) return;
            _snareNode5Property.value = value;
            InvalidateReliableLength();
            FireSnareNode5DidChange(value);
        }
    }
    
    public bool snareNode6 {
        get {
            return _snareNode6Property.value;
        }
        set {
            if (_snareNode6Property.value == value) return;
            _snareNode6Property.value = value;
            InvalidateReliableLength();
            FireSnareNode6DidChange(value);
        }
    }
    
    public bool snareNode7 {
        get {
            return _snareNode7Property.value;
        }
        set {
            if (_snareNode7Property.value == value) return;
            _snareNode7Property.value = value;
            InvalidateReliableLength();
            FireSnareNode7DidChange(value);
        }
    }
    
    public bool snareNode8 {
        get {
            return _snareNode8Property.value;
        }
        set {
            if (_snareNode8Property.value == value) return;
            _snareNode8Property.value = value;
            InvalidateReliableLength();
            FireSnareNode8DidChange(value);
        }
    }
    
    public bool snareNode9 {
        get {
            return _snareNode9Property.value;
        }
        set {
            if (_snareNode9Property.value == value) return;
            _snareNode9Property.value = value;
            InvalidateReliableLength();
            FireSnareNode9DidChange(value);
        }
    }
    
    public bool snareNode10 {
        get {
            return _snareNode10Property.value;
        }
        set {
            if (_snareNode10Property.value == value) return;
            _snareNode10Property.value = value;
            InvalidateReliableLength();
            FireSnareNode10DidChange(value);
        }
    }
    
    public bool snareNode11 {
        get {
            return _snareNode11Property.value;
        }
        set {
            if (_snareNode11Property.value == value) return;
            _snareNode11Property.value = value;
            InvalidateReliableLength();
            FireSnareNode11DidChange(value);
        }
    }
    
    public bool snareNode12 {
        get {
            return _snareNode12Property.value;
        }
        set {
            if (_snareNode12Property.value == value) return;
            _snareNode12Property.value = value;
            InvalidateReliableLength();
            FireSnareNode12DidChange(value);
        }
    }
    
    public bool snareNode13 {
        get {
            return _snareNode13Property.value;
        }
        set {
            if (_snareNode13Property.value == value) return;
            _snareNode13Property.value = value;
            InvalidateReliableLength();
            FireSnareNode13DidChange(value);
        }
    }
    
    public bool snareNode14 {
        get {
            return _snareNode14Property.value;
        }
        set {
            if (_snareNode14Property.value == value) return;
            _snareNode14Property.value = value;
            InvalidateReliableLength();
            FireSnareNode14DidChange(value);
        }
    }
    
    public bool snareNode15 {
        get {
            return _snareNode15Property.value;
        }
        set {
            if (_snareNode15Property.value == value) return;
            _snareNode15Property.value = value;
            InvalidateReliableLength();
            FireSnareNode15DidChange(value);
        }
    }
    
    public bool snareNode16 {
        get {
            return _snareNode16Property.value;
        }
        set {
            if (_snareNode16Property.value == value) return;
            _snareNode16Property.value = value;
            InvalidateReliableLength();
            FireSnareNode16DidChange(value);
        }
    }
    
    public bool tomNode1 {
        get {
            return _tomNode1Property.value;
        }
        set {
            if (_tomNode1Property.value == value) return;
            _tomNode1Property.value = value;
            InvalidateReliableLength();
            FireTomNode1DidChange(value);
        }
    }
    
    public bool tomNode2 {
        get {
            return _tomNode2Property.value;
        }
        set {
            if (_tomNode2Property.value == value) return;
            _tomNode2Property.value = value;
            InvalidateReliableLength();
            FireTomNode2DidChange(value);
        }
    }
    
    public bool tomNode3 {
        get {
            return _tomNode3Property.value;
        }
        set {
            if (_tomNode3Property.value == value) return;
            _tomNode3Property.value = value;
            InvalidateReliableLength();
            FireTomNode3DidChange(value);
        }
    }
    
    public bool tomNode4 {
        get {
            return _tomNode4Property.value;
        }
        set {
            if (_tomNode4Property.value == value) return;
            _tomNode4Property.value = value;
            InvalidateReliableLength();
            FireTomNode4DidChange(value);
        }
    }
    
    public bool tomNode5 {
        get {
            return _tomNode5Property.value;
        }
        set {
            if (_tomNode5Property.value == value) return;
            _tomNode5Property.value = value;
            InvalidateReliableLength();
            FireTomNode5DidChange(value);
        }
    }
    
    public bool tomNode6 {
        get {
            return _tomNode6Property.value;
        }
        set {
            if (_tomNode6Property.value == value) return;
            _tomNode6Property.value = value;
            InvalidateReliableLength();
            FireTomNode6DidChange(value);
        }
    }
    
    public bool tomNode7 {
        get {
            return _tomNode7Property.value;
        }
        set {
            if (_tomNode7Property.value == value) return;
            _tomNode7Property.value = value;
            InvalidateReliableLength();
            FireTomNode7DidChange(value);
        }
    }
    
    public bool tomNode8 {
        get {
            return _tomNode8Property.value;
        }
        set {
            if (_tomNode8Property.value == value) return;
            _tomNode8Property.value = value;
            InvalidateReliableLength();
            FireTomNode8DidChange(value);
        }
    }
    
    public bool tomNode9 {
        get {
            return _tomNode9Property.value;
        }
        set {
            if (_tomNode9Property.value == value) return;
            _tomNode9Property.value = value;
            InvalidateReliableLength();
            FireTomNode9DidChange(value);
        }
    }
    
    public bool tomNode10 {
        get {
            return _tomNode10Property.value;
        }
        set {
            if (_tomNode10Property.value == value) return;
            _tomNode10Property.value = value;
            InvalidateReliableLength();
            FireTomNode10DidChange(value);
        }
    }
    
    public bool tomNode11 {
        get {
            return _tomNode11Property.value;
        }
        set {
            if (_tomNode11Property.value == value) return;
            _tomNode11Property.value = value;
            InvalidateReliableLength();
            FireTomNode11DidChange(value);
        }
    }
    
    public bool tomNode12 {
        get {
            return _tomNode12Property.value;
        }
        set {
            if (_tomNode12Property.value == value) return;
            _tomNode12Property.value = value;
            InvalidateReliableLength();
            FireTomNode12DidChange(value);
        }
    }
    
    public bool tomNode13 {
        get {
            return _tomNode13Property.value;
        }
        set {
            if (_tomNode13Property.value == value) return;
            _tomNode13Property.value = value;
            InvalidateReliableLength();
            FireTomNode13DidChange(value);
        }
    }
    
    public bool tomNode14 {
        get {
            return _tomNode14Property.value;
        }
        set {
            if (_tomNode14Property.value == value) return;
            _tomNode14Property.value = value;
            InvalidateReliableLength();
            FireTomNode14DidChange(value);
        }
    }
    
    public bool tomNode15 {
        get {
            return _tomNode15Property.value;
        }
        set {
            if (_tomNode15Property.value == value) return;
            _tomNode15Property.value = value;
            InvalidateReliableLength();
            FireTomNode15DidChange(value);
        }
    }
    
    public bool tomNode16 {
        get {
            return _tomNode16Property.value;
        }
        set {
            if (_tomNode16Property.value == value) return;
            _tomNode16Property.value = value;
            InvalidateReliableLength();
            FireTomNode16DidChange(value);
        }
    }
    
    public bool cymbalNode1 {
        get {
            return _cymbalNode1Property.value;
        }
        set {
            if (_cymbalNode1Property.value == value) return;
            _cymbalNode1Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode1DidChange(value);
        }
    }
    
    public bool cymbalNode2 {
        get {
            return _cymbalNode2Property.value;
        }
        set {
            if (_cymbalNode2Property.value == value) return;
            _cymbalNode2Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode2DidChange(value);
        }
    }
    
    public bool cymbalNode3 {
        get {
            return _cymbalNode3Property.value;
        }
        set {
            if (_cymbalNode3Property.value == value) return;
            _cymbalNode3Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode3DidChange(value);
        }
    }
    
    public bool cymbalNode4 {
        get {
            return _cymbalNode4Property.value;
        }
        set {
            if (_cymbalNode4Property.value == value) return;
            _cymbalNode4Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode4DidChange(value);
        }
    }
    
    public bool cymbalNode5 {
        get {
            return _cymbalNode5Property.value;
        }
        set {
            if (_cymbalNode5Property.value == value) return;
            _cymbalNode5Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode5DidChange(value);
        }
    }
    
    public bool cymbalNode6 {
        get {
            return _cymbalNode6Property.value;
        }
        set {
            if (_cymbalNode6Property.value == value) return;
            _cymbalNode6Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode6DidChange(value);
        }
    }
    
    public bool cymbalNode7 {
        get {
            return _cymbalNode7Property.value;
        }
        set {
            if (_cymbalNode7Property.value == value) return;
            _cymbalNode7Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode7DidChange(value);
        }
    }
    
    public bool cymbalNode8 {
        get {
            return _cymbalNode8Property.value;
        }
        set {
            if (_cymbalNode8Property.value == value) return;
            _cymbalNode8Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode8DidChange(value);
        }
    }
    
    public bool cymbalNode9 {
        get {
            return _cymbalNode9Property.value;
        }
        set {
            if (_cymbalNode9Property.value == value) return;
            _cymbalNode9Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode9DidChange(value);
        }
    }
    
    public bool cymbalNode10 {
        get {
            return _cymbalNode10Property.value;
        }
        set {
            if (_cymbalNode10Property.value == value) return;
            _cymbalNode10Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode10DidChange(value);
        }
    }
    
    public bool cymbalNode11 {
        get {
            return _cymbalNode11Property.value;
        }
        set {
            if (_cymbalNode11Property.value == value) return;
            _cymbalNode11Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode11DidChange(value);
        }
    }
    
    public bool cymbalNode12 {
        get {
            return _cymbalNode12Property.value;
        }
        set {
            if (_cymbalNode12Property.value == value) return;
            _cymbalNode12Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode12DidChange(value);
        }
    }
    
    public bool cymbalNode13 {
        get {
            return _cymbalNode13Property.value;
        }
        set {
            if (_cymbalNode13Property.value == value) return;
            _cymbalNode13Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode13DidChange(value);
        }
    }
    
    public bool cymbalNode14 {
        get {
            return _cymbalNode14Property.value;
        }
        set {
            if (_cymbalNode14Property.value == value) return;
            _cymbalNode14Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode14DidChange(value);
        }
    }
    
    public bool cymbalNode15 {
        get {
            return _cymbalNode15Property.value;
        }
        set {
            if (_cymbalNode15Property.value == value) return;
            _cymbalNode15Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode15DidChange(value);
        }
    }
    
    public bool cymbalNode16 {
        get {
            return _cymbalNode16Property.value;
        }
        set {
            if (_cymbalNode16Property.value == value) return;
            _cymbalNode16Property.value = value;
            InvalidateReliableLength();
            FireCymbalNode16DidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(NodeSyncModel model, T value);
    public event PropertyChangedHandler<bool> kickNode1DidChange;
    public event PropertyChangedHandler<bool> kickNode2DidChange;
    public event PropertyChangedHandler<bool> kickNode3DidChange;
    public event PropertyChangedHandler<bool> kickNode4DidChange;
    public event PropertyChangedHandler<bool> kickNode5DidChange;
    public event PropertyChangedHandler<bool> kickNode6DidChange;
    public event PropertyChangedHandler<bool> kickNode7DidChange;
    public event PropertyChangedHandler<bool> kickNode8DidChange;
    public event PropertyChangedHandler<bool> kickNode9DidChange;
    public event PropertyChangedHandler<bool> kickNode10DidChange;
    public event PropertyChangedHandler<bool> kickNode11DidChange;
    public event PropertyChangedHandler<bool> kickNode12DidChange;
    public event PropertyChangedHandler<bool> kickNode13DidChange;
    public event PropertyChangedHandler<bool> kickNode14DidChange;
    public event PropertyChangedHandler<bool> kickNode15DidChange;
    public event PropertyChangedHandler<bool> kickNode16DidChange;
    public event PropertyChangedHandler<bool> hiHatNode1DidChange;
    public event PropertyChangedHandler<bool> hiHatNode2DidChange;
    public event PropertyChangedHandler<bool> hiHatNode3DidChange;
    public event PropertyChangedHandler<bool> hiHatNode4DidChange;
    public event PropertyChangedHandler<bool> hiHatNode5DidChange;
    public event PropertyChangedHandler<bool> hiHatNode6DidChange;
    public event PropertyChangedHandler<bool> hiHatNode7DidChange;
    public event PropertyChangedHandler<bool> hiHatNode8DidChange;
    public event PropertyChangedHandler<bool> hiHatNode9DidChange;
    public event PropertyChangedHandler<bool> hiHatNode10DidChange;
    public event PropertyChangedHandler<bool> hiHatNode11DidChange;
    public event PropertyChangedHandler<bool> hiHatNode12DidChange;
    public event PropertyChangedHandler<bool> hiHatNode13DidChange;
    public event PropertyChangedHandler<bool> hiHatNode14DidChange;
    public event PropertyChangedHandler<bool> hiHatNode15DidChange;
    public event PropertyChangedHandler<bool> hiHatNode16DidChange;
    public event PropertyChangedHandler<bool> snareNode1DidChange;
    public event PropertyChangedHandler<bool> snareNode2DidChange;
    public event PropertyChangedHandler<bool> snareNode3DidChange;
    public event PropertyChangedHandler<bool> snareNode4DidChange;
    public event PropertyChangedHandler<bool> snareNode5DidChange;
    public event PropertyChangedHandler<bool> snareNode6DidChange;
    public event PropertyChangedHandler<bool> snareNode7DidChange;
    public event PropertyChangedHandler<bool> snareNode8DidChange;
    public event PropertyChangedHandler<bool> snareNode9DidChange;
    public event PropertyChangedHandler<bool> snareNode10DidChange;
    public event PropertyChangedHandler<bool> snareNode11DidChange;
    public event PropertyChangedHandler<bool> snareNode12DidChange;
    public event PropertyChangedHandler<bool> snareNode13DidChange;
    public event PropertyChangedHandler<bool> snareNode14DidChange;
    public event PropertyChangedHandler<bool> snareNode15DidChange;
    public event PropertyChangedHandler<bool> snareNode16DidChange;
    public event PropertyChangedHandler<bool> tomNode1DidChange;
    public event PropertyChangedHandler<bool> tomNode2DidChange;
    public event PropertyChangedHandler<bool> tomNode3DidChange;
    public event PropertyChangedHandler<bool> tomNode4DidChange;
    public event PropertyChangedHandler<bool> tomNode5DidChange;
    public event PropertyChangedHandler<bool> tomNode6DidChange;
    public event PropertyChangedHandler<bool> tomNode7DidChange;
    public event PropertyChangedHandler<bool> tomNode8DidChange;
    public event PropertyChangedHandler<bool> tomNode9DidChange;
    public event PropertyChangedHandler<bool> tomNode10DidChange;
    public event PropertyChangedHandler<bool> tomNode11DidChange;
    public event PropertyChangedHandler<bool> tomNode12DidChange;
    public event PropertyChangedHandler<bool> tomNode13DidChange;
    public event PropertyChangedHandler<bool> tomNode14DidChange;
    public event PropertyChangedHandler<bool> tomNode15DidChange;
    public event PropertyChangedHandler<bool> tomNode16DidChange;
    public event PropertyChangedHandler<bool> cymbalNode1DidChange;
    public event PropertyChangedHandler<bool> cymbalNode2DidChange;
    public event PropertyChangedHandler<bool> cymbalNode3DidChange;
    public event PropertyChangedHandler<bool> cymbalNode4DidChange;
    public event PropertyChangedHandler<bool> cymbalNode5DidChange;
    public event PropertyChangedHandler<bool> cymbalNode6DidChange;
    public event PropertyChangedHandler<bool> cymbalNode7DidChange;
    public event PropertyChangedHandler<bool> cymbalNode8DidChange;
    public event PropertyChangedHandler<bool> cymbalNode9DidChange;
    public event PropertyChangedHandler<bool> cymbalNode10DidChange;
    public event PropertyChangedHandler<bool> cymbalNode11DidChange;
    public event PropertyChangedHandler<bool> cymbalNode12DidChange;
    public event PropertyChangedHandler<bool> cymbalNode13DidChange;
    public event PropertyChangedHandler<bool> cymbalNode14DidChange;
    public event PropertyChangedHandler<bool> cymbalNode15DidChange;
    public event PropertyChangedHandler<bool> cymbalNode16DidChange;
    
    public enum PropertyID : uint {
        KickNode1 = 1,
        KickNode2 = 2,
        KickNode3 = 3,
        KickNode4 = 4,
        KickNode5 = 5,
        KickNode6 = 6,
        KickNode7 = 7,
        KickNode8 = 8,
        KickNode9 = 9,
        KickNode10 = 10,
        KickNode11 = 11,
        KickNode12 = 12,
        KickNode13 = 13,
        KickNode14 = 14,
        KickNode15 = 15,
        KickNode16 = 16,
        HiHatNode1 = 17,
        HiHatNode2 = 18,
        HiHatNode3 = 19,
        HiHatNode4 = 20,
        HiHatNode5 = 21,
        HiHatNode6 = 22,
        HiHatNode7 = 23,
        HiHatNode8 = 24,
        HiHatNode9 = 25,
        HiHatNode10 = 26,
        HiHatNode11 = 27,
        HiHatNode12 = 28,
        HiHatNode13 = 29,
        HiHatNode14 = 30,
        HiHatNode15 = 31,
        HiHatNode16 = 32,
        SnareNode1 = 33,
        SnareNode2 = 34,
        SnareNode3 = 35,
        SnareNode4 = 36,
        SnareNode5 = 37,
        SnareNode6 = 38,
        SnareNode7 = 39,
        SnareNode8 = 40,
        SnareNode9 = 41,
        SnareNode10 = 42,
        SnareNode11 = 43,
        SnareNode12 = 44,
        SnareNode13 = 45,
        SnareNode14 = 46,
        SnareNode15 = 47,
        SnareNode16 = 48,
        TomNode1 = 49,
        TomNode2 = 50,
        TomNode3 = 51,
        TomNode4 = 52,
        TomNode5 = 53,
        TomNode6 = 54,
        TomNode7 = 55,
        TomNode8 = 56,
        TomNode9 = 57,
        TomNode10 = 58,
        TomNode11 = 59,
        TomNode12 = 60,
        TomNode13 = 61,
        TomNode14 = 62,
        TomNode15 = 63,
        TomNode16 = 64,
        CymbalNode1 = 65,
        CymbalNode2 = 66,
        CymbalNode3 = 67,
        CymbalNode4 = 68,
        CymbalNode5 = 69,
        CymbalNode6 = 70,
        CymbalNode7 = 71,
        CymbalNode8 = 72,
        CymbalNode9 = 73,
        CymbalNode10 = 74,
        CymbalNode11 = 75,
        CymbalNode12 = 76,
        CymbalNode13 = 77,
        CymbalNode14 = 78,
        CymbalNode15 = 79,
        CymbalNode16 = 80,
    }
    
    #region Properties
    
    private ReliableProperty<bool> _kickNode1Property;
    
    private ReliableProperty<bool> _kickNode2Property;
    
    private ReliableProperty<bool> _kickNode3Property;
    
    private ReliableProperty<bool> _kickNode4Property;
    
    private ReliableProperty<bool> _kickNode5Property;
    
    private ReliableProperty<bool> _kickNode6Property;
    
    private ReliableProperty<bool> _kickNode7Property;
    
    private ReliableProperty<bool> _kickNode8Property;
    
    private ReliableProperty<bool> _kickNode9Property;
    
    private ReliableProperty<bool> _kickNode10Property;
    
    private ReliableProperty<bool> _kickNode11Property;
    
    private ReliableProperty<bool> _kickNode12Property;
    
    private ReliableProperty<bool> _kickNode13Property;
    
    private ReliableProperty<bool> _kickNode14Property;
    
    private ReliableProperty<bool> _kickNode15Property;
    
    private ReliableProperty<bool> _kickNode16Property;
    
    private ReliableProperty<bool> _hiHatNode1Property;
    
    private ReliableProperty<bool> _hiHatNode2Property;
    
    private ReliableProperty<bool> _hiHatNode3Property;
    
    private ReliableProperty<bool> _hiHatNode4Property;
    
    private ReliableProperty<bool> _hiHatNode5Property;
    
    private ReliableProperty<bool> _hiHatNode6Property;
    
    private ReliableProperty<bool> _hiHatNode7Property;
    
    private ReliableProperty<bool> _hiHatNode8Property;
    
    private ReliableProperty<bool> _hiHatNode9Property;
    
    private ReliableProperty<bool> _hiHatNode10Property;
    
    private ReliableProperty<bool> _hiHatNode11Property;
    
    private ReliableProperty<bool> _hiHatNode12Property;
    
    private ReliableProperty<bool> _hiHatNode13Property;
    
    private ReliableProperty<bool> _hiHatNode14Property;
    
    private ReliableProperty<bool> _hiHatNode15Property;
    
    private ReliableProperty<bool> _hiHatNode16Property;
    
    private ReliableProperty<bool> _snareNode1Property;
    
    private ReliableProperty<bool> _snareNode2Property;
    
    private ReliableProperty<bool> _snareNode3Property;
    
    private ReliableProperty<bool> _snareNode4Property;
    
    private ReliableProperty<bool> _snareNode5Property;
    
    private ReliableProperty<bool> _snareNode6Property;
    
    private ReliableProperty<bool> _snareNode7Property;
    
    private ReliableProperty<bool> _snareNode8Property;
    
    private ReliableProperty<bool> _snareNode9Property;
    
    private ReliableProperty<bool> _snareNode10Property;
    
    private ReliableProperty<bool> _snareNode11Property;
    
    private ReliableProperty<bool> _snareNode12Property;
    
    private ReliableProperty<bool> _snareNode13Property;
    
    private ReliableProperty<bool> _snareNode14Property;
    
    private ReliableProperty<bool> _snareNode15Property;
    
    private ReliableProperty<bool> _snareNode16Property;
    
    private ReliableProperty<bool> _tomNode1Property;
    
    private ReliableProperty<bool> _tomNode2Property;
    
    private ReliableProperty<bool> _tomNode3Property;
    
    private ReliableProperty<bool> _tomNode4Property;
    
    private ReliableProperty<bool> _tomNode5Property;
    
    private ReliableProperty<bool> _tomNode6Property;
    
    private ReliableProperty<bool> _tomNode7Property;
    
    private ReliableProperty<bool> _tomNode8Property;
    
    private ReliableProperty<bool> _tomNode9Property;
    
    private ReliableProperty<bool> _tomNode10Property;
    
    private ReliableProperty<bool> _tomNode11Property;
    
    private ReliableProperty<bool> _tomNode12Property;
    
    private ReliableProperty<bool> _tomNode13Property;
    
    private ReliableProperty<bool> _tomNode14Property;
    
    private ReliableProperty<bool> _tomNode15Property;
    
    private ReliableProperty<bool> _tomNode16Property;
    
    private ReliableProperty<bool> _cymbalNode1Property;
    
    private ReliableProperty<bool> _cymbalNode2Property;
    
    private ReliableProperty<bool> _cymbalNode3Property;
    
    private ReliableProperty<bool> _cymbalNode4Property;
    
    private ReliableProperty<bool> _cymbalNode5Property;
    
    private ReliableProperty<bool> _cymbalNode6Property;
    
    private ReliableProperty<bool> _cymbalNode7Property;
    
    private ReliableProperty<bool> _cymbalNode8Property;
    
    private ReliableProperty<bool> _cymbalNode9Property;
    
    private ReliableProperty<bool> _cymbalNode10Property;
    
    private ReliableProperty<bool> _cymbalNode11Property;
    
    private ReliableProperty<bool> _cymbalNode12Property;
    
    private ReliableProperty<bool> _cymbalNode13Property;
    
    private ReliableProperty<bool> _cymbalNode14Property;
    
    private ReliableProperty<bool> _cymbalNode15Property;
    
    private ReliableProperty<bool> _cymbalNode16Property;
    
    #endregion
    
    public NodeSyncModel() : base(null) {
        _kickNode1Property = new ReliableProperty<bool>(1, _kickNode1);
        _kickNode2Property = new ReliableProperty<bool>(2, _kickNode2);
        _kickNode3Property = new ReliableProperty<bool>(3, _kickNode3);
        _kickNode4Property = new ReliableProperty<bool>(4, _kickNode4);
        _kickNode5Property = new ReliableProperty<bool>(5, _kickNode5);
        _kickNode6Property = new ReliableProperty<bool>(6, _kickNode6);
        _kickNode7Property = new ReliableProperty<bool>(7, _kickNode7);
        _kickNode8Property = new ReliableProperty<bool>(8, _kickNode8);
        _kickNode9Property = new ReliableProperty<bool>(9, _kickNode9);
        _kickNode10Property = new ReliableProperty<bool>(10, _kickNode10);
        _kickNode11Property = new ReliableProperty<bool>(11, _kickNode11);
        _kickNode12Property = new ReliableProperty<bool>(12, _kickNode12);
        _kickNode13Property = new ReliableProperty<bool>(13, _kickNode13);
        _kickNode14Property = new ReliableProperty<bool>(14, _kickNode14);
        _kickNode15Property = new ReliableProperty<bool>(15, _kickNode15);
        _kickNode16Property = new ReliableProperty<bool>(16, _kickNode16);
        _hiHatNode1Property = new ReliableProperty<bool>(17, _hiHatNode1);
        _hiHatNode2Property = new ReliableProperty<bool>(18, _hiHatNode2);
        _hiHatNode3Property = new ReliableProperty<bool>(19, _hiHatNode3);
        _hiHatNode4Property = new ReliableProperty<bool>(20, _hiHatNode4);
        _hiHatNode5Property = new ReliableProperty<bool>(21, _hiHatNode5);
        _hiHatNode6Property = new ReliableProperty<bool>(22, _hiHatNode6);
        _hiHatNode7Property = new ReliableProperty<bool>(23, _hiHatNode7);
        _hiHatNode8Property = new ReliableProperty<bool>(24, _hiHatNode8);
        _hiHatNode9Property = new ReliableProperty<bool>(25, _hiHatNode9);
        _hiHatNode10Property = new ReliableProperty<bool>(26, _hiHatNode10);
        _hiHatNode11Property = new ReliableProperty<bool>(27, _hiHatNode11);
        _hiHatNode12Property = new ReliableProperty<bool>(28, _hiHatNode12);
        _hiHatNode13Property = new ReliableProperty<bool>(29, _hiHatNode13);
        _hiHatNode14Property = new ReliableProperty<bool>(30, _hiHatNode14);
        _hiHatNode15Property = new ReliableProperty<bool>(31, _hiHatNode15);
        _hiHatNode16Property = new ReliableProperty<bool>(32, _hiHatNode16);
        _snareNode1Property = new ReliableProperty<bool>(33, _snareNode1);
        _snareNode2Property = new ReliableProperty<bool>(34, _snareNode2);
        _snareNode3Property = new ReliableProperty<bool>(35, _snareNode3);
        _snareNode4Property = new ReliableProperty<bool>(36, _snareNode4);
        _snareNode5Property = new ReliableProperty<bool>(37, _snareNode5);
        _snareNode6Property = new ReliableProperty<bool>(38, _snareNode6);
        _snareNode7Property = new ReliableProperty<bool>(39, _snareNode7);
        _snareNode8Property = new ReliableProperty<bool>(40, _snareNode8);
        _snareNode9Property = new ReliableProperty<bool>(41, _snareNode9);
        _snareNode10Property = new ReliableProperty<bool>(42, _snareNode10);
        _snareNode11Property = new ReliableProperty<bool>(43, _snareNode11);
        _snareNode12Property = new ReliableProperty<bool>(44, _snareNode12);
        _snareNode13Property = new ReliableProperty<bool>(45, _snareNode13);
        _snareNode14Property = new ReliableProperty<bool>(46, _snareNode14);
        _snareNode15Property = new ReliableProperty<bool>(47, _snareNode15);
        _snareNode16Property = new ReliableProperty<bool>(48, _snareNode16);
        _tomNode1Property = new ReliableProperty<bool>(49, _tomNode1);
        _tomNode2Property = new ReliableProperty<bool>(50, _tomNode2);
        _tomNode3Property = new ReliableProperty<bool>(51, _tomNode3);
        _tomNode4Property = new ReliableProperty<bool>(52, _tomNode4);
        _tomNode5Property = new ReliableProperty<bool>(53, _tomNode5);
        _tomNode6Property = new ReliableProperty<bool>(54, _tomNode6);
        _tomNode7Property = new ReliableProperty<bool>(55, _tomNode7);
        _tomNode8Property = new ReliableProperty<bool>(56, _tomNode8);
        _tomNode9Property = new ReliableProperty<bool>(57, _tomNode9);
        _tomNode10Property = new ReliableProperty<bool>(58, _tomNode10);
        _tomNode11Property = new ReliableProperty<bool>(59, _tomNode11);
        _tomNode12Property = new ReliableProperty<bool>(60, _tomNode12);
        _tomNode13Property = new ReliableProperty<bool>(61, _tomNode13);
        _tomNode14Property = new ReliableProperty<bool>(62, _tomNode14);
        _tomNode15Property = new ReliableProperty<bool>(63, _tomNode15);
        _tomNode16Property = new ReliableProperty<bool>(64, _tomNode16);
        _cymbalNode1Property = new ReliableProperty<bool>(65, _cymbalNode1);
        _cymbalNode2Property = new ReliableProperty<bool>(66, _cymbalNode2);
        _cymbalNode3Property = new ReliableProperty<bool>(67, _cymbalNode3);
        _cymbalNode4Property = new ReliableProperty<bool>(68, _cymbalNode4);
        _cymbalNode5Property = new ReliableProperty<bool>(69, _cymbalNode5);
        _cymbalNode6Property = new ReliableProperty<bool>(70, _cymbalNode6);
        _cymbalNode7Property = new ReliableProperty<bool>(71, _cymbalNode7);
        _cymbalNode8Property = new ReliableProperty<bool>(72, _cymbalNode8);
        _cymbalNode9Property = new ReliableProperty<bool>(73, _cymbalNode9);
        _cymbalNode10Property = new ReliableProperty<bool>(74, _cymbalNode10);
        _cymbalNode11Property = new ReliableProperty<bool>(75, _cymbalNode11);
        _cymbalNode12Property = new ReliableProperty<bool>(76, _cymbalNode12);
        _cymbalNode13Property = new ReliableProperty<bool>(77, _cymbalNode13);
        _cymbalNode14Property = new ReliableProperty<bool>(78, _cymbalNode14);
        _cymbalNode15Property = new ReliableProperty<bool>(79, _cymbalNode15);
        _cymbalNode16Property = new ReliableProperty<bool>(80, _cymbalNode16);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _kickNode1Property.UnsubscribeCallback();
        _kickNode2Property.UnsubscribeCallback();
        _kickNode3Property.UnsubscribeCallback();
        _kickNode4Property.UnsubscribeCallback();
        _kickNode5Property.UnsubscribeCallback();
        _kickNode6Property.UnsubscribeCallback();
        _kickNode7Property.UnsubscribeCallback();
        _kickNode8Property.UnsubscribeCallback();
        _kickNode9Property.UnsubscribeCallback();
        _kickNode10Property.UnsubscribeCallback();
        _kickNode11Property.UnsubscribeCallback();
        _kickNode12Property.UnsubscribeCallback();
        _kickNode13Property.UnsubscribeCallback();
        _kickNode14Property.UnsubscribeCallback();
        _kickNode15Property.UnsubscribeCallback();
        _kickNode16Property.UnsubscribeCallback();
        _hiHatNode1Property.UnsubscribeCallback();
        _hiHatNode2Property.UnsubscribeCallback();
        _hiHatNode3Property.UnsubscribeCallback();
        _hiHatNode4Property.UnsubscribeCallback();
        _hiHatNode5Property.UnsubscribeCallback();
        _hiHatNode6Property.UnsubscribeCallback();
        _hiHatNode7Property.UnsubscribeCallback();
        _hiHatNode8Property.UnsubscribeCallback();
        _hiHatNode9Property.UnsubscribeCallback();
        _hiHatNode10Property.UnsubscribeCallback();
        _hiHatNode11Property.UnsubscribeCallback();
        _hiHatNode12Property.UnsubscribeCallback();
        _hiHatNode13Property.UnsubscribeCallback();
        _hiHatNode14Property.UnsubscribeCallback();
        _hiHatNode15Property.UnsubscribeCallback();
        _hiHatNode16Property.UnsubscribeCallback();
        _snareNode1Property.UnsubscribeCallback();
        _snareNode2Property.UnsubscribeCallback();
        _snareNode3Property.UnsubscribeCallback();
        _snareNode4Property.UnsubscribeCallback();
        _snareNode5Property.UnsubscribeCallback();
        _snareNode6Property.UnsubscribeCallback();
        _snareNode7Property.UnsubscribeCallback();
        _snareNode8Property.UnsubscribeCallback();
        _snareNode9Property.UnsubscribeCallback();
        _snareNode10Property.UnsubscribeCallback();
        _snareNode11Property.UnsubscribeCallback();
        _snareNode12Property.UnsubscribeCallback();
        _snareNode13Property.UnsubscribeCallback();
        _snareNode14Property.UnsubscribeCallback();
        _snareNode15Property.UnsubscribeCallback();
        _snareNode16Property.UnsubscribeCallback();
        _tomNode1Property.UnsubscribeCallback();
        _tomNode2Property.UnsubscribeCallback();
        _tomNode3Property.UnsubscribeCallback();
        _tomNode4Property.UnsubscribeCallback();
        _tomNode5Property.UnsubscribeCallback();
        _tomNode6Property.UnsubscribeCallback();
        _tomNode7Property.UnsubscribeCallback();
        _tomNode8Property.UnsubscribeCallback();
        _tomNode9Property.UnsubscribeCallback();
        _tomNode10Property.UnsubscribeCallback();
        _tomNode11Property.UnsubscribeCallback();
        _tomNode12Property.UnsubscribeCallback();
        _tomNode13Property.UnsubscribeCallback();
        _tomNode14Property.UnsubscribeCallback();
        _tomNode15Property.UnsubscribeCallback();
        _tomNode16Property.UnsubscribeCallback();
        _cymbalNode1Property.UnsubscribeCallback();
        _cymbalNode2Property.UnsubscribeCallback();
        _cymbalNode3Property.UnsubscribeCallback();
        _cymbalNode4Property.UnsubscribeCallback();
        _cymbalNode5Property.UnsubscribeCallback();
        _cymbalNode6Property.UnsubscribeCallback();
        _cymbalNode7Property.UnsubscribeCallback();
        _cymbalNode8Property.UnsubscribeCallback();
        _cymbalNode9Property.UnsubscribeCallback();
        _cymbalNode10Property.UnsubscribeCallback();
        _cymbalNode11Property.UnsubscribeCallback();
        _cymbalNode12Property.UnsubscribeCallback();
        _cymbalNode13Property.UnsubscribeCallback();
        _cymbalNode14Property.UnsubscribeCallback();
        _cymbalNode15Property.UnsubscribeCallback();
        _cymbalNode16Property.UnsubscribeCallback();
    }
    
    private void FireKickNode1DidChange(bool value) {
        try {
            kickNode1DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode2DidChange(bool value) {
        try {
            kickNode2DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode3DidChange(bool value) {
        try {
            kickNode3DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode4DidChange(bool value) {
        try {
            kickNode4DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode5DidChange(bool value) {
        try {
            kickNode5DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode6DidChange(bool value) {
        try {
            kickNode6DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode7DidChange(bool value) {
        try {
            kickNode7DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode8DidChange(bool value) {
        try {
            kickNode8DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode9DidChange(bool value) {
        try {
            kickNode9DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode10DidChange(bool value) {
        try {
            kickNode10DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode11DidChange(bool value) {
        try {
            kickNode11DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode12DidChange(bool value) {
        try {
            kickNode12DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode13DidChange(bool value) {
        try {
            kickNode13DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode14DidChange(bool value) {
        try {
            kickNode14DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode15DidChange(bool value) {
        try {
            kickNode15DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKickNode16DidChange(bool value) {
        try {
            kickNode16DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode1DidChange(bool value) {
        try {
            hiHatNode1DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode2DidChange(bool value) {
        try {
            hiHatNode2DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode3DidChange(bool value) {
        try {
            hiHatNode3DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode4DidChange(bool value) {
        try {
            hiHatNode4DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode5DidChange(bool value) {
        try {
            hiHatNode5DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode6DidChange(bool value) {
        try {
            hiHatNode6DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode7DidChange(bool value) {
        try {
            hiHatNode7DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode8DidChange(bool value) {
        try {
            hiHatNode8DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode9DidChange(bool value) {
        try {
            hiHatNode9DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode10DidChange(bool value) {
        try {
            hiHatNode10DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode11DidChange(bool value) {
        try {
            hiHatNode11DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode12DidChange(bool value) {
        try {
            hiHatNode12DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode13DidChange(bool value) {
        try {
            hiHatNode13DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode14DidChange(bool value) {
        try {
            hiHatNode14DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode15DidChange(bool value) {
        try {
            hiHatNode15DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireHiHatNode16DidChange(bool value) {
        try {
            hiHatNode16DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode1DidChange(bool value) {
        try {
            snareNode1DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode2DidChange(bool value) {
        try {
            snareNode2DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode3DidChange(bool value) {
        try {
            snareNode3DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode4DidChange(bool value) {
        try {
            snareNode4DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode5DidChange(bool value) {
        try {
            snareNode5DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode6DidChange(bool value) {
        try {
            snareNode6DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode7DidChange(bool value) {
        try {
            snareNode7DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode8DidChange(bool value) {
        try {
            snareNode8DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode9DidChange(bool value) {
        try {
            snareNode9DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode10DidChange(bool value) {
        try {
            snareNode10DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode11DidChange(bool value) {
        try {
            snareNode11DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode12DidChange(bool value) {
        try {
            snareNode12DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode13DidChange(bool value) {
        try {
            snareNode13DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode14DidChange(bool value) {
        try {
            snareNode14DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode15DidChange(bool value) {
        try {
            snareNode15DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSnareNode16DidChange(bool value) {
        try {
            snareNode16DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode1DidChange(bool value) {
        try {
            tomNode1DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode2DidChange(bool value) {
        try {
            tomNode2DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode3DidChange(bool value) {
        try {
            tomNode3DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode4DidChange(bool value) {
        try {
            tomNode4DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode5DidChange(bool value) {
        try {
            tomNode5DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode6DidChange(bool value) {
        try {
            tomNode6DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode7DidChange(bool value) {
        try {
            tomNode7DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode8DidChange(bool value) {
        try {
            tomNode8DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode9DidChange(bool value) {
        try {
            tomNode9DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode10DidChange(bool value) {
        try {
            tomNode10DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode11DidChange(bool value) {
        try {
            tomNode11DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode12DidChange(bool value) {
        try {
            tomNode12DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode13DidChange(bool value) {
        try {
            tomNode13DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode14DidChange(bool value) {
        try {
            tomNode14DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode15DidChange(bool value) {
        try {
            tomNode15DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireTomNode16DidChange(bool value) {
        try {
            tomNode16DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode1DidChange(bool value) {
        try {
            cymbalNode1DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode2DidChange(bool value) {
        try {
            cymbalNode2DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode3DidChange(bool value) {
        try {
            cymbalNode3DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode4DidChange(bool value) {
        try {
            cymbalNode4DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode5DidChange(bool value) {
        try {
            cymbalNode5DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode6DidChange(bool value) {
        try {
            cymbalNode6DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode7DidChange(bool value) {
        try {
            cymbalNode7DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode8DidChange(bool value) {
        try {
            cymbalNode8DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode9DidChange(bool value) {
        try {
            cymbalNode9DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode10DidChange(bool value) {
        try {
            cymbalNode10DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode11DidChange(bool value) {
        try {
            cymbalNode11DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode12DidChange(bool value) {
        try {
            cymbalNode12DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode13DidChange(bool value) {
        try {
            cymbalNode13DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode14DidChange(bool value) {
        try {
            cymbalNode14DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode15DidChange(bool value) {
        try {
            cymbalNode15DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireCymbalNode16DidChange(bool value) {
        try {
            cymbalNode16DidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _kickNode1Property.WriteLength(context);
        length += _kickNode2Property.WriteLength(context);
        length += _kickNode3Property.WriteLength(context);
        length += _kickNode4Property.WriteLength(context);
        length += _kickNode5Property.WriteLength(context);
        length += _kickNode6Property.WriteLength(context);
        length += _kickNode7Property.WriteLength(context);
        length += _kickNode8Property.WriteLength(context);
        length += _kickNode9Property.WriteLength(context);
        length += _kickNode10Property.WriteLength(context);
        length += _kickNode11Property.WriteLength(context);
        length += _kickNode12Property.WriteLength(context);
        length += _kickNode13Property.WriteLength(context);
        length += _kickNode14Property.WriteLength(context);
        length += _kickNode15Property.WriteLength(context);
        length += _kickNode16Property.WriteLength(context);
        length += _hiHatNode1Property.WriteLength(context);
        length += _hiHatNode2Property.WriteLength(context);
        length += _hiHatNode3Property.WriteLength(context);
        length += _hiHatNode4Property.WriteLength(context);
        length += _hiHatNode5Property.WriteLength(context);
        length += _hiHatNode6Property.WriteLength(context);
        length += _hiHatNode7Property.WriteLength(context);
        length += _hiHatNode8Property.WriteLength(context);
        length += _hiHatNode9Property.WriteLength(context);
        length += _hiHatNode10Property.WriteLength(context);
        length += _hiHatNode11Property.WriteLength(context);
        length += _hiHatNode12Property.WriteLength(context);
        length += _hiHatNode13Property.WriteLength(context);
        length += _hiHatNode14Property.WriteLength(context);
        length += _hiHatNode15Property.WriteLength(context);
        length += _hiHatNode16Property.WriteLength(context);
        length += _snareNode1Property.WriteLength(context);
        length += _snareNode2Property.WriteLength(context);
        length += _snareNode3Property.WriteLength(context);
        length += _snareNode4Property.WriteLength(context);
        length += _snareNode5Property.WriteLength(context);
        length += _snareNode6Property.WriteLength(context);
        length += _snareNode7Property.WriteLength(context);
        length += _snareNode8Property.WriteLength(context);
        length += _snareNode9Property.WriteLength(context);
        length += _snareNode10Property.WriteLength(context);
        length += _snareNode11Property.WriteLength(context);
        length += _snareNode12Property.WriteLength(context);
        length += _snareNode13Property.WriteLength(context);
        length += _snareNode14Property.WriteLength(context);
        length += _snareNode15Property.WriteLength(context);
        length += _snareNode16Property.WriteLength(context);
        length += _tomNode1Property.WriteLength(context);
        length += _tomNode2Property.WriteLength(context);
        length += _tomNode3Property.WriteLength(context);
        length += _tomNode4Property.WriteLength(context);
        length += _tomNode5Property.WriteLength(context);
        length += _tomNode6Property.WriteLength(context);
        length += _tomNode7Property.WriteLength(context);
        length += _tomNode8Property.WriteLength(context);
        length += _tomNode9Property.WriteLength(context);
        length += _tomNode10Property.WriteLength(context);
        length += _tomNode11Property.WriteLength(context);
        length += _tomNode12Property.WriteLength(context);
        length += _tomNode13Property.WriteLength(context);
        length += _tomNode14Property.WriteLength(context);
        length += _tomNode15Property.WriteLength(context);
        length += _tomNode16Property.WriteLength(context);
        length += _cymbalNode1Property.WriteLength(context);
        length += _cymbalNode2Property.WriteLength(context);
        length += _cymbalNode3Property.WriteLength(context);
        length += _cymbalNode4Property.WriteLength(context);
        length += _cymbalNode5Property.WriteLength(context);
        length += _cymbalNode6Property.WriteLength(context);
        length += _cymbalNode7Property.WriteLength(context);
        length += _cymbalNode8Property.WriteLength(context);
        length += _cymbalNode9Property.WriteLength(context);
        length += _cymbalNode10Property.WriteLength(context);
        length += _cymbalNode11Property.WriteLength(context);
        length += _cymbalNode12Property.WriteLength(context);
        length += _cymbalNode13Property.WriteLength(context);
        length += _cymbalNode14Property.WriteLength(context);
        length += _cymbalNode15Property.WriteLength(context);
        length += _cymbalNode16Property.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _kickNode1Property.Write(stream, context);
        writes |= _kickNode2Property.Write(stream, context);
        writes |= _kickNode3Property.Write(stream, context);
        writes |= _kickNode4Property.Write(stream, context);
        writes |= _kickNode5Property.Write(stream, context);
        writes |= _kickNode6Property.Write(stream, context);
        writes |= _kickNode7Property.Write(stream, context);
        writes |= _kickNode8Property.Write(stream, context);
        writes |= _kickNode9Property.Write(stream, context);
        writes |= _kickNode10Property.Write(stream, context);
        writes |= _kickNode11Property.Write(stream, context);
        writes |= _kickNode12Property.Write(stream, context);
        writes |= _kickNode13Property.Write(stream, context);
        writes |= _kickNode14Property.Write(stream, context);
        writes |= _kickNode15Property.Write(stream, context);
        writes |= _kickNode16Property.Write(stream, context);
        writes |= _hiHatNode1Property.Write(stream, context);
        writes |= _hiHatNode2Property.Write(stream, context);
        writes |= _hiHatNode3Property.Write(stream, context);
        writes |= _hiHatNode4Property.Write(stream, context);
        writes |= _hiHatNode5Property.Write(stream, context);
        writes |= _hiHatNode6Property.Write(stream, context);
        writes |= _hiHatNode7Property.Write(stream, context);
        writes |= _hiHatNode8Property.Write(stream, context);
        writes |= _hiHatNode9Property.Write(stream, context);
        writes |= _hiHatNode10Property.Write(stream, context);
        writes |= _hiHatNode11Property.Write(stream, context);
        writes |= _hiHatNode12Property.Write(stream, context);
        writes |= _hiHatNode13Property.Write(stream, context);
        writes |= _hiHatNode14Property.Write(stream, context);
        writes |= _hiHatNode15Property.Write(stream, context);
        writes |= _hiHatNode16Property.Write(stream, context);
        writes |= _snareNode1Property.Write(stream, context);
        writes |= _snareNode2Property.Write(stream, context);
        writes |= _snareNode3Property.Write(stream, context);
        writes |= _snareNode4Property.Write(stream, context);
        writes |= _snareNode5Property.Write(stream, context);
        writes |= _snareNode6Property.Write(stream, context);
        writes |= _snareNode7Property.Write(stream, context);
        writes |= _snareNode8Property.Write(stream, context);
        writes |= _snareNode9Property.Write(stream, context);
        writes |= _snareNode10Property.Write(stream, context);
        writes |= _snareNode11Property.Write(stream, context);
        writes |= _snareNode12Property.Write(stream, context);
        writes |= _snareNode13Property.Write(stream, context);
        writes |= _snareNode14Property.Write(stream, context);
        writes |= _snareNode15Property.Write(stream, context);
        writes |= _snareNode16Property.Write(stream, context);
        writes |= _tomNode1Property.Write(stream, context);
        writes |= _tomNode2Property.Write(stream, context);
        writes |= _tomNode3Property.Write(stream, context);
        writes |= _tomNode4Property.Write(stream, context);
        writes |= _tomNode5Property.Write(stream, context);
        writes |= _tomNode6Property.Write(stream, context);
        writes |= _tomNode7Property.Write(stream, context);
        writes |= _tomNode8Property.Write(stream, context);
        writes |= _tomNode9Property.Write(stream, context);
        writes |= _tomNode10Property.Write(stream, context);
        writes |= _tomNode11Property.Write(stream, context);
        writes |= _tomNode12Property.Write(stream, context);
        writes |= _tomNode13Property.Write(stream, context);
        writes |= _tomNode14Property.Write(stream, context);
        writes |= _tomNode15Property.Write(stream, context);
        writes |= _tomNode16Property.Write(stream, context);
        writes |= _cymbalNode1Property.Write(stream, context);
        writes |= _cymbalNode2Property.Write(stream, context);
        writes |= _cymbalNode3Property.Write(stream, context);
        writes |= _cymbalNode4Property.Write(stream, context);
        writes |= _cymbalNode5Property.Write(stream, context);
        writes |= _cymbalNode6Property.Write(stream, context);
        writes |= _cymbalNode7Property.Write(stream, context);
        writes |= _cymbalNode8Property.Write(stream, context);
        writes |= _cymbalNode9Property.Write(stream, context);
        writes |= _cymbalNode10Property.Write(stream, context);
        writes |= _cymbalNode11Property.Write(stream, context);
        writes |= _cymbalNode12Property.Write(stream, context);
        writes |= _cymbalNode13Property.Write(stream, context);
        writes |= _cymbalNode14Property.Write(stream, context);
        writes |= _cymbalNode15Property.Write(stream, context);
        writes |= _cymbalNode16Property.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.KickNode1: {
                    changed = _kickNode1Property.Read(stream, context);
                    if (changed) FireKickNode1DidChange(kickNode1);
                    break;
                }
                case (uint) PropertyID.KickNode2: {
                    changed = _kickNode2Property.Read(stream, context);
                    if (changed) FireKickNode2DidChange(kickNode2);
                    break;
                }
                case (uint) PropertyID.KickNode3: {
                    changed = _kickNode3Property.Read(stream, context);
                    if (changed) FireKickNode3DidChange(kickNode3);
                    break;
                }
                case (uint) PropertyID.KickNode4: {
                    changed = _kickNode4Property.Read(stream, context);
                    if (changed) FireKickNode4DidChange(kickNode4);
                    break;
                }
                case (uint) PropertyID.KickNode5: {
                    changed = _kickNode5Property.Read(stream, context);
                    if (changed) FireKickNode5DidChange(kickNode5);
                    break;
                }
                case (uint) PropertyID.KickNode6: {
                    changed = _kickNode6Property.Read(stream, context);
                    if (changed) FireKickNode6DidChange(kickNode6);
                    break;
                }
                case (uint) PropertyID.KickNode7: {
                    changed = _kickNode7Property.Read(stream, context);
                    if (changed) FireKickNode7DidChange(kickNode7);
                    break;
                }
                case (uint) PropertyID.KickNode8: {
                    changed = _kickNode8Property.Read(stream, context);
                    if (changed) FireKickNode8DidChange(kickNode8);
                    break;
                }
                case (uint) PropertyID.KickNode9: {
                    changed = _kickNode9Property.Read(stream, context);
                    if (changed) FireKickNode9DidChange(kickNode9);
                    break;
                }
                case (uint) PropertyID.KickNode10: {
                    changed = _kickNode10Property.Read(stream, context);
                    if (changed) FireKickNode10DidChange(kickNode10);
                    break;
                }
                case (uint) PropertyID.KickNode11: {
                    changed = _kickNode11Property.Read(stream, context);
                    if (changed) FireKickNode11DidChange(kickNode11);
                    break;
                }
                case (uint) PropertyID.KickNode12: {
                    changed = _kickNode12Property.Read(stream, context);
                    if (changed) FireKickNode12DidChange(kickNode12);
                    break;
                }
                case (uint) PropertyID.KickNode13: {
                    changed = _kickNode13Property.Read(stream, context);
                    if (changed) FireKickNode13DidChange(kickNode13);
                    break;
                }
                case (uint) PropertyID.KickNode14: {
                    changed = _kickNode14Property.Read(stream, context);
                    if (changed) FireKickNode14DidChange(kickNode14);
                    break;
                }
                case (uint) PropertyID.KickNode15: {
                    changed = _kickNode15Property.Read(stream, context);
                    if (changed) FireKickNode15DidChange(kickNode15);
                    break;
                }
                case (uint) PropertyID.KickNode16: {
                    changed = _kickNode16Property.Read(stream, context);
                    if (changed) FireKickNode16DidChange(kickNode16);
                    break;
                }
                case (uint) PropertyID.HiHatNode1: {
                    changed = _hiHatNode1Property.Read(stream, context);
                    if (changed) FireHiHatNode1DidChange(hiHatNode1);
                    break;
                }
                case (uint) PropertyID.HiHatNode2: {
                    changed = _hiHatNode2Property.Read(stream, context);
                    if (changed) FireHiHatNode2DidChange(hiHatNode2);
                    break;
                }
                case (uint) PropertyID.HiHatNode3: {
                    changed = _hiHatNode3Property.Read(stream, context);
                    if (changed) FireHiHatNode3DidChange(hiHatNode3);
                    break;
                }
                case (uint) PropertyID.HiHatNode4: {
                    changed = _hiHatNode4Property.Read(stream, context);
                    if (changed) FireHiHatNode4DidChange(hiHatNode4);
                    break;
                }
                case (uint) PropertyID.HiHatNode5: {
                    changed = _hiHatNode5Property.Read(stream, context);
                    if (changed) FireHiHatNode5DidChange(hiHatNode5);
                    break;
                }
                case (uint) PropertyID.HiHatNode6: {
                    changed = _hiHatNode6Property.Read(stream, context);
                    if (changed) FireHiHatNode6DidChange(hiHatNode6);
                    break;
                }
                case (uint) PropertyID.HiHatNode7: {
                    changed = _hiHatNode7Property.Read(stream, context);
                    if (changed) FireHiHatNode7DidChange(hiHatNode7);
                    break;
                }
                case (uint) PropertyID.HiHatNode8: {
                    changed = _hiHatNode8Property.Read(stream, context);
                    if (changed) FireHiHatNode8DidChange(hiHatNode8);
                    break;
                }
                case (uint) PropertyID.HiHatNode9: {
                    changed = _hiHatNode9Property.Read(stream, context);
                    if (changed) FireHiHatNode9DidChange(hiHatNode9);
                    break;
                }
                case (uint) PropertyID.HiHatNode10: {
                    changed = _hiHatNode10Property.Read(stream, context);
                    if (changed) FireHiHatNode10DidChange(hiHatNode10);
                    break;
                }
                case (uint) PropertyID.HiHatNode11: {
                    changed = _hiHatNode11Property.Read(stream, context);
                    if (changed) FireHiHatNode11DidChange(hiHatNode11);
                    break;
                }
                case (uint) PropertyID.HiHatNode12: {
                    changed = _hiHatNode12Property.Read(stream, context);
                    if (changed) FireHiHatNode12DidChange(hiHatNode12);
                    break;
                }
                case (uint) PropertyID.HiHatNode13: {
                    changed = _hiHatNode13Property.Read(stream, context);
                    if (changed) FireHiHatNode13DidChange(hiHatNode13);
                    break;
                }
                case (uint) PropertyID.HiHatNode14: {
                    changed = _hiHatNode14Property.Read(stream, context);
                    if (changed) FireHiHatNode14DidChange(hiHatNode14);
                    break;
                }
                case (uint) PropertyID.HiHatNode15: {
                    changed = _hiHatNode15Property.Read(stream, context);
                    if (changed) FireHiHatNode15DidChange(hiHatNode15);
                    break;
                }
                case (uint) PropertyID.HiHatNode16: {
                    changed = _hiHatNode16Property.Read(stream, context);
                    if (changed) FireHiHatNode16DidChange(hiHatNode16);
                    break;
                }
                case (uint) PropertyID.SnareNode1: {
                    changed = _snareNode1Property.Read(stream, context);
                    if (changed) FireSnareNode1DidChange(snareNode1);
                    break;
                }
                case (uint) PropertyID.SnareNode2: {
                    changed = _snareNode2Property.Read(stream, context);
                    if (changed) FireSnareNode2DidChange(snareNode2);
                    break;
                }
                case (uint) PropertyID.SnareNode3: {
                    changed = _snareNode3Property.Read(stream, context);
                    if (changed) FireSnareNode3DidChange(snareNode3);
                    break;
                }
                case (uint) PropertyID.SnareNode4: {
                    changed = _snareNode4Property.Read(stream, context);
                    if (changed) FireSnareNode4DidChange(snareNode4);
                    break;
                }
                case (uint) PropertyID.SnareNode5: {
                    changed = _snareNode5Property.Read(stream, context);
                    if (changed) FireSnareNode5DidChange(snareNode5);
                    break;
                }
                case (uint) PropertyID.SnareNode6: {
                    changed = _snareNode6Property.Read(stream, context);
                    if (changed) FireSnareNode6DidChange(snareNode6);
                    break;
                }
                case (uint) PropertyID.SnareNode7: {
                    changed = _snareNode7Property.Read(stream, context);
                    if (changed) FireSnareNode7DidChange(snareNode7);
                    break;
                }
                case (uint) PropertyID.SnareNode8: {
                    changed = _snareNode8Property.Read(stream, context);
                    if (changed) FireSnareNode8DidChange(snareNode8);
                    break;
                }
                case (uint) PropertyID.SnareNode9: {
                    changed = _snareNode9Property.Read(stream, context);
                    if (changed) FireSnareNode9DidChange(snareNode9);
                    break;
                }
                case (uint) PropertyID.SnareNode10: {
                    changed = _snareNode10Property.Read(stream, context);
                    if (changed) FireSnareNode10DidChange(snareNode10);
                    break;
                }
                case (uint) PropertyID.SnareNode11: {
                    changed = _snareNode11Property.Read(stream, context);
                    if (changed) FireSnareNode11DidChange(snareNode11);
                    break;
                }
                case (uint) PropertyID.SnareNode12: {
                    changed = _snareNode12Property.Read(stream, context);
                    if (changed) FireSnareNode12DidChange(snareNode12);
                    break;
                }
                case (uint) PropertyID.SnareNode13: {
                    changed = _snareNode13Property.Read(stream, context);
                    if (changed) FireSnareNode13DidChange(snareNode13);
                    break;
                }
                case (uint) PropertyID.SnareNode14: {
                    changed = _snareNode14Property.Read(stream, context);
                    if (changed) FireSnareNode14DidChange(snareNode14);
                    break;
                }
                case (uint) PropertyID.SnareNode15: {
                    changed = _snareNode15Property.Read(stream, context);
                    if (changed) FireSnareNode15DidChange(snareNode15);
                    break;
                }
                case (uint) PropertyID.SnareNode16: {
                    changed = _snareNode16Property.Read(stream, context);
                    if (changed) FireSnareNode16DidChange(snareNode16);
                    break;
                }
                case (uint) PropertyID.TomNode1: {
                    changed = _tomNode1Property.Read(stream, context);
                    if (changed) FireTomNode1DidChange(tomNode1);
                    break;
                }
                case (uint) PropertyID.TomNode2: {
                    changed = _tomNode2Property.Read(stream, context);
                    if (changed) FireTomNode2DidChange(tomNode2);
                    break;
                }
                case (uint) PropertyID.TomNode3: {
                    changed = _tomNode3Property.Read(stream, context);
                    if (changed) FireTomNode3DidChange(tomNode3);
                    break;
                }
                case (uint) PropertyID.TomNode4: {
                    changed = _tomNode4Property.Read(stream, context);
                    if (changed) FireTomNode4DidChange(tomNode4);
                    break;
                }
                case (uint) PropertyID.TomNode5: {
                    changed = _tomNode5Property.Read(stream, context);
                    if (changed) FireTomNode5DidChange(tomNode5);
                    break;
                }
                case (uint) PropertyID.TomNode6: {
                    changed = _tomNode6Property.Read(stream, context);
                    if (changed) FireTomNode6DidChange(tomNode6);
                    break;
                }
                case (uint) PropertyID.TomNode7: {
                    changed = _tomNode7Property.Read(stream, context);
                    if (changed) FireTomNode7DidChange(tomNode7);
                    break;
                }
                case (uint) PropertyID.TomNode8: {
                    changed = _tomNode8Property.Read(stream, context);
                    if (changed) FireTomNode8DidChange(tomNode8);
                    break;
                }
                case (uint) PropertyID.TomNode9: {
                    changed = _tomNode9Property.Read(stream, context);
                    if (changed) FireTomNode9DidChange(tomNode9);
                    break;
                }
                case (uint) PropertyID.TomNode10: {
                    changed = _tomNode10Property.Read(stream, context);
                    if (changed) FireTomNode10DidChange(tomNode10);
                    break;
                }
                case (uint) PropertyID.TomNode11: {
                    changed = _tomNode11Property.Read(stream, context);
                    if (changed) FireTomNode11DidChange(tomNode11);
                    break;
                }
                case (uint) PropertyID.TomNode12: {
                    changed = _tomNode12Property.Read(stream, context);
                    if (changed) FireTomNode12DidChange(tomNode12);
                    break;
                }
                case (uint) PropertyID.TomNode13: {
                    changed = _tomNode13Property.Read(stream, context);
                    if (changed) FireTomNode13DidChange(tomNode13);
                    break;
                }
                case (uint) PropertyID.TomNode14: {
                    changed = _tomNode14Property.Read(stream, context);
                    if (changed) FireTomNode14DidChange(tomNode14);
                    break;
                }
                case (uint) PropertyID.TomNode15: {
                    changed = _tomNode15Property.Read(stream, context);
                    if (changed) FireTomNode15DidChange(tomNode15);
                    break;
                }
                case (uint) PropertyID.TomNode16: {
                    changed = _tomNode16Property.Read(stream, context);
                    if (changed) FireTomNode16DidChange(tomNode16);
                    break;
                }
                case (uint) PropertyID.CymbalNode1: {
                    changed = _cymbalNode1Property.Read(stream, context);
                    if (changed) FireCymbalNode1DidChange(cymbalNode1);
                    break;
                }
                case (uint) PropertyID.CymbalNode2: {
                    changed = _cymbalNode2Property.Read(stream, context);
                    if (changed) FireCymbalNode2DidChange(cymbalNode2);
                    break;
                }
                case (uint) PropertyID.CymbalNode3: {
                    changed = _cymbalNode3Property.Read(stream, context);
                    if (changed) FireCymbalNode3DidChange(cymbalNode3);
                    break;
                }
                case (uint) PropertyID.CymbalNode4: {
                    changed = _cymbalNode4Property.Read(stream, context);
                    if (changed) FireCymbalNode4DidChange(cymbalNode4);
                    break;
                }
                case (uint) PropertyID.CymbalNode5: {
                    changed = _cymbalNode5Property.Read(stream, context);
                    if (changed) FireCymbalNode5DidChange(cymbalNode5);
                    break;
                }
                case (uint) PropertyID.CymbalNode6: {
                    changed = _cymbalNode6Property.Read(stream, context);
                    if (changed) FireCymbalNode6DidChange(cymbalNode6);
                    break;
                }
                case (uint) PropertyID.CymbalNode7: {
                    changed = _cymbalNode7Property.Read(stream, context);
                    if (changed) FireCymbalNode7DidChange(cymbalNode7);
                    break;
                }
                case (uint) PropertyID.CymbalNode8: {
                    changed = _cymbalNode8Property.Read(stream, context);
                    if (changed) FireCymbalNode8DidChange(cymbalNode8);
                    break;
                }
                case (uint) PropertyID.CymbalNode9: {
                    changed = _cymbalNode9Property.Read(stream, context);
                    if (changed) FireCymbalNode9DidChange(cymbalNode9);
                    break;
                }
                case (uint) PropertyID.CymbalNode10: {
                    changed = _cymbalNode10Property.Read(stream, context);
                    if (changed) FireCymbalNode10DidChange(cymbalNode10);
                    break;
                }
                case (uint) PropertyID.CymbalNode11: {
                    changed = _cymbalNode11Property.Read(stream, context);
                    if (changed) FireCymbalNode11DidChange(cymbalNode11);
                    break;
                }
                case (uint) PropertyID.CymbalNode12: {
                    changed = _cymbalNode12Property.Read(stream, context);
                    if (changed) FireCymbalNode12DidChange(cymbalNode12);
                    break;
                }
                case (uint) PropertyID.CymbalNode13: {
                    changed = _cymbalNode13Property.Read(stream, context);
                    if (changed) FireCymbalNode13DidChange(cymbalNode13);
                    break;
                }
                case (uint) PropertyID.CymbalNode14: {
                    changed = _cymbalNode14Property.Read(stream, context);
                    if (changed) FireCymbalNode14DidChange(cymbalNode14);
                    break;
                }
                case (uint) PropertyID.CymbalNode15: {
                    changed = _cymbalNode15Property.Read(stream, context);
                    if (changed) FireCymbalNode15DidChange(cymbalNode15);
                    break;
                }
                case (uint) PropertyID.CymbalNode16: {
                    changed = _cymbalNode16Property.Read(stream, context);
                    if (changed) FireCymbalNode16DidChange(cymbalNode16);
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
        _kickNode1 = kickNode1;
        _kickNode2 = kickNode2;
        _kickNode3 = kickNode3;
        _kickNode4 = kickNode4;
        _kickNode5 = kickNode5;
        _kickNode6 = kickNode6;
        _kickNode7 = kickNode7;
        _kickNode8 = kickNode8;
        _kickNode9 = kickNode9;
        _kickNode10 = kickNode10;
        _kickNode11 = kickNode11;
        _kickNode12 = kickNode12;
        _kickNode13 = kickNode13;
        _kickNode14 = kickNode14;
        _kickNode15 = kickNode15;
        _kickNode16 = kickNode16;
        _hiHatNode1 = hiHatNode1;
        _hiHatNode2 = hiHatNode2;
        _hiHatNode3 = hiHatNode3;
        _hiHatNode4 = hiHatNode4;
        _hiHatNode5 = hiHatNode5;
        _hiHatNode6 = hiHatNode6;
        _hiHatNode7 = hiHatNode7;
        _hiHatNode8 = hiHatNode8;
        _hiHatNode9 = hiHatNode9;
        _hiHatNode10 = hiHatNode10;
        _hiHatNode11 = hiHatNode11;
        _hiHatNode12 = hiHatNode12;
        _hiHatNode13 = hiHatNode13;
        _hiHatNode14 = hiHatNode14;
        _hiHatNode15 = hiHatNode15;
        _hiHatNode16 = hiHatNode16;
        _snareNode1 = snareNode1;
        _snareNode2 = snareNode2;
        _snareNode3 = snareNode3;
        _snareNode4 = snareNode4;
        _snareNode5 = snareNode5;
        _snareNode6 = snareNode6;
        _snareNode7 = snareNode7;
        _snareNode8 = snareNode8;
        _snareNode9 = snareNode9;
        _snareNode10 = snareNode10;
        _snareNode11 = snareNode11;
        _snareNode12 = snareNode12;
        _snareNode13 = snareNode13;
        _snareNode14 = snareNode14;
        _snareNode15 = snareNode15;
        _snareNode16 = snareNode16;
        _tomNode1 = tomNode1;
        _tomNode2 = tomNode2;
        _tomNode3 = tomNode3;
        _tomNode4 = tomNode4;
        _tomNode5 = tomNode5;
        _tomNode6 = tomNode6;
        _tomNode7 = tomNode7;
        _tomNode8 = tomNode8;
        _tomNode9 = tomNode9;
        _tomNode10 = tomNode10;
        _tomNode11 = tomNode11;
        _tomNode12 = tomNode12;
        _tomNode13 = tomNode13;
        _tomNode14 = tomNode14;
        _tomNode15 = tomNode15;
        _tomNode16 = tomNode16;
        _cymbalNode1 = cymbalNode1;
        _cymbalNode2 = cymbalNode2;
        _cymbalNode3 = cymbalNode3;
        _cymbalNode4 = cymbalNode4;
        _cymbalNode5 = cymbalNode5;
        _cymbalNode6 = cymbalNode6;
        _cymbalNode7 = cymbalNode7;
        _cymbalNode8 = cymbalNode8;
        _cymbalNode9 = cymbalNode9;
        _cymbalNode10 = cymbalNode10;
        _cymbalNode11 = cymbalNode11;
        _cymbalNode12 = cymbalNode12;
        _cymbalNode13 = cymbalNode13;
        _cymbalNode14 = cymbalNode14;
        _cymbalNode15 = cymbalNode15;
        _cymbalNode16 = cymbalNode16;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
