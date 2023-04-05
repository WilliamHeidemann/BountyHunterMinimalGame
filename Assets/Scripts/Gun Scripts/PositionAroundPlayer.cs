using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAroundPlayer : MonoBehaviour
{
    [SerializeField] private Transform owner;
    private bool _hasTarget;
    private Transform _target;
    private PotentialTargetTracker _potentialTargetTracker;
    
    private void Start()
    {
        _potentialTargetTracker = GetComponent<PotentialTargetTracker>();
        _potentialTargetTracker.OnPotentialTargetChanged += SetTarget;
    }

    private void SetTarget(GameObject previousTarget, GameObject newTarget)
    {
        if (newTarget == null)
        {
            _hasTarget = false;
            transform.position = owner.position;
        }
        else
        {
            _hasTarget = true;
            _target = newTarget.transform;
        }
    }

    private void Update()
    {
        print(_hasTarget);
        if (_hasTarget)
        {
            var circleVector = Vector3.Normalize(_target.position - owner.position);
            transform.position = circleVector + owner.position;
        }
    }
}
