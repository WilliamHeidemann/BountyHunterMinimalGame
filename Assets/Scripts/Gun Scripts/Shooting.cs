using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shooting : NetworkBehaviour
{
    // When the aimTarget is set (current) invoke aimStarted (shooter, target)
    // When the aim is cancelled invoke aimCancelled (shooter, target)
    // When the aim is completed invoke aimCompleted (shooter, target)

    private PotentialTargetTracker _potentialTargetTracker;
    private AimTargetTracker _aimTargetTracker;
    private Coroutine _aimCoroutine;
    private NetworkDespawner _networkDespawner;
    // field to store information about gun aim time
    public delegate void OnKillDelegate(GameObject killer, GameObject target);
    public event OnKillDelegate OnKill;
    
    private void Start()
    {
        _potentialTargetTracker = GetComponent<PotentialTargetTracker>();
        _aimTargetTracker = GetComponent<AimTargetTracker>();
        _aimTargetTracker.OnAimTargetSetDelegate += CheckAimTarget;
        _networkDespawner = FindObjectOfType<NetworkDespawner>();

        OnKill += ResetTrackers;
    }

    private void CheckAimTarget(GameObject previousTarget, GameObject newTarget)
    {
        if (newTarget == null)
        {
            CancelAim(previousTarget);
        }
        else
        {
            StartAim(newTarget);
        }
    }

    private void CancelAim(GameObject previousTarget)
    {
        if (_aimCoroutine != null) StopCoroutine(_aimCoroutine);
    }

    private void StartAim(GameObject target)
    {
        _aimCoroutine = StartCoroutine(AimCountdown(target));
    }

    private IEnumerator AimCountdown(GameObject target)
    {
        yield return new WaitForSeconds(1); // Replace with gun aim time
        OnKill?.Invoke(gameObject, target);

        // Network Despawning
        var targetNetworkObjectId = target.gameObject.GetComponent<NetworkObject>().NetworkObjectId;
        _networkDespawner.DeSpawnServerRpc(targetNetworkObjectId);
    }

    private void ResetTrackers(GameObject killer, GameObject target)
    {
        _aimTargetTracker.SetAimTarget(null);
        _potentialTargetTracker.SetPotentialTarget(null);
    }
}
