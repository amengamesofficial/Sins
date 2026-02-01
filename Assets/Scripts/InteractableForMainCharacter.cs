using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableForMainCharacter : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float showOffTime = 0.1f;
    
    public float nextShowOffTime = 2;
    bool canShowOff = true;
    bool activation = false;

    public GameObject interactIconEye;
    public GameObject interactIconHand;
    public GameObject interactIconFoot;
    public GameObject interactIconBelly;

    
    

    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!activation) return;
        if (canShowOff) StartCoroutine(ShowOff());
    }

    IEnumerator ShowOff()
    {
        canShowOff = false;
        yield return new WaitForSeconds(nextShowOffTime);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(showOffTime);
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(showOffTime);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(nextShowOffTime);
        canShowOff = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            activation = true;
        }

        if (collision.CompareTag("Player") && GameManager.instance.eyedooropen && !GameManager.instance.handdooropen)
        {
            interactIconEye.SetActive(true);
        }
        else if (collision.CompareTag("Player") && GameManager.instance.handdooropen && !GameManager.instance.footdooropen)
        {
            interactIconHand.SetActive(true);
        }
        else if (collision.CompareTag("Player") && GameManager.instance.footdooropen && !GameManager.instance.bellydooropen)
        {
            interactIconFoot.SetActive(true);
        }
        else if(collision.CompareTag("Player") && GameManager.instance.bellydooropen)
        {
            interactIconBelly.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            activation = false;
        }

        if (collision.CompareTag("Player"))
        {
            interactIconEye.SetActive(false);
            interactIconHand.SetActive(false);
            interactIconFoot.SetActive(false);
            interactIconBelly.SetActive(false);
               
            
        }
    }
}
