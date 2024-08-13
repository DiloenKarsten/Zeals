using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Gun : MonoBehaviour
{
    [Header("Gun Specifications")]
    [SerializeField] private bool allowButtonHold;
    [SerializeField] private float  range, reloadTime, timeBetweenShooting, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private int bulletsLeft, bulletsShot;


    [Header("Weapon States")]
    //bools 
    public bool isShooting;
    public bool isReadyToShoot , isReloading;

    //Reference
    [Header("References")]
   public Transform attackPoint;
   public LayerMask whatIsEnemy;


    [Header("Laser Settings")]
    public Laser laser;
    [SerializeField] private float laserRange;

    private void Start()
    {
        isReadyToShoot = true;
        if (laser != null)
        {
            laser.SetUpLaser(laserRange, transform);
        }
    }

    private void Update()
    {
        if (laser != null)
        {
            var hit = laser.RenderLaser();
            if (Input.GetMouseButtonDown(0))
            {
                if (isReadyToShoot)
                {
                    isReadyToShoot = false;
                    ShootWeapon(hit);
                    Invoke("ResetShot", timeBetweenShooting);
                }
                
            }
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
        bulletsLeft = magazineSize;
        isReloading = false;
    }
}
