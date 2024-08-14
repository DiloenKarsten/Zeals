using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform primaryWeapon;
    private Gun currentWeapon;

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
    }

    public void EquipWeapon(string weaponName)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        GameObject weaponPrefab = Resources.Load<GameObject>($"Prefabs/Weapons/{weaponName}");
        Debug.Log($"Loading weapon from path: Prefabs/Weapons/{weaponName}");
        if (weaponPrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, primaryWeapon.position, primaryWeapon.rotation, primaryWeapon);
            currentWeapon = weaponInstance.GetComponent<Gun>();
            if (currentWeapon == null)
            {
                Debug.LogError("No Gun component found on the weapon prefab.");
            }
        }
        else
        {
            Debug.LogError($"Weapon prefab not found: {weaponName}");
        }

    }
}
