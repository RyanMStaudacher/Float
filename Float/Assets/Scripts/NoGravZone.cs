using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravZone : MonoBehaviour
{
    float originalGravity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            originalGravity = collision.GetComponent<Rigidbody2D>().gravityScale;

            collision.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = originalGravity;
        }
    }
}
