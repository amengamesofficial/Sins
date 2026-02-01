using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleMovment : MonoBehaviour
{
    public static AngleMovment instance;
    Rigidbody2D rb;
    Animator animator;
    public float moveSpeed = 5f;
    public float animatorSpeed = 0.5f;
    public Joystick joystick;
    int joystickX;
    int joystickY;
    private float angle;
    public bool isBusy = false;




    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = animatorSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick == null)
            return;
        joystickX = Mathf.RoundToInt(joystick.Horizontal);
        joystickY = Mathf.RoundToInt(joystick.Vertical);
        angle = Mathf.Atan2(joystickX, joystickY) * Mathf.Rad2Deg;




    }

    private void FixedUpdate()
    {
        if (isBusy)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Move();

        
    }

    void Move()
    {


        rb.velocity = new Vector2(joystickX * moveSpeed, joystickY * moveSpeed);


        if (joystickX < -0.3f)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

        }

        else if (joystickX > 0.3f)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);

        }

        if (angle == 90 || angle == -90)
        {
            animator.SetInteger("MoveMode", 1);
        }

        else if (joystickY > 0.3f || angle == 45 || angle == -45)
        {

            animator.SetInteger("MoveMode", 2);

        }
        else if (joystickY < -0.3f || angle == 135 || angle == -135)
        {

            animator.SetInteger("MoveMode", 3);
        }

        else
        {

            rb.velocity = Vector2.zero;
            animator.SetInteger("MoveMode", 0);

        }

    }

    public void PickUp()
    {

        StartCoroutine(AfterPickUp());
    }

    IEnumerator AfterPickUp()
    {
        isBusy = true;
        animator.SetInteger("MoveMode", 4);
        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("MoveMode", 0);
        isBusy = false;
    }

    public void Use()
    {

        StartCoroutine(Afteruse());
    }

    IEnumerator Afteruse()
    {
        isBusy = true;
        animator.SetInteger("MoveMode", 5);
        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("MoveMode", 0);
        isBusy = false;
    }


    
    
    
}
