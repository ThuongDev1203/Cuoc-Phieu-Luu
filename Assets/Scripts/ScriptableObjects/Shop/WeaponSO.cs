using UnityEngine;
using ScriptableObjects.Shop;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private WeaponData _data;
    public WeaponData Data => _data;
}
