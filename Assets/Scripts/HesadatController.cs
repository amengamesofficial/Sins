using System.Collections;
using UnityEngine;

public class HesadatController : MonoBehaviour
{
    public static HesadatController instance;

    [Header("References")]
    public Transform player;
 
   public GameObject HitEffect;
    public GameObject CameraShake;

    [Header("Settings")]
    public float detectRange = 10f;
    public float stopRange = 2f;
    public float moveSpeed = 3f;
    public float distance = 0;

    [Header("States")]
    public bool IsDead = false;
    bool IsAttacking = false;

    Animator animator;
    Rigidbody2D rb;
    bool isFacingRight = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsDead || IsAttacking) return;
        if (PlayerMovement.instance.isDead) { animator.SetInteger("EnemyMode", 0); rb.velocity = Vector2.zero; return; }

        distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stopRange)
        {
            if (!IsAttacking)
                StartCoroutine(Attack());
        }
        else if (distance <= detectRange)
        {
            MoveTowardPlayer();
        }
        else if (distance > detectRange)
        {
            rb.velocity = Vector2.zero;
            if (animator != null)
                animator.SetInteger("EnemyMode", 0);
        }

        Die();
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (direction.x > 0 && !isFacingRight)
            Flip();
        else if (direction.x < 0 && isFacingRight)
            Flip();

        if (animator != null)
            animator.SetInteger("EnemyMode", 1);
    }

    IEnumerator Attack()
    {
        IsAttacking = true;
        rb.velocity = Vector2.zero;

        animator.SetInteger("EnemyMode", 2);
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.7f);
        animator.SetBool("isAttacking", false);
        animator.SetInteger("EnemyMode", 0);
        yield return new WaitForSeconds(1f);
        IsAttacking = false;
    }

    public void Hit()
    {
        HitEffect.SetActive(true);
        CameraShake.SetActive(true);
    }

    void Die()
    {
        if (EnemyHealthBar.instance == null)
            return;

        if (EnemyHealthBar.instance.Health.value == 0)
        {
            Destroy(gameObject);
            IsDead = true;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}