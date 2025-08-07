using UnityEngine;

public class BossAggroTrigger : MonoBehaviour
{
    [SerializeField] private BossController _boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _boss.SetPlayerInChaseRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _boss.SetPlayerInChaseRange(false);
        }
    }
}
