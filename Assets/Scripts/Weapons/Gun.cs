using System.Collections;
using UnityEngine;
using Weapons;

public class Gun : MonoBehaviour
{
    [Header("Gun Specifications")]
    [SerializeField] private bool allowButtonHold;
    [SerializeField] private float range, reloadTime, timeBetweenShooting, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private int ammoCount;

    [Header("Weapon States")]
    public bool isShooting;
    public bool isReadyToShoot, isReloading;

    [Header("References")]
    public Transform attackPoint;
    public LayerMask whatIsEnemy;

    [Header("Laser Settings")]
    public Laser laser;
    [SerializeField] private float laserRange;

    private void Start()
    {
        ammoCount = magazineSize;
        isReadyToShoot = true;
        if (laser != null)
        {
            laser.SetUpLaser(laserRange, transform);
        }
    }

    private void Update()
    {
        RaycastHit hit = laser.RenderLaser();
        HandleInput();

        if (isShooting && isReadyToShoot && !isReloading && ammoCount > 0)
        {
            StartCoroutine(HandleShooting());
        }

        if (ammoCount <= 0 && !isReloading)
        {
            ReloadWeapon();
        }
    }

    private void HandleInput()
    {
        if (allowButtonHold)
            isShooting = Input.GetKey(KeyCode.Mouse0);
        else
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
    }

    private IEnumerator HandleShooting()
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
            Destroy(hit.collider.gameObject);
        }
    }

    private void ResetShot()
    {
        isReadyToShoot = true;
    }

    private void ReloadWeapon()
    {
        isReloading = true;
        Invoke("Reload", reloadTime);
    }

    private void Reload()
    {
        ammoCount = magazineSize;
        isReloading = false;
        ResetShot();
    }
}
