using System.Collections.Generic;
using Manager;
using Other.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class UIShop : UIPanel
    {
        [Header("Element UI")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private UIDepInfo _depInfoPanel;
        [SerializeField] private List<UIShopItem> _shopItems;
        [SerializeField] private List<WeaponSO> _weaponSOs; // Danh sách SO

        [Header("Coin UI")]
        public TMP_Text coinText;
        public TMP_Text diamondText;

        private void Start()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(Hide);

            for (int i = 0; i < _shopItems.Count && i < _weaponSOs.Count; i++)
            {
                _shopItems[i].Init(_weaponSOs[i], _depInfoPanel);
            }
        }

        public override void Show()
        {
            base.Show();

            SetCoinText(GameManager.Instance.coinManager.TotalCoins);
            SetDiamondText(GameManager.Instance.diamondManager.TotalDiamond);
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void SetCoinText(int coin)
        {
            if (coinText != null)
                coinText.text = coin.ToString();
        }

        public void SetDiamondText(int diamond)
        {
            if (diamondText != null)
                diamondText.text = diamond.ToString();
        }
    }
}
