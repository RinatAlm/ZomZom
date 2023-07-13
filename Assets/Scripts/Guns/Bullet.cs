using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : AutoDestroyPoolableObject
{
    public Rigidbody bulletBody;
    public GunnerArm bulletOfArm;
    public int numOfAimsToDestr;
    public bool isRicochetBullet;
    public bool isExplosive;
    private Collider[] _hitColliders;
    private float _sphereCheckRadius = 2;
    private Vector3 direction;

    public float BulletHit()
    {
        //Do smth on hit
        if (isRicochetBullet)
            Ricochet();
        if (isExplosive)
            Boom();
       
        return bulletOfArm.weapon.damage;
    }

    public void Ricochet()
    {
        //Play Sound
        _hitColliders = Physics.OverlapSphere(transform.position, _sphereCheckRadius);
        foreach (Collider col in _hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                direction = col.gameObject.transform.position - transform.position;
                if (numOfAimsToDestr > 0)
                {
                    bulletBody.velocity = direction.normalized * bulletOfArm.weapon.bulletSpeed;
                    break;
                }
            }
        }
       
    }
    public void Boom()
    {
        GameObject explosion = Instantiate(bulletOfArm.weapon.boomPrefab, transform.position, Quaternion.Euler(90, 0, 0));
        explosion.GetComponent<Animator>().Play("Explosion");
        Collider[] colliders = Physics.OverlapSphere(explosion.transform.position,0.5f);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletOfArm.weapon.damage);
            }
        }
        Shake.instance.ShakeScreen();
        Destroy(explosion, 1.23f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }       
    }
}
