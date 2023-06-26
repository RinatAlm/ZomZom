using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
 
    public float maxHealth;
    public Slider healthSlider;
    public float Health { get { return _health; } set { _health = value; } }
    private float _health;
    private void Start()
    {
        _health = maxHealth;
        healthSlider.gameObject.SetActive(false);
    }

    #region Idamagable Interface implementation
    public void TakeDamage(float damage)
    {
        _health -= damage;
        healthSlider.value = _health;//Displaying Health 
        if (_health == maxHealth)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }
        if (_health <= 0)
        {          
            GameManager.instance.GameOver();
        }
    }
    #endregion
}
