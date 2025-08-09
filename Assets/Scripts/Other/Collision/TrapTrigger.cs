using UnityEngine;
using ScriptableObjects.TrapSO;
using Other.Collision;

[RequireComponent(typeof(Rigidbody2D))]
public class TrapTrigger2D : MonoBehaviour
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
            isFalling = true;
            Invoke(nameof(StartFalling), trapSO.Data.FallDelay);
        }
    }

    private void StartFalling()
    {
        rb.gravityScale = trapSO.Data.GravityScale;
        Invoke(nameof(DestroyTrap), trapSO.Data.DestroyAfter);
    }

    // Khi trap đang rơi va chạm với player → gây damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling) return; // chỉ gây damage khi đang rơi

        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent<PlayerCollision>(out var player))
            {
                player.TakeDamage(trapSO.Data.Damage);
            }

            DestroyTrap();
        }
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
