using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossShow : MonoBehaviour
{
    public Animator[] animators;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<PlayableDirector>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;

            if (animators != null)
            {
                foreach (Animator animator in animators)
                {
                    animator.Rebind();
                    animator.Update(0f);
                    animator.enabled = false;
                }
            }
        }
    }

}
