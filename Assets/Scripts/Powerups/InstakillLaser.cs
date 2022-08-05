using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstakillLaser : MonoBehaviour
{
    [SerializeField] float cooldownTime = 5f;

    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        StartCoroutine(CooldownTimer(cooldownTime));
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    IEnumerator CooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
