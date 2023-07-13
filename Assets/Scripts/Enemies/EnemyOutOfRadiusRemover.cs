using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOutOfRadiusRemover : MonoBehaviour
{
    public List<GameObject> enemies = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().Disable(false);
            enemies.Remove(other.gameObject);
        }
    }
}
