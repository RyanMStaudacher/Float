using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxPlayerSpeed = 15f;
    public float playerAcceleration = 5000f;
    public float jumpSpeed = 20f;
    public float jumpDelay = 0.5f;
    public bool isGrounded = true;
    public bool gravityOn = true;
    [Range(0.5f, 1.0f)]
    public float raycastDistance = 0.8f;

    private Rigidbody2D rb;
    private GravityHammer gravHam;
    [HideInInspector]
    public bool canJump = true;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravHam = GetComponent<GravityHammer>();
    }

    private void Update()
    {
        if(gravityOn)
        {
            CheckIfGrounded();
            Jump();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(gravHam.canSwing && gravityOn)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 
            || Input.GetAxis("jHorizontal") > 0 || Input.GetAxis("jHorizontal") < 0)
        {
            if(isGrounded)
            {
                rb.AddForce((Input.GetAxis("Horizontal") + Input.GetAxis("jHorizontal")) * transform.right * playerAcceleration * Time.deltaTime);
            }
            else if(!isGrounded)
            {
                rb.AddForce((Input.GetAxis("Horizontal") + Input.GetAxis("jHorizontal")) * transform.right * (playerAcceleration / 2) * Time.deltaTime);
            }

            ClampSpeed();
        }
    }

    private void ClampSpeed()
    {
        if (rb.velocity.x > maxPlayerSpeed || rb.velocity.x < -maxPlayerSpeed)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxPlayerSpeed, maxPlayerSpeed), rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded && canJump && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(JumpDelay());
        }
    }

    private void CheckIfGrounded()
    {
        Vector3 rayDist = new Vector3(0, raycastDistance, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position - rayDist);

        if(hit.collider != null || rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Debug.DrawLine(transform.position, transform.position - rayDist);
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }
}
