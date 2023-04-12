using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy> ();
    public List<GameObject> gunnerArms = new List<GameObject> ();
    private void OnTriggerEnter(Collider other)//Mark enemy on entering the trigger
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            for (int i = 0;i<gunnerArms.Count;i++)
            {
                if (gunnerArms[i].GetComponent<GunnerArm>().targetEnemy == null)
                {
                    gunnerArms[i].GetComponent<GunnerArm>().SetTarget(enemy);
                    break;
                }
            }
               


            /*
            foreach(GameObject arm in gunnerArms)
            {
                if(arm.GetComponent<GunnerArm>().targetEnemy==null)
                {
                    arm.GetComponent<GunnerArm>().SetTarget(enemy);
                }
            }   
            */
            enemies.Add(enemy);
        }
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            foreach (GameObject arm in gunnerArms)
            {
                if (arm.GetComponent<GunnerArm>().targetEnemy == enemy)
                {
                    arm.GetComponent<GunnerArm>().SetTarget(null);
                }
            }
            enemies.Remove(enemy);
        }
    }



}
