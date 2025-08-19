using UnityEngine;
using SriptableObjects.PlayerSO;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private PlayerDataSO _data;
    public PlayerDataSO Data => _data;
    public void ResetData()
    {
        _data.ResetHealth();
    }

}
