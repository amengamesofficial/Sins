using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    Animator animator;

    

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
        if (GameManager.instance.eyeanimation) animator.SetInteger("Mode", 1);
        else if (GameManager.instance.handanimation) animator.SetInteger("Mode", 2);
        else if (GameManager.instance.footanimation) StartCoroutine(AfterKeyAnimation());
        else if (GameManager.instance.bellyanimation) StartCoroutine(AfterMedicineAnimation());
    }

    IEnumerator AfterKeyAnimation()
    {
        animator.SetInteger("Mode", 3);
        yield return new WaitForSeconds(3f);
        animator.SetInteger("Mode", 4);

    }

    IEnumerator AfterMedicineAnimation()
    {
        animator.SetInteger("Mode", 5);
        yield return new WaitForSeconds(2.5f);
        animator.SetInteger("Mode", 6);
    }
}
