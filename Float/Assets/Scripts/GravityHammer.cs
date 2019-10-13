using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHammer : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController controller;
    AudioSource audioSource;
    [SerializeField]
    AudioClip swingSound;
    [SerializeField]
    GameObject directionArrow;

    public float swingDelay = 1f;
    public float noGravSwingDelay = 0.5f;
    public float hammerForce = 25f;
    public bool canSwing = true;

    private Vector2 dir;
    private float currentSwingDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        directionArrow.transform.Find("Grav Ham Blast").gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DirectionIndicator();
        HandleCrosshair();
        HandleHamSlam();

        if(controller.gravityOn)
        {
            currentSwingDelay = swingDelay;
        }
        else if(!controller.gravityOn)
        {
            currentSwingDelay = noGravSwingDelay;
        }
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

        audioSource.clip = swingSound;
        audioSource.Play();

        canSwing = false;

        StartCoroutine(HammerDelay());
    }

    private void DirectionIndicator()
    {
        if(Input.GetAxis("jrHorizontal") > 0 || Input.GetAxis("jrHorizontal") < 0 
            || Input.GetAxis("jrVertical") > 0 || Input.GetAxis("jrVertical") < 0)
        {
            directionArrow.transform.Find("Directional Arrow").gameObject.GetComponent<SpriteRenderer>().enabled = true;

            float heading = Mathf.Atan2(Input.GetAxis("jrHorizontal"), Input.GetAxis("jrVertical"));

            directionArrow.transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);

            if(Input.GetAxis("Fire1") == 1 && canSwing)
            {
                SwingHammer();
            }

            Cursor.visible = false;
        }
        else
        {
            directionArrow.transform.Find("Directional Arrow").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void HandleCrosshair()
    {
        if(Input.GetAxis("Fire1") == 1 && canSwing)
        {
            SwingHammer();
        }
    }

    private void HandleHamSlam()
    {
        if(Cursor.visible == false)
        {
            if(!canSwing)
            {
                directionArrow.transform.Find("Grav Ham Blast").gameObject.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(BlastDelay());
            }
        }
    }

    IEnumerator HammerDelay()
    {
        yield return new WaitForSeconds(currentSwingDelay);
        canSwing = true;
    }

    IEnumerator BlastDelay()
    {
        yield return new WaitForSeconds(0.1f);
        directionArrow.transform.Find("Grav Ham Blast").gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
