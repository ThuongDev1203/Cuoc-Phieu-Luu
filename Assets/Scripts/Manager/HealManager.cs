using UnityEngine;
using SriptableObjects.PlayerSO;

namespace Manager
{
    public class HealManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerSO playerSO;

        [Header("Heal Settings")]
        [SerializeField] private int healAmount = 5;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Heal"))
            {
                Heal(healAmount);

                Destroy(collision.gameObject);
            }
        }

        /// <summary>
        /// Hồi máu cho Player
        /// </summary>
        public void Heal(int amount)
        {
            int currentHealth = playerSO.Data.Health;
            int maxHealth = playerSO.Data.MaxHealth;

            playerSO.Data.Health = currentHealth + amount;

            Debug.Log($"[HealManager] Player healed {amount} HP. Current HP: {playerSO.Data.Health}/{maxHealth}");
        }
    }

}