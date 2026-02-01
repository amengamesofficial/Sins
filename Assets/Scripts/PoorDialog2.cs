using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorDialog2 : MonoBehaviour
{
    public GameObject Poor2;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Poor2.SetActive(true);
            Poor.instance.isGetMoney = true;
            Poor.instance.GetComponent<Collider2D>().enabled = true;
            
        }
    }
}
