using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class BountyHunter : NetworkBehaviour
{
    //private IHunt _aiming;
    private NetworkVariable<int> _netBountyOnHead = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private TextMeshPro _bountySizeText;

    private void Start()
    {
        // _aiming = GetComponentInChildren<IHunt>();
        // _aiming.KillLanded += OnKill;
        _bountySizeText = GetComponentInChildren<TextMeshPro>();
    }

    public override void OnNetworkSpawn()
    {
        _netBountyOnHead.OnValueChanged += (previous, current) => _bountySizeText.text = current.ToString();
    }

    private void OnKill(GameObject target, bool wasInDefense)
    {
        if (!wasInDefense)
        {
            _netBountyOnHead.Value += 1;
        }
    }
}
