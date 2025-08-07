using UnityEngine;

[CreateAssetMenu(fileName = "BossAnimationSO", menuName = "ScriptableObjects/BossAnimationSO")]
public class BossAnimationSO : ScriptableObject
{
    public string idleTrigger = "Idle";
    public string runTrigger = "Run";
    public string attackTrigger = "Attack";
    public string hitTrigger = "Hit";
    public string deathTrigger = "Death";
}
