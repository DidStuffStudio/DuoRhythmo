using System.Collections;
using System.Collections.Generic;
using Network;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine;

public class NodeDictionarySync : RealtimeComponent<NodeDictionarySyncModel> {
    public NodeManager[] nodeManagers = new NodeManager[5];

    protected override void OnRealtimeModelReplaced(NodeDictionarySyncModel previousModel,
        NodeDictionarySyncModel currentModel) {
        if (previousModel != null) {
            previousModel.kickNodeDictionary.modelAdded -= KickNodeAddedOrRemoved;
            previousModel.kickNodeDictionary.modelRemoved -= KickNodeAddedOrRemoved;
            previousModel.kickNodeDictionary.modelReplaced -= KickNodeDidChange;

            previousModel.snareNodeDictionary.modelAdded -= SnareNodeAddedOrRemoved;
            previousModel.snareNodeDictionary.modelRemoved -= SnareNodeAddedOrRemoved;
            previousModel.snareNodeDictionary.modelReplaced -= SnareNodeDidChange;

            previousModel.hihatNodeDictionary.modelAdded -= HiHatNodeAddedOrRemoved;
            previousModel.hihatNodeDictionary.modelRemoved -= HiHatNodeAddedOrRemoved;
            previousModel.hihatNodeDictionary.modelReplaced -= HiHatNodeDidChange;

            previousModel.tomNodeDictionary.modelAdded -= TomNodeAddedOrRemoved;
            previousModel.tomNodeDictionary.modelRemoved -= TomNodeAddedOrRemoved;
            previousModel.tomNodeDictionary.modelReplaced -= TomNodeDidChange;

            previousModel.cymbalNodeDictionary.modelAdded -= CymbalNodeAddedOrRemoved;
            previousModel.cymbalNodeDictionary.modelRemoved -= CymbalNodeAddedOrRemoved;
            previousModel.cymbalNodeDictionary.modelReplaced -= CymbalNodeDidChange;
        }

        if (currentModel != null) {
            if (currentModel.isFreshModel) { }

            UpdateNodes();

            currentModel.kickNodeDictionary.modelAdded += KickNodeAddedOrRemoved;
            currentModel.kickNodeDictionary.modelRemoved += KickNodeAddedOrRemoved;
            currentModel.kickNodeDictionary.modelReplaced += KickNodeDidChange;

            currentModel.snareNodeDictionary.modelAdded += SnareNodeAddedOrRemoved;
            currentModel.snareNodeDictionary.modelRemoved += SnareNodeAddedOrRemoved;
            currentModel.snareNodeDictionary.modelReplaced += SnareNodeDidChange;

            currentModel.hihatNodeDictionary.modelAdded += HiHatNodeAddedOrRemoved;
            currentModel.hihatNodeDictionary.modelRemoved += HiHatNodeAddedOrRemoved;
            currentModel.hihatNodeDictionary.modelReplaced += HiHatNodeDidChange;

            currentModel.tomNodeDictionary.modelAdded += TomNodeAddedOrRemoved;
            currentModel.tomNodeDictionary.modelRemoved += TomNodeAddedOrRemoved;
            currentModel.tomNodeDictionary.modelReplaced += TomNodeDidChange;

            currentModel.cymbalNodeDictionary.modelAdded += CymbalNodeAddedOrRemoved;
            currentModel.cymbalNodeDictionary.modelRemoved += CymbalNodeAddedOrRemoved;
            currentModel.cymbalNodeDictionary.modelReplaced += CymbalNodeDidChange;
        }
    }

    private void CymbalNodeDidChange(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel oldmodel,
        NodeModel newmodel, bool remote) {
        nodeManagers[4].SetNodeFromServer((int) key, remote);
    }

    private void CymbalNodeAddedOrRemoved(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel nodeModel,
        bool remote) {
        nodeManagers[4].SetNodeFromServer((int) key, remote);
    }

    private void TomNodeDidChange(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel oldmodel,
        NodeModel newmodel, bool remote) {
        nodeManagers[3].SetNodeFromServer((int) key, remote);
    }

    private void TomNodeAddedOrRemoved(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel nodeModel,
        bool remote) {
        nodeManagers[3].SetNodeFromServer((int) key, remote);
    }

    private void HiHatNodeDidChange(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel oldmodel,
        NodeModel newmodel, bool remote) {
        nodeManagers[2].SetNodeFromServer((int) key, remote);
    }

    private void HiHatNodeAddedOrRemoved(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel nodeModel,
        bool remote) {
        nodeManagers[2].SetNodeFromServer((int) key, remote);
    }

    private void SnareNodeDidChange(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel oldmodel,
        NodeModel newmodel, bool remote) {
        nodeManagers[1].SetNodeFromServer((int) key, remote);
    }

    private void SnareNodeAddedOrRemoved(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel nodeModel,
        bool remote) {
        nodeManagers[1].SetNodeFromServer((int) key, remote);
    }

    private void KickNodeDidChange(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel oldmodel,
        NodeModel newmodel, bool remote) {
        nodeManagers[0].SetNodeFromServer((int) key, remote);
    }

    private void KickNodeAddedOrRemoved(RealtimeDictionary<NodeModel> dictionary, uint key, NodeModel nodeModel,
        bool remote) {
        print("Cymbal dictionary has changed: Node" + key + " is " + remote);
        nodeManagers[0].SetNodeFromServer((int) key, remote);
    }

    private void UpdateNodes() {
        for (int i = 0; i < nodeManagers.Length; i++) {
            for (int j = 0; j < 16; j++) {
                nodeManagers[i].SetNodeFromServer(j, model.kickNodeDictionary[(uint) j].active);
            }
        }
    }

    public void SetNodeOnServer(DrumType drumType, int index, bool activate) {
        switch (drumType) {
            case DrumType.Kick:
                model.kickNodeDictionary[(uint) index].active = activate;
                break;
            case DrumType.Snare:
                model.snareNodeDictionary[(uint) index].active = activate;
                break;
            case DrumType.HiHat:
                model.hihatNodeDictionary[(uint) index].active = activate;
                break;
            case DrumType.Tom:
                model.tomNodeDictionary[(uint) index].active = activate;
                break;
            case DrumType.Cymbal:
                model.cymbalNodeDictionary[(uint) index].active = activate;
                break;
        }
    }
}