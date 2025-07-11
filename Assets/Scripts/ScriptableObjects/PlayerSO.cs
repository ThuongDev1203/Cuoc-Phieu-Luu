using UnityEngine;
using SriptableObjects;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private PlayerDataSO data;
    public PlayerDataSO Data => data;

    public void LoadData()
    {
        // Validate or preprocess if needed
    }
}