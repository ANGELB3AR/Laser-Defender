using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EffectHandler : NetworkBehaviour
{
    ParticleSystem particleInstance;

    public override void OnNetworkSpawn()
    {
        particleInstance = GetComponent<ParticleSystem>();

        float delay = particleInstance.main.duration + particleInstance.main.startLifetime.constantMax;

        Invoke(nameof(DestroySelfServerRpc), delay);
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroySelfServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
