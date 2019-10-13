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

    private bool canSwing = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DirectionIndicator();
    }

    private void SwingHammer()
    {

    }

    private void DirectionIndicator()
    {
        if(Input.GetAxis("jrHorizontal") > 0 || Input.GetAxis("jrHorizontal") < 0 
            || Input.GetAxis("jrVertical") > 0 || Input.GetAxis("jrVertical") < 0)
        {
            float heading = Mathf.Atan2(-Input.GetAxis("jrHorizontal"), -Input.GetAxis("jrVertical"));

            directionArrow.transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);
        }

        
    }

    IEnumerator HammerDelay()
    {
        yield return new WaitForSeconds(swingDelay);
        canSwing = true;
    }
}
