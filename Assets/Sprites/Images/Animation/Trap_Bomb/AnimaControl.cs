using System.Collections;
using UnityEngine;
using Animation.Player.Controller;
using Other.Collision;

public class AnimaControl : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private TrapSO BomSO;
    public float explosionRadius = 2f;
    public LayerMask targetLayer;

    [Header("References")]
    public Animator anim;
    private PlayerCollision _playerCollision;
    private PlayerController _player;

    private bool hasExploded = false;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerCollision = _player != null ? _player.GetComponent<PlayerCollision>() : null;
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Player"))
        {
            hasExploded = true;

            // Kích hoạt animation nổ
            if (anim != null)
            {
                anim.SetTrigger("Hitbomb");
            }

            ExecuteBoom();
        }
    }

    private void ExecuteBoom()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayer);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                if (_playerCollision != null && BomSO?.Data != null)
                {
                    _playerCollision.TakeDamage(BomSO.Data.Damage);
                }
            }
        }

        Destroy(gameObject, 1.5f);
    }
}
