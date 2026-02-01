using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    public Animator fire;
    public Animator _light;

    

    // Start is called before the first frame update
    void Start()
    {

        fire.enabled = false;
        _light.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainCamera"))
        {
            fire.enabled = true;
            _light.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            fire.enabled = false;
            _light.enabled = false;
        }
    }
}
