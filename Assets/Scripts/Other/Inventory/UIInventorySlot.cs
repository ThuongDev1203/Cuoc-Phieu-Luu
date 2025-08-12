using UnityEngine;
using UnityEngine.UI;
using UIs;

namespace Other.Inventory
{
    public class UIInventorySlot : MonoBehaviour
    {
        [SerializeField] private Image icon;
        private WeaponSO weaponData;
        private UIInventory parentInventory;

        public void Setup(WeaponSO weapon, UIInventory inventory)
        {
            if (weapon == null || weapon.Data == null)
            {
                Debug.LogError("Weapon hoặc Weapon.Data bị null trong UIInventorySlot");
                return;
            }

            if (icon == null)
            {
                Transform itemTransform = transform.Find("Item");
                if (itemTransform != null)
                    icon = itemTransform.GetComponent<Image>();
            }

            weaponData = weapon;
            parentInventory = inventory;

            if (icon != null)
                icon.sprite = weapon.Data.Icon;

            // Đổi màu viền hoặc hiệu ứng đang được dùng
            string selectedWeaponName = PlayerPrefs.GetString("SelectedWeapon", "");
            Image bg = GetComponent<Image>();
            if (bg != null)
            {
                if (weapon.name == selectedWeaponName)
                    bg.color = new Color(0.0f, 0.2f, 0.4f); // màu xanh đậm
                else
                    bg.color = Color.white;
            }

            // Gán sự kiện click
            Button btn = GetComponent<Button>();
            if (btn == null)
                btn = GetComponentInChildren<Button>();

            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(OnClickSlot);
            }
        }


        private void OnClickSlot()
        {
            if (parentInventory != null && weaponData != null)
            {
                parentInventory.Setup(weaponData);
            }
            else
            {
                Debug.LogWarning("parentInventory hoặc weaponData bị null khi click slot");
            }
        }
    }
}
