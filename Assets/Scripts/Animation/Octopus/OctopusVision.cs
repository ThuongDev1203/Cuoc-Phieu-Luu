using UnityEngine;

public class OctopusVision : MonoBehaviour
{
    [SerializeField] private float detectWidth = 5f;
    [SerializeField] private float detectHeight = 2f;
    [SerializeField] private LayerMask playerLayer;

    public bool IsPlayerDetected(out Collider2D player)
    {
        Vector2 center = (Vector2)transform.position + Vector2.right * detectWidth / 2f;
        player = Physics2D.OverlapBox(center, new Vector2(detectWidth, detectHeight), 0f, playerLayer);
        return player != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 center = (Vector2)transform.position + Vector2.right * detectWidth / 2f;
        Gizmos.DrawWireCube(center, new Vector2(detectWidth, detectHeight));
    }
}
