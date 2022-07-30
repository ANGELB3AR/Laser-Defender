using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Health health;

    void OnEnable()
    {
        health = GetComponent<Health>();
        health.ToggleCanReceiveDamage();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Shield triggered by enemy");
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    void OnDisable()
    {
        health.ToggleCanReceiveDamage();
    }
}
