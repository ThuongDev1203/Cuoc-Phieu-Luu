using Other.Collision;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

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
        Destroy(gameObject, bulletData.Data.Lifetime);
        _playerCollision = GetComponent<PlayerCollision>();
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerCollision>();
            if (player != null)
                player.TakeDamage(bulletData.Data.Damage);
            Destroy(gameObject);
        }

    }
    //     void movetarget()
    //     {
    //         if (target != null)
    //         {
    //             Vector2 direction = (target.position - transform.position).normalized;
    //             transform.position += (Vector3)direction * bulletData.Data.Speed * Time.deltaTime;

    //             // Nếu viên đạn tới gần target thì huỷ
    //             float distance = Vector2.Distance(transform.position, target.position);
    //             if (distance < 0.1f) 
    //             {
    //                 animator.SetTrigger("impact");
    //                 Destroy(gameObject, 0.3f);
    //             }
    //         }
    //     }
    // }
}