using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType type;
    public int startingAmmo;

    [SerializeField] GameObject projectile;
    [SerializeField] float moveSpeed = 1f;

    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
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
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    void FireHomingMissiles()
    {
        throw new NotImplementedException();
    }

    void ActivateInstakillLasers()
    {
        throw new NotImplementedException();
    }
}