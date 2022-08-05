using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 1f;
    [SerializeField] AudioClip fireSecondaryClip;
    [SerializeField] [Range(0f, 1f)] float fireSecondaryVolume = 1f;


    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    [Header("Shield")]
    [SerializeField] AudioClip shieldHitClip;
    [SerializeField] [Range(0f, 1f)] float shieldHitVolume = 1f;
    [SerializeField] AudioClip shieldActivateClip;
    [SerializeField] [Range(0f, 1f)] float shieldActivateVolume = 1f;
    [SerializeField] AudioClip shieldDeactivateClip;
    [SerializeField] [Range(0f, 1f)] float shieldDeactivateVolume = 1f;

    [Header("Pickups")]
    [SerializeField] AudioClip weaponPickupClip;
    [SerializeField] [Range(0f, 1f)] float weaponPickupVolume = 1f;
    [SerializeField] AudioClip healthPickupClip;
    [SerializeField] [Range(0f, 1f)] float healthPickupVolume = 1f;
    [SerializeField] AudioClip rapidfirePickupClip;
    [SerializeField] [Range(0f, 1f)] float rapidfirePickupVolume = 1f;
    [SerializeField] AudioClip rapidfireDeactivationClip;
    [SerializeField] [Range(0f, 1f)] float rapidfireDeactivationVolume = 1f;


    static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayFireSecondaryClip()
    {
        PlayClip(fireSecondaryClip, fireSecondaryVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayShieldHitClip()
    {
        PlayClip(shieldHitClip, shieldHitVolume);
    }

    public void PlayShieldActivateClip()
    {
        PlayClip(shieldActivateClip, shieldActivateVolume);
    }

    public void PlayShieldDeactivateClip()
    {
        PlayClip(shieldDeactivateClip, shieldDeactivateVolume);
    }

    public void PlayWeaponPickupClip()
    {
        PlayClip(weaponPickupClip, weaponPickupVolume);
    }

    public void PlayHealthPickupClip()
    {
        PlayClip(healthPickupClip, healthPickupVolume);
    }

    public void PlayRapidfirePickupClip()
    {
        PlayClip(rapidfirePickupClip, rapidfirePickupVolume);
    }

    public void PlayRapidfireDeactivationClip()
    {
        PlayClip(rapidfireDeactivationClip, rapidfireDeactivationVolume);
    }


    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
