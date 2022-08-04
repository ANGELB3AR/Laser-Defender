using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool shouldBeDestroyedOnImpact = true;

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
    }
}
