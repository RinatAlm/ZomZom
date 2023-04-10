using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon : MonoBehaviour
{  
    public float damage;
    public float shootTimer;
    public GameObject bulletTrail;
    public float range = 10;

    public void SetPosition(Vector3 startPos)//Setting positions for bullets
    {
        bulletTrail.GetComponent<BulletTrail>().SetStartPosition(startPos);       
    }


}
