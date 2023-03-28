using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shooter : NetworkBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    public float baseFiringRate = 0.2f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    public float minFiringRate = 1f;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    GameObject projectileInstance;
    
    [HideInInspector] public bool isFiring;
    [HideInInspector] public float initialBaseFiringRate;
    [HideInInspector] public float initialMinFiringRate;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }

        initialBaseFiringRate = baseFiringRate;
        minFiringRate = initialMinFiringRate;
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            SpawnProjectileServerRpc();
            
            Destroy(projectileInstance, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                                        baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnProjectileServerRpc()
    {
        projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileInstance.GetComponent<NetworkObject>().Spawn();

        Rigidbody2D rb2d = projectileInstance.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.velocity = transform.up * projectileSpeed;
        }
    }

    //[ServerRpc(RequireOwnership = false)]
    //void DestroyProjectileServerRpc()
    //{
    //    projectileInstance.GetComponent<NetworkObject>().Despawn();
    //}
}
