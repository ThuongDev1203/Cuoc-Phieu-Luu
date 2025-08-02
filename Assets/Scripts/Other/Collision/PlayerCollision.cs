using UnityEngine;
using Animation.Player.Controller;
using UIs;
using System.Collections;

namespace Other.Collision
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private PlayerSO playerSO;
        [SerializeField] private PlayerAnimatorController _animatorController;
        private UIDeath _uiDeath;


        private int _currentHealth;

        private void Start()
        {
            _currentHealth = playerSO.Data.Health;
            _uiDeath = FindObjectOfType<UIDeath>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<EnemyCollision>(out var enemy))
            {
                int damage = (int)enemy.GetAttackDamage();
                TakeDamage(damage);
            }
        }

        private void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log($"Player nhận {damage} damage, còn {_currentHealth} máu");

            if (_currentHealth <= 0)
            {
                Debug.Log("Player chết.");
                _animatorController.TriggerDeath();

                StartCoroutine(HandleDeathUI());
            }
        }

        private IEnumerator HandleDeathUI()
        {
            _animatorController.TriggerDeath();

            // Đợi animation kết thúc
            yield return new WaitUntil(() => _animatorController.IsAnimationFinished());

            // Hiện UI
            _uiDeath.Show();

            yield return new WaitForSecondsRealtime(0.2f);

            Time.timeScale = 0f;
        }

    }
}
