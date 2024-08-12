using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Laser Settings")]
    public Laser laser;
    [SerializeField] private float laserRange;

    private void Start()
    {
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
                FireGun(hit);
            }
        }
    }

    private void FireGun(RaycastHit hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Destroy(hit.collider.gameObject);
        }
    }
}