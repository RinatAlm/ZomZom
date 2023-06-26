using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public float Health { get { return _health; } set { _health = value; } }
    public float maxHealth;
    public Slider healthSlider;
    private float _health;
    private void Start()
    {
        Health = maxHealth;
        healthSlider.gameObject.SetActive(false);
    }

    #region Idamagable Interface implementation
    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthSlider.value = Health;//Displaying Health 
        if (Health == maxHealth)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }     
    }
    #endregion
}
