using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isBroken = false;

    public void Break()
    {
        if (isBroken) return;
        isBroken = true;

        if (animator != null)
        {
            animator.SetTrigger("Break");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Gọi ở Animation Event frame cuối
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
