using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{
    private PoolableObject prefab;
    private List<PoolableObject> availableObjects = new List<PoolableObject>();

    private ObjectPool(PoolableObject prefab,int size)
    {
        this.prefab = prefab;
        availableObjects = new List<PoolableObject>(size);
    }

    public static ObjectPool CreateInstance(PoolableObject prefab,int size)
    {
        ObjectPool pool = new ObjectPool(prefab,size);
        GameObject poolObjects = new GameObject(prefab.name + " Pool");
        pool.CreateObjects(poolObjects.transform,size);

        return pool;
    }

    private void CreateObjects(Transform parent,int size)
    {
        for(int i = 0; i < size; i++)
        {
            PoolableObject poolableObject = GameObject.Instantiate(prefab, prefab.transform.position, Quaternion.identity, parent.transform);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }

    public void ReturnObjectToPool(PoolableObject poolableObject)
    {
        availableObjects.Add(poolableObject);
       
    }

    public PoolableObject GetObject()
    {
        
        if (availableObjects.Count > 0)
        {
            PoolableObject instance = availableObjects[0];
            availableObjects.RemoveAt(0);
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            Debug.LogWarning("Error could not get object from pool");
            return null;

        }

    }
   
}
