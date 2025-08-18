using UnityEngine;
using SriptableObjects.PlayerSO;

namespace Manager
{
    public class HealManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerSO playerSO;

        [Header("Heal Settings")]
        [SerializeField] private HealSO healSO;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Heal"))
            {
                Heal(healSO.Data.Healing);
                Destroy(other.gameObject);
            }
        }

        /// <summary>
        /// Hồi máu cho Player, đảm bảo không vượt quá MaxHealth
        /// </summary>
        public void Heal(int amount)
        {
            int beforeHeal = playerSO.Data.Health;

            // Công thức chuẩn: không vượt quá MaxHealth
            int newHealth = playerSO.Data.Health + amount;
            if (newHealth > playerSO.Data.MaxHealth)
            {
                newHealth = playerSO.Data.MaxHealth;
            }

            playerSO.Data.Health = newHealth;

            int healed = newHealth - beforeHeal;

            Debug.Log($"[HealManager] Player hồi {healed}/{amount}. Máu hiện tại: {playerSO.Data.Health}/{playerSO.Data.MaxHealth}");
        }


        /// <summary>
        /// Lấy máu hiện tại
        /// </summary>
        public int GetCurrentHealth()
        {
            return playerSO.Data.Health;
        }
    }
}
