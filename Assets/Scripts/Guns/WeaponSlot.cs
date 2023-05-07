using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class WeaponSlot : MonoBehaviour
{
    public Weapon weapon;
    public Image WeaponImageUI;
    public GameObject pickedObject;
    public Button button;

    private void Start()
    {
        pickedObject = gameObject;
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => { LogName(pickedObject); });
    }
    public void LogName(GameObject pickedObject)
    {
        Debug.Log(pickedObject);
    }


    [ContextMenu("SetImage")]
    public void SetWeaponImage()
    {
        WeaponImageUI.sprite = weapon.weaponSprite;
    }     
    }
