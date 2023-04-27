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
        if (targetEnemy != null)
        {
            direction = targetEnemy.transform.position - transform.position;
            weapon.SetPosition(transform.position);
           if(Physics.SphereCast(transform.position, SphereCastRadius, direction.normalized, out hit, mask))//If no obstacles => shoot
            {
                bulletSpawnManager.DoSpawnBullet(weapon.bulletIndex,transform.position,direction.normalized,weapon.bulletSpeed);
                targetEnemy.enemyHealth.TakeDamage(weapon.damage);
            }

            //
            //Physics.Raycast(transform.position, targetEnemy.gameObject.transform.position, out hit, weapon.range);//Shoot bullet in direction of 
            //{

            //    var trail = Instantiate(weapon.bulletTrail, transform.position, Quaternion.LookRotation(direction));
            //    trail.GetComponent<BulletTrail>().targetPosition = targetEnemy.transform.position;
            //    targetEnemy.enemyHealth.TakeDamage(weapon.damage);
            //    Destroy(trail, 1);

            //}
            if (!targetEnemy.enemyHealth.gameObject.activeSelf)//Checking if enemy is dead and not active
            {
                gunManager.enemies.Remove(targetEnemy);
                targetEnemy = null;
            }
        }

    }

    public void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }


   
}
