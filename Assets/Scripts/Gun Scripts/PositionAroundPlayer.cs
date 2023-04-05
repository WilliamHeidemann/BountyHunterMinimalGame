using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PositionAroundPlayer : NetworkBehaviour
{
    [SerializeField] private Transform owner;
    private Transform _target;
    private PotentialTargetTracker _potentialTargetTracker;
    
    private void Start()
    {
        _potentialTargetTracker = GetComponent<PotentialTargetTracker>();
        _potentialTargetTracker.OnPotentialTargetChanged += SetTarget;
    }

    private void SetTarget(GameObject previousTarget, GameObject newTarget)
    {
        if (newTarget)
        {
            _target = newTarget.transform;
        }
        else
        {
            transform.position = owner.position;
        }
    }

    private void Update()
    {
        if (_target)
        {
            var circleVector = Vector3.Normalize(_target.position - owner.position);
            transform.position = circleVector + owner.position;
        }
    }
}
