using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PotentialTargetTracker : MonoBehaviour
{
    public delegate void OnTargetChangedDelegate(GameObject previousTarget, GameObject newTarget);
    public event OnTargetChangedDelegate OnPotentialTargetChanged;
    public GameObject PotentialTarget { get; private set; }
    
    private Camera _cam;
    private float _minDistanceFromGun = 5f;
    private float _minDistanceFromMouse = 2f;

    private AimTargetTracker _aimTargetTracker;
    
    private void Start()
    {
        _cam = Camera.main;
        _aimTargetTracker = GetComponent<AimTargetTracker>();
    }
    
    private void Update()
    {
        if (_aimTargetTracker.aimTarget != null) return;
        var target = FindTarget();
        if (target != PotentialTarget)
        {
            var previousTarget = PotentialTarget;
            PotentialTarget = target;
            OnPotentialTargetChanged?.Invoke(previousTarget, target);
        }
    }
    
    public void SetPotentialTarget(GameObject newTarget)
    {
        var previousTarget = PotentialTarget;
        PotentialTarget = newTarget;
        OnPotentialTargetChanged?.Invoke(previousTarget, PotentialTarget);
    }

    private GameObject FindTarget()
    {
        var mousePosition = FindNearestShootable(out var nearest);
        if (nearest == null) return null;
        return IsNearMouse(nearest.transform.position, mousePosition) ? nearest : null;
    }
    
    private Vector3 FindNearestShootable(out GameObject nearest)
    {
        var mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        var potentialTargets = GameObject.FindGameObjectsWithTag("Killable");
        nearest = potentialTargets.Where(x => Vector2.Distance(x.transform.position, transform.position) < _minDistanceFromGun).OrderBy(x => Vector2.Distance(x.transform.position, mousePosition)).FirstOrDefault();
        return mousePosition;
    }
    
    private bool IsNearMouse(Vector3 target, Vector3 mousePosition)
    {
        return Vector2.Distance(target, mousePosition) < _minDistanceFromMouse;
    }
}
