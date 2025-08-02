using UnityEngine;
using ScriptableObjects.TrapSO;

[CreateAssetMenu(fileName = "TrapSO", menuName = "ScriptableObjects/TrapSO")]
public class TrapSO : ScriptableObject
{
    [SerializeField] private TrapDataSO _data;
    public TrapDataSO Data => _data;
}
