using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
   
    public GameObject player;



    void LateUpdate()
    {
      
       
        transform.position = player.transform.position+offset;
        // Make the camera look at the player
        
    }
}
