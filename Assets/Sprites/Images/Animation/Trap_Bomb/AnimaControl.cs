using System.Collections;
using UnityEngine;
using Animation.Player.Controller;
using Other.Collision;

public class AnimaControl : MonoBehaviour
{
        [Header("Bomb Settings")]
    [SerializeField] private TrapSO bombSO;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask targetLayer;

    [Header("References")]
    private Animator anim;
    private PlayerCollision playerCollision;
    private PlayerController player;

    private bool hasExploded = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerCollision = player != null ? player.GetComponent<PlayerCollision>() : null;
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu đã nổ rồi thì bỏ qua
        if (hasExploded) return;

        // Chỉ kích hoạt khi va chạm Player
        if (collision.gameObject.CompareTag("Player"))
        {
            hasExploded = true;

            if (anim != null)
            {
                anim.SetTrigger("Hitbomb"); // Trigger animation nổ
            }
            else
            {
                ExecuteBoom(); // Nếu không có Animator thì nổ ngay
            }
        }
    }

    // Gọi từ animation event hoặc fallback trực tiếp
    public void ExecuteBoom()
    {
        // Tìm các collider trong bán kính nổ
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayer);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player") && playerCollision != null && bombSO?.Data != null)
            {
                playerCollision.TakeDamage(bombSO.Data.Damage);
            }
            if (col.CompareTag("Box"))
            {
                Destroy(col.gameObject); // Phá hủy hộp
            }
        }

        // Xóa object bẫy sau khi nổ
        Destroy(gameObject, 1.5f);
    }

    // Vẽ bán kính nổ trong Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
