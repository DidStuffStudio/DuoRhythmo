using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Managers;
using UnityEngine;

public class DidStuffSolo : MonoBehaviour
{

    private NodeManager _nodeManager;

    private void Start()
    {
        _nodeManager = GetComponentInParent<NodeManager>();
    }


    public void Mute(int drumType)
    {
        MasterManager.Instance.audioManager.mixerGroups[_nodeManager.drumType];
    }

}
