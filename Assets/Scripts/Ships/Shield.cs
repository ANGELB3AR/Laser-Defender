using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Health health;
    bool isActive = false;

    public void ActivateShield()
    {
        health = GetComponentInParent<Health>();
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        health.ToggleCanReceiveDamage();
        Debug.Log("Shields activated!");
    }

    public void DeactivateShield()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        health.ToggleCanReceiveDamage();
        Debug.Log("Shields down");
    }
}
