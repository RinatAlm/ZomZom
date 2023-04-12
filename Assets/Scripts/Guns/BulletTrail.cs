using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private float progress;
    [SerializeField] private float speed = 1f;
   
    void Update()
    {
    
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
      
    }
    
    public void SetStartPosition(Vector3 startPosition)
    {
        this.startPosition = startPosition;
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
