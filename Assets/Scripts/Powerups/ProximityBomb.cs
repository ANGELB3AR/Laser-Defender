using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProximityBomb : NetworkBehaviour
{
    [SerializeField] float destroySelfDelayTime = 0.5f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject damageEffects;

    GameObject damageEffectsInstance;

    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        damageEffectsInstance =Instantiate(damageEffects, transform);
        SpawnDamageEffectsServerRpc();

        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Invoke(nameof(DespawnDamageEffectsServerRpc), destroySelfDelayTime);
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnDamageEffectsServerRpc()
    {
        damageEffectsInstance.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    void DespawnDamageEffectsServerRpc()
    {
        damageEffectsInstance.GetComponent<NetworkObject>().Despawn();
    }
}
