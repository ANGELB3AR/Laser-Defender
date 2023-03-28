using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    
    WaveConfigSO currentWave;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    SpawnEnemyServerRpc(i);
                    
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping);
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnEnemyServerRpc(int enemyPrefabIndex)
    {
        GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(enemyPrefabIndex),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform);
        enemy.GetComponent<NetworkObject>().Spawn();
    }
}
