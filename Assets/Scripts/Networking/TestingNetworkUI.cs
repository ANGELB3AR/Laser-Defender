using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TestingNetworkUI : MonoBehaviour
{
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        Hide();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
