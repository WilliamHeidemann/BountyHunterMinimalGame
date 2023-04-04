using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunAimUI : MonoBehaviour
{
    private Slider _slider;
    private bool _aiming;
    // Add reference to the equipped gun and put its aim time as the max value of the slider
    
    private void Start()
    {
        _slider = GetComponent<Slider>();
        SpawnInitialization.OnPlayerSpawn += TrackGun;
    }

    private void TrackGun(GameObject player)
    {
        _slider.maxValue = 1; // Inject gun's aim time
        player.GetComponentInChildren<AimTargetTracker>().OnAimTargetSetDelegate += CheckAim;
    }

    private void CheckAim(GameObject previousTarget, GameObject newTarget)
    {
        if (newTarget == null)
        {
            _aiming = false;
            _slider.value = _slider.minValue;
        }
        else
        {
            _aiming = true;
        }
    }
    
    private void Update()
    {
        if (_aiming)
        {
            _slider.value += Time.deltaTime;
        }
    }
}
