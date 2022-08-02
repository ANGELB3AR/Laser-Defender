using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Health health;

    public void ActivateShield(bool isActivated)
    {
        if (isActivated)
        {
            health = GetComponentInParent<Health>();
        }
        GetComponent<CircleCollider2D>().enabled = isActivated;
        GetComponent<SpriteRenderer>().enabled = isActivated;
        health.ToggleCanReceiveDamage(!isActivated);
    }
}
