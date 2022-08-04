using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    [SerializeField] float moveSpeed;

    Shield shield;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            shield = collision.GetComponentInChildren<Shield>();
            shield.ActivateShield(true);
            audioPlayer.PlayShieldActivateClip();
            StartCoroutine(CooldownTimer());
        }
    }

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(cooldownTime);
        shield.ActivateShield(false);
        audioPlayer.PlayShieldDeactivateClip();
        Destroy(gameObject);
    }
}
