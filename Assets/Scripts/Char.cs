using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Char : MonoBehaviour
{

    Animator animator;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            animator.SetBool("Move", true);
        }
        else
        {
            transform.Translate(Vector2.zero * Time.deltaTime * speed);
            animator.SetBool("Move", false);
        }
    }
}
