using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HealthPowerup : NetworkBehaviour
{
    [SerializeField] int amountOfHealthToRestore;
    [SerializeField] float moveSpeed;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().RestoreHealth(amountOfHealthToRestore);
            audioPlayer.PlayHealthPickupClip();
            DestroyPickupServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyPickupServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
