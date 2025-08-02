using UnityEngine;
using ScriptableObjects.DepDataSO;


[CreateAssetMenu(fileName = "DepSO", menuName = "ScriptableObjects/DepSO")]
public class DepSO : ScriptableObject
{
    [SerializeField] private DepDataSO _data;
    public DepDataSO Data => _data;
}