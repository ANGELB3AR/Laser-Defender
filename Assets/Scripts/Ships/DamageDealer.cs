using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DamageDealer : NetworkBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool shouldBeDestroyedOnImpact = true;
    [SerializeField] float timeDelay = 1f;

    private void Start()
    {
        StartCoroutine(DestroyOnDelay(timeDelay));
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (!shouldBeDestroyedOnImpact) { return; }

        DestroyProjectileClientRpc();
    }

    IEnumerator DestroyOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyProjectileClientRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyProjectileServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }

    [ClientRpc]
    void DestroyProjectileClientRpc()
    {
        if (!IsServer) { return; }

        DestroyProjectileServerRpc();
    }
}
