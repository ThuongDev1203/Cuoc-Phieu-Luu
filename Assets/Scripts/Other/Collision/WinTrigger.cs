using UnityEngine;
using Manager;
using UIs;

public class WinTrigger : MonoBehaviour
{
    private bool hasWon = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasWon && collision.CompareTag("Player"))
        {
            hasWon = true;

            // Gọi TransitionController tự động
            TransitionController transition = FindObjectOfType<TransitionController>();
            if (transition != null)
            {
                transition.ShowTransition(() =>
                {
                    // Khi transition xong thì hiện UI Win
                    UIManager uiManager = FindObjectOfType<UIManager>();
                    if (uiManager != null)
                    {
                        uiManager.ShowDialog(uiManager.uiWin);
                    }
                });
            }
            else
            {
                Debug.LogWarning("Không tìm thấy TransitionController trong scene!");
            }
        }
    }
}
