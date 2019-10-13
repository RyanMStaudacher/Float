using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravZone : MonoBehaviour
{
    public float initialFloatForce = 10f;

    private float originalGravity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            originalGravity = rb.gravityScale;

            rb.velocity = rb.velocity / 2;

            rb.gravityScale = 0.0f;
            collision.GetComponent<PlayerController>().gravityOn = false;

            rb.AddForce(Vector2.up * initialFloatForce, ForceMode2D.Impulse);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = originalGravity;

            collision.GetComponent<PlayerController>().gravityOn = true;
        }
    }
}
