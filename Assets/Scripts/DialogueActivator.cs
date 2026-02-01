using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
            
    }
}
