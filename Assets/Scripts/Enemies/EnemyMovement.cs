using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float updateRate = 0.1f;
    public Transform target;
    public float minDistance = 0.5f;
    public float speed = 2;
   public Enemy enemyCharacter;


    public void StartChasing()
    {
        
     StartCoroutine(FollowTarget());
        
    }
    private void Start()
    {
        target = GameObject.Find("Character").GetComponent<Transform>();//Identifying player and speed 
        enemyCharacter.navComponent.speed = speed;
    }
    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateRate);
        while (enabled)
        {

            enemyCharacter.navComponent.SetDestination(target.position);
            yield return wait;
        }
    }

}
