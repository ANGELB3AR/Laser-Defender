using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
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
        Instantiate(projectile, player.transform.position, Quaternion.identity);
        audioPlayer.PlayFireSecondaryClip();
    }

    void FireHomingMissiles()
    {
        Instantiate(projectile, player.transform.position, Quaternion.identity);
        audioPlayer.PlayFireSecondaryClip();
    }

    void ActivateInstakillLasers()
    {
        Instantiate(projectile, player.transform.position, Quaternion.identity);
        audioPlayer.PlayFireSecondaryClip();
    }

    public void DeactivateWeapon()
    {
        Destroy(gameObject);
    }
}