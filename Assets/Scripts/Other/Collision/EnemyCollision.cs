using System.Collections;
using System.Collections.Generic;
using UIs;
using UnityEngine;

namespace Other.Collision
{
    /// <summary>
    /// EnemyCollision class for handling enemy collision logic
    /// </summary>
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private EnemyAISO enemySO;
        private float _currentHealth;

        private void Start()
        {
            _currentHealth = enemySO.Data.Health;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            Debug.Log($"{enemySO.Data.EnemyName} nhận {damage} damage, còn {_currentHealth} máu");

            // Cập nhật UI thanh máu
            if (_currentHealth < enemySO.Data.Health) // nghĩa là đã bị đánh
            {
                UIHealthBar.Instance.SetTarget(
                    enemySO.Data.EnemyIcon,
                    enemySO.Data.EnemyName,
                    Mathf.RoundToInt(enemySO.Data.Health),
                    Mathf.RoundToInt(_currentHealth)
                );
            }

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public float GetAttackDamage()
        {
            return enemySO.Data.AttackDamage;
        }

        private void Die()
        {
            Debug.Log($"{enemySO.Data.EnemyName} đã chết.");
            Destroy(gameObject);
        }
    }
}