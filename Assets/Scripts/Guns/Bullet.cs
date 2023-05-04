using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : AutoDestroyPoolableObject
{
    public Rigidbody bulletBody;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public GunnerArm bulletOfArm;
    public int numOfAimsToDestr;
       
    public void SetStartPosition(Vector3 startPosition)
    {
        this.startPosition = startPosition;
    }
    public override void OnDisable()
    {
        base.OnDisable();       
        bulletBody.velocity = Vector3.zero;
    }
}
