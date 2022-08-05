using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecondaryWeapon : MonoBehaviour
{
    public WeaponPickup currentWeapon;
    public int currentAmmo;

    void OnFireSecondary(InputValue value)
    {
        if (value.isPressed)
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        if (currentWeapon == null) { return; }
        currentWeapon.FireWeapon(currentWeapon.type);
        currentAmmo--;

        if (currentAmmo == 0)
        {
            currentWeapon = null;
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public Sprite GetWeaponSprite()
    {
        if (currentWeapon == null) { return null; }
        return currentWeapon.GetComponent<Sprite>();
    }
}
