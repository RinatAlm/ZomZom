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
    
   

   
    private void Update()
    {
        if (weapon != null && targetEnemy != null)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer<=0)
            {
                shootTimer = weapon.shootTimerMax;
                Shoot();
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
        }
       
    }

    public void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

}
