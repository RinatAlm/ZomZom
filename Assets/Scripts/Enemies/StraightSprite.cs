using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightSprite : MonoBehaviour
{
    void FixedUpdate()//Script to hold sprite straight to the user
    {
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
