using System.Collections.Generic;
using Other.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class UIShop : UIPanel
    {
        [Header("Element UI")]
        [SerializeField] private Button closeButton;
        [SerializeField] private UIDepInfo depInfoPanel;
        [SerializeField] private List<UIShopItem> shopItems;
        [SerializeField] private List<WeaponSO> weaponSOs; // Danh sách SO

        private void Start()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);

            for (int i = 0; i < shopItems.Count && i < weaponSOs.Count; i++)
            {
                shopItems[i].Init(weaponSOs[i], depInfoPanel);
            }
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}
