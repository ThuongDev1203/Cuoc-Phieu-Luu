using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects.Shop;

namespace Manager
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private List<WeaponSO> ownedWeapons = new List<WeaponSO>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void AddItem(WeaponSO weapon)
        {
            if (!ownedWeapons.Contains(weapon))
                ownedWeapons.Add(weapon);
        }

        public List<WeaponSO> GetOwnedWeapons()
        {
            return ownedWeapons;
        }
    }
}
