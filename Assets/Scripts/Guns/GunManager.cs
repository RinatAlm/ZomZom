using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy> ();
    public List<GameObject> gunnerArms = new List<GameObject> ();
    public Sprite emptySprite;
    public GameManager gameManager;
    
    private void OnTriggerEnter(Collider other)//Mark enemy on entering the trigger
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            for (int i = 0;i<gunnerArms.Count;i++)
            {
                if (gunnerArms[i].GetComponent<GunnerArm>().targetEnemy == null && gunnerArms[i].GetComponent<GunnerArm>().weapon.weaponSprite != emptySprite)
                {
                    gunnerArms[i].GetComponent<GunnerArm>().SetTarget(enemy);
                    break;
                }
            }
            enemies.Add(enemy);
        }
    }

    private void Update()
    {
        foreach (GameObject arm in gunnerArms)
        {
            if (arm.GetComponent<GunnerArm>().targetEnemy == null && enemies.Count !=0 && arm.GetComponent<GunnerArm>().weapon.weaponSprite != emptySprite)
            {
                arm.GetComponent<GunnerArm>().SetTarget(enemies[Random.Range(0,enemies.Count)]);//Take first enemy from the list
            }
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

    public void RemoveTarget(Enemy enemy)
    {
        enemies.Remove(enemy);
    }



}
