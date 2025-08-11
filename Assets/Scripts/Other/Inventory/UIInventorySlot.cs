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

            // Nếu chưa gán icon trong Inspector thì tìm trong con tên "Item"
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

            // Thêm sự kiện click
            Button btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(OnClickSlot);
            }
            else
            {
                Debug.LogWarning("Không có Button trên UIInventorySlot");
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
