using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> objectsToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }
    void Start()//Instantiate objects
    {
        
        pooledObjects = new List<GameObject>();
        GameObject tmp;      
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectsToPool[0]);

                tmp.SetActive(false);

                pooledObjects.Add(tmp);
            }
    }

    public GameObject GetPooledObject()//Get objects from the list
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

}
