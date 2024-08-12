using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRB;
    private GameObject player;
    [SerializeField] private float speed;
   

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
       if (enemyRB.velocity.magnitude < speed )
        {
            enemyRB.AddForce(lookDirection * speed);
        }
        
        // player.transform.position = transform.position;
    }
}
