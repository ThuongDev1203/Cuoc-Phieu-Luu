using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIs;

namespace Other.Health
{
    using UnityEngine;

    public class EnemyHealth : MonoBehaviour, ITargetInfo
    {
        [SerializeField] private EnemyAISO enemyData;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => Mathf.RoundToInt(enemyData.Data.Health);

        public Sprite Icon => enemyData.Data.EnemyIcon;
        public string DisplayName => enemyData.Data.EnemyName;

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            // Nếu lần đầu bị đánh, show UI trước
            if (!UIHealthBar.Instance.gameObject.activeSelf)
            {
                ShowOnUI();
            }

            CurrentHealth -= damage;
            if (CurrentHealth < 0) CurrentHealth = 0;

            UIHealthBar.Instance.UpdateHealth(CurrentHealth);

            if (CurrentHealth <= 0)
                Die();
        }


        public void ShowOnUI()
        {
            UIHealthBar.Instance.SetTarget(Icon, DisplayName, MaxHealth, CurrentHealth);
        }

        private void Die()
        {
            UIs.UIHealthBar.Instance.Hide();
            Destroy(gameObject);
        }
    }

}
