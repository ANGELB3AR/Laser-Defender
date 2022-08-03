using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] int currentAmmo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        weapon.FireWeapon(weapon.type);
        currentAmmo--;
    }
}
