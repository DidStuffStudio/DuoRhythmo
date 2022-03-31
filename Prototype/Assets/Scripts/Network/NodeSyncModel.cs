using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;


    [RealtimeModel]
    public partial class NodeSyncModel {
        [RealtimeProperty(1, true)] private Boolean _kickNode1;
        [RealtimeProperty(2, true)] private Boolean _kickNode2;
        [RealtimeProperty(3, true)] private Boolean _kickNode3;
        [RealtimeProperty(4, true)] private Boolean _kickNode4;
        [RealtimeProperty(5, true)] private Boolean _kickNode5;
        [RealtimeProperty(6, true)] private Boolean _kickNode6;
        [RealtimeProperty(7, true)] private Boolean _kickNode7;
        [RealtimeProperty(8, true)] private Boolean _kickNode8;
        [RealtimeProperty(9, true)] private Boolean _kickNode9;
        [RealtimeProperty(10, true)] private Boolean _kickNode10;
        [RealtimeProperty(11, true)] private Boolean _kickNode11;
        [RealtimeProperty(12, true)] private Boolean _kickNode12;
        [RealtimeProperty(13, true)] private Boolean _kickNode13;
        [RealtimeProperty(14, true)] private Boolean _kickNode14;
        [RealtimeProperty(15, true)] private Boolean _kickNode15;
        [RealtimeProperty(16, true)] private Boolean _kickNode16;
        
        [RealtimeProperty(17, true)] private Boolean _hiHatNode1;
        [RealtimeProperty(18, true)] private Boolean _hiHatNode2;
        [RealtimeProperty(19, true)] private Boolean _hiHatNode3;
        [RealtimeProperty(20, true)] private Boolean _hiHatNode4;
        [RealtimeProperty(21, true)] private Boolean _hiHatNode5;
        [RealtimeProperty(22, true)] private Boolean _hiHatNode6;
        [RealtimeProperty(23, true)] private Boolean _hiHatNode7;
        [RealtimeProperty(24, true)] private Boolean _hiHatNode8;
        [RealtimeProperty(25, true)] private Boolean _hiHatNode9;
        [RealtimeProperty(26, true)] private Boolean _hiHatNode10;
        [RealtimeProperty(27, true)] private Boolean _hiHatNode11;
        [RealtimeProperty(28, true)] private Boolean _hiHatNode12;
        [RealtimeProperty(29, true)] private Boolean _hiHatNode13;
        [RealtimeProperty(30, true)] private Boolean _hiHatNode14;
        [RealtimeProperty(31, true)] private Boolean _hiHatNode15;
        [RealtimeProperty(32, true)] private Boolean _hiHatNode16;
        
        [RealtimeProperty(33, true)] private Boolean _snareNode1;
        [RealtimeProperty(34, true)] private Boolean _snareNode2;
        [RealtimeProperty(35, true)] private Boolean _snareNode3;
        [RealtimeProperty(36, true)] private Boolean _snareNode4;
        [RealtimeProperty(37, true)] private Boolean _snareNode5;
        [RealtimeProperty(38, true)] private Boolean _snareNode6;
        [RealtimeProperty(39, true)] private Boolean _snareNode7;
        [RealtimeProperty(40, true)] private Boolean _snareNode8;
        [RealtimeProperty(41, true)] private Boolean _snareNode9;
        [RealtimeProperty(42, true)] private Boolean _snareNode10;
        [RealtimeProperty(43, true)] private Boolean _snareNode11;
        [RealtimeProperty(44, true)] private Boolean _snareNode12;
        [RealtimeProperty(45, true)] private Boolean _snareNode13;
        [RealtimeProperty(46, true)] private Boolean _snareNode14;
        [RealtimeProperty(47, true)] private Boolean _snareNode15;
        [RealtimeProperty(48, true)] private Boolean _snareNode16;
        
        [RealtimeProperty(49, true)] private Boolean _tomNode1;
        [RealtimeProperty(50, true)] private Boolean _tomNode2;
        [RealtimeProperty(51, true)] private Boolean _tomNode3;
        [RealtimeProperty(52, true)] private Boolean _tomNode4;
        [RealtimeProperty(53, true)] private Boolean _tomNode5;
        [RealtimeProperty(54, true)] private Boolean _tomNode6;
        [RealtimeProperty(55, true)] private Boolean _tomNode7;
        [RealtimeProperty(56, true)] private Boolean _tomNode8;
        [RealtimeProperty(57, true)] private Boolean _tomNode9;
        [RealtimeProperty(58, true)] private Boolean _tomNode10;
        [RealtimeProperty(59, true)] private Boolean _tomNode11;
        [RealtimeProperty(60, true)] private Boolean _tomNode12;
        [RealtimeProperty(61, true)] private Boolean _tomNode13;
        [RealtimeProperty(62, true)] private Boolean _tomNode14;
        [RealtimeProperty(63, true)] private Boolean _tomNode15;
        [RealtimeProperty(64, true)] private Boolean _tomNode16;
        
        [RealtimeProperty(65, true)] private Boolean _cymbalNode1;
        [RealtimeProperty(66, true)] private Boolean _cymbalNode2;
        [RealtimeProperty(67, true)] private Boolean _cymbalNode3;
        [RealtimeProperty(68, true)] private Boolean _cymbalNode4;
        [RealtimeProperty(69, true)] private Boolean _cymbalNode5;
        [RealtimeProperty(70, true)] private Boolean _cymbalNode6;
        [RealtimeProperty(71, true)] private Boolean _cymbalNode7;
        [RealtimeProperty(72, true)] private Boolean _cymbalNode8;
        [RealtimeProperty(73, true)] private Boolean _cymbalNode9;
        [RealtimeProperty(74, true)] private Boolean _cymbalNode10;
        [RealtimeProperty(75, true)] private Boolean _cymbalNode11;
        [RealtimeProperty(76, true)] private Boolean _cymbalNode12;
        [RealtimeProperty(77, true)] private Boolean _cymbalNode13;
        [RealtimeProperty(78, true)] private Boolean _cymbalNode14;
        [RealtimeProperty(79, true)] private Boolean _cymbalNode15;
        [RealtimeProperty(80, true)] private Boolean _cymbalNode16;
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
        }
    }
    
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
                    _kickNode1 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode2: {
                    _kickNode2 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode3: {
                    _kickNode3 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode4: {
                    _kickNode4 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode5: {
                    _kickNode5 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode6: {
                    _kickNode6 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode7: {
                    _kickNode7 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode8: {
                    _kickNode8 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode9: {
                    _kickNode9 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode10: {
                    _kickNode10 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode11: {
                    _kickNode11 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode12: {
                    _kickNode12 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode13: {
                    _kickNode13 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode14: {
                    _kickNode14 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode15: {
                    _kickNode15 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.KickNode16: {
                    _kickNode16 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode1: {
                    _hiHatNode1 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode2: {
                    _hiHatNode2 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode3: {
                    _hiHatNode3 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode4: {
                    _hiHatNode4 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode5: {
                    _hiHatNode5 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode6: {
                    _hiHatNode6 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode7: {
                    _hiHatNode7 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode8: {
                    _hiHatNode8 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode9: {
                    _hiHatNode9 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode10: {
                    _hiHatNode10 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode11: {
                    _hiHatNode11 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode12: {
                    _hiHatNode12 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode13: {
                    _hiHatNode13 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode14: {
                    _hiHatNode14 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode15: {
                    _hiHatNode15 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.HiHatNode16: {
                    _hiHatNode16 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode1: {
                    _snareNode1 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode2: {
                    _snareNode2 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode3: {
                    _snareNode3 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode4: {
                    _snareNode4 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode5: {
                    _snareNode5 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode6: {
                    _snareNode6 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode7: {
                    _snareNode7 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode8: {
                    _snareNode8 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode9: {
                    _snareNode9 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode10: {
                    _snareNode10 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode11: {
                    _snareNode11 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode12: {
                    _snareNode12 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode13: {
                    _snareNode13 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode14: {
                    _snareNode14 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode15: {
                    _snareNode15 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.SnareNode16: {
                    _snareNode16 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode1: {
                    _tomNode1 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode2: {
                    _tomNode2 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode3: {
                    _tomNode3 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode4: {
                    _tomNode4 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode5: {
                    _tomNode5 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode6: {
                    _tomNode6 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode7: {
                    _tomNode7 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode8: {
                    _tomNode8 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode9: {
                    _tomNode9 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode10: {
                    _tomNode10 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode11: {
                    _tomNode11 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode12: {
                    _tomNode12 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode13: {
                    _tomNode13 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode14: {
                    _tomNode14 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode15: {
                    _tomNode15 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.TomNode16: {
                    _tomNode16 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode1: {
                    _cymbalNode1 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode2: {
                    _cymbalNode2 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode3: {
                    _cymbalNode3 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode4: {
                    _cymbalNode4 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode5: {
                    _cymbalNode5 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode6: {
                    _cymbalNode6 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode7: {
                    _cymbalNode7 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode8: {
                    _cymbalNode8 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode9: {
                    _cymbalNode9 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode10: {
                    _cymbalNode10 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode11: {
                    _cymbalNode11 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode12: {
                    _cymbalNode12 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode13: {
                    _cymbalNode13 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode14: {
                    _cymbalNode14 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode15: {
                    _cymbalNode15 = (stream.ReadVarint32() != 0);
                    break;
                }
                case (uint)PropertyID.CymbalNode16: {
                    _cymbalNode16 = (stream.ReadVarint32() != 0);
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
