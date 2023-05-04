using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EnemyHealth
{
    public GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(health<=0)
        {
            
            gameManager.GameOver();
        }
    }
}
