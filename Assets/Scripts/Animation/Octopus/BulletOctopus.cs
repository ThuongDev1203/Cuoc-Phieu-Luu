using Other.Collision;
using UnityEngine;

public class BulletOctopus : MonoBehaviour
{
    [SerializeField] private BulletSO bulletData;
    public Transform target;
    public Animator animator;
    private Rigidbody2D _rb;
    private PlayerCollision _playerCollision;
    private Vector2 _direction = Vector2.right;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _playerCollision = GetComponent<PlayerCollision>();
    }

    void Update()
    {
        transform.Translate(_direction * bulletData.Data.Speed * Time.deltaTime, Space.World);
    }
    public void SetData(BulletSO newBulletData)
    {
        bulletData = newBulletData;
        Destroy(gameObject, bulletData.Data.Lifetime);
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerCollision>();
            if (player != null)
                player.TakeDamage(bulletData.Data.Damage);
        }

    }
}