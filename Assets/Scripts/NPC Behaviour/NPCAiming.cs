using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCAiming : MonoBehaviour//, IHunt
{
    // public event Action<Shootable> TargetSet;
    // public event Action<Shootable> TargetLost;
    // public event Action<Shootable> AimStarted;
    // public event Action<Shootable> AimCanceled;
    // public event Action<Shootable> AimCompleted;

    // private List<Shootable> shootables;
    private NPCMovement _movement;

    private void Start()
    {
        // shootables = FindObjectsOfType<Shootable>().ToList();
        // shootables.Remove(transform.parent.GetComponentInParent<Shootable>());

        _movement = GetComponentInParent<NPCMovement>();
    }
}
