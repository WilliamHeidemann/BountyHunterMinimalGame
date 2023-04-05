using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class JoinGame : MonoBehaviour
{
    [SerializeField] private GameObject networkUI;
    public void JoinAsHost()
    {
        NetworkManager.Singleton.StartHost();
        networkUI.SetActive(false);
    }

    public void JoinAsClient()
    {
        NetworkManager.Singleton.StartClient();
        networkUI.SetActive(false);
    }
}
