using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    [SerializeField] int amountOfHealthToRestore;
    [SerializeField] float moveSpeed;

    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().RestoreHealth(amountOfHealthToRestore);
            Destroy(gameObject);
        }
    }
}
