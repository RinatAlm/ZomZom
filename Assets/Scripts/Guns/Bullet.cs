using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : PoolableObject
{
    public Rigidbody bulletBody;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    void Update()
    {
    
       // progress += Time.deltaTime * speed;
       // transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
      
    }
    
    public void SetStartPosition(Vector3 startPosition)
    {
        this.startPosition = startPosition;
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        bulletBody.velocity = Vector3.zero;
    }
}
