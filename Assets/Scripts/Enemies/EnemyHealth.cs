using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public float Health { get { return _health; } set { _health = value; } }
    public float maxHealth;
    public float hidingTime;
    public Slider healthSlider;
    private float _health;
    private Enemy _thisEnemy;
    private void Start()
    {
        Health = maxHealth;
        _thisEnemy = GetComponent<Enemy>();
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
            healthSlider.gameObject.SetActive(true);
        }
        StartCoroutine(HealthHidingCoroutine());
    }

    public IEnumerator HealthHidingCoroutine()
    {
        yield return new WaitForSeconds(hidingTime);
        healthSlider.gameObject.SetActive(false);
        _thisEnemy.gunManagers.Clear();
    }
    #endregion
}
