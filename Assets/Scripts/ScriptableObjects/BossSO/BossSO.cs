using UnityEngine;
using ScriptableObjects.BossSO;

[CreateAssetMenu(fileName = "BossSO", menuName = "ScriptableObjects/BossSO")]
public class BossSO : ScriptableObject
{
    [SerializeField] private BossSOData _data;
    public BossSOData Data => _data;

    [Header("Animation Triggers")]
    public BossAnimationSO animationSO;
}
