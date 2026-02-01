using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorDialog : MonoBehaviour
{
    public GameObject Speak;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Speak.SetActive(true);
            GetComponent<Collider2D>().enabled = false;

        }
    }


}
