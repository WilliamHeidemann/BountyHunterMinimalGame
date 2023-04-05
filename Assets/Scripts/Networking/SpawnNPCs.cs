using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnNPCs : NetworkBehaviour
{
    [SerializeField] private GameObject NPCprefab;
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        for (int i = 0; i < 3; i++)
        {
            var npc = Instantiate(NPCprefab);
            npc.GetComponent<NetworkObject>().Spawn();
        }
    }
}
