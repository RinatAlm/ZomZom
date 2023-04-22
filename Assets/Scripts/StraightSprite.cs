using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightSprite : MonoBehaviour
{
    [SerializeField]
    private Transform follow = null;
    
    void FixedUpdate()//Script to hold sprite straight to the user
    {
        transform.position = follow.position;
    }
}
