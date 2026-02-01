using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSowrd : MonoBehaviour
{
    public GameObject Sword;
    public Animator animator;
    GameObject CanSword;
    public RuntimeAnimatorController playerAnimator;

    void Start()
    {
        CanSword = GameObject.Find("CanSword");
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        Sword = GameObject.Find("Sword");

    }

    // Update is called once per frame
    public void OnMouseDown()
    {
        animator.runtimeAnimatorController = playerAnimator;
        Destroy(Sword);
    }
}

