using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBelongingsAndParams : MonoBehaviour
{
    public static PlayerBelongingsAndParams instance;
    public List<GameObject> damagableNonZombieObjects = new();
    private void Awake()
    {
        instance = this;
    }
}
