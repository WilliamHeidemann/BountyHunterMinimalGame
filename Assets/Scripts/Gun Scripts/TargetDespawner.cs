using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TargetDespawner : NetworkBehaviour
{
    // When the target is killed, call a serverRpc that despawns the target
    [ServerRpc(RequireOwnership = false)]
    public void DeSpawnServerRpc(ulong networkObjectId)
    {
        NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId].Despawn();
    }
}
