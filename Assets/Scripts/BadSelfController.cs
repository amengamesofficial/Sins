using System.Collections;
using UnityEngine;

public class BadSelfController : MonoBehaviour
{
    public static BadSelfController instance;

    [Header("References")]
    Transform player;
    GameObject PlayerUI;
    public GameObject icePrefab;
    public GameObject lightningPrefab;
    public GameObject rockPrefab;
    public GameObject yellowLightningPrefab;
    public GameObject fireEnvironment;
    public GameObject iceEnvironment;
    GameObject HitEffect;
    GameObject CameraShake;
    GameObject Snow;
    Camera cam;


    [Header("Settings")]
    public float detectRange = 10f;
    public float stopRange = 2f;
    public float moveSpeed = 3f;
    public float distance = 0;
    int CurrentAttack = 1;
    public float attackCooldown;
    private float nextAttackTime = 0f;

    public float iceOffsetY = -1f;
    public float lightningOffsetY = 3f;

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
        cam = GameObject.FindObjectOfType<Camera>();
        Snow = GameObject.Find("Snow");
        PlayerUI = GameObject.Find("PlayerUI");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        HitEffect = GameObject.Find("BadSelfHitEffect");
        CameraShake = GameObject.Find("BadSelfCameraShake");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsDead || IsAttacking) return;
        if (PlayerMovement.instance.isDead) { animator.SetInteger("EnemyMode", 0); rb.velocity = Vector2.zero; return; }

        if (!PlayerMovement.instance.isDead)
        {
            FacePlayer();
        }
        CheckPhaseTwo();
        if (!phasetwoattack)
        {
            distance = Vector2.Distance(transform.position, player.position);

            if (distance <= stopRange)
            {
                rb.velocity = Vector2.zero;

                if (!IsAttacking && Time.time >= nextAttackTime)
                {
                    StartCoroutine(Attack());
                }
            }
            else if (distance <= detectRange)
            {
                MoveTowardPlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;
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

        while (t < 2.5f)
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
        Snow.SetActive(false);
        iceEnvironment.SetActive(false);
        fireEnvironment.SetActive(true);
        if (ColorUtility.TryParseHtmlString("#EAA23B", out Color color))
        {
            cam.backgroundColor = color;
        }
        while (phasetwoattack && !IsDead)
        {
            yield return new WaitForSeconds(3f);
            phasetwoattack = false;
        }
    }

    void MoveTowardPlayer()
    {
        if (phasetwoattack) return;

        float moveDir = player.position.x > transform.position.x ? 1 : -1;
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);

        if (animator != null)
            animator.SetInteger("EnemyMode", 1);
    }

    IEnumerator Attack()
    {
        IsAttacking = true;
        rb.velocity = Vector2.zero;

        CurrentAttack++;
        if (CurrentAttack > 3) CurrentAttack = 1;

        animator.SetInteger("AttackMode", CurrentAttack);
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(1.5f);

        animator.SetBool("isAttacking", false);
        animator.SetInteger("AttackMode", 0);
        animator.SetInteger("EnemyMode", 0);
        nextAttackTime = Time.time + attackCooldown;

        yield return new WaitForSeconds(1.0f);

        IsAttacking = false;

    }

    public void SpawnBossAttack(string attackType)
    {
        Vector3 playerPos = player.transform.position;
        float direction = (playerPos.x > transform.position.x) ? 1f : -1f;

        if (attackType == "Ice")
        {

            GameObject prefabToSpawn = isPhasetwo ? rockPrefab : icePrefab;

            Vector3 spawnPos = new Vector3(playerPos.x, playerPos.y + iceOffsetY, 0);
            GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            Vector3 newScale = obj.transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * direction;
            obj.transform.localScale = newScale;

            Destroy(obj, 2f);
        }
        else if (attackType == "Lightning")
        {

            GameObject prefabToSpawn = isPhasetwo ? yellowLightningPrefab : lightningPrefab;

            Vector3 spawnPos = new Vector3(playerPos.x, playerPos.y + lightningOffsetY, 0);
            GameObject obj = Instantiate(prefabToSpawn, spawnPos, prefabToSpawn.transform.rotation);

            Destroy(obj, 1.5f);
        }
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
                animator.SetInteger("EnemyMode", 5);
        }
    }

    void FacePlayer()
    {
        if (player.position.x < transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && isFacingRight)
        {
            Flip();
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