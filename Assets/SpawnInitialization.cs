using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnInitialization : NetworkBehaviour
{
    public delegate void OnPlayerSpawnDelegate(GameObject player);
    public static OnPlayerSpawnDelegate OnPlayerSpawn;

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) Destroy(this);
        OnPlayerSpawn?.Invoke(gameObject);
    }
}
