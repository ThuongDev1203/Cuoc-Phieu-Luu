using UnityEngine;
using SriptableObjects.EnemyAISO;


[CreateAssetMenu(fileName = "EnemyAISO", menuName = "ScriptableObjects/EnemyAISO")]
public class EnemyAISO : ScriptableObject
{
    [SerializeField] private EnemyDataAISO _data;
    public EnemyDataAISO Data => _data;
}