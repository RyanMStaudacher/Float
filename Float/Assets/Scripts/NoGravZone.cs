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
            originalGravity = collision.GetComponent<Rigidbody2D>().gravityScale;

            collision.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * initialFloatForce, ForceMode2D.Impulse);

            collision.GetComponent<PlayerController>().gravityOn = false;
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
