using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCMovement : NetworkBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float roamSpeed;
    private Vector2 _target;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _target = NextTarget();
    }

    private void Update()
    {
        if (!IsServer) return;
        Move();
        if (Vector2.Distance(transform.position,_target) < .5f)
        {
            _target = NextTarget();
        }
    }

    private void Move()
    {
        var position = transform.position;
        var newPosition = Vector2.MoveTowards(position, _target, roamSpeed * Time.deltaTime);
        _controller.Move(newPosition - (Vector2)position);
    }
    
    private Vector2 NextTarget()
    {
        // Inject x,y from map
        var x = Random.Range(-10f, 10f);
        var y = Random.Range(-5f, 5f);
        return new Vector2(x, y);
    }

}
