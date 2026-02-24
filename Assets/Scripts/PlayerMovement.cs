using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public Joystick joystick;
    public float MoveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isGrounded = true;
    bool isAttacking = false;
    public bool isDefend = false;
    public bool isDead = false;
    bool isRun = false;
    public float attackDuration = 0.5f;
    int currentAttack = 1;
    int currentHeavyAttack = 4;
    public bool isInCutscene = false;
    public GameObject CameraShaking;
    public GameObject HitEffect;
    public GameObject RunEffect;

    private bool comboRequested = false;

    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    private bool isKnockback = false;

    public bool isGuardBroken = false;
    public float guardBreakDuration = 2f;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead || isGuardBroken) return;

        if (isInCutscene)
            joystick.OnPointerUp(null);

        Die();
        RegenerateStamina();
    }

    void FixedUpdate()
    {
        if (isDead || isGuardBroken|| isDashing) return;
        Move();
    }


    void Move()
    {
        if (joystick == null || isKnockback) return;

        if (isAttacking || isDefend)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        float direction = Mathf.RoundToInt(joystick.Horizontal);

        if (direction > 0.3f || direction < -0.3f)
        {
            float currentSpeed = MoveSpeed;

            if (isRun && PlayerBreathBar.instance.BreathBar.value > 0)
            {
                currentSpeed *= 1.3f;
                ConsumeStaminaGradually(1.5f);
            }
            else
            {
                isRun = false;
            }

            rb.velocity = new Vector2(direction * currentSpeed, rb.velocity.y);
            transform.localScale = new Vector2(Mathf.Sign(direction) * Mathf.Abs(transform.localScale.x), transform.localScale.y);

            if (isGrounded && isRun) animator.SetInteger("Mode", 2);
            else if (isGrounded) animator.SetInteger("Mode", 1);

            RunEffect.SetActive(true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            RunEffect.SetActive(false);
            if (isGrounded)
                animator.SetInteger("Mode", 0);
        }
    }

    void ConsumeStaminaGradually(float amountPerSecond)
    {
        if (PlayerBreathBar.instance.BreathBar.value > 0)
        {
            PlayerBreathBar.instance.BreathBar.value -= amountPerSecond * Time.deltaTime;
        }
        else
        {
            PlayerBreathBar.instance.BreathBar.value = 0;
            if (isDefend) DefendRelease();
        }
    }

    void RegenerateStamina()
    {
        if (PlayerBreathBar.instance != null && PlayerBreathBar.instance.BreathBar != null)
        {
            bool isMoving = joystick != null && Mathf.Abs(joystick.Horizontal) > 0.1f;

            if (!isAttacking && !isDefend && (!isRun || !isMoving))
            {
                if (PlayerBreathBar.instance.BreathBar.value < PlayerBreathBar.instance.BreathBar.maxValue)
                {
                    PlayerBreathBar.instance.BreathBar.value += 5f * Time.deltaTime;
                }
            }

            PlayerBreathBar.instance.BreathBar.value = Mathf.Clamp(PlayerBreathBar.instance.BreathBar.value, 0, PlayerBreathBar.instance.BreathBar.maxValue);
        }
    }

    public void DashButton() 
    {
       
        if (isAttacking || isDead || !canDash || PlayerBreathBar.instance.BreathBar.value < 5f) return;

        StartCoroutine(PerformDash());
    }

    IEnumerator PerformDash()
{
    canDash = false;
    isDashing = true;
    
    PlayerBreathBar.instance.BreathBar.value -= 5;

    float originalGravity = rb.gravityScale;
    rb.gravityScale = 0f;

    float dashDirection = Mathf.Sign(transform.localScale.x);
    
    rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);

    animator.SetBool("isDash", true); 

    yield return new WaitForSeconds(dashTime);

    
    rb.gravityScale = originalGravity;
    rb.velocity = Vector2.zero; 
    animator.SetBool("isDash", false);
    isDashing = false;
    canDash = true;
}

    
    

    public void StopFastMoveMent()
    {
        isRun = false;
    }

    public void AttackButton()
    {
        if (isDead || isDefend || isGuardBroken) return;

        if (PlayerBreathBar.instance.BreathBar.value >= 1)
        {
            if (isAttacking)
            {
                comboRequested = true;
            }
            else
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        currentAttack = 1;

        while (isAttacking)
        {
            PlayerBreathBar.instance.BreathBar.value -= 1; 
            comboRequested = false;
            GetComponent<TakeDamage>().StartAttack();
            animator.SetInteger("AttackMode", currentAttack);
            rb.velocity = Vector2.zero;

            float timer = 0;

            while (timer < attackDuration)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            if (comboRequested && currentAttack < 3 && PlayerBreathBar.instance.BreathBar.value >= 2)
            {
                currentAttack++;
            }
            else
            {
                isAttacking = false;
            }
        }

        currentAttack = 1;
        animator.SetInteger("AttackMode", 0);
        animator.SetBool("isAttacking", false);
    }

     public void HeavyAttackButton()
    {
        if (isDead || isDefend || isGuardBroken) return;

        if (PlayerBreathBar.instance.BreathBar.value >= 1)
        {
            if (isAttacking)
            {
                comboRequested = true;
            }
            else
            {
                StartCoroutine(HeavyAttack());
            }
        }
    }

    IEnumerator HeavyAttack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
             currentHeavyAttack = 4;

        while (isAttacking)
        {
            PlayerBreathBar.instance.BreathBar.value -= 4; 
            comboRequested = false;
            GetComponent<TakeDamage>().StartAttack();
            animator.SetInteger("AttackMode", currentHeavyAttack);
            rb.velocity = Vector2.zero;

            float timer = 0;

            while (timer < attackDuration)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            if (comboRequested && currentHeavyAttack < 6 && PlayerBreathBar.instance.BreathBar.value >= 4)
            {
                currentHeavyAttack++;
            }
            else
            {
                isAttacking = false;
            }
        }

        currentHeavyAttack = 4;
        animator.SetInteger("AttackMode", 0);
        animator.SetBool("isAttacking", false);
    }

    public void DefendButton()
    {
        if (isAttacking || isDead || isGuardBroken || PlayerBreathBar.instance.BreathBar.value <= 0) return;
        isDefend = true;
        animator.SetBool("isDefend", true);
    }

    public void DefendRelease()
    {
        isDefend = false;
        animator.SetBool("isDefend", false);
    }

    public void Hit()
    {
        CameraShaking.SetActive(true);
        HitEffect.SetActive(true);
    }

    public void BreakGuard()
    {
        if (isGuardBroken) return;

        StartCoroutine(GuardBreakRecovery());
    }

    IEnumerator GuardBreakRecovery()
    {
        isDefend = false;
        animator.SetBool("isDefend", false);
        isGuardBroken = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isGuardBroken", true);

        yield return new WaitForSeconds(guardBreakDuration);

        animator.SetBool("isGuardBroken", false);
        isGuardBroken = false;
    }

    void Die()
    {
        if (PlayerHealthBar.instance == null)
            return;

        if (PlayerHealthBar.instance.Health.value <= 0)
        {
            isDead = true;
            animator.SetInteger("Mode", 5);
        }
    }

    public bool IsFacing(Vector2 targetPosition)
    {
        float lookDirection = transform.localScale.x;

        if (targetPosition.x > transform.position.x && lookDirection > 0) return true;

        if (targetPosition.x < transform.position.x && lookDirection < 0) return true;

        return false;
    }

    public void GetKnockback(Vector2 damageSourcePosition, int stamina)
    {
        if (!isDead)
        {
            StartCoroutine(KnockbackRoutine(damageSourcePosition, stamina));
        }
    }

    IEnumerator KnockbackRoutine(Vector2 damageSourcePosition, int stamina)
    {
        isKnockback = true;

        float direction = transform.position.x < damageSourcePosition.x ? -1 : 1;
        PlayerBreathBar.instance.BreathBar.value -= stamina;

        RunEffect.SetActive(true);
        rb.velocity = new Vector2(direction * knockbackForce, rb.velocity.y);

        yield return new WaitForSeconds(knockbackDuration);
        RunEffect.SetActive(false);
        isKnockback = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Gnd"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Gnd"))
        {
            isGrounded = false;
        }
    }
}