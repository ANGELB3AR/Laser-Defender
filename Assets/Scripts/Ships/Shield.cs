using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Health health;
    bool isActive = false;

    public void ActivateShield()
    {
        isActive = true;
        health = GetComponentInParent<Health>();
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        health.ReceiveDamage(!isActive);
        Debug.Log("Shields activated!");
    }

    public void DeactivateShield()
    {
        isActive = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        health.ReceiveDamage(!isActive);
        Debug.Log("Shields down");
    }
}
