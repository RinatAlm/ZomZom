using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Airdrop : MonoBehaviour
{
    public Weapon weapon;
    public GameManager gameManager;
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


    private void LateUpdate()
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
            weapon = GenerateWeapon();
            gameManager.inventoryManager.weaponExchangeSlot.GetComponent<WeaponSlot>().weapon = weapon;
            gameManager.inventoryManager.weaponExchangeSlot.GetComponent<WeaponSlot>().SetWeaponImage();
            ArrowObjectSet.SetActive(false);
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
