using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBomb : MonoBehaviour
{
    [SerializeField] CircleCollider2D explosionRadius;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        explosionRadius.enabled = true;
    }
}
