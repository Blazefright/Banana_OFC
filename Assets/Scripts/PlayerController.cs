using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField]
    private float speed = 2.5f;

    [SerializeField]
    private float jump = 2.5f;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); 
    }

    
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerRB.AddForce(Vector2.left * speed, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerRB.AddForce(Vector2.right * speed, ForceMode2D.Force);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }
}
