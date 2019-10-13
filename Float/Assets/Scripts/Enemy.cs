using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float enemySpeed = 10f;
    public float maxEnemySpeed = 10f;
    public float attackDamage = 100f;
    public float enemyHealth = 100f;

    Rigidbody2D rb;
    Vector3 currentDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDir = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        CastTheRaysMan();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
        ClampSpeed();
    }

    private void CastTheRaysMan()
    {
        RaycastHit2D downHit = Physics2D.Linecast(transform.position, transform.position + Vector3.down);
        RaycastHit2D rightHit = Physics2D.Linecast(transform.position, transform.position + Vector3.right);
        RaycastHit2D leftHit = Physics2D.Linecast(transform.position, transform.position + -Vector3.right);

        if(downHit.collider == null && currentDir == transform.right)
        {
            currentDir = -transform.right;
        }
        else if(downHit.collider == null && currentDir == -transform.right)
        {
            currentDir = transform.right;
        }

        if (rightHit.collider != null)
        {
            currentDir = -transform.right;
        }

        if (leftHit.collider != null)
        {
            currentDir = transform.right;
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.down);
        Debug.DrawLine(transform.position, transform.position + Vector3.right);
        Debug.DrawLine(transform.position, transform.position + -Vector3.right);
    }

    private void MoveEnemy()
    {
        rb.AddForce(currentDir * enemySpeed * Time.deltaTime);
    }

    private void ClampSpeed()
    {
        if (rb.velocity.x > maxEnemySpeed || rb.velocity.x < -maxEnemySpeed)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxEnemySpeed, maxEnemySpeed), rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            controller.playerHealth -= attackDamage;
        }
    }
}
