using System.Collections;
using UnityEngine;

public class OctopusControl : MonoBehaviour
{
    [SerializeField] private OctopusVision vision;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BulletSO currentSO;
    public Animator animator;

    private float _attackCooldown = 3f; // 3 giây
    private float _lastAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (vision.IsPlayerDetected(out Collider2D player))
        {
            TryAttack();
        }
        else
        {
            animator.SetBool("isIdle", true);
        }
    }

    private void TryAttack()
    {
        if (Time.time - _lastAttackTime < _attackCooldown) return;

        _lastAttackTime = Time.time;
        animator.SetTrigger("isAttacking");

        // Gọi bắn ngay 1 viên
        Shoot();

        // Reset lại sau khi bắn
        StartCoroutine(ResetAttackAnim());
    }

    private IEnumerator ResetAttackAnim()
    {
        yield return new WaitForSeconds(0.5f); // thời gian anim kết thúc
        animator.ResetTrigger("isAttacking");
        animator.SetBool("isIdle", true);
    }

    private void Shoot()
    {
        Instantiate(currentSO.Data.Prefab, spawnPoint.position, Quaternion.identity);
    }
}
