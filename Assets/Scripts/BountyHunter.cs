using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class BountyHunter : NetworkBehaviour
{
    private readonly NetworkVariable<int> _bounty = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private TextMeshPro _bountySizeText;
    private Shooting _shooting;

    private void Start()
    {
        _bountySizeText = GetComponentInChildren<TextMeshPro>();
        _shooting = GetComponentInChildren<Shooting>();
        _shooting.OnKill += IncreaseBounty;
        _bountySizeText.text = _bounty.Value.ToString();
    }

    public override void OnNetworkSpawn()
    {
        _bounty.OnValueChanged += (previous, current) => _bountySizeText.text = current.ToString();
    }

    private void IncreaseBounty(GameObject killer, GameObject target)
    {
        _bounty.Value += 1;
    }
}
