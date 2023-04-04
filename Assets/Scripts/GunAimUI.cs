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
    }

    public void TrackGun()
    {
        
    }

    private void Update()
    {
        if (_aiming)
        {
            _slider.value += Time.deltaTime;
        }
    }

    private void FillBar()
    {
        _aiming = true;
    }
    
    private void ResetBar()
    {
        _aiming = false;
        _slider.value = _slider.minValue;
    }
}
