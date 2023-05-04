using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{  
    public float health = 100f;
    public float maxHealth;
    public Slider healthSlider;
    private void Start()
    {
        maxHealth = health;
        healthSlider.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;//Displaying Health 
        if (health == maxHealth)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }     
    }
}
