using UnityEngine;
using SriptableObjects.PlayerSO;
using ScriptableObjects;

[CreateAssetMenu(fileName = "HealSO", menuName = "ScriptableObjects/HealSO")]
public class HealSO : ScriptableObject
{
    [SerializeField] private HealDataSO _data;
    public HealDataSO Data => _data;
}
