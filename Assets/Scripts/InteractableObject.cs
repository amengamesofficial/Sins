using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;


    public float showOffTime = 0.1f;
    
    public float nextShowOffTime = 2;
    bool canShowOff = true;
    bool activation = false;

    GameObject interactIcon;

    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactIcon = transform.GetChild(0).gameObject;
        interactIcon.SetActive(false);
        
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
        if(collision.CompareTag("MainCamera"))
        {
            activation = true;
        }

        if(collision.CompareTag("Player"))
        {
            if (interactIcon != null)
                interactIcon.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(interactIcon != null)
                interactIcon.SetActive(true);
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
            if (interactIcon != null)
                interactIcon.SetActive(false);
        }
    }


 
}
