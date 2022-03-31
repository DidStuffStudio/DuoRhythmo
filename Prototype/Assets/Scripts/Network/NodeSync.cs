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


    private void Start()
    {
        drumModel = model;
        nodeManagers = MasterManager.Instance._nodeManagers.ToArray();
        StartCoroutine(CheckForUpdates());
    }


    public void GetNodeFromServer()
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

     private IEnumerator CheckForUpdates()
     {
         while (true)
         {
             yield return new WaitForSeconds(updateDelta);
             print("Called");
             GetNodeFromServer();
         }
     }
     
}
