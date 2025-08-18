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
        private bool _uiShown = false;

        private void Start()
        {
            _currentHealth = enemySO.Data.Health;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            Debug.Log($"{enemySO.Data.EnemyName} nhận {damage} damage, còn {_currentHealth} máu");

            if (UIHealthBar.Instance != null)
            {
                if (_currentHealth <= 0)
                {
                    UIHealthBar.Instance.UpdateHealth(0);
                    Die();
                }
                else
                {
                    UIHealthBar.Instance.SetTarget(
                        enemySO.Data.EnemyIcon,
                        enemySO.Data.EnemyName,
                        Mathf.RoundToInt(enemySO.Data.Health),
                        Mathf.RoundToInt(_currentHealth)
                    );
                }
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
