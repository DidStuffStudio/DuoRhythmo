using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Managers;
using UnityEngine;

public class DidStuffSolo : MonoBehaviour
{

    private NodeManager _nodeManager;
    public int drumTypeIndex = 0;

    private void Start()
    {
        _nodeManager = GetComponentInParent<NodeManager>();
    }


    public void Mute()
    {
        MasterManager.Instance.audioManager.MuteOthers(drumTypeIndex);
    }

}
