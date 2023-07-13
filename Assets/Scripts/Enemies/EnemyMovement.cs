using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float updateRate = 0.25f;
    public float minDistance = 0.5f;
    public float speed = 2;
    public Enemy enemyCharacter;
    private PlayerBelongingsAndParams _playerBelongingsAndParams;
    [SerializeField]private Vector3 _closestTarget;


    private void Awake()
    {
        _playerBelongingsAndParams = PlayerBelongingsAndParams.instance;
    }
    private void Start()
    {          
        enemyCharacter.navComponent.speed = speed;
    }

    public void StartChasing()
    {
        StartCoroutine(FollowTarget());
    }
    /// <summary>
    /// Hurt player entering the collider
    /// </summary>
    private void OnTriggerStay(Collider other)
    {     
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(enemyCharacter.Damage);
        }
        if(other.CompareTag("Turret"))
        {
            other.GetComponent<Turret>().TakeDamage(enemyCharacter.Damage);
        }
    }


    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateRate);
        while (enabled)
        {
            enemyCharacter.navComponent.SetDestination(FindClosestTarget());
            yield return wait;
        }
    }

    private Vector3 FindClosestTarget()
    {
        if(_playerBelongingsAndParams.damagableNonZombieObjects.Count!=0)
        {
            _closestTarget = _playerBelongingsAndParams.damagableNonZombieObjects[0].transform.position;
            float clostestDistance = Vector3.Distance(transform.position,_closestTarget);
            foreach (GameObject nonZombieObject in _playerBelongingsAndParams.damagableNonZombieObjects)
            {             
                float distance = Vector3.Distance(transform.position, nonZombieObject.transform.position);
                if(distance < clostestDistance)
                {
                    clostestDistance = distance;
                    _closestTarget = nonZombieObject.transform.position;
                }              
            }           
            return _closestTarget;
        }     
        else
        {
            Debug.Log("List is empty");
            return Vector3.zero;
        }
       
    }
}
