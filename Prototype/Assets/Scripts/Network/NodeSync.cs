using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class NodeSync : RealtimeComponent<NodeSyncModel>
{
    [SerializeField] private float updateDelta = 0.5f;
    private RealtimeModel drumModel;
    public NodeManager[] nodeManagers = new NodeManager[5];
    public bool startedJammin = false;


    private void Start() => drumModel = model;

    protected override void OnRealtimeModelReplaced(NodeSyncModel previousModel, NodeSyncModel currentModel)
    {
        if(!startedJammin) return;
        if (previousModel != null) {
            // Unregister from events
            previousModel.kickNode1DidChange -= KickNode1DidChange;
            previousModel.kickNode2DidChange -= KickNode2DidChange;
            previousModel.kickNode3DidChange -= KickNode3DidChange;
            previousModel.kickNode4DidChange -= KickNode4DidChange;
            previousModel.kickNode5DidChange -= KickNode5DidChange;
            previousModel.kickNode6DidChange -= KickNode6DidChange;
            previousModel.kickNode7DidChange -= KickNode7DidChange;
            previousModel.kickNode8DidChange -= KickNode8DidChange;
            previousModel.kickNode9DidChange -= KickNode9DidChange;
            previousModel.kickNode10DidChange -= KickNode10DidChange;
            previousModel.kickNode11DidChange -= KickNode11DidChange;
            previousModel.kickNode12DidChange -= KickNode12DidChange;
            previousModel.kickNode13DidChange -= KickNode13DidChange;
            previousModel.kickNode14DidChange -= KickNode14DidChange;
            previousModel.kickNode15DidChange -= KickNode15DidChange;
            previousModel.kickNode16DidChange -= KickNode16DidChange;
            
            
            previousModel.snareNode1DidChange -= SnareNode1DidChange;
            previousModel.snareNode2DidChange -= SnareNode2DidChange;
            previousModel.snareNode3DidChange -= SnareNode3DidChange;
            previousModel.snareNode4DidChange -= SnareNode4DidChange;
            previousModel.snareNode5DidChange -= SnareNode5DidChange;
            previousModel.snareNode6DidChange -= SnareNode6DidChange;
            previousModel.snareNode7DidChange -= SnareNode7DidChange;
            previousModel.snareNode8DidChange -= SnareNode8DidChange;
            previousModel.snareNode9DidChange -= SnareNode9DidChange;
            previousModel.snareNode10DidChange -= SnareNode10DidChange;
            previousModel.snareNode11DidChange -= SnareNode11DidChange;
            previousModel.snareNode12DidChange -= SnareNode12DidChange;
            previousModel.snareNode13DidChange -= SnareNode13DidChange;
            previousModel.snareNode14DidChange -= SnareNode14DidChange;
            previousModel.snareNode15DidChange -= SnareNode15DidChange;
            previousModel.snareNode16DidChange -= SnareNode16DidChange;
            
            previousModel.hiHatNode1DidChange -= HiHatNode1DidChange;
            previousModel.hiHatNode2DidChange -= HiHatNode2DidChange;
            previousModel.hiHatNode3DidChange -= HiHatNode3DidChange;
            previousModel.hiHatNode4DidChange -= HiHatNode4DidChange;
            previousModel.hiHatNode5DidChange -= HiHatNode5DidChange;
            previousModel.hiHatNode6DidChange -= HiHatNode6DidChange;
            previousModel.hiHatNode7DidChange -= HiHatNode7DidChange;
            previousModel.hiHatNode8DidChange -= HiHatNode8DidChange;
            previousModel.hiHatNode9DidChange -= HiHatNode9DidChange;
            previousModel.hiHatNode10DidChange -= HiHatNode10DidChange;
            previousModel.hiHatNode11DidChange -= HiHatNode11DidChange;
            previousModel.hiHatNode12DidChange -= HiHatNode12DidChange;
            previousModel.hiHatNode13DidChange -= HiHatNode13DidChange;
            previousModel.hiHatNode14DidChange -= HiHatNode14DidChange;
            previousModel.hiHatNode15DidChange -= HiHatNode15DidChange;
            previousModel.hiHatNode16DidChange -= HiHatNode16DidChange;
            
            previousModel.tomNode1DidChange -= TomNode1DidChange;
            previousModel.tomNode2DidChange -= TomNode2DidChange;
            previousModel.tomNode3DidChange -= TomNode3DidChange;
            previousModel.tomNode4DidChange -= TomNode4DidChange;
            previousModel.tomNode5DidChange -= TomNode5DidChange;
            previousModel.tomNode6DidChange -= TomNode6DidChange;
            previousModel.tomNode7DidChange -= TomNode7DidChange;
            previousModel.tomNode8DidChange -= TomNode8DidChange;
            previousModel.tomNode9DidChange -= TomNode9DidChange;
            previousModel.tomNode10DidChange -= TomNode10DidChange;
            previousModel.tomNode11DidChange -= TomNode11DidChange;
            previousModel.tomNode12DidChange -= TomNode12DidChange;
            previousModel.tomNode13DidChange -= TomNode13DidChange;
            previousModel.tomNode14DidChange -= TomNode14DidChange;
            previousModel.tomNode15DidChange -= TomNode15DidChange;
            previousModel.tomNode16DidChange -= TomNode16DidChange;
            
            previousModel.cymbalNode1DidChange -= CymbalNode1DidChange;
            previousModel.cymbalNode2DidChange -= CymbalNode2DidChange;
            previousModel.cymbalNode3DidChange -= CymbalNode3DidChange;
            previousModel.cymbalNode4DidChange -= CymbalNode4DidChange;
            previousModel.cymbalNode5DidChange -= CymbalNode5DidChange;
            previousModel.cymbalNode6DidChange -= CymbalNode6DidChange;
            previousModel.cymbalNode7DidChange -= CymbalNode7DidChange;
            previousModel.cymbalNode8DidChange -= CymbalNode8DidChange;
            previousModel.cymbalNode9DidChange -= CymbalNode9DidChange;
            previousModel.cymbalNode10DidChange -= CymbalNode10DidChange;
            previousModel.cymbalNode11DidChange -= CymbalNode11DidChange;
            previousModel.cymbalNode12DidChange -= CymbalNode12DidChange;
            previousModel.cymbalNode13DidChange -= CymbalNode13DidChange;
            previousModel.cymbalNode14DidChange -= CymbalNode14DidChange;
            previousModel.cymbalNode15DidChange -= CymbalNode15DidChange;
            previousModel.cymbalNode16DidChange -= CymbalNode16DidChange;
        }

        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
            {
                currentModel.kickNode1 = false;
                currentModel.kickNode2 = false;
                currentModel.kickNode3 = false;
                currentModel.kickNode4 = false;
                currentModel.kickNode5 = false;
                currentModel.kickNode6 = false;
                currentModel.kickNode7 = false;
                currentModel.kickNode8 = false;
                currentModel.kickNode9 = false;
                currentModel.kickNode10 = false;
                currentModel.kickNode11 = false;
                currentModel.kickNode12 = false;
                currentModel.kickNode13 = false;
                currentModel.kickNode14 = false;
                currentModel.kickNode15 = false;
                currentModel.kickNode16 = false;
                
                currentModel.snareNode1 = false;
                currentModel.snareNode2 = false;
                currentModel.snareNode3 = false;
                currentModel.snareNode4 = false;
                currentModel.snareNode5 = false;
                currentModel.snareNode6 = false;
                currentModel.snareNode7 = false;
                currentModel.snareNode8 = false;
                currentModel.snareNode9 = false;
                currentModel.snareNode10 = false;
                currentModel.snareNode11 = false;
                currentModel.snareNode12 = false;
                currentModel.snareNode13 = false;
                currentModel.snareNode14 = false;
                currentModel.snareNode15 = false;
                currentModel.snareNode16 = false;
                
                currentModel.hiHatNode1 = false;
                currentModel.hiHatNode2 = false;
                currentModel.hiHatNode3 = false;
                currentModel.hiHatNode4 = false;
                currentModel.hiHatNode5 = false;
                currentModel.hiHatNode6 = false;
                currentModel.hiHatNode7 = false;
                currentModel.hiHatNode8 = false;
                currentModel.hiHatNode9 = false;
                currentModel.hiHatNode10 = false;
                currentModel.hiHatNode11 = false;
                currentModel.hiHatNode12 = false;
                currentModel.hiHatNode13 = false;
                currentModel.hiHatNode14 = false;
                currentModel.hiHatNode15 = false;
                currentModel.hiHatNode16 = false;
                
                currentModel.tomNode1 = false;
                currentModel.tomNode2 = false;
                currentModel.tomNode3 = false;
                currentModel.tomNode4 = false;
                currentModel.tomNode5 = false;
                currentModel.tomNode6 = false;
                currentModel.tomNode7 = false;
                currentModel.tomNode8 = false;
                currentModel.tomNode9 = false;
                currentModel.tomNode10 = false;
                currentModel.tomNode11 = false;
                currentModel.tomNode12 = false;
                currentModel.tomNode13 = false;
                currentModel.tomNode14 = false;
                currentModel.tomNode15 = false;
                currentModel.tomNode16 = false;
                
                currentModel.cymbalNode1 = false;
                currentModel.cymbalNode2 = false;
                currentModel.cymbalNode3 = false;
                currentModel.cymbalNode4 = false;
                currentModel.cymbalNode5 = false;
                currentModel.cymbalNode6 = false;
                currentModel.cymbalNode7 = false;
                currentModel.cymbalNode8 = false;
                currentModel.cymbalNode9 = false;
                currentModel.cymbalNode10 = false;
                currentModel.cymbalNode11 = false;
                currentModel.cymbalNode12 = false;
                currentModel.cymbalNode13 = false;
                currentModel.cymbalNode14 = false;
                currentModel.cymbalNode15 = false;
                currentModel.cymbalNode16 = false;
                
                
            }

            // Update the mesh render to match the new model
            UpdateNodes();

            // Register for events so we'll know if the color changes later
            currentModel.kickNode1DidChange += KickNode1DidChange;
            currentModel.kickNode2DidChange += KickNode2DidChange;
            currentModel.kickNode3DidChange += KickNode3DidChange;
            currentModel.kickNode4DidChange += KickNode4DidChange;
            currentModel.kickNode5DidChange += KickNode5DidChange;
            currentModel.kickNode6DidChange += KickNode6DidChange;
            currentModel.kickNode7DidChange += KickNode7DidChange;
            currentModel.kickNode8DidChange += KickNode8DidChange;
            currentModel.kickNode9DidChange += KickNode9DidChange;
            currentModel.kickNode10DidChange += KickNode10DidChange;
            currentModel.kickNode11DidChange += KickNode11DidChange;
            currentModel.kickNode12DidChange += KickNode12DidChange;
            currentModel.kickNode13DidChange += KickNode13DidChange;
            currentModel.kickNode14DidChange += KickNode14DidChange;
            currentModel.kickNode15DidChange += KickNode15DidChange;
            currentModel.kickNode16DidChange += KickNode16DidChange;
            
            
            currentModel.snareNode1DidChange += SnareNode1DidChange;
            currentModel.snareNode2DidChange += SnareNode2DidChange;
            currentModel.snareNode3DidChange += SnareNode3DidChange;
            currentModel.snareNode4DidChange += SnareNode4DidChange;
            currentModel.snareNode5DidChange += SnareNode5DidChange;
            currentModel.snareNode6DidChange += SnareNode6DidChange;
            currentModel.snareNode7DidChange += SnareNode7DidChange;
            currentModel.snareNode8DidChange += SnareNode8DidChange;
            currentModel.snareNode9DidChange += SnareNode9DidChange;
            currentModel.snareNode10DidChange += SnareNode10DidChange;
            currentModel.snareNode11DidChange += SnareNode11DidChange;
            currentModel.snareNode12DidChange += SnareNode12DidChange;
            currentModel.snareNode13DidChange += SnareNode13DidChange;
            currentModel.snareNode14DidChange += SnareNode14DidChange;
            currentModel.snareNode15DidChange += SnareNode15DidChange;
            currentModel.snareNode16DidChange += SnareNode16DidChange;
            
            currentModel.hiHatNode1DidChange += HiHatNode1DidChange;
            currentModel.hiHatNode2DidChange += HiHatNode2DidChange;
            currentModel.hiHatNode3DidChange += HiHatNode3DidChange;
            currentModel.hiHatNode4DidChange += HiHatNode4DidChange;
            currentModel.hiHatNode5DidChange += HiHatNode5DidChange;
            currentModel.hiHatNode6DidChange += HiHatNode6DidChange;
            currentModel.hiHatNode7DidChange += HiHatNode7DidChange;
            currentModel.hiHatNode8DidChange += HiHatNode8DidChange;
            currentModel.hiHatNode9DidChange += HiHatNode9DidChange;
            currentModel.hiHatNode10DidChange += HiHatNode10DidChange;
            currentModel.hiHatNode11DidChange += HiHatNode11DidChange;
            currentModel.hiHatNode12DidChange += HiHatNode12DidChange;
            currentModel.hiHatNode13DidChange += HiHatNode13DidChange;
            currentModel.hiHatNode14DidChange += HiHatNode14DidChange;
            currentModel.hiHatNode15DidChange += HiHatNode15DidChange;
            currentModel.hiHatNode16DidChange += HiHatNode16DidChange;
            
            currentModel.tomNode1DidChange += TomNode1DidChange;
            currentModel.tomNode2DidChange += TomNode2DidChange;
            currentModel.tomNode3DidChange += TomNode3DidChange;
            currentModel.tomNode4DidChange += TomNode4DidChange;
            currentModel.tomNode5DidChange += TomNode5DidChange;
            currentModel.tomNode6DidChange += TomNode6DidChange;
            currentModel.tomNode7DidChange += TomNode7DidChange;
            currentModel.tomNode8DidChange += TomNode8DidChange;
            currentModel.tomNode9DidChange += TomNode9DidChange;
            currentModel.tomNode10DidChange += TomNode10DidChange;
            currentModel.tomNode11DidChange += TomNode11DidChange;
            currentModel.tomNode12DidChange += TomNode12DidChange;
            currentModel.tomNode13DidChange += TomNode13DidChange;
            currentModel.tomNode14DidChange += TomNode14DidChange;
            currentModel.tomNode15DidChange += TomNode15DidChange;
            currentModel.tomNode16DidChange += TomNode16DidChange;
            
            currentModel.cymbalNode1DidChange += CymbalNode1DidChange;
            currentModel.cymbalNode2DidChange += CymbalNode2DidChange;
            currentModel.cymbalNode3DidChange += CymbalNode3DidChange;
            currentModel.cymbalNode4DidChange += CymbalNode4DidChange;
            currentModel.cymbalNode5DidChange += CymbalNode5DidChange;
            currentModel.cymbalNode6DidChange += CymbalNode6DidChange;
            currentModel.cymbalNode7DidChange += CymbalNode7DidChange;
            currentModel.cymbalNode8DidChange += CymbalNode8DidChange;
            currentModel.cymbalNode9DidChange += CymbalNode9DidChange;
            currentModel.cymbalNode10DidChange += CymbalNode10DidChange;
            currentModel.cymbalNode11DidChange += CymbalNode11DidChange;
            currentModel.cymbalNode12DidChange += CymbalNode12DidChange;
            currentModel.cymbalNode13DidChange += CymbalNode13DidChange;
            currentModel.cymbalNode14DidChange += CymbalNode14DidChange;
            currentModel.cymbalNode15DidChange += CymbalNode15DidChange;
            currentModel.cymbalNode16DidChange += CymbalNode16DidChange;
        }
    }

    private void KickNode1DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(1, value);
    private void KickNode2DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(2, value);
    private void KickNode3DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(3, value);
    private void KickNode4DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(4, value);
    private void KickNode5DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(5, value);
    private void KickNode6DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(6, value);
    private void KickNode7DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(7, value);
    private void KickNode8DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(8, value);
    private void KickNode9DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(9, value);
    private void KickNode10DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(10, value);
    private void KickNode11DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(11, value);
    private void KickNode12DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(12, value);
    private void KickNode13DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(13, value);
    private void KickNode14DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(14, value);
    private void KickNode15DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(15, value);
    private void KickNode16DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[0].SetNodeFromServer(16, value);
    
    
    
    
    private void SnareNode1DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(1, value);
    private void SnareNode2DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(2, value);
    private void SnareNode3DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(3, value);
    private void SnareNode4DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(4, value);
    private void SnareNode5DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(5, value);
    private void SnareNode6DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(6, value);
    private void SnareNode7DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(7, value);
    private void SnareNode8DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(8, value);
    private void SnareNode9DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(9, value);
    private void SnareNode10DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(10, value);
    private void SnareNode11DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(11, value);
    private void SnareNode12DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(12, value);
    private void SnareNode13DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(13, value);
    private void SnareNode14DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(14, value);
    private void SnareNode15DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(15, value);
    private void SnareNode16DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[1].SetNodeFromServer(16, value);
    
    
    
    private void HiHatNode1DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(1, value);
    private void HiHatNode2DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(2, value);
    private void HiHatNode3DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(3, value);
    private void HiHatNode4DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(4, value);
    private void HiHatNode5DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(5, value);
    private void HiHatNode6DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(6, value);
    private void HiHatNode7DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(7, value);
    private void HiHatNode8DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(8, value);
    private void HiHatNode9DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(9, value);
    private void HiHatNode10DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(10, value);
    private void HiHatNode11DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(11, value);
    private void HiHatNode12DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(12, value);
    private void HiHatNode13DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(13, value);
    private void HiHatNode14DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(14, value);
    private void HiHatNode15DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(15, value);
    private void HiHatNode16DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[2].SetNodeFromServer(16, value);
    
    
    
    
    private void TomNode1DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(1, value);
    private void TomNode2DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(2, value);
    private void TomNode3DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(3, value);
    private void TomNode4DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(4, value);
    private void TomNode5DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(5, value);
    private void TomNode6DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(6, value);
    private void TomNode7DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(7, value);
    private void TomNode8DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(8, value);
    private void TomNode9DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(9, value);
    private void TomNode10DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(10, value);
    private void TomNode11DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(11, value);
    private void TomNode12DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(12, value);
    private void TomNode13DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(13, value);
    private void TomNode14DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(14, value);
    private void TomNode15DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(15, value);
    private void TomNode16DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[3].SetNodeFromServer(16, value);
    
    
    
    private void CymbalNode1DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(1, value);
    private void CymbalNode2DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(2, value);
    private void CymbalNode3DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(3, value);
    private void CymbalNode4DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(4, value);
    private void CymbalNode5DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(5, value);
    private void CymbalNode6DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(6, value);
    private void CymbalNode7DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(7, value);
    private void CymbalNode8DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(8, value);
    private void CymbalNode9DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(9, value);
    private void CymbalNode10DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(10, value);
    private void CymbalNode11DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(11, value);
    private void CymbalNode12DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(12, value);
    private void CymbalNode13DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(13, value);
    private void CymbalNode14DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(14, value);
    private void CymbalNode15DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(15, value);
    private void CymbalNode16DidChange(NodeSyncModel nodeSyncModel, bool value) => nodeManagers[4].SetNodeFromServer(16, value);


    public void UpdateNodes()
    {

        nodeManagers[0].SetNodeFromServer(0, model.kickNode1);
        nodeManagers[0].SetNodeFromServer(1, model.kickNode2);
        nodeManagers[0].SetNodeFromServer(2, model.kickNode3);
        nodeManagers[0].SetNodeFromServer(3, model.kickNode4);
        nodeManagers[0].SetNodeFromServer(4, model.kickNode5);
        nodeManagers[0].SetNodeFromServer(5, model.kickNode6);
        nodeManagers[0].SetNodeFromServer(6, model.kickNode7);
        nodeManagers[0].SetNodeFromServer(7, model.kickNode8);
        nodeManagers[0].SetNodeFromServer(8, model.kickNode9);
        nodeManagers[0].SetNodeFromServer(9, model.kickNode10);
        nodeManagers[0].SetNodeFromServer(10, model.kickNode11);
        nodeManagers[0].SetNodeFromServer(11, model.kickNode12);
        nodeManagers[0].SetNodeFromServer(12, model.kickNode13);
        nodeManagers[0].SetNodeFromServer(13, model.kickNode14);
        nodeManagers[0].SetNodeFromServer(14, model.kickNode15);
        nodeManagers[0].SetNodeFromServer(15, model.kickNode16);
        
        nodeManagers[1].SetNodeFromServer(0, model.snareNode1);
        nodeManagers[1].SetNodeFromServer(1, model.snareNode2);
        nodeManagers[1].SetNodeFromServer(2, model.snareNode3);
        nodeManagers[1].SetNodeFromServer(3, model.snareNode4);
        nodeManagers[1].SetNodeFromServer(4, model.snareNode5);
        nodeManagers[1].SetNodeFromServer(5, model.snareNode6);
        nodeManagers[1].SetNodeFromServer(6, model.snareNode7);
        nodeManagers[1].SetNodeFromServer(7, model.snareNode8);
        nodeManagers[1].SetNodeFromServer(8, model.snareNode9);
        nodeManagers[1].SetNodeFromServer(9, model.snareNode10);
        nodeManagers[1].SetNodeFromServer(10, model.snareNode11);
        nodeManagers[1].SetNodeFromServer(11, model.snareNode12);
        nodeManagers[1].SetNodeFromServer(12, model.snareNode13);
        nodeManagers[1].SetNodeFromServer(13, model.snareNode14);
        nodeManagers[1].SetNodeFromServer(14, model.snareNode15);
        nodeManagers[1].SetNodeFromServer(15, model.snareNode16);
        
        nodeManagers[2].SetNodeFromServer(0, model.hiHatNode1);
        nodeManagers[2].SetNodeFromServer(1, model.hiHatNode2);
        nodeManagers[2].SetNodeFromServer(2, model.hiHatNode3);
        nodeManagers[2].SetNodeFromServer(3, model.hiHatNode4);
        nodeManagers[2].SetNodeFromServer(4, model.hiHatNode5);
        nodeManagers[2].SetNodeFromServer(5, model.hiHatNode6);
        nodeManagers[2].SetNodeFromServer(6, model.hiHatNode7);
        nodeManagers[2].SetNodeFromServer(7, model.hiHatNode8);
        nodeManagers[2].SetNodeFromServer(8, model.hiHatNode9);
        nodeManagers[2].SetNodeFromServer(9, model.hiHatNode10);
        nodeManagers[2].SetNodeFromServer(10, model.hiHatNode11);
        nodeManagers[2].SetNodeFromServer(11, model.hiHatNode12);
        nodeManagers[2].SetNodeFromServer(12, model.hiHatNode13);
        nodeManagers[2].SetNodeFromServer(13, model.hiHatNode14);
        nodeManagers[2].SetNodeFromServer(14, model.hiHatNode15);
        nodeManagers[2].SetNodeFromServer(15, model.hiHatNode16);
        
        nodeManagers[3].SetNodeFromServer(0, model.tomNode1);
        nodeManagers[3].SetNodeFromServer(1, model.tomNode2);
        nodeManagers[3].SetNodeFromServer(2, model.tomNode3);
        nodeManagers[3].SetNodeFromServer(3, model.tomNode4);
        nodeManagers[3].SetNodeFromServer(4, model.tomNode5);
        nodeManagers[3].SetNodeFromServer(5, model.tomNode6);
        nodeManagers[3].SetNodeFromServer(6, model.tomNode7);
        nodeManagers[3].SetNodeFromServer(7, model.tomNode8);
        nodeManagers[3].SetNodeFromServer(8, model.tomNode9);
        nodeManagers[3].SetNodeFromServer(9, model.tomNode10);
        nodeManagers[3].SetNodeFromServer(10, model.tomNode11);
        nodeManagers[3].SetNodeFromServer(11, model.tomNode12);
        nodeManagers[3].SetNodeFromServer(12, model.tomNode13);
        nodeManagers[3].SetNodeFromServer(13, model.tomNode14);
        nodeManagers[3].SetNodeFromServer(14, model.tomNode15);
        nodeManagers[3].SetNodeFromServer(15, model.tomNode16);
        
        nodeManagers[4].SetNodeFromServer(0, model.cymbalNode1);
        nodeManagers[4].SetNodeFromServer(1, model.cymbalNode2);
        nodeManagers[4].SetNodeFromServer(2, model.cymbalNode3);
        nodeManagers[4].SetNodeFromServer(3, model.cymbalNode4);
        nodeManagers[4].SetNodeFromServer(4, model.cymbalNode5);
        nodeManagers[4].SetNodeFromServer(5, model.cymbalNode6);
        nodeManagers[4].SetNodeFromServer(6, model.cymbalNode7);
        nodeManagers[4].SetNodeFromServer(7, model.cymbalNode8);
        nodeManagers[4].SetNodeFromServer(8, model.cymbalNode9);
        nodeManagers[4].SetNodeFromServer(9, model.cymbalNode10);
        nodeManagers[4].SetNodeFromServer(10, model.cymbalNode11);
        nodeManagers[4].SetNodeFromServer(11, model.cymbalNode12);
        nodeManagers[4].SetNodeFromServer(12, model.cymbalNode13);
        nodeManagers[4].SetNodeFromServer(13, model.cymbalNode14);
        nodeManagers[4].SetNodeFromServer(14, model.cymbalNode15);
        nodeManagers[4].SetNodeFromServer(15, model.cymbalNode16);
      
        
    }
    
     public void SetNodeOnServer(DrumType drumType, int index, bool activate)
     {
         print("Setting node on server");
        GetComponent<RealtimeView>().RequestOwnership();
        switch (drumType)
        {
            case DrumType.Kick:
                if (index == 0) model.kickNode1 = activate;
                else if (index == 1) model.kickNode2 = activate;
                else if (index == 2) model.kickNode3 = activate;
                else if (index == 3) model.kickNode4 = activate;
                else if (index == 4) model.kickNode5 = activate;
                else if (index == 5) model.kickNode6 = activate;
                else if (index == 6) model.kickNode7 = activate;
                else if (index == 7) model.kickNode8 = activate;
                else if (index == 8) model.kickNode9 = activate;
                else if (index == 9) model.kickNode10 = activate;
                else if (index == 10) model.kickNode11 = activate;
                else if (index == 11) model.kickNode12 = activate;
                else if (index == 12) model.kickNode13 = activate;
                else if (index == 13) model.kickNode14 = activate;
                else if (index == 14) model.kickNode15 = activate;
                else if (index == 15) model.kickNode16 = activate;
                break;
            
            case DrumType.Snare:
                if (index == 0) model.snareNode1 = activate;
                else if (index == 1) model.snareNode2 = activate;
                else if (index == 2) model.snareNode3 = activate;
                else if (index == 3) model.snareNode4 = activate;
                else if (index == 4) model.snareNode5 = activate;
                else if (index == 5) model.snareNode6 = activate;
                else if (index == 6) model.snareNode7 = activate;
                else if (index == 7) model.snareNode8 = activate;
                else if (index == 8) model.snareNode9 = activate;
                else if (index == 9) model.snareNode10 = activate;
                else if (index == 10) model.snareNode11 = activate;
                else if (index == 11) model.snareNode12 = activate;
                else if (index == 12) model.snareNode13 = activate;
                else if (index == 13) model.snareNode14 = activate;
                else if (index == 14) model.snareNode15 = activate;
                else if (index == 15) model.snareNode16 = activate;
                break;
            
            case DrumType.HiHat:
                if (index == 0) model.hiHatNode1 = activate;
                else if (index == 1) model.hiHatNode2 = activate;
                else if (index == 2) model.hiHatNode3 = activate;
                else if (index == 3) model.hiHatNode4 = activate;
                else if (index == 4) model.hiHatNode5 = activate;
                else if (index == 5) model.hiHatNode6 = activate;
                else if (index == 6) model.hiHatNode7 = activate;
                else if (index == 7) model.hiHatNode8 = activate;
                else if (index == 8) model.hiHatNode9 = activate;
                else if (index == 9) model.hiHatNode10 = activate;
                else if (index == 10) model.hiHatNode11 = activate;
                else if (index == 11) model.hiHatNode12 = activate;
                else if (index == 12) model.hiHatNode13 = activate;
                else if (index == 13) model.hiHatNode14 = activate;
                else if (index == 14) model.hiHatNode15 = activate;
                else if (index == 15) model.hiHatNode16 = activate;
                break;
            
            case DrumType.Tom:
                if (index == 0) model.tomNode1 = activate;
                else if (index == 1) model.tomNode2 = activate;
                else if (index == 2) model.tomNode3 = activate;
                else if (index == 3) model.tomNode4 = activate;
                else if (index == 4) model.tomNode5 = activate;
                else if (index == 5) model.tomNode6 = activate;
                else if (index == 6) model.tomNode7 = activate;
                else if (index == 7) model.tomNode8 = activate;
                else if (index == 8) model.tomNode9 = activate;
                else if (index == 9) model.tomNode10 = activate;
                else if (index == 10) model.tomNode11 = activate;
                else if (index == 11) model.tomNode12 = activate;
                else if (index == 12) model.tomNode13 = activate;
                else if (index == 13) model.tomNode14 = activate;
                else if (index == 14) model.tomNode15 = activate;
                else if (index == 15) model.tomNode16 = activate;
                break;
            
            case DrumType.Cymbal:
                if (index == 0) model.cymbalNode1 = activate;
                else if (index == 1) model.cymbalNode2 = activate;
                else if (index == 2) model.cymbalNode3 = activate;
                else if (index == 3) model.cymbalNode4 = activate;
                else if (index == 4) model.cymbalNode5 = activate;
                else if (index == 5) model.cymbalNode6 = activate;
                else if (index == 6) model.cymbalNode7 = activate;
                else if (index == 7) model.cymbalNode8 = activate;
                else if (index == 8) model.cymbalNode9 = activate;
                else if (index == 9) model.cymbalNode10 = activate;
                else if (index == 10) model.cymbalNode11 = activate;
                else if (index == 11) model.cymbalNode12 = activate;
                else if (index == 12) model.cymbalNode13 = activate;
                else if (index == 13) model.cymbalNode14 = activate;
                else if (index == 14) model.cymbalNode15 = activate;
                else if (index == 15) model.cymbalNode16 = activate;
                break;
        }
        
    }
}
