using Mirror;

namespace DidStuffLab {
    // CHRISTIAN NOTE --> I'm trying to generalize a class for syncing networking behaviour - common network functions for DuoRhythmo.

    /// <summary>
    /// [Developed for DuoRhythmo]
    /// Contains custom methods for the common functions for syncing variables of type "T" with Mirror Networking.
    /// </summary>
    /// <typeparam name="T">Generic type T - tested with bool (for node active/inactive) and float (effects) </typeparam>
    public abstract class CustomSyncBehaviour<T> : NetworkBehaviour where T : new() {
        protected readonly SyncVar<T> Value = new SyncVar<T>(new T());
        private event System.Action<T> OnValueChanged;
        private void ValueChanged(T _, T newValue) => OnValueChanged?.Invoke(newValue);

        public override void OnStartClient() {
            base.OnStartClient();

            // initialize gameobjects (getcomponents...)
            Initialize();

            // make sure that the value will update when it changes
            Value.Callback += ValueChanged;

            // wire up all events to node handlers
            OnValueChanged = UpdateValueLocally;

            // invoke all event handlers with the initial data from spawn payload
            OnValueChanged.Invoke(Value);
        }

        public override void OnStartLocalPlayer() {
            base.OnStartLocalPlayer();
            // enable input for the users (they can start activating interacting with DuoRhythmo)
            gameObject.SetActive(true);
        }

        public override void OnStopClient() {
            base.OnStopClient();
            // disable value changed callback when the client has disconnected 
            Value.Callback -= ValueChanged;
            // disconnect event handlers
            OnValueChanged = null;
        }

        /// <summary>
        /// If value has been changed locally, update it by sending the new value to server.
        /// </summary>
        /// <param name="v">New Value to send to the server.</param>
        [Client]
        protected void SendToServer(T v) => CmdUpdateValue(v);

        /// <summary>
        /// Update the value locally - called whenever there is a change from on the value from the server.
        /// </summary>
        /// <param name="newValue"></param>
        protected virtual void UpdateValueLocally(T newValue) => Value.Value = newValue;

        /// <summary>
        /// Initialize/setup gameobjects (Use instead of Start method)
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Method to implement for sending a command from a client to a server --> to update the value
        /// Use [Command(requiresAuthority = false)] as the attribute
        /// (we don't require network identity authority for the nodes nor effect values)
        /// </summary>
        /// <param name="newValue"></param>
        protected abstract void CmdUpdateValue(T newValue);
    }
}