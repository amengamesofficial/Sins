using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poor : MonoBehaviour
{
    public static Poor instance;
    Animator animator;
 
    public GameObject interactIcon;
    public GameObject Poor2;
    public bool isGetMoney = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        instance = this;
    }

    void Update()
    {
        
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (!isGetMoney)
            {
                SterchHandAnimation();

            }

            else if (isGetMoney)
            {
                Poor2.SetActive(true);
                interactIcon.SetActive(true);
                SterchHandAnimation();
            }


        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            IdleAnimation();
            interactIcon.SetActive(false);
        }

    }


    public void SterchHandAnimation()
    {
        animator.SetInteger("PoorMode", 1);
    }
    public void IdleAnimation()
    {
        animator.SetInteger("PoorMode", 0);
    }

    public void AcceptsAnimation()
    {
        animator.SetInteger("PoorMode", 2);
    }

    public void AfterGetMoney()
    {
        animator.SetInteger("PoorMode", 3);
    }

    public void GiveHand()
    {
        animator.SetInteger("PoorMode", 4);
    }


  

  
                        

}
