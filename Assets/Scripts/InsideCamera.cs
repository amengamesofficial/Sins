using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideCamera : MonoBehaviour
{

    private void Start()
    {
        GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainCamera"))
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            GetComponent<Animator>().enabled = false;
        }
    }
}
