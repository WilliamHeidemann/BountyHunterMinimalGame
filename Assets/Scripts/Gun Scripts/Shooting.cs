using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // When the aimTarget is set (current) invoke aimStarted (shooter, target)
    // When the aim is cancelled invoke aimCancelled (shooter, target)
    // When the aim is completed invoke aimCompleted (shooter, target)

    private PotentialTargetTracker _potentialTargetTracker;
    private AimTargetTracker _aimTargetTracker;
    private Coroutine _aimCoroutine;
    private TargetDespawner _targetDeSpawner;
    // field to store information about gun aim time
    
    private void Start()
    {
        _potentialTargetTracker = GetComponent<PotentialTargetTracker>();
        _aimTargetTracker = GetComponent<AimTargetTracker>();
        _aimTargetTracker.OnAimTargetSetDelegate += CheckAimTarget;
        _targetDeSpawner = GetComponent<TargetDespawner>();
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
        _aimTargetTracker.SetAimTarget(null);
        _potentialTargetTracker.SetPotentialTarget(null);
        var targetNetworkObjectId = target.gameObject.GetComponent<NetworkObject>().NetworkObjectId;
        _targetDeSpawner.DeSpawnServerRpc(targetNetworkObjectId);
    }
}