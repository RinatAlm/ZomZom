using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public static Shake instance;
    public GameObject character;
    public float duration = 1f;
    public AnimationCurve curve;
    private bool _isShaking = false;
    Vector3 startPosition;
    Vector3 cameraOffset = new Vector3(0, 10, 0);

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        startPosition = character.transform.position + cameraOffset;
    }
    public void ShakeScreen()
    {
        //if(!_isShaking)
        {         
            StartCoroutine(Shaking());
        }     
       // else
        {
            //return;
        }
    }
    IEnumerator Shaking()
    {   
        float elapseTime = 0f;

        while (elapseTime < duration)
        {
            elapseTime+=Time.deltaTime;
            float strength = curve.Evaluate(elapseTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }

}
