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
            else
                Debug.LogWarning("Không tìm thấy Image cho icon trong UIInventorySlot");

            // Luôn tìm và gán sự kiện click
            Button btn = GetComponent<Button>();
            if (btn == null)
                btn = GetComponentInChildren<Button>(); // tìm trong con

            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(OnClickSlot);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Button trong UIInventorySlot");
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
