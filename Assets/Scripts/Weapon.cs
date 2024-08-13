using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        // Common properties
        public float damage;
        public float Range;
        public float ReloadTime;
        public int AmmoCount;
        public float ShootingDelay;
        public Transform LaserOrigin;
        public bool IsShooting;
        public bool IsReloading;
        public bool IsAutomatic;

        // Abstract methods


        // Default behavior
        protected void ShootWeapon(RaycastHit hit)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        protected void UpdateReloadState()
        {
            // Shared reload logic
        }
    }
    
}
