using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBomb : MonoBehaviour
{
    [SerializeField] CircleCollider2D explosionRadius;
    [SerializeField] float destroySelfDelayTime = 0.5f;
    [SerializeField] float moveSpeed = 5f;

    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
    }

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
        StartCoroutine(DelayBeforeDestroyingProjectile(destroySelfDelayTime));
        // TODO: Play explosion effects
    }

    IEnumerator DelayBeforeDestroyingProjectile(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
