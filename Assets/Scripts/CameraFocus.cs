using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize - 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize + 1;
        }
    }
}
