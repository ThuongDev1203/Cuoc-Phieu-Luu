using UnityEngine;

namespace Other.Collision
{
    public class AttackHitbox : MonoBehaviour
    {
        [SerializeField] private float radius = 1f;

        public void CheckAndDestroyBoxes()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Box"))
                {
                    BreakableBox box = hitCollider.GetComponent<BreakableBox>();
                    if (box != null)
                    {
                        box.Break();
                        Debug.Log("Phá thùng bằng OverlapCircle!");
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
