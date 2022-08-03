using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecondaryWeapon : MonoBehaviour
{
    public WeaponPickup currentWeapon;
    public int currentAmmo;

    void Update()
    {
        if (currentWeapon == null) { return; }
        OnSecondaryFire();
    }

    void OnSecondaryFire()
    {
        FireWeapon();
    }

    void FireWeapon()
    {
        currentWeapon.FireWeapon(currentWeapon.type);
        currentAmmo--;

        if (currentAmmo == 0)
        {
            currentWeapon = null;
        }
    }
}
