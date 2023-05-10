using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class WeaponSlot : MonoBehaviour
{
    public Weapon weapon;
    public Image WeaponImageUI;

    [Header("Slot Interaction")]
    public Sprite NotActiveSlotSprite;
    public Sprite ActiveSlotSprite;
    public bool isSelected = false;
    public InventoryManager inventoryManager;
    Button button;

    private void Start()
    {       
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => { Select(); });
        SetWeaponImage();

    }
    public void Select()
    {
        isSelected = !isSelected;
        if(isSelected)
        {
            gameObject.GetComponent<Image>().sprite = ActiveSlotSprite;
            inventoryManager.weaponsExchange.Add(this);
            if(!inventoryManager.isSwapping)
            inventoryManager.ShowWeaponInfo();
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = NotActiveSlotSprite;
            inventoryManager.weaponsExchange.Remove(this);
            if (!inventoryManager.isSwapping)
                inventoryManager.ShowWeaponInfo();
        }
       
    }


    [ContextMenu("SetImage")]
    public void SetWeaponImage()
    {
        if (weapon.weaponSprite != inventoryManager.emptySprite)
            WeaponImageUI.sprite = weapon.weaponSprite;
        else
            WeaponImageUI.sprite = inventoryManager.emptySprite;
    }     
    }
