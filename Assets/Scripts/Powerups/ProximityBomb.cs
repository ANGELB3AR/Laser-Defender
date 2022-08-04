using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityBomb : MonoBehaviour
{
    [SerializeField] float destroySelfDelayTime = 0.5f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject damageEffects;

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
        Instantiate(damageEffects, transform);
        print("Should have instatiated particle effect");
        StartCoroutine(DelayBeforeDestroyingProjectile(destroySelfDelayTime));
    }

    IEnumerator DelayBeforeDestroyingProjectile(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
