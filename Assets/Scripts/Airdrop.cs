using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Airdrop : MonoBehaviour
{
    public Weapon weapon;
    public GameManager gameManager;  
    public NavMeshAgent box;
   
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            weapon = GenerateWeapon();
            gameManager.inventoryManager.weaponExchangeSlot.GetComponent<WeaponSlot>().weapon = weapon;
            gameManager.inventoryManager.weaponExchangeSlot.GetComponent<WeaponSlot>().SetWeaponImage();
            gameManager.OpenInventory();
            gameManager.inventoryManager.weaponExchangeSlot.SetActive(true);
        }
    }

   public Weapon GenerateWeapon()
    {
        GameObject weapon = Instantiate(gameManager.weapons[Random.Range(0, gameManager.weapons.Count)], transform.position, transform.rotation);
        weapon.transform.SetParent(gameManager.guns.transform);
        return weapon.GetComponent<Weapon>();
    }

}
