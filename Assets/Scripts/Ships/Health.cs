using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] NetworkVariable<int> health = new NetworkVariable<int>(50);
    [SerializeField] bool isPlayer;
    [SerializeField] int points = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool canReceiveDamage = true;

    int initialHealth;
    ParticleSystem particleInstance;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    public event Action<int> OnHealthUpdated;

    private void OnEnable()
    {
        health.OnValueChanged += HealthOnValueChanged;
    }

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();

        initialHealth = health.Value;
    }

    private void OnDisable()
    {
        health.OnValueChanged -= HealthOnValueChanged;
    }

    public void ToggleCanReceiveDamage(bool status)
    {
        canReceiveDamage = status;
    }

    public int GetHealth()
    {
        return health.Value;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            if (canReceiveDamage)
            {
                TakeDamage(damageDealer.GetDamage());
                PlayHitEffect();
                audioPlayer.PlayDamageClip();
                ShakeCamera();
                damageDealer.Hit();
            }
            else
            {
                if (!collision.gameObject.CompareTag("Enemy"))
                {
                    audioPlayer.PlayShieldHitClip();
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    void TakeDamage(int damage)
    {
        if (!IsServer) { return; }

        health.Value -= damage;

        if (health.Value <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        health.Value += amount;
        health.Value = Mathf.Clamp(health.Value, int.MinValue, initialHealth);
    }

    private void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(points);
        }
        else
        {
            levelManager.LoadGameOver();
        }

        DestroyOnDeathServerRpc();
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            particleInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            PlayHitEffectServerRpc();

            float delay = particleInstance.main.duration + particleInstance.main.startLifetime.constantMax;
            Invoke(nameof(DestroyHitParticleServerRpc), delay);

            DestroyHitParticleServerRpc();
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyOnDeathServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }

    [ServerRpc(RequireOwnership = false)]
    void PlayHitEffectServerRpc()
    {
        particleInstance.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyHitParticleServerRpc()
    {
        particleInstance.GetComponent<NetworkObject>().Despawn();
    }

    private void HealthOnValueChanged(int previousValue, int newValue)
    {
        OnHealthUpdated?.Invoke(newValue);
    }
}
