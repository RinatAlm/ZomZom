using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform target;
    public float minDistance = 0.5f;
    public float speed = 2;
    public NavMeshAgent navComponent;

    private void Start()
    {
        target = GameObject.Find("Character").GetComponent<Transform>();
        navComponent = GetComponent<NavMeshAgent>();
        navComponent.speed = speed;
    }
    private void Update()
    {
        /*
        if (Vector2.Distance(transform.position, target.position)> minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        */
        if (target != null)
        {
            navComponent.SetDestination(target.position);
            
        }

    }
}
