using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupEmitter : MonoBehaviour
{
    [SerializeField] List<GameObject> powerups = new List<GameObject>();
    [SerializeField] List<Transform> emissionPoints = new List<Transform>();
    [SerializeField] float minEmissionTime;
    [SerializeField] float maxEmissionTime;

    void Start()
    {
        StartCoroutine(WaitToEmitNextPowerup());
    }

    IEnumerator WaitToEmitNextPowerup()
    {
        yield return new WaitForSeconds(Random.Range(minEmissionTime, maxEmissionTime));
        EmitNewPowerup();
    }

    void EmitNewPowerup()
    {
        Transform nextEmissionPoint = emissionPoints[Random.Range(0, emissionPoints.Count)];
        GameObject nextPowerup = powerups[Random.Range(0, powerups.Count)];
        Instantiate(nextPowerup, nextEmissionPoint.position, Quaternion.identity);
        StartCoroutine(WaitToEmitNextPowerup());
    }
}
