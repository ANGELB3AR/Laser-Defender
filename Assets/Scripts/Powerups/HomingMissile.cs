using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    GameObject currentTarget;

    void Start()
    {
        FindNewTarget();
    }

    void Update()
    {
        if (currentTarget == null)
        {
            FindNewTarget();
        }

        ChaseTarget();
    }

    void FindNewTarget()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Enemy");
    }

    void ChaseTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
        transform.up = currentTarget.transform.position - transform.position;
    }
}
