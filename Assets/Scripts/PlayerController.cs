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

    private bool ableJump = true;

    [SerializeField]
    GameObject startingPos;

    [SerializeField]
    GameObject flagPos;

    private bool canRespawn = true;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerRB.transform.position = startingPos.transform.position;
    }

    
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.Space) && ableJump)
        {
            playerRB.AddForce(Vector2.up * jump, ForceMode2D.Impulse);

            ableJump = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRB.AddForce(Vector2.left * speed, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerRB.AddForce(Vector2.right * speed, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !ableJump)
        {
            ableJump = true;
        }
        if (collision.gameObject.CompareTag("StaticBad") && canRespawn)
        {
            playerRB.transform.position = flagPos.transform.position;
            canRespawn = false;
        }
        else if (collision.gameObject.CompareTag("StaticBad") && !canRespawn)
        {
            Debug.Log("Death at " + ((float)Time.deltaTime) + " seconds in");
            playerRB.transform.position = flagPos.transform.position;
        }
    }
}
