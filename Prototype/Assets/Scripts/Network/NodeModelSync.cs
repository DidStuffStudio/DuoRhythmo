using Normal.Realtime;

namespace Network {
    public class NodeModelSync : RealtimeComponent<NodeModel> {
        /*
        private bool _isActive = false;

        protected override void OnRealtimeModelReplaced(NodeModel previousModel, NodeModel currentModel) {
            if (previousModel != null) {
                previousModel.activeDidChange -= ActiveDidChange;
            }

            if (currentModel != null) {
                if (currentModel.isFreshModel) {
                    currentModel.active = false;
                }

                currentModel.activeDidChange += ActiveDidChange;
            }
        }

        private void ActiveDidChange(NodeModel nodeModel, bool value) => _isActive = value;
        public void SetNode(bool value) => model.active = value;

        private void UpdateNode() => _isActive = model.active;
        */
    }
}