using UnityEngine;
using Animation.Player.Controller;

namespace Other.Collision
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private PlayerSO playerSO;
        [SerializeField] private PlayerAnimatorController _animatorController;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = playerSO.Data.Health;
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
                if (_animatorController != null)
                {
                    _animatorController.TriggerDeath();
                }
                Debug.Log("Player chết.");
            }
        }
    }
}
