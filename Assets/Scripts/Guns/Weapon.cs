using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon : MonoBehaviour
{  
    public float damage;
    public GameObject bullet;
    public float range = 10;
    public float shootTimerMax = 0.5f;
    public float numberOfShots = 3;
    public float delay = 0.1f;
    public int bulletIndex;
    public int bulletSpeed;
    
    public void SetPosition(Vector3 startPos)//Setting positions for bullets
    {
        bullet.GetComponent<Bullet>().SetStartPosition(startPos);       
    }


}
