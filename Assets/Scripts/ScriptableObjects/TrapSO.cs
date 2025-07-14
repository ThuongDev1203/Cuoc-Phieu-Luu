using UnityEngine;
using SriptableObjects;

[CreateAssetMenu(fileName = "TrapSO", menuName = "ScriptableObjects/TrapSO")]
public class TrapSO : ScriptableObject
{
    [SerializeField] private TrapDataSO data;
    public TrapDataSO Data => data;
}