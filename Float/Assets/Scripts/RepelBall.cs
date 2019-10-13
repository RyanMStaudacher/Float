using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelBall : MonoBehaviour
{
    public float repelForce = 10f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            rb.AddForce((collision.transform.position - transform.position) * repelForce);
        }
    }
}
