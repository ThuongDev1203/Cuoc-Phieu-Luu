using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableObjects.Shop;
using Manager;
using Other.Inventory;

namespace UIs
{
    public class UIInventory : UIPanel
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Text _rankText;
        [SerializeField] private TMP_Text _itemNameText;
        [SerializeField] private TMP_Text _infoText;

        [Header("Stats")]
        [SerializeField] private TMP_Text _attackValueText;
        [SerializeField] private TMP_Text _speedValueText;

        [Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _selectButton;

        [Header("Slots")]
        [SerializeField] private UIInventorySlot slotPrefab;
        [SerializeField] private Transform slotParent;

        [Header("Coin UI")]
        public TMP_Text coinText;
        public TMP_Text diamondText;

        private void Start()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(Hide);

            if (_selectButton != null)
                _selectButton.onClick.AddListener(OnSelectItem);
        }

        public override void Show()
        {
            base.Show();
            PopulateSlots();

            SetCoinText(GameManager.Instance.coinManager.TotalCoins);
            SetDiamondText(GameManager.Instance.diamondManager.TotalDiamond);
        }

        private void PopulateSlots()
        {
            foreach (Transform child in slotParent)
                Destroy(child.gameObject);

            List<WeaponSO> ownedWeapons = InventoryManager.Instance.GetOwnedWeapons();
            foreach (var weapon in ownedWeapons)
            {
                var slot = Instantiate(slotPrefab, slotParent);
                slot.Setup(weapon, this);
            }
        }

        public void Setup(WeaponSO weaponSO)
        {
            if (weaponSO == null) return;
            WeaponData weapon = weaponSO.Data;

            _rankText.text = weapon.RarityName;
            _itemNameText.text = weapon.WeaponName;
            _infoText.text = weapon.Description;
            _attackValueText.text = "+" + weapon.Damage;
            _speedValueText.text = "+" + weapon.MoveSpeed;
        }

        private void OnSelectItem()
        {
            Debug.Log("Item selected!");
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
