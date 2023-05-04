using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PoolableObject
{
    public List<GunnerArm> gunnerArms = new List<GunnerArm>();
    public EnemyHealth enemyHealth;
    public EnemyMovement enemyMovement;
    public NavMeshAgent navComponent;
    private EnemySpawnManager spawnManager;
    private GunManager gunManager;
    public float Damage;

    private void Awake()
    {
        spawnManager = GameObject.FindObjectOfType<EnemySpawnManager>().GetComponent<EnemySpawnManager>();
        gunManager = GameObject.FindObjectOfType<GunManager>().GetComponent<GunManager>();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        enemyHealth.health = 100;
        if(Time.timeScale != 0)
        {
            spawnManager.spawnedEnemies--;
        }
       
    }

    public void OnEnable()
    {
        enemyHealth.TakeDamage(0);
    }

    public void Disable()
    {
        GameManager.totalyKilled++;
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") == true)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            enemyHealth.TakeDamage(bullet.bulletOfArm.weapon.damage);
            if (enemyHealth.health <= 0)
            {
               
                foreach (GunnerArm arm in gunnerArms)
                {                  
                    arm.ResetTarget();                                   
                }
                gunManager.RemoveTarget(this);
                Disable();

            }
            bullet.numOfAimsToDestr--;
            if(bullet.numOfAimsToDestr<=0)
            bullet.Disable();
           
            
        }
    }
}
