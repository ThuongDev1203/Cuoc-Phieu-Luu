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

            LoadInventory();
        }

        public void AddItem(WeaponSO weapon)
        {
            if (!ownedWeapons.Contains(weapon))
            {
                ownedWeapons.Add(weapon);

                // Lưu trạng thái đã mua
                PlayerPrefs.SetInt("WeaponPurchased_" + weapon.name, 1);
                PlayerPrefs.Save();
            }
        }

        public List<WeaponSO> GetOwnedWeapons()
        {
            return ownedWeapons;
        }

        private void LoadInventory()
        {
            // Đọc tất cả WeaponSO từ Resources/SO/Shop
            WeaponSO[] allWeapons = Resources.LoadAll<WeaponSO>("SO/Shop");

            foreach (var weapon in allWeapons)
            {
                if (PlayerPrefs.GetInt("WeaponPurchased_" + weapon.name, 0) == 1)
                {
                    ownedWeapons.Add(weapon);
                }
            }
        }

        //Xóa hết dữ liệu đã mua (dùng khi test)
        public void ClearInventory()
        {
            ownedWeapons.Clear();

            WeaponSO[] allWeapons = Resources.LoadAll<WeaponSO>("SO/Shop");
            foreach (var weapon in allWeapons)
            {
                PlayerPrefs.DeleteKey("WeaponPurchased_" + weapon.name);
            }

            PlayerPrefs.Save();
        }
    }
}
