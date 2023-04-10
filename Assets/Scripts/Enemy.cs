using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
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
       
       
            navComponent.SetDestination(target.position);
           
    }
}
