using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerArm : MonoBehaviour
{
    public Weapon weapon;
    public Enemy targetEnemy;
    public Vector3 direction;



    private void Start()
    {
        
    }

   
    private void Update()
    {
        if (weapon != null && targetEnemy != null)
        {
            Shoot();
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
            }
        }
       
    }

    public void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

}
