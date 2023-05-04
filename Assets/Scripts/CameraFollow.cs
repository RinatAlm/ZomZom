using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void FixedUpdate()
    {
        
        transform.position = new Vector3(player.transform.position.x,gameObject.transform.position.y , player.transform.position.z);
    }
}
