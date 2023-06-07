using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSmth : MonoBehaviour
{
    [SerializeField]
    float minRad = 2;
    [SerializeField]
    float maxRad = 5;
    [SerializeField]
    private float sphereCheckRadius;
    public Vector3 spawnPos;
    public GameObject prefab;
    



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spawnPos = transform.position + CalculateSpawnPosition();
            Collider[] hitColliders = Physics.OverlapSphere(spawnPos, sphereCheckRadius);
            if (hitColliders.Length == 1)//Mesh is in count
            {
                Instantiate(prefab, spawnPos, prefab.transform.rotation);
            }
        }      
    }
    Vector3 CalculateSpawnPosition()
    {
        Vector3 offset = Random.onUnitSphere * Random.Range(minRad, maxRad);
        return new Vector3(offset.x,0,offset.z);//Special offset
    }
}
