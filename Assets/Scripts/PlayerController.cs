using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float turnSpeed = 5.0f; // Speed of rotation
    public Camera cam;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
     


        // Smoothly rotate the player towards the mouse position
        SmoothRotateTowardsMouse();
    }
    // Handle movement input
    void MovePlayer()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sidewaysInput = Input.GetAxis("Horizontal");

        // Combine input into a single vector
        Vector3 movement = new Vector3(sidewaysInput, 0, forwardInput).normalized;

        // Apply force based on the normalized movement vector
        if (movement != Vector3.zero)
        {
            playerRB.AddRelativeForce(movement * speed);
        }
        else
        {
            playerRB.velocity = Vector3.zero;
        }
       

        // Cap the player's velocity to max speed
        if (playerRB.velocity.magnitude > speed)
        {
            playerRB.velocity = playerRB.velocity.normalized * speed;
        }
    }
    void SmoothRotateTowardsMouse()
    {
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));

        // Calculate the point of intersection between the line going through the camera and the mouse position with the XZ-Plane
        float t = cam.transform.position.y / (cam.transform.position.y - point.y);
        Vector3 finalPoint = new Vector3(t * (point.x - cam.transform.position.x) + cam.transform.position.x, 1, t * (point.z - cam.transform.position.z) + cam.transform.position.z);

        // Calculate the direction on the XZ plane (ignore Y component)
        Vector3 direction = new Vector3(finalPoint.x - transform.position.x, 0, finalPoint.z - transform.position.z);

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Rotate towards the target rotation at a constant speed
        float step = turnSpeed * Time.deltaTime; // Calculate the rotation step based on turn speed and frame time
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
    }

}
