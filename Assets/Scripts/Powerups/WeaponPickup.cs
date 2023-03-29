using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WeaponPickup : NetworkBehaviour
{
    public WeaponType type;
    public int startingAmmo;
    public Sprite icon;

    [SerializeField] GameObject projectile;
    [SerializeField] float moveSpeed = 1f;

    Player player;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            SecondaryWeapon gun = collision.GetComponent<SecondaryWeapon>();
            GetComponent<SpriteRenderer>().enabled = false;
            gun.currentWeapon = this;
            gun.currentAmmo = startingAmmo;
            audioPlayer.PlayWeaponPickupClip();
        }
    }


    public void FireWeapon(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.ProximityBomb:
                FireProximityBomb();
                break;
            case WeaponType.HomingMissiles:
                FireHomingMissiles();
                break;
            case WeaponType.InstakillLasers:
                ActivateInstakillLasers();
                break;
        }
    }

    void FireProximityBomb()
    {
        ActivateWeaponServerRpc();
        audioPlayer.PlayFireSecondaryClip();
    }

    void FireHomingMissiles()
    {
        ActivateWeaponServerRpc();
        audioPlayer.PlayFireSecondaryClip();
    }

    void ActivateInstakillLasers()
    {
        ActivateWeaponServerRpc();
        audioPlayer.PlayFireSecondaryClip();
    }

    [ServerRpc(RequireOwnership = false)]
    void ActivateWeaponServerRpc()
    {
        GameObject projectileInstance = Instantiate(projectile, player.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void DeactivateWeaponServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}