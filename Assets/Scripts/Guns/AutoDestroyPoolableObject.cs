using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyPoolableObject : PoolableObject
{
    public float AutoDestroyTime = 3;
    private const string DisableMethodName = "Disable";

    public virtual void OnEnable()
    {
        CancelInvoke(DisableMethodName);
        Invoke(DisableMethodName, AutoDestroyTime);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

}
