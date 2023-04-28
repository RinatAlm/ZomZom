using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunnerArm : MonoBehaviour
{
    public Weapon weapon;
    public Enemy targetEnemy;
    public Vector3 direction;
    private float shootTimer;
    public GunManager gunManager;
    public LayerMask mask;
    private short shots;
    private float SphereCastRadius = 0.05f;
    private RaycastHit hit;
    public BulletSpawnManager bulletSpawnManager;
    
    private void Start()
    {
        gunManager = FindObjectOfType<GunManager>();
    }


    private void Update()
    {   
        if(targetEnemy == null)
        {
            shootTimer = weapon.delay;
        }
        if (weapon != null && targetEnemy != null && shots != weapon.numberOfShots)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = weapon.delay;
                shots++;
                Shoot();
                if (shots == weapon.numberOfShots)
                {
                    shots = 0;
                    shootTimer = weapon.shootTimerMax;
                }
            }

        }
    }

    public void Shoot()
    {
     
        if (targetEnemy!=null)
        {
            direction = targetEnemy.transform.position - transform.position;
          
           if(Physics.SphereCast(transform.position, SphereCastRadius, direction.normalized, out hit, mask))//If no obstacles => shoot
            {
                bulletSpawnManager.DoSpawnBullet(this,transform.position,direction.normalized);              
            }          
        }

    }

    public void ResetTarget()
    {      
        targetEnemy = null;
    }
    public void SetTarget(Enemy targetEnemy)
    {
     
            this.targetEnemy = targetEnemy;
             if (targetEnemy != null)
            this.targetEnemy.gunnerArms.Add(this);
        
    }


   
}
