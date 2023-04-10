using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject gunnerArm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            gunnerArm.GetComponent<GunnerArm>().SetTarget(enemy);
            Debug.Log(enemy.transform.position);
        }
    }


}
