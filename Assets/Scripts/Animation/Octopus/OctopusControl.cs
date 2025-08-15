using System.Collections;
using System.Collections.Generic;
using Other.Collision;
using UnityEditor.Experimental;
using UnityEngine;

public class OctopusControl : MonoBehaviour
{
    [SerializeField] private EnemyAISO octopus;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BulletSO currentSO;
    public Animator animator;
    private Rigidbody2D _rb;
    private float raycast = 5f;
    private Vector2 _direction = Vector2.right; 
    private PlayerCollision _playerCollision;

    // Start is called before the first frame update
    void Start()
    {
         _playerCollision = GetComponent<PlayerCollision>();
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
    }

    void checkwraycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, raycast);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (currentSO != null && currentSO.Data.Prefab != null)
            {
                Debug.LogWarning("DepSO hoặc prefab dép chưa được gán!");
                return;
            }

            GameObject dep = Instantiate(
                currentSO.Data.Prefab,
                spawnPoint.position,
                Quaternion.identity
            );

            // Vector2 direction = _facingRight ? Vector2.right : Vector2.left;
            // dep.GetComponent<DepBullet>().SetDirection(direction);
            }
    }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
