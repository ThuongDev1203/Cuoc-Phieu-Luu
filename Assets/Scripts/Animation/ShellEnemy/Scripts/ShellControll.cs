using System.Collections;
using System.Collections.Generic;
using Animation.Player.Controller;
using Animation.Player.States;
using Manager;
using Other.Collision;
using UnityEngine;

public class ShellControll : MonoBehaviour
{
    public EnemyAISO enemyAISO;
    public Transform attackRange;
    public float attackRangeRadius = 2f;
    public LayerMask playerLayer;
    public bool isFacingRight = true;
    public int damage;
    private int hitCount = 0;
    private bool isDead = false;
    private bool isAttacking = false;
    public Animator animator;
    public EnemyVFX enemyVFX;
    private Rigidbody2D _rb;
    private PlayerController _player;
    private PlayerCollision _playerCollision;

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerCollision = _player.GetComponent<PlayerCollision>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the ShellControll script.");
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }

    void Update()
    {
        if (isDead) return;

        Collider2D player = Physics2D.OverlapCircle(attackRange.position, attackRangeRadius, playerLayer);
        if (player != null)
        {
            isAttacking = true;
            animator.SetTrigger("isAttacking");
            animator.SetBool("isIdle", false);
            FacePlayer(player.transform);
        }
        else
        {
            isAttacking = false;
            animator.SetBool("isIdle", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Kiểm tra nếu Player nhảy lên đầu Shell
            if (collision.transform.position.y > transform.position.y)
            {
                hitCount++;
                if (hitCount >= 3)
                {
                    if (enemyVFX != null)
                    {
                        enemyVFX.PlayVFX(transform.position);
                    }
                    isDead = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void FacePlayer(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        direction.z = 0;

        bool playerIsOnRight = direction.x > 0;
        if (playerIsOnRight && !isFacingRight) Flip();
        else if (!playerIsOnRight && isFacingRight) Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void OnBite()
    {
        if (isDead) return;

        Collider2D player = Physics2D.OverlapCircle(attackRange.position, attackRangeRadius, playerLayer);
        if (player != null)
        {
            damage = (int)enemyAISO.Data.AttackDamage;

            if (_playerCollision != null)
                _playerCollision.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackRange == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackRange.position, attackRangeRadius);
    }
}
