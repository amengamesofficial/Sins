using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableForDoors : MonoBehaviour
{

    public GameObject Interactable;


    // Update is called once per frame
    void Update()
    {


    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Interactable.CompareTag("Eye") && !GameManager.instance.eyedooropen) Interactable.SetActive(true);
            else if (Interactable.CompareTag("Hand") && GameManager.instance.eyedooropen && !GameManager.instance.handdooropen) Interactable.SetActive(true);
            else if (Interactable.CompareTag("Foot") && GameManager.instance.handdooropen && !GameManager.instance.footdooropen) Interactable.SetActive(true);
            else if (Interactable.CompareTag("Belly") && GameManager.instance.footdooropen) Interactable.SetActive(true);
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) Interactable.SetActive(false);
    }
}
