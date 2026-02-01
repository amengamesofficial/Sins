using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Moon : MonoBehaviour
{

    public Light2D _light;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Window"))
        {
            _light.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Window"))
        {
            _light.enabled = false;
        }
    }
}
