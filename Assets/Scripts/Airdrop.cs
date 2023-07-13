using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

public class Airdrop : MonoBehaviour
{
    private Weapon _weapon;
    public NavMeshAgent box;
    //AirdropArrow
    public Transform playerTransform;
    public Camera _camera;
    public GameObject ArrowObjectSet;
    public Transform worldPointer;
    public Transform arrowIconTransform;
    public PointerIcon pointerIcon;
    public float arrowOffset = 1f;
    public bool isReady;


    private void FixedUpdate()
    {
        if(isReady)
        {
            Vector3 fromIconToAirdrop = transform.position - worldPointer.position;
            Vector3 fromPlayerToAirdrop = transform.position - playerTransform.position;
            Ray ray = new Ray(playerTransform.position, fromPlayerToAirdrop);
            Debug.DrawRay(playerTransform.position, fromPlayerToAirdrop);

            //[0]=Left, [1]=Right, [2] = Down, [3] = Up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            float minDistance = Mathf.Infinity;
            for (int i = 0; i < 4; i++)
            {
                if (planes[i].Raycast(ray, out float distance))
                {
                    if (distance < minDistance)
                        minDistance = distance;
                }
            }

            minDistance = Mathf.Clamp(minDistance, 0, arrowOffset);
            Vector3 worldPosition = ray.GetPoint(minDistance);
            worldPointer.position = worldPosition;
            ArrowObjectSet.transform.position = _camera.WorldToScreenPoint(worldPosition);
            arrowIconTransform.transform.rotation = Quaternion.FromToRotation(Vector2.right, new Vector2(fromIconToAirdrop.x, fromIconToAirdrop.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            OpenAirdropBox();
        }
    }

    private void OpenAirdropBox()
    {
        _weapon = GenerateWeapon();
        GameManager.instance.isAirbox= true;      
        GameManager.instance.OpenInventory();
        InventoryManager.instance.weaponExchangeButton.GetComponent<WeaponSlot>().weapon = _weapon;
        InventoryManager.instance.weaponExchangeButton.GetComponent<WeaponSlot>().SetWeaponImage();
        InventoryManager.instance.exchangeButton.SetActive(true);

        ArrowObjectSet.SetActive(false);
    }

    public Weapon GenerateWeapon()
    {
        GameObject weapon = Instantiate(GameManager.instance.weapons[Random.Range(0, GameManager.instance.weapons.Count)], transform.position, transform.rotation);
        weapon.transform.SetParent(GameManager.instance.guns.transform);
        return weapon.GetComponent<Weapon>();
    }

}
