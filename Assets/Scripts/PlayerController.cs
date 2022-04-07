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

    private Animator playerAN;

    private SpriteRenderer playerSR;

    [SerializeField]
    int maxIdleTimer = 0;

    private bool canRespawn = true;

    void Start()
    {
        playerSR = GetComponent<SpriteRenderer>();
        playerAN = GetComponent<Animator>();
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
            playerAN.SetBool("OnGround", false);
            playerAN.SetBool("LongSit", false);
            maxIdleTimer = 0;

        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRB.AddForce(Vector2.left * speed, ForceMode2D.Force);
            playerAN.SetBool("Idle", false);
            playerAN.SetBool("Walking", true);
            playerSR.flipX = true;
            playerAN.SetBool("LongSit", false);
            maxIdleTimer = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerRB.AddForce(Vector2.right * speed, ForceMode2D.Force);
            playerAN.SetBool("Idle", false);
            playerAN.SetBool("Walking", true);
            playerSR.flipX = false;
            playerAN.SetBool("LongSit", false);
            maxIdleTimer = 0;
        }
        else
        {
            playerAN.SetBool("Walking", false);
            playerAN.SetBool("Idle", true);
        }

        if (playerRB.velocity.y > 0.1f)
        {
            playerAN.SetBool("Idle", false);
            playerAN.SetBool("JumpUp", true);
            playerAN.SetBool("Falling", false);
            playerAN.SetBool("LongSit", false);
            maxIdleTimer = 0;
        }
        else if (playerRB.velocity.y < -0.1f)
        {
            playerAN.SetBool("JumpUp", false);
            playerAN.SetBool("Falling", true);
            playerAN.SetBool("Idle", false);
            playerAN.SetBool("LongSit", false);
            maxIdleTimer = 0;
        }
        else
        {
            playerAN.SetBool("JumpUp", false);
            playerAN.SetBool("Falling", false);
        }

        if (playerAN.GetBool("Idle") == true && maxIdleTimer < 5)
        {
            StartCoroutine(Sittertimer());

        }
    }

    IEnumerator Sittertimer()
    {
        
        new WaitForSecondsRealtime(25);
        maxIdleTimer += 1;

        if (maxIdleTimer == 5)
        {
            playerAN.SetBool("LongSit", true);
        }

        yield return maxIdleTimer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !ableJump)
        {
            ableJump = true;

            
            playerAN.SetBool("JumpUp", false);
            playerAN.SetBool("OnGround", true);
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
