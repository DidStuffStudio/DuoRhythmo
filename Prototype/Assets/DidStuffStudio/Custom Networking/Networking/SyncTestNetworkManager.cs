using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SyncTestNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
        base.OnServerAddPlayer(conn);
    }
}
