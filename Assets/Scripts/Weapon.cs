using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Gun Specifications")]
        [SerializeField] private bool allowButtonHold;
        [SerializeField] private float range, reloadTime, timeBetweenShooting, timeBetweenShots;
        [SerializeField] private int magazineSize, bulletsPerTap;
        [SerializeField] private int bulletsLeft, bulletsShot;


        [Header("Weapon States")]
        //bools 
        public bool isShooting;
        public bool isReadyToShoot, isReloading;

        //Reference
        [Header("References")]
        public Transform attackPoint;
        public LayerMask whatIsEnemy;


        [Header("Laser Settings")]
        public Laser laser;
        [SerializeField] private float laserRange;


        // Default behavior
        protected virtual void ShootWeapon(RaycastHit hit)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        protected void ReloadWeapon()
        {
            isReloading = true;
            Invoke("Reload", reloadTime);
        }
       protected void Reload()
        {
            bulletsLeft = magazineSize;
            isReloading = false;
        }
    }
    
}
