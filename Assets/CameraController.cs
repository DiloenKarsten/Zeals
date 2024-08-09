using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public GameObject player;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        //transform.rotation = transform.rotation * Quaternion.Euler(0, playerController.PlayerAngle(), 0);
    }
}
