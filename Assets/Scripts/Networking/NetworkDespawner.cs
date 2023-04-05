using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkDespawner : NetworkBehaviour
{
    [ServerRpc(RequireOwnership = false)]
    public void DeSpawnServerRpc(ulong networkObjectId)
    {
        NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId].Despawn();
    }
}
