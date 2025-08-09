using UnityEngine;
using ScriptableObjects.BossSO;
using System.Collections;
using Other.Collision;
using Animation.Player.Controller;

public class BossController : MonoBehaviour
{
    [Header("Scriptable Object")]
    public BossSO bossData;

    [Header("Components")]
    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;

    private float chaseRange;
    private float moveSpeed;
    private int health;
    private bool canAttack = true;

    private float lastAttackTime;
    private float attackCooldown;

    private bool isPlayerInChaseRange = false;
    private bool isPlayerInAttackRange = false;
    private bool isFacingRight = false;
    private bool isInHitState = false;

    private enum State { Idle, Run, Attack, Hit, Death }
    private State currentState = State.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (bossData != null)
        {
            chaseRange = bossData.Data.ChaseRange;
            moveSpeed = bossData.Data.MoveSpeed;
            health = bossData.Data.MaxHealth;
            attackCooldown = bossData.Data.AttackCooldown;
        }

        lastAttackTime = Time.time - attackCooldown;
    }

    void Update()
    {
        if (player == null || currentState == State.Death || isInHitState) return;

        if (health <= 0)
        {
            ChangeState(State.Death);
            return;
        }

        // Quyết định trạng thái
        if (isPlayerInAttackRange)
            ChangeState(State.Attack);
        else if (isPlayerInChaseRange)
            ChangeState(State.Run);
        else
            ChangeState(State.Idle);

        HandleState();
        FlipTowardsPlayer();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case State.Idle:
                rb.velocity = Vector2.zero;
                break;
            case State.Run:
                Vector2 dir = (player.position - transform.position).normalized;
                rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
                break;
            case State.Attack:
                rb.velocity = Vector2.zero;
                if (canAttack)
                {
                    Attack();
                }
                break;
            case State.Hit:
            case State.Death:
                rb.velocity = Vector2.zero;
                break;
        }
    }

    void ChangeState(State newState)
    {
        if (currentState == newState) return;

        Debug.Log($"Chuyển trạng thái từ {currentState} → {newState}");

        currentState = newState;
        var animSO = bossData.animationSO;
        if (animSO == null) return;

        switch (newState)
        {
            case State.Idle:
                animator.SetTrigger(animSO.idleTrigger);
                break;
            case State.Run:
                animator.SetTrigger(animSO.runTrigger);
                break;
            case State.Attack:
                animator.SetTrigger(animSO.attackTrigger);
                break;
            case State.Hit:
                animator.SetTrigger(animSO.hitTrigger);
                break;
            case State.Death:
                animator.SetTrigger(animSO.deathTrigger);
                break;
        }
    }

    void Attack()
    {
        if (!canAttack) return;

        canAttack = false;
        Debug.Log("Boss tấn công!");

        // Gây damage cho player
        if (player != null)
        {
            var playerCollision = player.GetComponent<PlayerCollision>();
            if (playerCollision != null)
            {
                playerCollision.TakeDamage(bossData.Data.AttackDamage);
                playerCollision.TriggerHit();
                Debug.Log($"Boss gây {bossData.Data.AttackDamage} damage cho Player");
            }
        }

        StartCoroutine(AttackCooldownRoutine());
    }

    IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        if (currentState == State.Death) return;

        health -= damage;
        Debug.Log($"Boss nhận {damage} sát thương. Máu còn lại: {health}");

        if (health > 0)
        {
            StartCoroutine(HitRoutine());
        }
        else
        {
            StopAllCoroutines(); //Dừng tất cả coroutine cũ (HitRoutine)
            ChangeState(State.Death);
            isInHitState = false; // Cho phép Update() thoát hẳn
        }
    }

    private IEnumerator HitRoutine()
    {
        isInHitState = true;
        ChangeState(State.Hit);

        yield return new WaitForSeconds(0.3f);

        if (currentState == State.Death) yield break; // Đã chết thì không đổi gì nữa

        if (isPlayerInAttackRange)
            ChangeState(State.Attack);
        else if (isPlayerInChaseRange)
            ChangeState(State.Run);
        else
            ChangeState(State.Idle);

        isInHitState = false;
    }


    public void SetPlayerInChaseRange(bool inRange)
    {
        isPlayerInChaseRange = inRange;
    }

    public void SetPlayerInAttackRange(bool inRange)
    {
        isPlayerInAttackRange = inRange;
    }

    void FlipTowardsPlayer()
    {
        if ((player.position.x < transform.position.x && isFacingRight) ||
            (player.position.x > transform.position.x && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}



