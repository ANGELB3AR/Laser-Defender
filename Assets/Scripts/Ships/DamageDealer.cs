using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool shouldBeDestroyedOnImpact = true;
    [SerializeField] float timeDelay = 1f;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (shouldBeDestroyedOnImpact)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyOnDelay(timeDelay));
        }
    }

    IEnumerator DestroyOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
