using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInNearby : MonoBehaviour
{
    public GameObject[] objects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            foreach(GameObject obj in objects)
            {
                obj.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }
}
