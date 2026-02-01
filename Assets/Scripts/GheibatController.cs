using System.Collections;
using UnityEngine;

public class GheibatController : MonoBehaviour
{
    public static GheibatController instance;

    [Header("References")]
    public Transform player;
    public GameObject LevelDoor;
    public GameObject PlayerUI;
    GameObject HitEffect;
    GameObject CameraShake;

    [Header("Settings")]
    public float detectRange = 10f;
    public float stopRange = 2f;
    public float moveSpeed = 3f;
    public float distance = 0;
    int CurrentAttack = 1;

    [Header("Phases")]
    bool isPhasetwo = false;
    bool phasetwoattack = false;

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
        CameraShake = GameObject.Find("GeibatCameraShake");

        HitEffect = GameObject.Find("GeibatHitEffect");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsDead) LevelDoor.SetActive(true);
        if (IsDead || IsAttacking || phasetwoattack) return;
        if (PlayerMovement.instance.isDead)
        {
            animator.SetInteger("EnemyMode", 0);
            return;
        }

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

        CheckPhaseTwo();
        Die();
    }

    void CheckPhaseTwo()
    {
        if (EnemyHealthBar.instance == null)
            return;

        if (!phasetwoattack && !isPhasetwo && EnemyHealthBar.instance.Health.value <= EnemyHealthBar.instance.Health.maxValue / 2)
        {
            isPhasetwo = true;
            phasetwoattack = true;
            StartCoroutine(StartPhaseTwo());
        }
    }

    IEnumerator StartPhaseTwo()
    {
        float t = 0;
        PlayerUI.SetActive(false);
        float runDirection = (player.position.x > transform.position.x) ? -1f : 1f;

        while (t < 3f)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * runDirection, transform.localScale.y);
            t += Time.deltaTime * 2;
            rb.velocity = new Vector2(runDirection * moveSpeed, rb.velocity.y);

            animator.SetInteger("EnemyMode", 1);
            yield return null;
        }

        EnemyHealthBar.instance.Health.value = EnemyHealthBar.instance.Health.maxValue;
        rb.velocity = Vector2.zero;
        animator.SetInteger("EnemyMode", 0);

        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -runDirection, transform.localScale.y);

        yield return new WaitForSeconds(1f);
        PlayerUI.SetActive(true);
        StartCoroutine(PhaseTwoAttack());
    }

    IEnumerator PhaseTwoAttack()
    {
        while (phasetwoattack && !IsDead)
        {
            animator.SetBool("isHit", true);

            yield return new WaitForSeconds(3f);
            animator.SetBool("isHit", false);
            animator.SetInteger("EnemyMode", 0);
            phasetwoattack = false;
        }
    }

    void MoveTowardPlayer()
    {
        if (phasetwoattack) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (direction.x < 0 && !isFacingRight)
            Flip();
        else if (direction.x > 0 && isFacingRight)
            Flip();

        if (animator != null)
            animator.SetInteger("EnemyMode", 1);
    }

    IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        IsAttacking = true;

        rb.velocity = Vector2.zero;
        animator.SetInteger("AttackMode", CurrentAttack);
        CurrentAttack++;
        if (CurrentAttack > 3)
            CurrentAttack = 1;

        yield return new WaitForSeconds(1f);

        animator.SetBool("isAttacking", false);
        IsAttacking = false;
    }

    public void Hit()
    {
        HitEffect.SetActive(true);
        CameraShake.SetActive(true);
    }

    void Die()
    {
        if (isPhasetwo && EnemyHealthBar.instance.Health.value <= 0)
        {
            IsDead = true;
            rb.velocity = Vector2.zero;
            if (animator != null)
                animator.SetBool("isDead", true);
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