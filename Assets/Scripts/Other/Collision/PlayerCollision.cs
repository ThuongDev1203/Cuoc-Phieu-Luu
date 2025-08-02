using UnityEngine;

namespace Other.Collision
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private PlayerSO playerSO;
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
                Debug.Log("Player chết.");
                // Xử lý chết nếu cần
            }
        }
    }
}
