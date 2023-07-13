using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
 
    public float maxHealth;
    public float regenPerSec;
    public float hidingTime;
    public Slider healthSlider;
    private bool isRegenerating;
    public float Health { get { return _health; } set { _health = value; } }
    private float _health;
    private void Start()
    {
        Health = maxHealth;
        healthSlider.gameObject.SetActive(false);
    }

    #region Idamagable Interface implementation
    public void TakeDamage(float damage)
    {
        StopCoroutine(HealthHidingCoroutine());
       
        Health -= damage;
        healthSlider.value = Health;//Displaying Health 
        if (Health == maxHealth)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            if(!isRegenerating)
            StartCoroutine(Regeneration());
            healthSlider.gameObject.SetActive(true);
        }
        if (Health <= 0)
        {          
            GameManager.instance.GameOver();
        }
        
    }

    public IEnumerator HealthHidingCoroutine()
    {
        yield return new WaitForSeconds(hidingTime);
        healthSlider.gameObject.SetActive(false);
    }
    #endregion

    IEnumerator Regeneration()
    {
        isRegenerating = true;
        while(Health<=maxHealth)
        {
            yield return new WaitForSeconds(1);
            Health += regenPerSec;
            healthSlider.value = Health;

        }
        isRegenerating = false;
        StartCoroutine(HealthHidingCoroutine());
    }
}
