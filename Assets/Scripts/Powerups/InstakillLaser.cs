using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InstakillLaser : MonoBehaviour
{
    [SerializeField] float cooldownTime = 5f;
    [SerializeField] Vector3 offset = new Vector3();

    Player player;
    Vector2 currentPosition = new Vector2();

    void Start()
    {
        StartCoroutine(CooldownTimer(cooldownTime));
    }

    void Update()
    {
        currentPosition = player.transform.position + offset;
        FollowPlayerServerRpc();
    }

    public void SetActivatingPlayer(Player activatingPlayer)
    {
        player = activatingPlayer;
    }

    IEnumerator CooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        DespawnInstakillLasers();
    }

    [ServerRpc(RequireOwnership = false)]
    void DespawnInstakillLasers()
    {
        GetComponent<NetworkObject>().Despawn();
    }

    [ServerRpc (RequireOwnership = false)]
    void FollowPlayerServerRpc()
    {
        transform.position = currentPosition;
    }
}
