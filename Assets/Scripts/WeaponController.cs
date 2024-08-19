using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform primaryWeapon;
    public Transform secondaryWeapon;
    public int Currency;
    private Gun currentWeapon;
    private Gun spareWeapon;
    private bool isBuying;

    // Start is called before the first frame update
    void Awake()
    {
        EquipWeapon("Pistol");
       
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (currentWeapon.isShooting && currentWeapon.isReadyToShoot && !currentWeapon.isReloading && currentWeapon.ammoCount > 0)
        {
            StartCoroutine(currentWeapon.HandleShooting());
        }

        if (currentWeapon.ammoCount <= 0 && !currentWeapon.isReloading)
        {
            currentWeapon.ReloadWeapon();
        }
    }

    private void HandleInput()
    {
        if (currentWeapon.allowButtonHold)
            currentWeapon.isShooting = Input.GetKey(KeyCode.Mouse0);
        else
            currentWeapon.isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        
        if (Input.GetKeyDown(KeyCode.Q))
            SwapWeapon();
        if (Input.GetKeyDown(KeyCode.R))
            currentWeapon.ReloadWeapon();
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(BuyDelay());
        }
       
       
    }
    IEnumerator BuyDelay()
    {
        isBuying = true;
        yield return new WaitForSeconds(0.2f);
        isBuying = false;
    }

    public void EquipWeapon(string weaponName)
    {
        GameObject weaponPrefab = Resources.Load<GameObject>($"Prefabs/Weapons/{weaponName}");
        Debug.Log($"Loading weapon from path: Prefabs/Weapons/{weaponName}");

        if (currentWeapon == null)
        {
            currentWeapon = weaponSlot(weaponPrefab, primaryWeapon);
        }
        else if (spareWeapon == null)
        {
            spareWeapon = weaponSlot(weaponPrefab, secondaryWeapon);
        }
    }

    public Gun weaponSlot(GameObject weaponPrefab, Transform pos)
    {
        Gun gunInstance = null;
        if (weaponPrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, pos.position, pos.rotation, pos);
            gunInstance = weaponInstance.GetComponent<Gun>();
            if (gunInstance == null)
            {
                Debug.LogError("No Gun component found on the weapon prefab.");
            }
        }
        return gunInstance;
    }
    public void SwapWeapon()
    {
        if (currentWeapon != null && spareWeapon != null)
        {
            // Swap the Gun references
            Gun tempGun = currentWeapon;
            currentWeapon = spareWeapon;
            spareWeapon = tempGun;

            // Re-parent the weapons to the correct slots
            currentWeapon.transform.SetParent(primaryWeapon);
            spareWeapon.transform.SetParent(secondaryWeapon);

            // Reset their local position and rotation to match the slot
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            spareWeapon.transform.localPosition = Vector3.zero;
            spareWeapon.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Store") && isBuying)
        {
            WeaponStore store = other.GetComponent<WeaponStore>();
            if (store == null)
            {
                Debug.Log("No Store component found on the object.");
            }
            else
            {
                if (Currency >= store.price)
                {
                    Currency -= store.price;
                    EquipWeapon(other.name); // Assuming you moved the EquipWeapon to WeaponStore
                    isBuying = false;
                }
                else
                {
                    Debug.Log("Not enough currency to buy this weapon.");
                }
            }
        }
    }

}
