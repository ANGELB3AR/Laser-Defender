using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Health health;
    bool isActive = false;

    public void ActivateShield()
    {
        health = GetComponent<Health>();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Enemy"))
        {
            Debug.Log("Shield triggered by enemy");
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
