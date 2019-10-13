using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDoor : MonoBehaviour
{
    [SerializeField]
    GameObject winText;

    public bool hasWon = false;

    // Update is called once per frame
    void Update()
    {
        if(hasWon)
        {
            if(Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            winText.SetActive(true);
            hasWon = true;
        }
    }
}
