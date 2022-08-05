using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstakillLaser : MonoBehaviour
{
    [SerializeField] float cooldownTime = 5f;
    [SerializeField] Vector3 offset = new Vector3();

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
        transform.position = player.transform.position + offset;
    }

    IEnumerator CooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
