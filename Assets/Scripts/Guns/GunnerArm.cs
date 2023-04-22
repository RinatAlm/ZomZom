using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunnerArm : MonoBehaviour
{
    public Weapon weapon;
    public Enemy targetEnemy;
    public Vector3 direction;
    public float shootTimer;
    public GunManager gunManager;
    public short shots;

    private void Start()
    {
        gunManager = FindObjectOfType<GunManager>();
    }


    private void Update()
    {
        if (weapon != null && targetEnemy != null && shots != weapon.numberOfShots)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer<=0)
            {
                shootTimer = weapon.delay;
                shots++;
                Shoot();
                if(shots == weapon.numberOfShots)
                {
                    shots = 0;
                    shootTimer = weapon.shootTimerMax;
                }
            }
            
        }
    }

    public void Shoot()
    {
        if(targetEnemy!=null)
        {
            Debug.Log(targetEnemy.transform.position);
            direction = targetEnemy.transform.position - transform.position;
            weapon.SetPosition(transform.position);
            RaycastHit hit;
            Physics.Raycast(transform.position, targetEnemy.gameObject.transform.position, out hit, weapon.range);
            {
                var trail = Instantiate(weapon.bulletTrail, transform.position, Quaternion.LookRotation(direction));                
                trail.GetComponent<BulletTrail>().targetPosition = targetEnemy.transform.position;
                targetEnemy.health -= weapon.damage;
                Destroy(trail, 1);
                
            }
            if(targetEnemy.health<=0)
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
