using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class WeaponSlot : MonoBehaviour
{
    public Weapon weapon;
    public Image WeaponImageUI;

    [Header("Slot Interaction")]
    public bool isSelected = false;
    Button button;

    private void Start()
    {       
        button = gameObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => { Select(); });
        SetWeaponImage();

    }
    public void Select()
    {
        isSelected = !isSelected;
        if(isSelected)
        {
            gameObject.GetComponent<Image>().color = InventoryManager.instance.activeSlotColor;
            InventoryManager.instance.weaponsExchange.Add(this);
            if(!InventoryManager.instance.isSwapping)
                InventoryManager.instance.ShowWeaponInfo();
        }
        else
        {
            gameObject.GetComponent<Image>().color = InventoryManager.instance.inactiveSlotColor;
            InventoryManager.instance.weaponsExchange.Remove(this);
            if (!InventoryManager.instance.isSwapping)
                InventoryManager.instance.ShowWeaponInfo();
        }
       
    }


    [ContextMenu("SetImage")]
    public void SetWeaponImage()
    {
        if (weapon.weaponSprite != InventoryManager.instance.emptySprite)
            WeaponImageUI.sprite = weapon.weaponSprite;
        else
            WeaponImageUI.sprite = InventoryManager.instance.emptySprite;
    }     
    }
