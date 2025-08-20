using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckwinLV5 : MonoBehaviour
{
    public GameObject OpenWool;
    BossController bossController;

    private void Start()
    {
        bossController = FindObjectOfType<BossController>();
        if (bossController == null)
        {
            Debug.LogError("BossController not found in the scene.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bossController != null && bossController.health <= 0){
            OpenWool.SetActive(true);
        }
    }
}
