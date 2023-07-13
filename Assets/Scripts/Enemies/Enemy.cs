using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PoolableObject
{
    public List<GunnerArm> gunnerArms = new();
    public HashSet <GunManager> gunManagers = new();
    public EnemyHealth enemyHealth;
    public EnemyMovement enemyMovement;
    public NavMeshAgent navComponent;
    private EnemySpawnManager spawnManager;
    public float Damage;

    private void Awake()
    {
        spawnManager = FindObjectOfType<EnemySpawnManager>().GetComponent<EnemySpawnManager>();       
    }
    public override void OnDisable()
    {
        base.OnDisable();
        enemyHealth.Health = enemyHealth.maxHealth;//Give enemyHealth back before respawning
        if(Time.timeScale != 0)
        {
            spawnManager.DecrementEnemiesNumber();
        }      
    }

    public void OnEnable()
    {
        enemyHealth.TakeDamage(0);
    }

    public void Disable(bool isKilled)
    {
        if(isKilled)
        {
            GameManager.totalyKilled++;
        }
        else
        {
        //Remove enemy
        }
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            BulletTouch(bullet);
        }
    }

    public void BulletTouch(Bullet bullet)
    {     
        enemyHealth.TakeDamage(bullet.BulletHit());//Take Damage from BulletHit
        gunManagers.Add(bullet.bulletOfArm.gunManager);
        if (enemyHealth.Health <= 0)
        {
            foreach (GunManager gunManager in gunManagers)
            {
                gunManager.RemoveTarget(this);               
            }
            gunManagers.Clear();
            foreach (GunnerArm arm in gunnerArms)
            {
                arm.ResetTarget();
            }
            gunnerArms.Clear();
            GameManager.instance.IncreaseLevelBar();
            Disable(true);
        }
        bullet.numOfAimsToDestr--;
        if (bullet.numOfAimsToDestr <= 0)
            bullet.Disable();
    }
}
