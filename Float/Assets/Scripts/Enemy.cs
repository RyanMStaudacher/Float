using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CastTheRaysMan();
    }

    private void CastTheRaysMan()
    {
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector3.down);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector3.right);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, -Vector3.right);

        if(downHit.collider != null)
        {
            Debug.Log("Down " + downHit.collider.gameObject.name);
        }

        if (rightHit.collider != null)
        {
            Debug.Log("Right " + rightHit.collider.gameObject.name);
        }

        if (leftHit.collider != null)
        {
            Debug.Log("Left " + leftHit.collider.gameObject.name);
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.down);
        Debug.DrawLine(transform.position, transform.position + Vector3.right);
        Debug.DrawLine(transform.position, transform.position + -Vector3.right);
    }
}
