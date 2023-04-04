using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float moveSpeed;
    private bool _isAiming;
    private AimTargetTracker _aimTargetTracker;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _aimTargetTracker = GetComponentInChildren<AimTargetTracker>();
        _aimTargetTracker.OnAimTargetSetDelegate += CheckIsAiming;
    }

    private void CheckIsAiming(GameObject previousTarget, GameObject newTarget)
    {
        _isAiming = newTarget != null;
    }

    private void LateUpdate()
    {
        if (!IsOwner) return;
        if (_isAiming) return;
        var direction = Direction();
        _controller.Move(moveSpeed * Time.deltaTime * direction);
    }

    private static Vector2 Direction()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");
        var direction = new Vector2(xInput, yInput).normalized;
        return direction;
    }
}
