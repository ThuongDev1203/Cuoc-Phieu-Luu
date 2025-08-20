using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLevelBoss : MonoBehaviour
{

    private bool pass;
    BossController bossController;
    private void Start()
    {
        bossController = FindObjectOfType<BossController>();
        if (bossController == null)
        {
            Debug.LogError("BossController not found in the scene.");
        }
    }
    private bool checkPass()
    {
        if (bossController != null && bossController.health <= 0)
        {
            return true;
        }
    }
    void Update()
    {
        if (bossController != null && bossController.health <= 0 && !pass)
        {
            
        }
    }
    private void showkey()
    {
        gameObject.SetActive(true);
    }
    private void hidekey()
    {
        gameObject.SetActive(false);
    }

}
