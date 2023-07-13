using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public Sprite weaponSprite;
    public float damage;
    public GameObject bullet;
    public float shootTimerMax = 0.5f;
    public float numberOfTriggerPressing = 3;
    public float delay = 0.1f;
    public int bulletIndex;
    public int bulletSpeed;
    public float spreadAngle;
    public float numOfBulletsPerPressing;
    public int penetration;
    public Sound shootSound;
    public string weaponDescription;
    public bool isRickochetBullet;
    public GameObject boomPrefab;

    public void Start()
    {
        shootSound.source = gameObject.AddComponent<AudioSource>();
        shootSound.source.clip = shootSound.clip;
        shootSound.source.volume = shootSound.volume;
        shootSound.source.pitch = shootSound.pitch;
        shootSound.source.loop = shootSound.loop;
    }
    public void Play()
    {       
        if (shootSound == null)
            return;
        shootSound.source.Play();
    }
}
