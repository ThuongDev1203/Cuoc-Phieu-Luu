using ScriptableObjects.BulletEnemy;
using UnityEngine;



[CreateAssetMenu(fileName = "BulletEnemySO", menuName = "ScriptableObjects/BulletEnemySO")]
public class BulletSO : ScriptableObject
{
    [SerializeField] private BulletDataSO _data;
    public BulletDataSO Data => _data;
}