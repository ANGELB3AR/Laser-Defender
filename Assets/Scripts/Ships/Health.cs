using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] bool isPlayer;
    [SerializeField] int points = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool canReceiveDamage = true;

    int initialHealth;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        initialHealth = health;
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
                audioPlayer.PlayShieldHitClip();
                Destroy(collision.gameObject);
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, int.MinValue, initialHealth);
    }

    private void Die()
    {if (!isPlayer)
        {
            scoreKeeper.ModifyScore(points);
        }
    else
        {
            levelManager.LoadGameOver();
        }
            Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public void ToggleCanReceiveDamage()
    {
        canReceiveDamage = !canReceiveDamage;
    }

    public void ReceiveDamage(bool status)
    {
        canReceiveDamage = status;
    }

    public int GetHealth()
    {
        return health;
    }
}
