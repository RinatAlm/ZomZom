using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public ObjectPool Parent;

    public virtual void OnDisable()
    {
        Debug.Log("Back");
        Parent.ReturnObjectToPool(this);
    }
}
