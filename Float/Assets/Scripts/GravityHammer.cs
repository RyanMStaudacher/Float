using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHammer : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController controller;
    [SerializeField]
    GameObject directionArrow;

    public float swingDelay = 1f;
    public float hammerForce = 10f;
    public bool canSwing = true;

    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        directionArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DirectionIndicator();
        HandleCrosshair();
    }

    private void SwingHammer()
    {
        var worldMousePosition = GetComponentInChildren<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        if (Cursor.visible == true)
        {
            dir = worldMousePosition - transform.position;
            dir.Normalize();

            rb.AddForce(-dir * hammerForce, ForceMode2D.Impulse);
        }
        else if (Cursor.visible == false)
        {
            dir = new Vector2(-Input.GetAxis("jrHorizontal"), Input.GetAxis("jrVertical"));

            rb.AddForce(dir * hammerForce, ForceMode2D.Impulse);
        }

        canSwing = false;

        StartCoroutine(HammerDelay());
    }

    private void DirectionIndicator()
    {
        if(Input.GetAxis("jrHorizontal") > 0 || Input.GetAxis("jrHorizontal") < 0 
            || Input.GetAxis("jrVertical") > 0 || Input.GetAxis("jrVertical") < 0)
        {
            directionArrow.SetActive(true);

            float heading = Mathf.Atan2(-Input.GetAxis("jrHorizontal"), -Input.GetAxis("jrVertical"));

            directionArrow.transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);

            if(Input.GetAxis("Fire1") == 1 && canSwing)
            {
                SwingHammer();
            }

            Cursor.visible = false;
        }
        else
        {
            directionArrow.SetActive(false);
        }
    }

    private void HandleCrosshair()
    {
        if(Input.GetAxis("Fire1") == 1 && canSwing)
        {
            SwingHammer();
        }
    }

    IEnumerator HammerDelay()
    {
        yield return new WaitForSeconds(swingDelay);
        canSwing = true;
    }
}
