using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private PotentialTargetTracker _potentialTargetTracker;
    private AimTargetTracker _aimTargetTracker;

    private Material _previousMarkOriginalMaterial;
    [SerializeField] private Material highLightMaterial;

    private void Start()
    {
        _potentialTargetTracker = GetComponent<PotentialTargetTracker>();
        _potentialTargetTracker.OnPotentialTargetChanged += HighLight;
    }

    private void HighLight(GameObject previousTarget, GameObject newTarget)
    {
        if (previousTarget != null)
        {
            previousTarget.GetComponent<SpriteRenderer>().material = _previousMarkOriginalMaterial;
        }

        if (newTarget != null)
        {
            var newTargetSpriteRenderer = newTarget.GetComponent<SpriteRenderer>();
            _previousMarkOriginalMaterial = newTargetSpriteRenderer.material;
            newTargetSpriteRenderer.material = highLightMaterial;
        }
    }
}
