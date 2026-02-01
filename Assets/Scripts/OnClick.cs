using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateHandle : MonoBehaviour
{
    public Animator anim;
    public Sprite[] sprites;
    public bool canPerform = true;


    private void OnMouseDown()
    {
        if (canPerform)
        {


            if (anim.GetBool("Open"))
            {
                anim.SetBool("Open", false);
                transform.parent.GetComponent<SpriteRenderer>().sprite = sprites[0];
            } 
            else
            {
                anim.SetBool("Open", true);
                transform.parent.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
                
        }
        
    }

}
