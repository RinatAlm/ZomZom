using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    public float minDistance = 0.5f;
    public float speed = 2;
    public NavMeshAgent navComponent;
    public float health = 100f;
    public Slider healthSlider;


    private void Start()
    {
        target = GameObject.Find("Character").GetComponent<Transform>();//Identifying player and speed 
        navComponent = GetComponent<NavMeshAgent>();
        navComponent.speed = speed;
    }
    private void Update()
    {     

        navComponent.SetDestination(target.position);//Following player

        healthSlider.value = health;//Displaying Health 
        if(health == 100)
        {
            healthSlider.gameObject.SetActive(false);
            
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }
        if(health<=0)
        {
            gameObject.SetActive(false);
        }
           
    }
}
