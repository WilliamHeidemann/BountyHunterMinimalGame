using System;
using UnityEngine;

public class AimTargetTracker : MonoBehaviour
{
    public GameObject AimTarget { get; private set; }
    public event PotentialTargetTracker.OnTargetChangedDelegate OnAimTargetSetDelegate;
    private PotentialTargetTracker _potentialTargetTracker;

    private void Start()
    {
        _potentialTargetTracker = GetComponent<PotentialTargetTracker>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && HasPotentialTarget())
        {
            SetAimTarget(_potentialTargetTracker.PotentialTarget);
        } 
        
        if (Input.GetMouseButtonUp(0))
        {
            SetAimTarget(null);
        }
    }

    public void SetAimTarget(GameObject newTarget)
    {
        var previousTarget = AimTarget;
        AimTarget = newTarget;
        OnAimTargetSetDelegate?.Invoke(previousTarget, AimTarget);
    }

    private bool HasPotentialTarget()
    {
        return _potentialTargetTracker.PotentialTarget != null;
    }
}