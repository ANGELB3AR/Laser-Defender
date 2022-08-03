using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Pickup", menuName = "Pickups/Secondary Weapon")]
public class WeaponPickup : ScriptableObject
{
    public WeaponType type;
    public int startingAmmo;

    [SerializeField] GameObject projectile;
    [SerializeField] Sprite pickupIcon;



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
        throw new NotImplementedException();
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