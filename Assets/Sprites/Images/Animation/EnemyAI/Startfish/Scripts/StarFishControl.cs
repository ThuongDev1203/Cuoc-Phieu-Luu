using System.Collections;
using System.Collections.Generic;
using Animation.Player.Controller;
using Other.Collision;
using ScriptableObjects.Shop;
using UnityEngine;
namespace Animation.EnemyAI.Startfish.Scripts
{
    public class StarFishControl : MonoBehaviour
    {
        [SerializeField] private EnemyAISO starFishData;
        public Animator animator;
        private Rigidbody2D _rb;
        private PlayerController _player;
        private PlayerCollision _playerCollision;
        private int _damage;
        private float _health;
        private bool isDead = false;
        private bool isAttacking = false;
        private bool isFacingRight = true;

        void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            _playerCollision = _player.GetComponent<PlayerCollision>();
            _damage = CurrentDamage();
            _health = CurrentHealth();

            animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }
        int CurrentDamage() => (int)starFishData.Data.AttackDamage;
        int CurrentHealth() => (int)starFishData.Data.Health;


        private void ResetAttack()
        {
            isAttacking = false;
        }
        public void StarFishTakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                isDead = true;
                animator.SetTrigger("isDead");
                _rb.velocity = Vector2.zero;
            }
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player detected in collision");
                if (!isAttacking)
                {
                    isAttacking = true;
                    animator.SetTrigger("isAttacking");
                    animator.SetBool("isIdle", false);
                    _playerCollision.TakeDamage(_damage);
                }
                FacePlayer(collision.transform);
            }
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ResetAttack();
                animator.SetBool("isIdle", true);
            }
        }
        void FacePlayer(Transform player)
        {
            Vector3 direction = player.position - transform.position;
            direction.z = 0;

            bool playerIsOnRight = direction.x > 0;

            if (playerIsOnRight && !isFacingRight)
            {
                Flip();
            }
            else if (!playerIsOnRight && isFacingRight)
            {
                Flip();
            }
        }
        void Flip()
        {
            isFacingRight = !isFacingRight;

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}