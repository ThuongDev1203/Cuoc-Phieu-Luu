using UnityEngine;
using ScriptableObjects.BossSO;
using UIs; // để gọi UIHealthBar

public class BossHealth : MonoBehaviour, ITargetInfo
{
    [SerializeField] private BossSO bossData;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => bossData.Data.MaxHealth;

    public Sprite Icon => bossData.Data.BossIcon;
    public string DisplayName => bossData.Data.BossName;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0;

        // Cập nhật thanh máu UI
        UIHealthBar.Instance.UpdateHealth(CurrentHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void ShowOnUI()
    {
        // Hiển thị UI khi Boss bị tấn công
        UIHealthBar.Instance.SetTarget(Icon, DisplayName, MaxHealth, CurrentHealth);
    }

    private void Die()
    {
        UIHealthBar.Instance.Hide();
        Destroy(gameObject);
    }
}
