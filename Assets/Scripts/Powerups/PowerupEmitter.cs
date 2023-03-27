using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PowerupEmitter : NetworkBehaviour
{
    [SerializeField] List<GameObject> powerups = new List<GameObject>();
    [SerializeField] List<Transform> emissionPoints = new List<Transform>();
    [SerializeField] float minEmissionTime;
    [SerializeField] float maxEmissionTime;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        StartCoroutine(WaitToEmitNextPowerup());
    }

    IEnumerator WaitToEmitNextPowerup()
    {
        yield return new WaitForSeconds(Random.Range(minEmissionTime, maxEmissionTime));
        SelectNewPowerup();
    }

    void SelectNewPowerup()
    {
        GameObject nextPowerup = powerups[Random.Range(0, powerups.Count)];
        Transform nextEmissionPoint = emissionPoints[Random.Range(0, emissionPoints.Count)];

        SpawnPowerupServerRpc(GetPowerupIndex(nextPowerup), GetEmissionPointIndex(nextEmissionPoint));
        
        StartCoroutine(WaitToEmitNextPowerup());
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnPowerupServerRpc(int powerupIndex, int emissionPointIndex)
    {
        GameObject nextPowerup = GetPowerupFromIndex(powerupIndex);
        Transform nextEmissionPoint = GetEmissionPointFromIndex(emissionPointIndex);

        GameObject powerup = Instantiate(nextPowerup, nextEmissionPoint.position, Quaternion.identity);

        powerup.GetComponent<NetworkObject>().Spawn();
    }

    int GetPowerupIndex(GameObject powerup)
    {
        return powerups.IndexOf(powerup);
    }

    GameObject GetPowerupFromIndex(int powerupIndex)
    {
        return powerups[powerupIndex];
    }

    int GetEmissionPointIndex(Transform emissionPoint)
    {
        return emissionPoints.IndexOf(emissionPoint);
    }

    Transform GetEmissionPointFromIndex(int emissionPointIndex)
    {
        return emissionPoints[emissionPointIndex];
    }

}
