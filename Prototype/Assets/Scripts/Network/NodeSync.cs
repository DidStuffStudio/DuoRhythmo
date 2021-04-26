using Normal.Realtime;
using UnityEngine;

public class NodeSync : RealtimeComponent<NodeModel> {
    private bool _isActivated;
    private Node _node;
    public bool IsActivated => _isActivated;

    private void Start() {
        _node = GetComponent<Node>();
    }

    protected override void OnRealtimeModelReplaced(NodeModel previousModel, NodeModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.nodeActivatedDidChange -= ActivationDidChange;
        }

        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel) {
                currentModel.nodeActivated = _isActivated;
            }

            // Update the mesh render to match the new model
            UpdateNode();

            // Register for events so we'll know if the color changes later
            currentModel.nodeActivatedDidChange += ActivationDidChange;
        }
    }

    private void UpdateNode() => _isActivated = model.nodeActivated;

    private void ActivationDidChange(NodeModel model, bool value) {
        _isActivated = model.nodeActivated;
        _node.Activate(value);
    }

    public void SetActive(bool value) {
        if (RealTimeInstance.Instance.isSoloMode) return;
        print("Want to set it to " + value);
        model.nodeActivated = value;
    }
}