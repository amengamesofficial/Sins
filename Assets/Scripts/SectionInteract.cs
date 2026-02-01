using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionInteract : MonoBehaviour
{
    public bool isInput = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            if(isInput == true)
                transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            else
                transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

    }

 
}
