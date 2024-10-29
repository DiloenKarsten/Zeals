using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Specifications")]
    public bool allowButtonHold;
    [SerializeField] private float damage, range, reloadTime, timeBetweenShooting, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    public int ammoCount;

    [Header("Weapon States")]
    public bool isShooting;
    public bool isReadyToShoot, isReloading;

    [Header("References")]
    public LayerMask whatIsEnemy;

    [Header("Laser Settings")]
    public Laser laser;

    private WeaponController weaponController;


    private void Start()
    {
        weaponController = GetComponentInParent<WeaponController>();
        ammoCount = magazineSize;
        isReadyToShoot = true;
        if (laser != null)
        {
            laser.SetUpLaser(range, transform);
        }
    }

    private void Update()
    {
        if (this == weaponController.CurrentWeapon) 
        {
            laser.RenderLaser();
        }
        
         
    }

    public IEnumerator HandleShooting()
    {
        isReadyToShoot = false;

        for (int i = 0; i < bulletsPerTap; i++)
        {
            if (ammoCount <= 0) break;

            HandleSingleShot();

            yield return new WaitForSeconds(timeBetweenShots);
        }

        yield return new WaitForSeconds(timeBetweenShooting);
        ResetShot();
    }

    private void HandleSingleShot()
    {
        ammoCount--;

        if (laser != null)
        {
            RaycastHit hit = laser.RenderLaser();
            ShootWeapon(hit);
        }
    }

    private void ShootWeapon(RaycastHit hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Enemy zeal = hit.collider.GetComponent<Enemy>();
            weaponController.Currency += zeal.takeDamage(damage);
            // zeal.BloodEffect();


        }
    }

    private void ResetShot()
    {
        isReadyToShoot = true;
    }

    public void ReloadWeapon()
    {
        isReloading = true;
        Invoke(nameof(Reload), reloadTime);
    }

    private void Reload()
    {
        ammoCount = magazineSize;
        isReloading = false;
        ResetShot();
    }
}
