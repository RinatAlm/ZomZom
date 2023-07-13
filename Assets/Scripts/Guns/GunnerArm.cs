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
    private float shootRadius = 1;
    private float SphereCastRadius = 0.1f;
    private RaycastHit hit;
    
    
    private void Start()
    {
        gunManager = FindObjectOfType<GunManager>();
    }


    private void FixedUpdate()
    {
        if (targetEnemy == null)
        {
            shootTimer = weapon.delay;
        }
        if (weapon != null && targetEnemy != null && shots != weapon.numberOfTriggerPressing)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = weapon.delay;
                shots++;
                Shoot();
                if (shots == weapon.numberOfTriggerPressing)
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
            
           if (Physics.SphereCast(transform.position, SphereCastRadius, direction.normalized, out hit, shootRadius, mask))//If no obstacles => shoot
           {
                if (!hit.collider.CompareTag("Obstacle"))
                {
                    for (int i = 0; i < weapon.numOfBulletsPerPressing; i++)
                    {
                        BulletSpawnManager.instance.DoSpawnBullet(this, transform.position, RandomizeVector(direction.normalized));
                    }
                    weapon.Play();

                }
                else
                {
                    ResetTarget();
                }
            }          
        }

    }

    public Vector3 RandomizeVector(Vector3 dir)
    {
        Vector3 spread = new Vector3(
            Random.Range(dir.x - weapon.spreadAngle, dir.x + weapon.spreadAngle),
            dir.y,
            Random.Range(dir.z - weapon.spreadAngle, dir.z + weapon.spreadAngle)
            );
        return spread;
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
