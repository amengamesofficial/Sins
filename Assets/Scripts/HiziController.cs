using System.Collections;
using UnityEngine;

public class HiziController : MonoBehaviour
{
    public static HiziController instance;

    [Header("References")]
    public Transform player;
    public GameObject LevelDoor;
    GameObject PlayerUI;

    public LaserScript laser;
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
        PlayerUI = GameObject.Find("PlayerUI");
        CameraShake = GameObject.Find("HiziCameraShake");
        HitEffect = GameObject.Find("HiziHitEffect");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(IsDead) LevelDoor.SetActive(true);
        if (IsDead || IsAttacking) return;
        if (PlayerMovement.instance.isDead) { animator.SetInteger("EnemyMode", 0); rb.velocity = Vector2.zero; return; }

        CheckPhaseTwo();
        if (!phasetwoattack)
        {
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
        }

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

        while (t < 7)
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
        IsAttacking = true;
        rb.velocity = Vector2.zero;


        CurrentAttack++;


        int maxAttackCount = isPhasetwo ? 4 : 3;

        if (CurrentAttack > maxAttackCount)
            CurrentAttack = 1;


        animator.SetInteger("AttackMode", CurrentAttack);
        animator.SetBool("isAttacking", true);


        if (isPhasetwo && CurrentAttack == 4)
        {
            animator.SetBool("isHit", true);
            yield return new WaitForSeconds(3.0f);
            animator.SetBool("isHit", false);
            animator.SetBool("isAttacking", false);
            animator.SetInteger("EnemyMode", 0);

        }
        else
        {

            yield return new WaitForSeconds(0.5f);
            animator.SetBool("isAttacking", false);
            animator.SetInteger("AttackMode", 0);
            animator.SetInteger("EnemyMode", 0);
        }


        float cooldown = (isPhasetwo && CurrentAttack == 4) ? 2.0f : 1.0f;
        yield return new WaitForSeconds(cooldown);

        IsAttacking = false;
    }

    public void StartLaserAttack()
    {
        laser.isFiring = true;
    }

    public void EndLaserAttack()
    {
        laser.isFiring = false;
        laser.StopLaser();
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
            IsDead = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
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