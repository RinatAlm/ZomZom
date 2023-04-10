using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightSprite : MonoBehaviour
{
    [SerializeField]
    private Transform follow = null;
    
    void FixedUpdate()
    {
        transform.position = follow.position;
    }
}
