using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePowerup : MonoBehaviour
{
    [SerializeField] float rapidFireRate;
    [SerializeField] float cooldownTime;
    [SerializeField] float moveSpeed;

    Shooter shooter;

    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            shooter = collision.GetComponent<Shooter>();
            shooter.baseFiringRate = rapidFireRate;
            shooter.minFiringRate = rapidFireRate;
            StartCoroutine(CooldownTimer());
            
        }
    }

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(cooldownTime);
        shooter.baseFiringRate = shooter.initialBaseFiringRate;
        shooter.minFiringRate = shooter.initialMinFiringRate;
        Destroy(gameObject);
    }
}
