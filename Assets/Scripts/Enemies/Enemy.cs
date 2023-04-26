using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PoolableObject
{
    public EnemyHealth enemyHealth;
    public EnemyMovement enemyMovement;
    public NavMeshAgent navComponent;
    private EnemySpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = GameObject.FindObjectOfType<EnemySpawnManager>().GetComponent<EnemySpawnManager>();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        enemyHealth.health = 100;
        spawnManager.spawnedEnemies--;
    }

    public void OnEnable()
    {
        enemyHealth.TakeDamage(0);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
