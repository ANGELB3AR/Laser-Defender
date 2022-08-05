using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    GameObject currentTarget;

    void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        transform.LookAt(currentTarget.transform.position);
        Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
    }
}
