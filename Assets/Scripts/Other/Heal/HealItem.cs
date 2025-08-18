using UnityEngine;
using ScriptableObjects;
using Manager;

namespace Other.Heal
{
    public class HealItem : MonoBehaviour
    {
        [SerializeField] private HealSO healSO;
        public HealSO HealSO => healSO;
    }
}
