using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;


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
            return _cache.LookForValueInCache(_kickNode1, entry => entry.kickNode1Set, entry => entry.kickNode1);
        }
        set {
            if (this.kickNode1 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode1Set = true; entry.kickNode1 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode1DidChange(value);
        }
    }
    
    public bool kickNode2 {
        get {
            return _cache.LookForValueInCache(_kickNode2, entry => entry.kickNode2Set, entry => entry.kickNode2);
        }
        set {
            if (this.kickNode2 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode2Set = true; entry.kickNode2 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode2DidChange(value);
        }
    }
    
    public bool kickNode3 {
        get {
            return _cache.LookForValueInCache(_kickNode3, entry => entry.kickNode3Set, entry => entry.kickNode3);
        }
        set {
            if (this.kickNode3 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode3Set = true; entry.kickNode3 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode3DidChange(value);
        }
    }
    
    public bool kickNode4 {
        get {
            return _cache.LookForValueInCache(_kickNode4, entry => entry.kickNode4Set, entry => entry.kickNode4);
        }
        set {
            if (this.kickNode4 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode4Set = true; entry.kickNode4 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode4DidChange(value);
        }
    }
    
    public bool kickNode5 {
        get {
            return _cache.LookForValueInCache(_kickNode5, entry => entry.kickNode5Set, entry => entry.kickNode5);
        }
        set {
            if (this.kickNode5 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode5Set = true; entry.kickNode5 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode5DidChange(value);
        }
    }
    
    public bool kickNode6 {
        get {
            return _cache.LookForValueInCache(_kickNode6, entry => entry.kickNode6Set, entry => entry.kickNode6);
        }
        set {
            if (this.kickNode6 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode6Set = true; entry.kickNode6 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode6DidChange(value);
        }
    }
    
    public bool kickNode7 {
        get {
            return _cache.LookForValueInCache(_kickNode7, entry => entry.kickNode7Set, entry => entry.kickNode7);
        }
        set {
            if (this.kickNode7 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode7Set = true; entry.kickNode7 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode7DidChange(value);
        }
    }
    
    public bool kickNode8 {
        get {
            return _cache.LookForValueInCache(_kickNode8, entry => entry.kickNode8Set, entry => entry.kickNode8);
        }
        set {
            if (this.kickNode8 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode8Set = true; entry.kickNode8 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode8DidChange(value);
        }
    }
    
    public bool kickNode9 {
        get {
            return _cache.LookForValueInCache(_kickNode9, entry => entry.kickNode9Set, entry => entry.kickNode9);
        }
        set {
            if (this.kickNode9 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode9Set = true; entry.kickNode9 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode9DidChange(value);
        }
    }
    
    public bool kickNode10 {
        get {
            return _cache.LookForValueInCache(_kickNode10, entry => entry.kickNode10Set, entry => entry.kickNode10);
        }
        set {
            if (this.kickNode10 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode10Set = true; entry.kickNode10 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode10DidChange(value);
        }
    }
    
    public bool kickNode11 {
        get {
            return _cache.LookForValueInCache(_kickNode11, entry => entry.kickNode11Set, entry => entry.kickNode11);
        }
        set {
            if (this.kickNode11 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode11Set = true; entry.kickNode11 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode11DidChange(value);
        }
    }
    
    public bool kickNode12 {
        get {
            return _cache.LookForValueInCache(_kickNode12, entry => entry.kickNode12Set, entry => entry.kickNode12);
        }
        set {
            if (this.kickNode12 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode12Set = true; entry.kickNode12 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode12DidChange(value);
        }
    }
    
    public bool kickNode13 {
        get {
            return _cache.LookForValueInCache(_kickNode13, entry => entry.kickNode13Set, entry => entry.kickNode13);
        }
        set {
            if (this.kickNode13 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode13Set = true; entry.kickNode13 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode13DidChange(value);
        }
    }
    
    public bool kickNode14 {
        get {
            return _cache.LookForValueInCache(_kickNode14, entry => entry.kickNode14Set, entry => entry.kickNode14);
        }
        set {
            if (this.kickNode14 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode14Set = true; entry.kickNode14 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode14DidChange(value);
        }
    }
    
    public bool kickNode15 {
        get {
            return _cache.LookForValueInCache(_kickNode15, entry => entry.kickNode15Set, entry => entry.kickNode15);
        }
        set {
            if (this.kickNode15 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode15Set = true; entry.kickNode15 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode15DidChange(value);
        }
    }
    
    public bool kickNode16 {
        get {
            return _cache.LookForValueInCache(_kickNode16, entry => entry.kickNode16Set, entry => entry.kickNode16);
        }
        set {
            if (this.kickNode16 == value) return;
            _cache.UpdateLocalCache(entry => { entry.kickNode16Set = true; entry.kickNode16 = value; return entry; });
            InvalidateReliableLength();
            FireKickNode16DidChange(value);
        }
    }
    
    public bool hiHatNode1 {
        get {
            return _cache.LookForValueInCache(_hiHatNode1, entry => entry.hiHatNode1Set, entry => entry.hiHatNode1);
        }
        set {
            if (this.hiHatNode1 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode1Set = true; entry.hiHatNode1 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode1DidChange(value);
        }
    }
    
    public bool hiHatNode2 {
        get {
            return _cache.LookForValueInCache(_hiHatNode2, entry => entry.hiHatNode2Set, entry => entry.hiHatNode2);
        }
        set {
            if (this.hiHatNode2 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode2Set = true; entry.hiHatNode2 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode2DidChange(value);
        }
    }
    
    public bool hiHatNode3 {
        get {
            return _cache.LookForValueInCache(_hiHatNode3, entry => entry.hiHatNode3Set, entry => entry.hiHatNode3);
        }
        set {
            if (this.hiHatNode3 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode3Set = true; entry.hiHatNode3 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode3DidChange(value);
        }
    }
    
    public bool hiHatNode4 {
        get {
            return _cache.LookForValueInCache(_hiHatNode4, entry => entry.hiHatNode4Set, entry => entry.hiHatNode4);
        }
        set {
            if (this.hiHatNode4 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode4Set = true; entry.hiHatNode4 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode4DidChange(value);
        }
    }
    
    public bool hiHatNode5 {
        get {
            return _cache.LookForValueInCache(_hiHatNode5, entry => entry.hiHatNode5Set, entry => entry.hiHatNode5);
        }
        set {
            if (this.hiHatNode5 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode5Set = true; entry.hiHatNode5 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode5DidChange(value);
        }
    }
    
    public bool hiHatNode6 {
        get {
            return _cache.LookForValueInCache(_hiHatNode6, entry => entry.hiHatNode6Set, entry => entry.hiHatNode6);
        }
        set {
            if (this.hiHatNode6 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode6Set = true; entry.hiHatNode6 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode6DidChange(value);
        }
    }
    
    public bool hiHatNode7 {
        get {
            return _cache.LookForValueInCache(_hiHatNode7, entry => entry.hiHatNode7Set, entry => entry.hiHatNode7);
        }
        set {
            if (this.hiHatNode7 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode7Set = true; entry.hiHatNode7 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode7DidChange(value);
        }
    }
    
    public bool hiHatNode8 {
        get {
            return _cache.LookForValueInCache(_hiHatNode8, entry => entry.hiHatNode8Set, entry => entry.hiHatNode8);
        }
        set {
            if (this.hiHatNode8 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode8Set = true; entry.hiHatNode8 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode8DidChange(value);
        }
    }
    
    public bool hiHatNode9 {
        get {
            return _cache.LookForValueInCache(_hiHatNode9, entry => entry.hiHatNode9Set, entry => entry.hiHatNode9);
        }
        set {
            if (this.hiHatNode9 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode9Set = true; entry.hiHatNode9 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode9DidChange(value);
        }
    }
    
    public bool hiHatNode10 {
        get {
            return _cache.LookForValueInCache(_hiHatNode10, entry => entry.hiHatNode10Set, entry => entry.hiHatNode10);
        }
        set {
            if (this.hiHatNode10 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode10Set = true; entry.hiHatNode10 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode10DidChange(value);
        }
    }
    
    public bool hiHatNode11 {
        get {
            return _cache.LookForValueInCache(_hiHatNode11, entry => entry.hiHatNode11Set, entry => entry.hiHatNode11);
        }
        set {
            if (this.hiHatNode11 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode11Set = true; entry.hiHatNode11 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode11DidChange(value);
        }
    }
    
    public bool hiHatNode12 {
        get {
            return _cache.LookForValueInCache(_hiHatNode12, entry => entry.hiHatNode12Set, entry => entry.hiHatNode12);
        }
        set {
            if (this.hiHatNode12 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode12Set = true; entry.hiHatNode12 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode12DidChange(value);
        }
    }
    
    public bool hiHatNode13 {
        get {
            return _cache.LookForValueInCache(_hiHatNode13, entry => entry.hiHatNode13Set, entry => entry.hiHatNode13);
        }
        set {
            if (this.hiHatNode13 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode13Set = true; entry.hiHatNode13 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode13DidChange(value);
        }
    }
    
    public bool hiHatNode14 {
        get {
            return _cache.LookForValueInCache(_hiHatNode14, entry => entry.hiHatNode14Set, entry => entry.hiHatNode14);
        }
        set {
            if (this.hiHatNode14 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode14Set = true; entry.hiHatNode14 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode14DidChange(value);
        }
    }
    
    public bool hiHatNode15 {
        get {
            return _cache.LookForValueInCache(_hiHatNode15, entry => entry.hiHatNode15Set, entry => entry.hiHatNode15);
        }
        set {
            if (this.hiHatNode15 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode15Set = true; entry.hiHatNode15 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode15DidChange(value);
        }
    }
    
    public bool hiHatNode16 {
        get {
            return _cache.LookForValueInCache(_hiHatNode16, entry => entry.hiHatNode16Set, entry => entry.hiHatNode16);
        }
        set {
            if (this.hiHatNode16 == value) return;
            _cache.UpdateLocalCache(entry => { entry.hiHatNode16Set = true; entry.hiHatNode16 = value; return entry; });
            InvalidateReliableLength();
            FireHiHatNode16DidChange(value);
        }
    }
    
    public bool snareNode1 {
        get {
            return _cache.LookForValueInCache(_snareNode1, entry => entry.snareNode1Set, entry => entry.snareNode1);
        }
        set {
            if (this.snareNode1 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode1Set = true; entry.snareNode1 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode1DidChange(value);
        }
    }
    
    public bool snareNode2 {
        get {
            return _cache.LookForValueInCache(_snareNode2, entry => entry.snareNode2Set, entry => entry.snareNode2);
        }
        set {
            if (this.snareNode2 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode2Set = true; entry.snareNode2 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode2DidChange(value);
        }
    }
    
    public bool snareNode3 {
        get {
            return _cache.LookForValueInCache(_snareNode3, entry => entry.snareNode3Set, entry => entry.snareNode3);
        }
        set {
            if (this.snareNode3 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode3Set = true; entry.snareNode3 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode3DidChange(value);
        }
    }
    
    public bool snareNode4 {
        get {
            return _cache.LookForValueInCache(_snareNode4, entry => entry.snareNode4Set, entry => entry.snareNode4);
        }
        set {
            if (this.snareNode4 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode4Set = true; entry.snareNode4 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode4DidChange(value);
        }
    }
    
    public bool snareNode5 {
        get {
            return _cache.LookForValueInCache(_snareNode5, entry => entry.snareNode5Set, entry => entry.snareNode5);
        }
        set {
            if (this.snareNode5 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode5Set = true; entry.snareNode5 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode5DidChange(value);
        }
    }
    
    public bool snareNode6 {
        get {
            return _cache.LookForValueInCache(_snareNode6, entry => entry.snareNode6Set, entry => entry.snareNode6);
        }
        set {
            if (this.snareNode6 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode6Set = true; entry.snareNode6 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode6DidChange(value);
        }
    }
    
    public bool snareNode7 {
        get {
            return _cache.LookForValueInCache(_snareNode7, entry => entry.snareNode7Set, entry => entry.snareNode7);
        }
        set {
            if (this.snareNode7 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode7Set = true; entry.snareNode7 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode7DidChange(value);
        }
    }
    
    public bool snareNode8 {
        get {
            return _cache.LookForValueInCache(_snareNode8, entry => entry.snareNode8Set, entry => entry.snareNode8);
        }
        set {
            if (this.snareNode8 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode8Set = true; entry.snareNode8 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode8DidChange(value);
        }
    }
    
    public bool snareNode9 {
        get {
            return _cache.LookForValueInCache(_snareNode9, entry => entry.snareNode9Set, entry => entry.snareNode9);
        }
        set {
            if (this.snareNode9 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode9Set = true; entry.snareNode9 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode9DidChange(value);
        }
    }
    
    public bool snareNode10 {
        get {
            return _cache.LookForValueInCache(_snareNode10, entry => entry.snareNode10Set, entry => entry.snareNode10);
        }
        set {
            if (this.snareNode10 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode10Set = true; entry.snareNode10 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode10DidChange(value);
        }
    }
    
    public bool snareNode11 {
        get {
            return _cache.LookForValueInCache(_snareNode11, entry => entry.snareNode11Set, entry => entry.snareNode11);
        }
        set {
            if (this.snareNode11 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode11Set = true; entry.snareNode11 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode11DidChange(value);
        }
    }
    
    public bool snareNode12 {
        get {
            return _cache.LookForValueInCache(_snareNode12, entry => entry.snareNode12Set, entry => entry.snareNode12);
        }
        set {
            if (this.snareNode12 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode12Set = true; entry.snareNode12 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode12DidChange(value);
        }
    }
    
    public bool snareNode13 {
        get {
            return _cache.LookForValueInCache(_snareNode13, entry => entry.snareNode13Set, entry => entry.snareNode13);
        }
        set {
            if (this.snareNode13 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode13Set = true; entry.snareNode13 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode13DidChange(value);
        }
    }
    
    public bool snareNode14 {
        get {
            return _cache.LookForValueInCache(_snareNode14, entry => entry.snareNode14Set, entry => entry.snareNode14);
        }
        set {
            if (this.snareNode14 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode14Set = true; entry.snareNode14 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode14DidChange(value);
        }
    }
    
    public bool snareNode15 {
        get {
            return _cache.LookForValueInCache(_snareNode15, entry => entry.snareNode15Set, entry => entry.snareNode15);
        }
        set {
            if (this.snareNode15 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode15Set = true; entry.snareNode15 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode15DidChange(value);
        }
    }
    
    public bool snareNode16 {
        get {
            return _cache.LookForValueInCache(_snareNode16, entry => entry.snareNode16Set, entry => entry.snareNode16);
        }
        set {
            if (this.snareNode16 == value) return;
            _cache.UpdateLocalCache(entry => { entry.snareNode16Set = true; entry.snareNode16 = value; return entry; });
            InvalidateReliableLength();
            FireSnareNode16DidChange(value);
        }
    }
    
    public bool tomNode1 {
        get {
            return _cache.LookForValueInCache(_tomNode1, entry => entry.tomNode1Set, entry => entry.tomNode1);
        }
        set {
            if (this.tomNode1 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode1Set = true; entry.tomNode1 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode1DidChange(value);
        }
    }
    
    public bool tomNode2 {
        get {
            return _cache.LookForValueInCache(_tomNode2, entry => entry.tomNode2Set, entry => entry.tomNode2);
        }
        set {
            if (this.tomNode2 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode2Set = true; entry.tomNode2 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode2DidChange(value);
        }
    }
    
    public bool tomNode3 {
        get {
            return _cache.LookForValueInCache(_tomNode3, entry => entry.tomNode3Set, entry => entry.tomNode3);
        }
        set {
            if (this.tomNode3 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode3Set = true; entry.tomNode3 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode3DidChange(value);
        }
    }
    
    public bool tomNode4 {
        get {
            return _cache.LookForValueInCache(_tomNode4, entry => entry.tomNode4Set, entry => entry.tomNode4);
        }
        set {
            if (this.tomNode4 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode4Set = true; entry.tomNode4 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode4DidChange(value);
        }
    }
    
    public bool tomNode5 {
        get {
            return _cache.LookForValueInCache(_tomNode5, entry => entry.tomNode5Set, entry => entry.tomNode5);
        }
        set {
            if (this.tomNode5 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode5Set = true; entry.tomNode5 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode5DidChange(value);
        }
    }
    
    public bool tomNode6 {
        get {
            return _cache.LookForValueInCache(_tomNode6, entry => entry.tomNode6Set, entry => entry.tomNode6);
        }
        set {
            if (this.tomNode6 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode6Set = true; entry.tomNode6 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode6DidChange(value);
        }
    }
    
    public bool tomNode7 {
        get {
            return _cache.LookForValueInCache(_tomNode7, entry => entry.tomNode7Set, entry => entry.tomNode7);
        }
        set {
            if (this.tomNode7 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode7Set = true; entry.tomNode7 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode7DidChange(value);
        }
    }
    
    public bool tomNode8 {
        get {
            return _cache.LookForValueInCache(_tomNode8, entry => entry.tomNode8Set, entry => entry.tomNode8);
        }
        set {
            if (this.tomNode8 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode8Set = true; entry.tomNode8 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode8DidChange(value);
        }
    }
    
    public bool tomNode9 {
        get {
            return _cache.LookForValueInCache(_tomNode9, entry => entry.tomNode9Set, entry => entry.tomNode9);
        }
        set {
            if (this.tomNode9 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode9Set = true; entry.tomNode9 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode9DidChange(value);
        }
    }
    
    public bool tomNode10 {
        get {
            return _cache.LookForValueInCache(_tomNode10, entry => entry.tomNode10Set, entry => entry.tomNode10);
        }
        set {
            if (this.tomNode10 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode10Set = true; entry.tomNode10 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode10DidChange(value);
        }
    }
    
    public bool tomNode11 {
        get {
            return _cache.LookForValueInCache(_tomNode11, entry => entry.tomNode11Set, entry => entry.tomNode11);
        }
        set {
            if (this.tomNode11 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode11Set = true; entry.tomNode11 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode11DidChange(value);
        }
    }
    
    public bool tomNode12 {
        get {
            return _cache.LookForValueInCache(_tomNode12, entry => entry.tomNode12Set, entry => entry.tomNode12);
        }
        set {
            if (this.tomNode12 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode12Set = true; entry.tomNode12 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode12DidChange(value);
        }
    }
    
    public bool tomNode13 {
        get {
            return _cache.LookForValueInCache(_tomNode13, entry => entry.tomNode13Set, entry => entry.tomNode13);
        }
        set {
            if (this.tomNode13 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode13Set = true; entry.tomNode13 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode13DidChange(value);
        }
    }
    
    public bool tomNode14 {
        get {
            return _cache.LookForValueInCache(_tomNode14, entry => entry.tomNode14Set, entry => entry.tomNode14);
        }
        set {
            if (this.tomNode14 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode14Set = true; entry.tomNode14 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode14DidChange(value);
        }
    }
    
    public bool tomNode15 {
        get {
            return _cache.LookForValueInCache(_tomNode15, entry => entry.tomNode15Set, entry => entry.tomNode15);
        }
        set {
            if (this.tomNode15 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode15Set = true; entry.tomNode15 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode15DidChange(value);
        }
    }
    
    public bool tomNode16 {
        get {
            return _cache.LookForValueInCache(_tomNode16, entry => entry.tomNode16Set, entry => entry.tomNode16);
        }
        set {
            if (this.tomNode16 == value) return;
            _cache.UpdateLocalCache(entry => { entry.tomNode16Set = true; entry.tomNode16 = value; return entry; });
            InvalidateReliableLength();
            FireTomNode16DidChange(value);
        }
    }
    
    public bool cymbalNode1 {
        get {
            return _cache.LookForValueInCache(_cymbalNode1, entry => entry.cymbalNode1Set, entry => entry.cymbalNode1);
        }
        set {
            if (this.cymbalNode1 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode1Set = true; entry.cymbalNode1 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode1DidChange(value);
        }
    }
    
    public bool cymbalNode2 {
        get {
            return _cache.LookForValueInCache(_cymbalNode2, entry => entry.cymbalNode2Set, entry => entry.cymbalNode2);
        }
        set {
            if (this.cymbalNode2 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode2Set = true; entry.cymbalNode2 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode2DidChange(value);
        }
    }
    
    public bool cymbalNode3 {
        get {
            return _cache.LookForValueInCache(_cymbalNode3, entry => entry.cymbalNode3Set, entry => entry.cymbalNode3);
        }
        set {
            if (this.cymbalNode3 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode3Set = true; entry.cymbalNode3 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode3DidChange(value);
        }
    }
    
    public bool cymbalNode4 {
        get {
            return _cache.LookForValueInCache(_cymbalNode4, entry => entry.cymbalNode4Set, entry => entry.cymbalNode4);
        }
        set {
            if (this.cymbalNode4 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode4Set = true; entry.cymbalNode4 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode4DidChange(value);
        }
    }
    
    public bool cymbalNode5 {
        get {
            return _cache.LookForValueInCache(_cymbalNode5, entry => entry.cymbalNode5Set, entry => entry.cymbalNode5);
        }
        set {
            if (this.cymbalNode5 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode5Set = true; entry.cymbalNode5 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode5DidChange(value);
        }
    }
    
    public bool cymbalNode6 {
        get {
            return _cache.LookForValueInCache(_cymbalNode6, entry => entry.cymbalNode6Set, entry => entry.cymbalNode6);
        }
        set {
            if (this.cymbalNode6 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode6Set = true; entry.cymbalNode6 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode6DidChange(value);
        }
    }
    
    public bool cymbalNode7 {
        get {
            return _cache.LookForValueInCache(_cymbalNode7, entry => entry.cymbalNode7Set, entry => entry.cymbalNode7);
        }
        set {
            if (this.cymbalNode7 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode7Set = true; entry.cymbalNode7 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode7DidChange(value);
        }
    }
    
    public bool cymbalNode8 {
        get {
            return _cache.LookForValueInCache(_cymbalNode8, entry => entry.cymbalNode8Set, entry => entry.cymbalNode8);
        }
        set {
            if (this.cymbalNode8 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode8Set = true; entry.cymbalNode8 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode8DidChange(value);
        }
    }
    
    public bool cymbalNode9 {
        get {
            return _cache.LookForValueInCache(_cymbalNode9, entry => entry.cymbalNode9Set, entry => entry.cymbalNode9);
        }
        set {
            if (this.cymbalNode9 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode9Set = true; entry.cymbalNode9 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode9DidChange(value);
        }
    }
    
    public bool cymbalNode10 {
        get {
            return _cache.LookForValueInCache(_cymbalNode10, entry => entry.cymbalNode10Set, entry => entry.cymbalNode10);
        }
        set {
            if (this.cymbalNode10 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode10Set = true; entry.cymbalNode10 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode10DidChange(value);
        }
    }
    
    public bool cymbalNode11 {
        get {
            return _cache.LookForValueInCache(_cymbalNode11, entry => entry.cymbalNode11Set, entry => entry.cymbalNode11);
        }
        set {
            if (this.cymbalNode11 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode11Set = true; entry.cymbalNode11 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode11DidChange(value);
        }
    }
    
    public bool cymbalNode12 {
        get {
            return _cache.LookForValueInCache(_cymbalNode12, entry => entry.cymbalNode12Set, entry => entry.cymbalNode12);
        }
        set {
            if (this.cymbalNode12 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode12Set = true; entry.cymbalNode12 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode12DidChange(value);
        }
    }
    
    public bool cymbalNode13 {
        get {
            return _cache.LookForValueInCache(_cymbalNode13, entry => entry.cymbalNode13Set, entry => entry.cymbalNode13);
        }
        set {
            if (this.cymbalNode13 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode13Set = true; entry.cymbalNode13 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode13DidChange(value);
        }
    }
    
    public bool cymbalNode14 {
        get {
            return _cache.LookForValueInCache(_cymbalNode14, entry => entry.cymbalNode14Set, entry => entry.cymbalNode14);
        }
        set {
            if (this.cymbalNode14 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode14Set = true; entry.cymbalNode14 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode14DidChange(value);
        }
    }
    
    public bool cymbalNode15 {
        get {
            return _cache.LookForValueInCache(_cymbalNode15, entry => entry.cymbalNode15Set, entry => entry.cymbalNode15);
        }
        set {
            if (this.cymbalNode15 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode15Set = true; entry.cymbalNode15 = value; return entry; });
            InvalidateReliableLength();
            FireCymbalNode15DidChange(value);
        }
    }
    
    public bool cymbalNode16 {
        get {
            return _cache.LookForValueInCache(_cymbalNode16, entry => entry.cymbalNode16Set, entry => entry.cymbalNode16);
        }
        set {
            if (this.cymbalNode16 == value) return;
            _cache.UpdateLocalCache(entry => { entry.cymbalNode16Set = true; entry.cymbalNode16 = value; return entry; });
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
    
    private struct LocalCacheEntry {
        public bool kickNode1Set;
        public bool kickNode1;
        public bool kickNode2Set;
        public bool kickNode2;
        public bool kickNode3Set;
        public bool kickNode3;
        public bool kickNode4Set;
        public bool kickNode4;
        public bool kickNode5Set;
        public bool kickNode5;
        public bool kickNode6Set;
        public bool kickNode6;
        public bool kickNode7Set;
        public bool kickNode7;
        public bool kickNode8Set;
        public bool kickNode8;
        public bool kickNode9Set;
        public bool kickNode9;
        public bool kickNode10Set;
        public bool kickNode10;
        public bool kickNode11Set;
        public bool kickNode11;
        public bool kickNode12Set;
        public bool kickNode12;
        public bool kickNode13Set;
        public bool kickNode13;
        public bool kickNode14Set;
        public bool kickNode14;
        public bool kickNode15Set;
        public bool kickNode15;
        public bool kickNode16Set;
        public bool kickNode16;
        public bool hiHatNode1Set;
        public bool hiHatNode1;
        public bool hiHatNode2Set;
        public bool hiHatNode2;
        public bool hiHatNode3Set;
        public bool hiHatNode3;
        public bool hiHatNode4Set;
        public bool hiHatNode4;
        public bool hiHatNode5Set;
        public bool hiHatNode5;
        public bool hiHatNode6Set;
        public bool hiHatNode6;
        public bool hiHatNode7Set;
        public bool hiHatNode7;
        public bool hiHatNode8Set;
        public bool hiHatNode8;
        public bool hiHatNode9Set;
        public bool hiHatNode9;
        public bool hiHatNode10Set;
        public bool hiHatNode10;
        public bool hiHatNode11Set;
        public bool hiHatNode11;
        public bool hiHatNode12Set;
        public bool hiHatNode12;
        public bool hiHatNode13Set;
        public bool hiHatNode13;
        public bool hiHatNode14Set;
        public bool hiHatNode14;
        public bool hiHatNode15Set;
        public bool hiHatNode15;
        public bool hiHatNode16Set;
        public bool hiHatNode16;
        public bool snareNode1Set;
        public bool snareNode1;
        public bool snareNode2Set;
        public bool snareNode2;
        public bool snareNode3Set;
        public bool snareNode3;
        public bool snareNode4Set;
        public bool snareNode4;
        public bool snareNode5Set;
        public bool snareNode5;
        public bool snareNode6Set;
        public bool snareNode6;
        public bool snareNode7Set;
        public bool snareNode7;
        public bool snareNode8Set;
        public bool snareNode8;
        public bool snareNode9Set;
        public bool snareNode9;
        public bool snareNode10Set;
        public bool snareNode10;
        public bool snareNode11Set;
        public bool snareNode11;
        public bool snareNode12Set;
        public bool snareNode12;
        public bool snareNode13Set;
        public bool snareNode13;
        public bool snareNode14Set;
        public bool snareNode14;
        public bool snareNode15Set;
        public bool snareNode15;
        public bool snareNode16Set;
        public bool snareNode16;
        public bool tomNode1Set;
        public bool tomNode1;
        public bool tomNode2Set;
        public bool tomNode2;
        public bool tomNode3Set;
        public bool tomNode3;
        public bool tomNode4Set;
        public bool tomNode4;
        public bool tomNode5Set;
        public bool tomNode5;
        public bool tomNode6Set;
        public bool tomNode6;
        public bool tomNode7Set;
        public bool tomNode7;
        public bool tomNode8Set;
        public bool tomNode8;
        public bool tomNode9Set;
        public bool tomNode9;
        public bool tomNode10Set;
        public bool tomNode10;
        public bool tomNode11Set;
        public bool tomNode11;
        public bool tomNode12Set;
        public bool tomNode12;
        public bool tomNode13Set;
        public bool tomNode13;
        public bool tomNode14Set;
        public bool tomNode14;
        public bool tomNode15Set;
        public bool tomNode15;
        public bool tomNode16Set;
        public bool tomNode16;
        public bool cymbalNode1Set;
        public bool cymbalNode1;
        public bool cymbalNode2Set;
        public bool cymbalNode2;
        public bool cymbalNode3Set;
        public bool cymbalNode3;
        public bool cymbalNode4Set;
        public bool cymbalNode4;
        public bool cymbalNode5Set;
        public bool cymbalNode5;
        public bool cymbalNode6Set;
        public bool cymbalNode6;
        public bool cymbalNode7Set;
        public bool cymbalNode7;
        public bool cymbalNode8Set;
        public bool cymbalNode8;
        public bool cymbalNode9Set;
        public bool cymbalNode9;
        public bool cymbalNode10Set;
        public bool cymbalNode10;
        public bool cymbalNode11Set;
        public bool cymbalNode11;
        public bool cymbalNode12Set;
        public bool cymbalNode12;
        public bool cymbalNode13Set;
        public bool cymbalNode13;
        public bool cymbalNode14Set;
        public bool cymbalNode14;
        public bool cymbalNode15Set;
        public bool cymbalNode15;
        public bool cymbalNode16Set;
        public bool cymbalNode16;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache = new LocalChangeCache<LocalCacheEntry>();
    
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
    
    public NodeSyncModel() : this(null) {
    }
    
    public NodeSyncModel(RealtimeModel parent) : base(null, parent) {
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        UnsubscribeClearCacheCallback();
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
        int length = 0;
        if (context.fullModel) {
            FlattenCache();
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode1, _kickNode1 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode2, _kickNode2 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode3, _kickNode3 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode4, _kickNode4 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode5, _kickNode5 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode6, _kickNode6 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode7, _kickNode7 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode8, _kickNode8 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode9, _kickNode9 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode10, _kickNode10 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode11, _kickNode11 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode12, _kickNode12 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode13, _kickNode13 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode14, _kickNode14 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode15, _kickNode15 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode16, _kickNode16 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode1, _hiHatNode1 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode2, _hiHatNode2 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode3, _hiHatNode3 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode4, _hiHatNode4 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode5, _hiHatNode5 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode6, _hiHatNode6 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode7, _hiHatNode7 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode8, _hiHatNode8 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode9, _hiHatNode9 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode10, _hiHatNode10 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode11, _hiHatNode11 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode12, _hiHatNode12 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode13, _hiHatNode13 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode14, _hiHatNode14 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode15, _hiHatNode15 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode16, _hiHatNode16 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode1, _snareNode1 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode2, _snareNode2 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode3, _snareNode3 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode4, _snareNode4 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode5, _snareNode5 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode6, _snareNode6 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode7, _snareNode7 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode8, _snareNode8 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode9, _snareNode9 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode10, _snareNode10 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode11, _snareNode11 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode12, _snareNode12 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode13, _snareNode13 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode14, _snareNode14 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode15, _snareNode15 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode16, _snareNode16 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode1, _tomNode1 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode2, _tomNode2 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode3, _tomNode3 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode4, _tomNode4 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode5, _tomNode5 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode6, _tomNode6 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode7, _tomNode7 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode8, _tomNode8 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode9, _tomNode9 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode10, _tomNode10 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode11, _tomNode11 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode12, _tomNode12 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode13, _tomNode13 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode14, _tomNode14 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode15, _tomNode15 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode16, _tomNode16 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode1, _cymbalNode1 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode2, _cymbalNode2 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode3, _cymbalNode3 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode4, _cymbalNode4 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode5, _cymbalNode5 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode6, _cymbalNode6 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode7, _cymbalNode7 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode8, _cymbalNode8 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode9, _cymbalNode9 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode10, _cymbalNode10 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode11, _cymbalNode11 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode12, _cymbalNode12 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode13, _cymbalNode13 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode14, _cymbalNode14 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode15, _cymbalNode15 ? 1u : 0u);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode16, _cymbalNode16 ? 1u : 0u);
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.kickNode1Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode1, entry.kickNode1 ? 1u : 0u);
            }
            if (entry.kickNode2Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode2, entry.kickNode2 ? 1u : 0u);
            }
            if (entry.kickNode3Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode3, entry.kickNode3 ? 1u : 0u);
            }
            if (entry.kickNode4Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode4, entry.kickNode4 ? 1u : 0u);
            }
            if (entry.kickNode5Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode5, entry.kickNode5 ? 1u : 0u);
            }
            if (entry.kickNode6Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode6, entry.kickNode6 ? 1u : 0u);
            }
            if (entry.kickNode7Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode7, entry.kickNode7 ? 1u : 0u);
            }
            if (entry.kickNode8Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode8, entry.kickNode8 ? 1u : 0u);
            }
            if (entry.kickNode9Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode9, entry.kickNode9 ? 1u : 0u);
            }
            if (entry.kickNode10Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode10, entry.kickNode10 ? 1u : 0u);
            }
            if (entry.kickNode11Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode11, entry.kickNode11 ? 1u : 0u);
            }
            if (entry.kickNode12Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode12, entry.kickNode12 ? 1u : 0u);
            }
            if (entry.kickNode13Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode13, entry.kickNode13 ? 1u : 0u);
            }
            if (entry.kickNode14Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode14, entry.kickNode14 ? 1u : 0u);
            }
            if (entry.kickNode15Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode15, entry.kickNode15 ? 1u : 0u);
            }
            if (entry.kickNode16Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.KickNode16, entry.kickNode16 ? 1u : 0u);
            }
            if (entry.hiHatNode1Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode1, entry.hiHatNode1 ? 1u : 0u);
            }
            if (entry.hiHatNode2Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode2, entry.hiHatNode2 ? 1u : 0u);
            }
            if (entry.hiHatNode3Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode3, entry.hiHatNode3 ? 1u : 0u);
            }
            if (entry.hiHatNode4Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode4, entry.hiHatNode4 ? 1u : 0u);
            }
            if (entry.hiHatNode5Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode5, entry.hiHatNode5 ? 1u : 0u);
            }
            if (entry.hiHatNode6Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode6, entry.hiHatNode6 ? 1u : 0u);
            }
            if (entry.hiHatNode7Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode7, entry.hiHatNode7 ? 1u : 0u);
            }
            if (entry.hiHatNode8Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode8, entry.hiHatNode8 ? 1u : 0u);
            }
            if (entry.hiHatNode9Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode9, entry.hiHatNode9 ? 1u : 0u);
            }
            if (entry.hiHatNode10Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode10, entry.hiHatNode10 ? 1u : 0u);
            }
            if (entry.hiHatNode11Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode11, entry.hiHatNode11 ? 1u : 0u);
            }
            if (entry.hiHatNode12Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode12, entry.hiHatNode12 ? 1u : 0u);
            }
            if (entry.hiHatNode13Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode13, entry.hiHatNode13 ? 1u : 0u);
            }
            if (entry.hiHatNode14Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode14, entry.hiHatNode14 ? 1u : 0u);
            }
            if (entry.hiHatNode15Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode15, entry.hiHatNode15 ? 1u : 0u);
            }
            if (entry.hiHatNode16Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.HiHatNode16, entry.hiHatNode16 ? 1u : 0u);
            }
            if (entry.snareNode1Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode1, entry.snareNode1 ? 1u : 0u);
            }
            if (entry.snareNode2Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode2, entry.snareNode2 ? 1u : 0u);
            }
            if (entry.snareNode3Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode3, entry.snareNode3 ? 1u : 0u);
            }
            if (entry.snareNode4Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode4, entry.snareNode4 ? 1u : 0u);
            }
            if (entry.snareNode5Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode5, entry.snareNode5 ? 1u : 0u);
            }
            if (entry.snareNode6Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode6, entry.snareNode6 ? 1u : 0u);
            }
            if (entry.snareNode7Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode7, entry.snareNode7 ? 1u : 0u);
            }
            if (entry.snareNode8Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode8, entry.snareNode8 ? 1u : 0u);
            }
            if (entry.snareNode9Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode9, entry.snareNode9 ? 1u : 0u);
            }
            if (entry.snareNode10Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode10, entry.snareNode10 ? 1u : 0u);
            }
            if (entry.snareNode11Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode11, entry.snareNode11 ? 1u : 0u);
            }
            if (entry.snareNode12Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode12, entry.snareNode12 ? 1u : 0u);
            }
            if (entry.snareNode13Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode13, entry.snareNode13 ? 1u : 0u);
            }
            if (entry.snareNode14Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode14, entry.snareNode14 ? 1u : 0u);
            }
            if (entry.snareNode15Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode15, entry.snareNode15 ? 1u : 0u);
            }
            if (entry.snareNode16Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.SnareNode16, entry.snareNode16 ? 1u : 0u);
            }
            if (entry.tomNode1Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode1, entry.tomNode1 ? 1u : 0u);
            }
            if (entry.tomNode2Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode2, entry.tomNode2 ? 1u : 0u);
            }
            if (entry.tomNode3Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode3, entry.tomNode3 ? 1u : 0u);
            }
            if (entry.tomNode4Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode4, entry.tomNode4 ? 1u : 0u);
            }
            if (entry.tomNode5Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode5, entry.tomNode5 ? 1u : 0u);
            }
            if (entry.tomNode6Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode6, entry.tomNode6 ? 1u : 0u);
            }
            if (entry.tomNode7Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode7, entry.tomNode7 ? 1u : 0u);
            }
            if (entry.tomNode8Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode8, entry.tomNode8 ? 1u : 0u);
            }
            if (entry.tomNode9Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode9, entry.tomNode9 ? 1u : 0u);
            }
            if (entry.tomNode10Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode10, entry.tomNode10 ? 1u : 0u);
            }
            if (entry.tomNode11Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode11, entry.tomNode11 ? 1u : 0u);
            }
            if (entry.tomNode12Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode12, entry.tomNode12 ? 1u : 0u);
            }
            if (entry.tomNode13Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode13, entry.tomNode13 ? 1u : 0u);
            }
            if (entry.tomNode14Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode14, entry.tomNode14 ? 1u : 0u);
            }
            if (entry.tomNode15Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode15, entry.tomNode15 ? 1u : 0u);
            }
            if (entry.tomNode16Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.TomNode16, entry.tomNode16 ? 1u : 0u);
            }
            if (entry.cymbalNode1Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode1, entry.cymbalNode1 ? 1u : 0u);
            }
            if (entry.cymbalNode2Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode2, entry.cymbalNode2 ? 1u : 0u);
            }
            if (entry.cymbalNode3Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode3, entry.cymbalNode3 ? 1u : 0u);
            }
            if (entry.cymbalNode4Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode4, entry.cymbalNode4 ? 1u : 0u);
            }
            if (entry.cymbalNode5Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode5, entry.cymbalNode5 ? 1u : 0u);
            }
            if (entry.cymbalNode6Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode6, entry.cymbalNode6 ? 1u : 0u);
            }
            if (entry.cymbalNode7Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode7, entry.cymbalNode7 ? 1u : 0u);
            }
            if (entry.cymbalNode8Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode8, entry.cymbalNode8 ? 1u : 0u);
            }
            if (entry.cymbalNode9Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode9, entry.cymbalNode9 ? 1u : 0u);
            }
            if (entry.cymbalNode10Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode10, entry.cymbalNode10 ? 1u : 0u);
            }
            if (entry.cymbalNode11Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode11, entry.cymbalNode11 ? 1u : 0u);
            }
            if (entry.cymbalNode12Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode12, entry.cymbalNode12 ? 1u : 0u);
            }
            if (entry.cymbalNode13Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode13, entry.cymbalNode13 ? 1u : 0u);
            }
            if (entry.cymbalNode14Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode14, entry.cymbalNode14 ? 1u : 0u);
            }
            if (entry.cymbalNode15Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode15, entry.cymbalNode15 ? 1u : 0u);
            }
            if (entry.cymbalNode16Set) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.CymbalNode16, entry.cymbalNode16 ? 1u : 0u);
            }
        }
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var didWriteProperties = false;
        
        if (context.fullModel) {
            stream.WriteVarint32((uint)PropertyID.KickNode1, _kickNode1 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode2, _kickNode2 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode3, _kickNode3 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode4, _kickNode4 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode5, _kickNode5 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode6, _kickNode6 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode7, _kickNode7 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode8, _kickNode8 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode9, _kickNode9 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode10, _kickNode10 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode11, _kickNode11 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode12, _kickNode12 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode13, _kickNode13 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode14, _kickNode14 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode15, _kickNode15 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.KickNode16, _kickNode16 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode1, _hiHatNode1 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode2, _hiHatNode2 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode3, _hiHatNode3 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode4, _hiHatNode4 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode5, _hiHatNode5 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode6, _hiHatNode6 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode7, _hiHatNode7 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode8, _hiHatNode8 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode9, _hiHatNode9 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode10, _hiHatNode10 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode11, _hiHatNode11 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode12, _hiHatNode12 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode13, _hiHatNode13 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode14, _hiHatNode14 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode15, _hiHatNode15 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.HiHatNode16, _hiHatNode16 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode1, _snareNode1 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode2, _snareNode2 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode3, _snareNode3 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode4, _snareNode4 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode5, _snareNode5 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode6, _snareNode6 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode7, _snareNode7 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode8, _snareNode8 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode9, _snareNode9 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode10, _snareNode10 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode11, _snareNode11 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode12, _snareNode12 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode13, _snareNode13 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode14, _snareNode14 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode15, _snareNode15 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.SnareNode16, _snareNode16 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode1, _tomNode1 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode2, _tomNode2 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode3, _tomNode3 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode4, _tomNode4 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode5, _tomNode5 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode6, _tomNode6 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode7, _tomNode7 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode8, _tomNode8 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode9, _tomNode9 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode10, _tomNode10 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode11, _tomNode11 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode12, _tomNode12 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode13, _tomNode13 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode14, _tomNode14 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode15, _tomNode15 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.TomNode16, _tomNode16 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode1, _cymbalNode1 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode2, _cymbalNode2 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode3, _cymbalNode3 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode4, _cymbalNode4 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode5, _cymbalNode5 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode6, _cymbalNode6 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode7, _cymbalNode7 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode8, _cymbalNode8 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode9, _cymbalNode9 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode10, _cymbalNode10 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode11, _cymbalNode11 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode12, _cymbalNode12 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode13, _cymbalNode13 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode14, _cymbalNode14 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode15, _cymbalNode15 ? 1u : 0u);
            stream.WriteVarint32((uint)PropertyID.CymbalNode16, _cymbalNode16 ? 1u : 0u);
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.kickNode1Set || entry.kickNode2Set || entry.kickNode3Set || entry.kickNode4Set || entry.kickNode5Set || entry.kickNode6Set || entry.kickNode7Set || entry.kickNode8Set || entry.kickNode9Set || entry.kickNode10Set || entry.kickNode11Set || entry.kickNode12Set || entry.kickNode13Set || entry.kickNode14Set || entry.kickNode15Set || entry.kickNode16Set || entry.hiHatNode1Set || entry.hiHatNode2Set || entry.hiHatNode3Set || entry.hiHatNode4Set || entry.hiHatNode5Set || entry.hiHatNode6Set || entry.hiHatNode7Set || entry.hiHatNode8Set || entry.hiHatNode9Set || entry.hiHatNode10Set || entry.hiHatNode11Set || entry.hiHatNode12Set || entry.hiHatNode13Set || entry.hiHatNode14Set || entry.hiHatNode15Set || entry.hiHatNode16Set || entry.snareNode1Set || entry.snareNode2Set || entry.snareNode3Set || entry.snareNode4Set || entry.snareNode5Set || entry.snareNode6Set || entry.snareNode7Set || entry.snareNode8Set || entry.snareNode9Set || entry.snareNode10Set || entry.snareNode11Set || entry.snareNode12Set || entry.snareNode13Set || entry.snareNode14Set || entry.snareNode15Set || entry.snareNode16Set || entry.tomNode1Set || entry.tomNode2Set || entry.tomNode3Set || entry.tomNode4Set || entry.tomNode5Set || entry.tomNode6Set || entry.tomNode7Set || entry.tomNode8Set || entry.tomNode9Set || entry.tomNode10Set || entry.tomNode11Set || entry.tomNode12Set || entry.tomNode13Set || entry.tomNode14Set || entry.tomNode15Set || entry.tomNode16Set || entry.cymbalNode1Set || entry.cymbalNode2Set || entry.cymbalNode3Set || entry.cymbalNode4Set || entry.cymbalNode5Set || entry.cymbalNode6Set || entry.cymbalNode7Set || entry.cymbalNode8Set || entry.cymbalNode9Set || entry.cymbalNode10Set || entry.cymbalNode11Set || entry.cymbalNode12Set || entry.cymbalNode13Set || entry.cymbalNode14Set || entry.cymbalNode15Set || entry.cymbalNode16Set) {
                _cache.PushLocalCacheToInflight(context.updateID);
                ClearCacheOnStreamCallback(context);
            }
            if (entry.kickNode1Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode1, entry.kickNode1 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode2Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode2, entry.kickNode2 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode3Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode3, entry.kickNode3 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode4Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode4, entry.kickNode4 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode5Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode5, entry.kickNode5 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode6Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode6, entry.kickNode6 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode7Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode7, entry.kickNode7 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode8Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode8, entry.kickNode8 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode9Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode9, entry.kickNode9 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode10Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode10, entry.kickNode10 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode11Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode11, entry.kickNode11 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode12Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode12, entry.kickNode12 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode13Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode13, entry.kickNode13 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode14Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode14, entry.kickNode14 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode15Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode15, entry.kickNode15 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.kickNode16Set) {
                stream.WriteVarint32((uint)PropertyID.KickNode16, entry.kickNode16 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode1Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode1, entry.hiHatNode1 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode2Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode2, entry.hiHatNode2 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode3Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode3, entry.hiHatNode3 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode4Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode4, entry.hiHatNode4 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode5Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode5, entry.hiHatNode5 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode6Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode6, entry.hiHatNode6 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode7Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode7, entry.hiHatNode7 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode8Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode8, entry.hiHatNode8 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode9Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode9, entry.hiHatNode9 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode10Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode10, entry.hiHatNode10 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode11Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode11, entry.hiHatNode11 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode12Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode12, entry.hiHatNode12 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode13Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode13, entry.hiHatNode13 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode14Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode14, entry.hiHatNode14 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode15Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode15, entry.hiHatNode15 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.hiHatNode16Set) {
                stream.WriteVarint32((uint)PropertyID.HiHatNode16, entry.hiHatNode16 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode1Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode1, entry.snareNode1 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode2Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode2, entry.snareNode2 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode3Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode3, entry.snareNode3 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode4Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode4, entry.snareNode4 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode5Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode5, entry.snareNode5 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode6Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode6, entry.snareNode6 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode7Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode7, entry.snareNode7 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode8Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode8, entry.snareNode8 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode9Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode9, entry.snareNode9 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode10Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode10, entry.snareNode10 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode11Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode11, entry.snareNode11 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode12Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode12, entry.snareNode12 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode13Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode13, entry.snareNode13 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode14Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode14, entry.snareNode14 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode15Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode15, entry.snareNode15 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.snareNode16Set) {
                stream.WriteVarint32((uint)PropertyID.SnareNode16, entry.snareNode16 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode1Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode1, entry.tomNode1 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode2Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode2, entry.tomNode2 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode3Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode3, entry.tomNode3 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode4Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode4, entry.tomNode4 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode5Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode5, entry.tomNode5 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode6Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode6, entry.tomNode6 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode7Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode7, entry.tomNode7 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode8Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode8, entry.tomNode8 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode9Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode9, entry.tomNode9 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode10Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode10, entry.tomNode10 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode11Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode11, entry.tomNode11 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode12Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode12, entry.tomNode12 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode13Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode13, entry.tomNode13 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode14Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode14, entry.tomNode14 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode15Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode15, entry.tomNode15 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.tomNode16Set) {
                stream.WriteVarint32((uint)PropertyID.TomNode16, entry.tomNode16 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode1Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode1, entry.cymbalNode1 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode2Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode2, entry.cymbalNode2 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode3Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode3, entry.cymbalNode3 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode4Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode4, entry.cymbalNode4 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode5Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode5, entry.cymbalNode5 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode6Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode6, entry.cymbalNode6 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode7Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode7, entry.cymbalNode7 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode8Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode8, entry.cymbalNode8 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode9Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode9, entry.cymbalNode9 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode10Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode10, entry.cymbalNode10 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode11Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode11, entry.cymbalNode11 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode12Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode12, entry.cymbalNode12 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode13Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode13, entry.cymbalNode13 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode14Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode14, entry.cymbalNode14 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode15Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode15, entry.cymbalNode15 ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.cymbalNode16Set) {
                stream.WriteVarint32((uint)PropertyID.CymbalNode16, entry.cymbalNode16 ? 1u : 0u);
                didWriteProperties = true;
            }
            
            if (didWriteProperties) InvalidateReliableLength();
        }
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.KickNode1: {
                    bool previousValue = _kickNode1;
                    _kickNode1 = (stream.ReadVarint32() != 0);
                    bool kickNode1ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode1Set);
                    if (!kickNode1ExistsInChangeCache && _kickNode1 != previousValue) {
                        FireKickNode1DidChange(_kickNode1);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode2: {
                    bool previousValue = _kickNode2;
                    _kickNode2 = (stream.ReadVarint32() != 0);
                    bool kickNode2ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode2Set);
                    if (!kickNode2ExistsInChangeCache && _kickNode2 != previousValue) {
                        FireKickNode2DidChange(_kickNode2);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode3: {
                    bool previousValue = _kickNode3;
                    _kickNode3 = (stream.ReadVarint32() != 0);
                    bool kickNode3ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode3Set);
                    if (!kickNode3ExistsInChangeCache && _kickNode3 != previousValue) {
                        FireKickNode3DidChange(_kickNode3);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode4: {
                    bool previousValue = _kickNode4;
                    _kickNode4 = (stream.ReadVarint32() != 0);
                    bool kickNode4ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode4Set);
                    if (!kickNode4ExistsInChangeCache && _kickNode4 != previousValue) {
                        FireKickNode4DidChange(_kickNode4);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode5: {
                    bool previousValue = _kickNode5;
                    _kickNode5 = (stream.ReadVarint32() != 0);
                    bool kickNode5ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode5Set);
                    if (!kickNode5ExistsInChangeCache && _kickNode5 != previousValue) {
                        FireKickNode5DidChange(_kickNode5);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode6: {
                    bool previousValue = _kickNode6;
                    _kickNode6 = (stream.ReadVarint32() != 0);
                    bool kickNode6ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode6Set);
                    if (!kickNode6ExistsInChangeCache && _kickNode6 != previousValue) {
                        FireKickNode6DidChange(_kickNode6);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode7: {
                    bool previousValue = _kickNode7;
                    _kickNode7 = (stream.ReadVarint32() != 0);
                    bool kickNode7ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode7Set);
                    if (!kickNode7ExistsInChangeCache && _kickNode7 != previousValue) {
                        FireKickNode7DidChange(_kickNode7);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode8: {
                    bool previousValue = _kickNode8;
                    _kickNode8 = (stream.ReadVarint32() != 0);
                    bool kickNode8ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode8Set);
                    if (!kickNode8ExistsInChangeCache && _kickNode8 != previousValue) {
                        FireKickNode8DidChange(_kickNode8);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode9: {
                    bool previousValue = _kickNode9;
                    _kickNode9 = (stream.ReadVarint32() != 0);
                    bool kickNode9ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode9Set);
                    if (!kickNode9ExistsInChangeCache && _kickNode9 != previousValue) {
                        FireKickNode9DidChange(_kickNode9);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode10: {
                    bool previousValue = _kickNode10;
                    _kickNode10 = (stream.ReadVarint32() != 0);
                    bool kickNode10ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode10Set);
                    if (!kickNode10ExistsInChangeCache && _kickNode10 != previousValue) {
                        FireKickNode10DidChange(_kickNode10);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode11: {
                    bool previousValue = _kickNode11;
                    _kickNode11 = (stream.ReadVarint32() != 0);
                    bool kickNode11ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode11Set);
                    if (!kickNode11ExistsInChangeCache && _kickNode11 != previousValue) {
                        FireKickNode11DidChange(_kickNode11);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode12: {
                    bool previousValue = _kickNode12;
                    _kickNode12 = (stream.ReadVarint32() != 0);
                    bool kickNode12ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode12Set);
                    if (!kickNode12ExistsInChangeCache && _kickNode12 != previousValue) {
                        FireKickNode12DidChange(_kickNode12);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode13: {
                    bool previousValue = _kickNode13;
                    _kickNode13 = (stream.ReadVarint32() != 0);
                    bool kickNode13ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode13Set);
                    if (!kickNode13ExistsInChangeCache && _kickNode13 != previousValue) {
                        FireKickNode13DidChange(_kickNode13);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode14: {
                    bool previousValue = _kickNode14;
                    _kickNode14 = (stream.ReadVarint32() != 0);
                    bool kickNode14ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode14Set);
                    if (!kickNode14ExistsInChangeCache && _kickNode14 != previousValue) {
                        FireKickNode14DidChange(_kickNode14);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode15: {
                    bool previousValue = _kickNode15;
                    _kickNode15 = (stream.ReadVarint32() != 0);
                    bool kickNode15ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode15Set);
                    if (!kickNode15ExistsInChangeCache && _kickNode15 != previousValue) {
                        FireKickNode15DidChange(_kickNode15);
                    }
                    break;
                }
                case (uint)PropertyID.KickNode16: {
                    bool previousValue = _kickNode16;
                    _kickNode16 = (stream.ReadVarint32() != 0);
                    bool kickNode16ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.kickNode16Set);
                    if (!kickNode16ExistsInChangeCache && _kickNode16 != previousValue) {
                        FireKickNode16DidChange(_kickNode16);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode1: {
                    bool previousValue = _hiHatNode1;
                    _hiHatNode1 = (stream.ReadVarint32() != 0);
                    bool hiHatNode1ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode1Set);
                    if (!hiHatNode1ExistsInChangeCache && _hiHatNode1 != previousValue) {
                        FireHiHatNode1DidChange(_hiHatNode1);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode2: {
                    bool previousValue = _hiHatNode2;
                    _hiHatNode2 = (stream.ReadVarint32() != 0);
                    bool hiHatNode2ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode2Set);
                    if (!hiHatNode2ExistsInChangeCache && _hiHatNode2 != previousValue) {
                        FireHiHatNode2DidChange(_hiHatNode2);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode3: {
                    bool previousValue = _hiHatNode3;
                    _hiHatNode3 = (stream.ReadVarint32() != 0);
                    bool hiHatNode3ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode3Set);
                    if (!hiHatNode3ExistsInChangeCache && _hiHatNode3 != previousValue) {
                        FireHiHatNode3DidChange(_hiHatNode3);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode4: {
                    bool previousValue = _hiHatNode4;
                    _hiHatNode4 = (stream.ReadVarint32() != 0);
                    bool hiHatNode4ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode4Set);
                    if (!hiHatNode4ExistsInChangeCache && _hiHatNode4 != previousValue) {
                        FireHiHatNode4DidChange(_hiHatNode4);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode5: {
                    bool previousValue = _hiHatNode5;
                    _hiHatNode5 = (stream.ReadVarint32() != 0);
                    bool hiHatNode5ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode5Set);
                    if (!hiHatNode5ExistsInChangeCache && _hiHatNode5 != previousValue) {
                        FireHiHatNode5DidChange(_hiHatNode5);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode6: {
                    bool previousValue = _hiHatNode6;
                    _hiHatNode6 = (stream.ReadVarint32() != 0);
                    bool hiHatNode6ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode6Set);
                    if (!hiHatNode6ExistsInChangeCache && _hiHatNode6 != previousValue) {
                        FireHiHatNode6DidChange(_hiHatNode6);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode7: {
                    bool previousValue = _hiHatNode7;
                    _hiHatNode7 = (stream.ReadVarint32() != 0);
                    bool hiHatNode7ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode7Set);
                    if (!hiHatNode7ExistsInChangeCache && _hiHatNode7 != previousValue) {
                        FireHiHatNode7DidChange(_hiHatNode7);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode8: {
                    bool previousValue = _hiHatNode8;
                    _hiHatNode8 = (stream.ReadVarint32() != 0);
                    bool hiHatNode8ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode8Set);
                    if (!hiHatNode8ExistsInChangeCache && _hiHatNode8 != previousValue) {
                        FireHiHatNode8DidChange(_hiHatNode8);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode9: {
                    bool previousValue = _hiHatNode9;
                    _hiHatNode9 = (stream.ReadVarint32() != 0);
                    bool hiHatNode9ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode9Set);
                    if (!hiHatNode9ExistsInChangeCache && _hiHatNode9 != previousValue) {
                        FireHiHatNode9DidChange(_hiHatNode9);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode10: {
                    bool previousValue = _hiHatNode10;
                    _hiHatNode10 = (stream.ReadVarint32() != 0);
                    bool hiHatNode10ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode10Set);
                    if (!hiHatNode10ExistsInChangeCache && _hiHatNode10 != previousValue) {
                        FireHiHatNode10DidChange(_hiHatNode10);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode11: {
                    bool previousValue = _hiHatNode11;
                    _hiHatNode11 = (stream.ReadVarint32() != 0);
                    bool hiHatNode11ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode11Set);
                    if (!hiHatNode11ExistsInChangeCache && _hiHatNode11 != previousValue) {
                        FireHiHatNode11DidChange(_hiHatNode11);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode12: {
                    bool previousValue = _hiHatNode12;
                    _hiHatNode12 = (stream.ReadVarint32() != 0);
                    bool hiHatNode12ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode12Set);
                    if (!hiHatNode12ExistsInChangeCache && _hiHatNode12 != previousValue) {
                        FireHiHatNode12DidChange(_hiHatNode12);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode13: {
                    bool previousValue = _hiHatNode13;
                    _hiHatNode13 = (stream.ReadVarint32() != 0);
                    bool hiHatNode13ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode13Set);
                    if (!hiHatNode13ExistsInChangeCache && _hiHatNode13 != previousValue) {
                        FireHiHatNode13DidChange(_hiHatNode13);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode14: {
                    bool previousValue = _hiHatNode14;
                    _hiHatNode14 = (stream.ReadVarint32() != 0);
                    bool hiHatNode14ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode14Set);
                    if (!hiHatNode14ExistsInChangeCache && _hiHatNode14 != previousValue) {
                        FireHiHatNode14DidChange(_hiHatNode14);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode15: {
                    bool previousValue = _hiHatNode15;
                    _hiHatNode15 = (stream.ReadVarint32() != 0);
                    bool hiHatNode15ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode15Set);
                    if (!hiHatNode15ExistsInChangeCache && _hiHatNode15 != previousValue) {
                        FireHiHatNode15DidChange(_hiHatNode15);
                    }
                    break;
                }
                case (uint)PropertyID.HiHatNode16: {
                    bool previousValue = _hiHatNode16;
                    _hiHatNode16 = (stream.ReadVarint32() != 0);
                    bool hiHatNode16ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.hiHatNode16Set);
                    if (!hiHatNode16ExistsInChangeCache && _hiHatNode16 != previousValue) {
                        FireHiHatNode16DidChange(_hiHatNode16);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode1: {
                    bool previousValue = _snareNode1;
                    _snareNode1 = (stream.ReadVarint32() != 0);
                    bool snareNode1ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode1Set);
                    if (!snareNode1ExistsInChangeCache && _snareNode1 != previousValue) {
                        FireSnareNode1DidChange(_snareNode1);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode2: {
                    bool previousValue = _snareNode2;
                    _snareNode2 = (stream.ReadVarint32() != 0);
                    bool snareNode2ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode2Set);
                    if (!snareNode2ExistsInChangeCache && _snareNode2 != previousValue) {
                        FireSnareNode2DidChange(_snareNode2);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode3: {
                    bool previousValue = _snareNode3;
                    _snareNode3 = (stream.ReadVarint32() != 0);
                    bool snareNode3ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode3Set);
                    if (!snareNode3ExistsInChangeCache && _snareNode3 != previousValue) {
                        FireSnareNode3DidChange(_snareNode3);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode4: {
                    bool previousValue = _snareNode4;
                    _snareNode4 = (stream.ReadVarint32() != 0);
                    bool snareNode4ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode4Set);
                    if (!snareNode4ExistsInChangeCache && _snareNode4 != previousValue) {
                        FireSnareNode4DidChange(_snareNode4);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode5: {
                    bool previousValue = _snareNode5;
                    _snareNode5 = (stream.ReadVarint32() != 0);
                    bool snareNode5ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode5Set);
                    if (!snareNode5ExistsInChangeCache && _snareNode5 != previousValue) {
                        FireSnareNode5DidChange(_snareNode5);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode6: {
                    bool previousValue = _snareNode6;
                    _snareNode6 = (stream.ReadVarint32() != 0);
                    bool snareNode6ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode6Set);
                    if (!snareNode6ExistsInChangeCache && _snareNode6 != previousValue) {
                        FireSnareNode6DidChange(_snareNode6);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode7: {
                    bool previousValue = _snareNode7;
                    _snareNode7 = (stream.ReadVarint32() != 0);
                    bool snareNode7ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode7Set);
                    if (!snareNode7ExistsInChangeCache && _snareNode7 != previousValue) {
                        FireSnareNode7DidChange(_snareNode7);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode8: {
                    bool previousValue = _snareNode8;
                    _snareNode8 = (stream.ReadVarint32() != 0);
                    bool snareNode8ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode8Set);
                    if (!snareNode8ExistsInChangeCache && _snareNode8 != previousValue) {
                        FireSnareNode8DidChange(_snareNode8);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode9: {
                    bool previousValue = _snareNode9;
                    _snareNode9 = (stream.ReadVarint32() != 0);
                    bool snareNode9ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode9Set);
                    if (!snareNode9ExistsInChangeCache && _snareNode9 != previousValue) {
                        FireSnareNode9DidChange(_snareNode9);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode10: {
                    bool previousValue = _snareNode10;
                    _snareNode10 = (stream.ReadVarint32() != 0);
                    bool snareNode10ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode10Set);
                    if (!snareNode10ExistsInChangeCache && _snareNode10 != previousValue) {
                        FireSnareNode10DidChange(_snareNode10);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode11: {
                    bool previousValue = _snareNode11;
                    _snareNode11 = (stream.ReadVarint32() != 0);
                    bool snareNode11ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode11Set);
                    if (!snareNode11ExistsInChangeCache && _snareNode11 != previousValue) {
                        FireSnareNode11DidChange(_snareNode11);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode12: {
                    bool previousValue = _snareNode12;
                    _snareNode12 = (stream.ReadVarint32() != 0);
                    bool snareNode12ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode12Set);
                    if (!snareNode12ExistsInChangeCache && _snareNode12 != previousValue) {
                        FireSnareNode12DidChange(_snareNode12);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode13: {
                    bool previousValue = _snareNode13;
                    _snareNode13 = (stream.ReadVarint32() != 0);
                    bool snareNode13ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode13Set);
                    if (!snareNode13ExistsInChangeCache && _snareNode13 != previousValue) {
                        FireSnareNode13DidChange(_snareNode13);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode14: {
                    bool previousValue = _snareNode14;
                    _snareNode14 = (stream.ReadVarint32() != 0);
                    bool snareNode14ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode14Set);
                    if (!snareNode14ExistsInChangeCache && _snareNode14 != previousValue) {
                        FireSnareNode14DidChange(_snareNode14);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode15: {
                    bool previousValue = _snareNode15;
                    _snareNode15 = (stream.ReadVarint32() != 0);
                    bool snareNode15ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode15Set);
                    if (!snareNode15ExistsInChangeCache && _snareNode15 != previousValue) {
                        FireSnareNode15DidChange(_snareNode15);
                    }
                    break;
                }
                case (uint)PropertyID.SnareNode16: {
                    bool previousValue = _snareNode16;
                    _snareNode16 = (stream.ReadVarint32() != 0);
                    bool snareNode16ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.snareNode16Set);
                    if (!snareNode16ExistsInChangeCache && _snareNode16 != previousValue) {
                        FireSnareNode16DidChange(_snareNode16);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode1: {
                    bool previousValue = _tomNode1;
                    _tomNode1 = (stream.ReadVarint32() != 0);
                    bool tomNode1ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode1Set);
                    if (!tomNode1ExistsInChangeCache && _tomNode1 != previousValue) {
                        FireTomNode1DidChange(_tomNode1);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode2: {
                    bool previousValue = _tomNode2;
                    _tomNode2 = (stream.ReadVarint32() != 0);
                    bool tomNode2ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode2Set);
                    if (!tomNode2ExistsInChangeCache && _tomNode2 != previousValue) {
                        FireTomNode2DidChange(_tomNode2);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode3: {
                    bool previousValue = _tomNode3;
                    _tomNode3 = (stream.ReadVarint32() != 0);
                    bool tomNode3ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode3Set);
                    if (!tomNode3ExistsInChangeCache && _tomNode3 != previousValue) {
                        FireTomNode3DidChange(_tomNode3);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode4: {
                    bool previousValue = _tomNode4;
                    _tomNode4 = (stream.ReadVarint32() != 0);
                    bool tomNode4ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode4Set);
                    if (!tomNode4ExistsInChangeCache && _tomNode4 != previousValue) {
                        FireTomNode4DidChange(_tomNode4);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode5: {
                    bool previousValue = _tomNode5;
                    _tomNode5 = (stream.ReadVarint32() != 0);
                    bool tomNode5ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode5Set);
                    if (!tomNode5ExistsInChangeCache && _tomNode5 != previousValue) {
                        FireTomNode5DidChange(_tomNode5);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode6: {
                    bool previousValue = _tomNode6;
                    _tomNode6 = (stream.ReadVarint32() != 0);
                    bool tomNode6ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode6Set);
                    if (!tomNode6ExistsInChangeCache && _tomNode6 != previousValue) {
                        FireTomNode6DidChange(_tomNode6);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode7: {
                    bool previousValue = _tomNode7;
                    _tomNode7 = (stream.ReadVarint32() != 0);
                    bool tomNode7ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode7Set);
                    if (!tomNode7ExistsInChangeCache && _tomNode7 != previousValue) {
                        FireTomNode7DidChange(_tomNode7);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode8: {
                    bool previousValue = _tomNode8;
                    _tomNode8 = (stream.ReadVarint32() != 0);
                    bool tomNode8ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode8Set);
                    if (!tomNode8ExistsInChangeCache && _tomNode8 != previousValue) {
                        FireTomNode8DidChange(_tomNode8);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode9: {
                    bool previousValue = _tomNode9;
                    _tomNode9 = (stream.ReadVarint32() != 0);
                    bool tomNode9ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode9Set);
                    if (!tomNode9ExistsInChangeCache && _tomNode9 != previousValue) {
                        FireTomNode9DidChange(_tomNode9);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode10: {
                    bool previousValue = _tomNode10;
                    _tomNode10 = (stream.ReadVarint32() != 0);
                    bool tomNode10ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode10Set);
                    if (!tomNode10ExistsInChangeCache && _tomNode10 != previousValue) {
                        FireTomNode10DidChange(_tomNode10);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode11: {
                    bool previousValue = _tomNode11;
                    _tomNode11 = (stream.ReadVarint32() != 0);
                    bool tomNode11ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode11Set);
                    if (!tomNode11ExistsInChangeCache && _tomNode11 != previousValue) {
                        FireTomNode11DidChange(_tomNode11);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode12: {
                    bool previousValue = _tomNode12;
                    _tomNode12 = (stream.ReadVarint32() != 0);
                    bool tomNode12ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode12Set);
                    if (!tomNode12ExistsInChangeCache && _tomNode12 != previousValue) {
                        FireTomNode12DidChange(_tomNode12);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode13: {
                    bool previousValue = _tomNode13;
                    _tomNode13 = (stream.ReadVarint32() != 0);
                    bool tomNode13ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode13Set);
                    if (!tomNode13ExistsInChangeCache && _tomNode13 != previousValue) {
                        FireTomNode13DidChange(_tomNode13);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode14: {
                    bool previousValue = _tomNode14;
                    _tomNode14 = (stream.ReadVarint32() != 0);
                    bool tomNode14ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode14Set);
                    if (!tomNode14ExistsInChangeCache && _tomNode14 != previousValue) {
                        FireTomNode14DidChange(_tomNode14);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode15: {
                    bool previousValue = _tomNode15;
                    _tomNode15 = (stream.ReadVarint32() != 0);
                    bool tomNode15ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode15Set);
                    if (!tomNode15ExistsInChangeCache && _tomNode15 != previousValue) {
                        FireTomNode15DidChange(_tomNode15);
                    }
                    break;
                }
                case (uint)PropertyID.TomNode16: {
                    bool previousValue = _tomNode16;
                    _tomNode16 = (stream.ReadVarint32() != 0);
                    bool tomNode16ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.tomNode16Set);
                    if (!tomNode16ExistsInChangeCache && _tomNode16 != previousValue) {
                        FireTomNode16DidChange(_tomNode16);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode1: {
                    bool previousValue = _cymbalNode1;
                    _cymbalNode1 = (stream.ReadVarint32() != 0);
                    bool cymbalNode1ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode1Set);
                    if (!cymbalNode1ExistsInChangeCache && _cymbalNode1 != previousValue) {
                        FireCymbalNode1DidChange(_cymbalNode1);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode2: {
                    bool previousValue = _cymbalNode2;
                    _cymbalNode2 = (stream.ReadVarint32() != 0);
                    bool cymbalNode2ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode2Set);
                    if (!cymbalNode2ExistsInChangeCache && _cymbalNode2 != previousValue) {
                        FireCymbalNode2DidChange(_cymbalNode2);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode3: {
                    bool previousValue = _cymbalNode3;
                    _cymbalNode3 = (stream.ReadVarint32() != 0);
                    bool cymbalNode3ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode3Set);
                    if (!cymbalNode3ExistsInChangeCache && _cymbalNode3 != previousValue) {
                        FireCymbalNode3DidChange(_cymbalNode3);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode4: {
                    bool previousValue = _cymbalNode4;
                    _cymbalNode4 = (stream.ReadVarint32() != 0);
                    bool cymbalNode4ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode4Set);
                    if (!cymbalNode4ExistsInChangeCache && _cymbalNode4 != previousValue) {
                        FireCymbalNode4DidChange(_cymbalNode4);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode5: {
                    bool previousValue = _cymbalNode5;
                    _cymbalNode5 = (stream.ReadVarint32() != 0);
                    bool cymbalNode5ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode5Set);
                    if (!cymbalNode5ExistsInChangeCache && _cymbalNode5 != previousValue) {
                        FireCymbalNode5DidChange(_cymbalNode5);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode6: {
                    bool previousValue = _cymbalNode6;
                    _cymbalNode6 = (stream.ReadVarint32() != 0);
                    bool cymbalNode6ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode6Set);
                    if (!cymbalNode6ExistsInChangeCache && _cymbalNode6 != previousValue) {
                        FireCymbalNode6DidChange(_cymbalNode6);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode7: {
                    bool previousValue = _cymbalNode7;
                    _cymbalNode7 = (stream.ReadVarint32() != 0);
                    bool cymbalNode7ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode7Set);
                    if (!cymbalNode7ExistsInChangeCache && _cymbalNode7 != previousValue) {
                        FireCymbalNode7DidChange(_cymbalNode7);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode8: {
                    bool previousValue = _cymbalNode8;
                    _cymbalNode8 = (stream.ReadVarint32() != 0);
                    bool cymbalNode8ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode8Set);
                    if (!cymbalNode8ExistsInChangeCache && _cymbalNode8 != previousValue) {
                        FireCymbalNode8DidChange(_cymbalNode8);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode9: {
                    bool previousValue = _cymbalNode9;
                    _cymbalNode9 = (stream.ReadVarint32() != 0);
                    bool cymbalNode9ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode9Set);
                    if (!cymbalNode9ExistsInChangeCache && _cymbalNode9 != previousValue) {
                        FireCymbalNode9DidChange(_cymbalNode9);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode10: {
                    bool previousValue = _cymbalNode10;
                    _cymbalNode10 = (stream.ReadVarint32() != 0);
                    bool cymbalNode10ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode10Set);
                    if (!cymbalNode10ExistsInChangeCache && _cymbalNode10 != previousValue) {
                        FireCymbalNode10DidChange(_cymbalNode10);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode11: {
                    bool previousValue = _cymbalNode11;
                    _cymbalNode11 = (stream.ReadVarint32() != 0);
                    bool cymbalNode11ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode11Set);
                    if (!cymbalNode11ExistsInChangeCache && _cymbalNode11 != previousValue) {
                        FireCymbalNode11DidChange(_cymbalNode11);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode12: {
                    bool previousValue = _cymbalNode12;
                    _cymbalNode12 = (stream.ReadVarint32() != 0);
                    bool cymbalNode12ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode12Set);
                    if (!cymbalNode12ExistsInChangeCache && _cymbalNode12 != previousValue) {
                        FireCymbalNode12DidChange(_cymbalNode12);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode13: {
                    bool previousValue = _cymbalNode13;
                    _cymbalNode13 = (stream.ReadVarint32() != 0);
                    bool cymbalNode13ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode13Set);
                    if (!cymbalNode13ExistsInChangeCache && _cymbalNode13 != previousValue) {
                        FireCymbalNode13DidChange(_cymbalNode13);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode14: {
                    bool previousValue = _cymbalNode14;
                    _cymbalNode14 = (stream.ReadVarint32() != 0);
                    bool cymbalNode14ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode14Set);
                    if (!cymbalNode14ExistsInChangeCache && _cymbalNode14 != previousValue) {
                        FireCymbalNode14DidChange(_cymbalNode14);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode15: {
                    bool previousValue = _cymbalNode15;
                    _cymbalNode15 = (stream.ReadVarint32() != 0);
                    bool cymbalNode15ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode15Set);
                    if (!cymbalNode15ExistsInChangeCache && _cymbalNode15 != previousValue) {
                        FireCymbalNode15DidChange(_cymbalNode15);
                    }
                    break;
                }
                case (uint)PropertyID.CymbalNode16: {
                    bool previousValue = _cymbalNode16;
                    _cymbalNode16 = (stream.ReadVarint32() != 0);
                    bool cymbalNode16ExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.cymbalNode16Set);
                    if (!cymbalNode16ExistsInChangeCache && _cymbalNode16 != previousValue) {
                        FireCymbalNode16DidChange(_cymbalNode16);
                    }
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
        }
    }
    
    #region Cache Operations
    
    private StreamEventDispatcher _streamEventDispatcher;
    
    private void FlattenCache() {
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
        _cache.Clear();
    }
    
    private void ClearCache(uint updateID) {
        _cache.RemoveUpdateFromInflight(updateID);
    }
    
    private void ClearCacheOnStreamCallback(StreamContext context) {
        if (_streamEventDispatcher != context.dispatcher) {
            UnsubscribeClearCacheCallback(); // unsub from previous dispatcher
        }
        _streamEventDispatcher = context.dispatcher;
        _streamEventDispatcher.AddStreamCallback(context.updateID, ClearCache);
    }
    
    private void UnsubscribeClearCacheCallback() {
        if (_streamEventDispatcher != null) {
            _streamEventDispatcher.RemoveStreamCallback(ClearCache);
            _streamEventDispatcher = null;
        }
    }
    
    #endregion
}
/* ----- End Normal Autogenerated Code ----- */
