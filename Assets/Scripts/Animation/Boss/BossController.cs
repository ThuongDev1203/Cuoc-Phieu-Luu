using UnityEngine;
using ScriptableObjects.BossSO;

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

    private float lastAttackTime;
    public float attackCooldown = 1f;

    private bool isPlayerInChaseRange = false;
    private bool isPlayerInAttackRange = false;
    private bool isFacingRight = false;

    private enum State { Idle, Run, Attack, Hit, Dead }
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
        }

        lastAttackTime = Time.time - attackCooldown;
    }

    void Update()
    {
        if (player == null || currentState == State.Dead) return;

        if (health <= 0)
        {
            ChangeState(State.Dead);
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
                if (Time.time - lastAttackTime > attackCooldown)
                {
                    lastAttackTime = Time.time;
                    Attack();
                }
                break;
            case State.Hit:
            case State.Dead:
                rb.velocity = Vector2.zero;
                break;
        }
    }

    void ChangeState(State newState)
    {
        if (currentState == newState) return;
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
            case State.Dead:
                animator.SetTrigger(animSO.deathTrigger);
                break;
        }
    }

    void Attack()
    {
        Debug.Log("Boss tấn công!");
    }

    public void TakeDamage(int damage)
    {
        if (currentState == State.Dead) return;

        health -= damage;
        if (health > 0)
            ChangeState(State.Hit);
        else
            ChangeState(State.Dead);
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
