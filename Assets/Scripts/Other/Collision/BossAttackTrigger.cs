using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    public BossController boss;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.SetPlayerInAttackRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.SetPlayerInAttackRange(false);
        }
    }

}
