using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Pathfinder : NetworkBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        if (!IsServer) { return; }

        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        if (!IsServer) { return; }

        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }    
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
