using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("GunnerArms")]
    public GunnerArm leftBottom;
    public GunnerArm rightBottom;
    public GunnerArm leftTop;
    public GunnerArm rightTop;
    [Space(10)]

    [Header("Slots")]
    public GameObject leftBottomSlot;
    public GameObject rightBottomSlot;
    public GameObject leftTopSlot;
    public GameObject rightTopSlot;
    public GameObject exchangeSlot;
    [Space(10)]

    [Header("WeaponDescription")]
    public Text damageText;
    public Text delayPerShotText;
    public Text reloadText;
    public Text numberOfShotsText;
    public Text numberOfBulletsPerShotText;
    public Text penetrationText;
    public Text spreadAngleText;
    public Text bulletSpeedText;


    public GameObject descriptionPanels;
    public GameObject descriptionPanel;
    public GameObject weaponExchangeSlot;
    public GameObject exchangeButton;
    public List<WeaponSlot> weaponsExchange = new List<WeaponSlot>();
    public Sprite emptySprite;
    public bool isSwapping = false;

    public void SetWeapon()
    {
        leftBottom.weapon = leftBottomSlot.GetComponent<Weapon>();
        rightBottom.weapon = rightBottomSlot.GetComponent<Weapon>();
        leftTop.weapon = leftTopSlot.GetComponent<Weapon>();
        rightTop.weapon = rightTopSlot.GetComponent<Weapon>();
    }


    void Start()
    {
        exchangeButton.SetActive(false);
    }
    public void ShowWeaponInfo()
    {
        if(weaponsExchange.Count == 0)
        {
            DoNotShowDescription();
        }
        else
        {
            descriptionPanel.SetActive(true);
            descriptionPanels.SetActive(true);
            int index = 0;
            if (weaponsExchange[index].weapon.weaponSprite == emptySprite && weaponsExchange.Count == 1)
            {
                DoNotShowDescription();
            }
            else if (weaponsExchange[index].weapon.weaponSprite != emptySprite && weaponsExchange.Count == 1)
            {
                ShowDescription(index);
            }
            else if (weaponsExchange[index].weapon.weaponSprite == emptySprite && weaponsExchange.Count == 2)
            {
                index++;//Incrementing index
                if (weaponsExchange[index].weapon.weaponSprite == emptySprite)
                {
                    DoNotShowDescription();
                }
                else
                {
                    descriptionPanel.SetActive(false);
                    exchangeButton.SetActive(true);
                    ShowDescription(index--, index);
                }
            }
            else if (weaponsExchange[index].weapon.weaponSprite != emptySprite && weaponsExchange.Count == 2)
            {              
                index++;
                if (weaponsExchange[index].weapon.weaponSprite == emptySprite)
                {                    
                    descriptionPanel.SetActive(false);
                    exchangeButton.SetActive(true);
                    ShowDescription(index--, index);
                }
                else
                {
                    descriptionPanel.SetActive(false);
                    exchangeButton.SetActive(true);
                    ShowDescription(index--, index);
                }
            }
            else
            {
                Debug.LogWarning("Error");
            }


        }

    }


    public void ShowDescription(int index)
    {
        damageText.text = "Damage:" + weaponsExchange[index].weapon.damage.ToString();
        reloadText.text = "Reload time:" + weaponsExchange[index].weapon.shootTimerMax.ToString();
        bulletSpeedText.text = "Bullet speed:" + weaponsExchange[index].weapon.bulletSpeed.ToString();
        numberOfBulletsPerShotText.text = "Bullets/Shot:" + weaponsExchange[index].weapon.numberOfTriggerPressing.ToString();
        numberOfShotsText.text = "Shots:" + weaponsExchange[index].weapon.numberOfTriggerPressing.ToString();
        penetrationText.text = "Penetration:" + weaponsExchange[index].weapon.penetration.ToString();
        delayPerShotText.text = "Delay:" + weaponsExchange[index].weapon.delay.ToString();
        spreadAngleText.text = "Spread:" + weaponsExchange[index].weapon.spreadAngle.ToString();
    }
    public void ShowDescription(int index2,int index1)
    {
        damageText.text = "Damage:" + weaponsExchange[index1].weapon.damage.ToString() +  "->" + weaponsExchange[index2].weapon.damage.ToString();
        reloadText.text = "Reload time:" + weaponsExchange[index1].weapon.shootTimerMax.ToString() + "->" + weaponsExchange[index2].weapon.shootTimerMax.ToString();
        bulletSpeedText.text = "Bullet speed:" + weaponsExchange[index1].weapon.bulletSpeed.ToString() + "->" + weaponsExchange[index2].weapon.bulletSpeed.ToString();
        numberOfBulletsPerShotText.text = "Bullets/Shot:" + weaponsExchange[index1].weapon.numberOfTriggerPressing.ToString() + "->" + weaponsExchange[index2].weapon.numberOfTriggerPressing.ToString();
        numberOfShotsText.text = "Shots:" + weaponsExchange[index1].weapon.numberOfTriggerPressing.ToString() + "->" + weaponsExchange[index2].weapon.numberOfTriggerPressing.ToString();
        penetrationText.text = "Penetration:" + weaponsExchange[index1].weapon.penetration.ToString() + "->" + weaponsExchange[index2].weapon.penetration.ToString();
        delayPerShotText.text = "Delay:" + weaponsExchange[index1].weapon.delay.ToString() + "->" + weaponsExchange[index2].weapon.delay.ToString();
        spreadAngleText.text = "Spread:" + weaponsExchange[index1].weapon.spreadAngle.ToString() + "->" + weaponsExchange[index2].weapon.spreadAngle.ToString();
    }
    public void DoNotShowDescription()
    {
        descriptionPanels.SetActive(false);
    }

    public void SwapWeapons()
    {
        //Replacing Weapons 
        isSwapping = true;
        Weapon tempWeapon = weaponsExchange[0].weapon;
        weaponsExchange[0].weapon = weaponsExchange[1].weapon;
        weaponsExchange[1].weapon = tempWeapon;
        for (int i = 0,j=0; i < 2; i++)
        {
            weaponsExchange[j].SetWeaponImage();
            weaponsExchange[j].Select();
        }
        exchangeButton.SetActive(false);
        descriptionPanels.SetActive(false);
        isSwapping = false;
    }

}
