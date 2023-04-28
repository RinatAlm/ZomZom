using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnManager : MonoBehaviour
{
    public int numberOfBulletsToSpawn;
    public List<Bullet> bulletPrefabs = new List<Bullet>();
    private Dictionary<int, ObjectPool> bulletObjectPools = new Dictionary<int, ObjectPool>();
    private void Start()
    {
        for (int i = 0; i < bulletPrefabs.Count; i++)
        {
            bulletObjectPools.Add(i, ObjectPool.CreateInstance(bulletPrefabs[i], numberOfBulletsToSpawn));
        }

    }
    
    public void DoSpawnBullet(GunnerArm gunnerArm,Vector3 InitialPosition,Vector3 direction)
    {
        PoolableObject poolableObject = bulletObjectPools[gunnerArm.weapon.bulletIndex].GetObject();
        if (poolableObject != null)
        {
            Bullet bullet = poolableObject.GetComponent<Bullet>();
            //Actions
            bullet.bulletOfArm = gunnerArm;
            bullet.transform.position = InitialPosition;
            bullet.bulletBody.velocity = direction  * gunnerArm.weapon.bulletSpeed;

        }
        else
        {
            Debug.LogWarning("Error -> no poolable objects");
        }
    }
    
}
