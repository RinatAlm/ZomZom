using System.Collections;
using System.Collections.Generic;
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


   public void SetWeapon()
    {
        leftBottom.weapon = leftBottomSlot.GetComponent<Weapon>();
        rightBottom.weapon = rightBottomSlot.GetComponent<Weapon>();
        leftTop.weapon = leftTopSlot.GetComponent<Weapon>();
        rightTop.weapon = rightTopSlot.GetComponent<Weapon>();
    }
   




}
