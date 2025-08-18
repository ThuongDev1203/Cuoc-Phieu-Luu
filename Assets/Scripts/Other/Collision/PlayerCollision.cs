using UnityEngine;
using Animation.Player.Controller;
using UIs;
using System.Collections;
using System;
using Manager;
using Other.Heal;

namespace Other.Collision
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private PlayerSO playerSO;
        [SerializeField] private PlayerAnimatorController _animatorController;
        private UIDeath _uiDeath;
        private UIGame _uiGame;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = playerSO.Data.Health;
            _uiDeath = FindObjectOfType<UIDeath>();
            _uiGame = FindObjectOfType<UIGame>();
            _uiGame?.SetHealthText(_currentHealth);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<EnemyCollision>(out var enemy))
            {
                ContactPoint2D contact = collision.GetContact(0);
                bool isStomp = contact.normal.y > 0.5f;

                if (isStomp)
                {
                    float damage = playerSO.Data.AttackDamage;
                    enemy.TakeDamage(damage);

                    Rigidbody2D rb = GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 10f);
                    }
                }
                else
                {
                    int damage = (int)enemy.GetAttackDamage();
                    TakeDamage(damage);
                }
            }
            else if (collision.gameObject.TryGetComponent<BreakableBox>(out var barrel))
            {
                ContactPoint2D contact = collision.GetContact(0);
                bool isStomp = contact.normal.y > 0.5f;

                if (isStomp)
                {
                    SoundManager.Instance.PLayBoxBreak();
                    barrel.Break();
                    Bounce();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Heal"))
            {
                HealItem healItem = other.GetComponent<HealItem>();
                if (healItem != null)
                {
                    HealManager healManager = FindObjectOfType<HealManager>();
                    if (healManager != null)
                    {
                        healManager.Heal(healItem.HealSO.Data.Healing);
                        _uiGame?.SetHealthText(healManager.GetCurrentHealth());
                    }
                }

                Destroy(other.gameObject);
            }
        }


        private void Bounce()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
        }

        public void TakeDamage(int damage)
        {
            // Trừ máu trực tiếp vào ScriptableObject
            playerSO.Data.Health -= damage;

            Debug.Log($"Player nhận {damage} damage, còn {playerSO.Data.Health} máu");

            _uiGame?.SetHealthText(playerSO.Data.Health);

            if (playerSO.Data.Health <= 0)
            {
                Debug.Log("Player death.");
                _animatorController.TriggerDeath();
                StartCoroutine(HandleDeathUI());
            }
        }


        private IEnumerator HandleDeathUI()
        {
            _animatorController.TriggerDeath();
            yield return new WaitUntil(() => _animatorController.IsAnimationFinished());

            _uiDeath.Show();
            yield return new WaitForSecondsRealtime(0.2f);

            Time.timeScale = 0f;
        }

        public void TriggerHit()
        {
            _animatorController.TriggerHit();
        }

    }
}
