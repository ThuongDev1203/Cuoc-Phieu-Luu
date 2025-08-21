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
        if (bossController != null && bossController.HealthDeath <= 0)
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        if (checkPass() && !pass)
        {
            hidepass();
            pass = true;
        }
    }

    private void hidepass()
    {
        gameObject.SetActive(false);
    }

}
