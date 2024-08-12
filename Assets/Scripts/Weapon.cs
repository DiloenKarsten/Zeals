using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Laser Settings")] 
    public LineRenderer lineRenderer;
    [SerializeField] float laserRange;
   
    void Start()
    {
        RunDebug();

       
    }

 
    void FixedUpdate()
    {
        if (lineRenderer == null) return;

        RaycastHit hit;

        // Use the Gun's position as the start of the laser
        // Ensure LineRenderer is set to world position
        lineRenderer.SetPosition(0, transform.position);

        // Cast a ray from the Gun's position in the forward direction
        if (Physics.Raycast(transform.position, transform.forward, out hit, laserRange))
        {
            // Set the end position of the LineRenderer to the point where the ray hit
            lineRenderer.SetPosition(1, hit.point);
            
        }
        else
        {
            // If the ray doesn't hit anything, set the end point at the max range
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserRange);
        }
    }

    private void RunDebug()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned in the Inspector.");
            return;
        }

    }
}
