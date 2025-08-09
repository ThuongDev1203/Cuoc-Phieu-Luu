using UnityEngine;
using ScriptableObjects.TrapSO;
using Other.Collision;
using Manager;

public class TrapTrigger : MonoBehaviour
{
    [Header("Trap Config")]
    [SerializeField] private TrapSO trapSO;

    [Header("References")]
    [SerializeField] private BoxCollider2D triggerZone;

    private Rigidbody2D rb;
    private bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Không rơi khi bắt đầu

        if (triggerZone != null)
            triggerZone.isTrigger = true;
    }

    // Khi player đi vào vùng trigger kích hoạt trap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFalling) return;

        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlayTrapFlalling();
            isFalling = true;
            Invoke(nameof(StartFalling), trapSO.Data.FallDelay);
        }
    }

    private void StartFalling()
    {
        rb.gravityScale = trapSO.Data.GravityScale;
        Invoke(nameof(DestroyTrap), trapSO.Data.DestroyAfter);
    }

    // Khi trap đang rơi va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling) return; // chỉ xử lý khi trap đang rơi

        // Va chạm với Player
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent<PlayerCollision>(out var player))
            {
                player.TakeDamage(trapSO.Data.Damage);
            }
            DestroyTrap();
        }
        // Va chạm với Ground
        else if (collision.collider.CompareTag("Ground"))
        {
            DestroyTrap();
        }
    }

    private void DestroyTrap()
    {
        Destroy(gameObject);
    }
}
