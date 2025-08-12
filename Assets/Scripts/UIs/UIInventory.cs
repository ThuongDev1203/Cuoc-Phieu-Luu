using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableObjects.Shop;
using Manager;
using Other.Inventory;
using Animation.Player.Controller;

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

        [Header("Save Dep selected")]
        private WeaponSO _selectedWeapon;

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

            // Lấy lại vũ khí đang được dùng
            string selectedWeaponName = PlayerPrefs.GetString("SelectedWeapon", "");
            if (!string.IsNullOrEmpty(selectedWeaponName))
            {
                WeaponSO currentWeapon = InventoryManager.Instance.GetOwnedWeapons()
                    .Find(w => w.name == selectedWeaponName);

                if (currentWeapon != null)
                    Setup(currentWeapon);
            }

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
            _selectedWeapon = weaponSO;
            WeaponData weapon = weaponSO.Data;

            _rankText.text = weapon.RarityName;
            _itemNameText.text = weapon.WeaponName;
            _infoText.text = weapon.Description;
            _attackValueText.text = "+" + weapon.Damage;
            _speedValueText.text = "+" + weapon.MoveSpeed;

            // Kiểm tra nếu vũ khí này đang được chọn
            string selectedWeaponName = PlayerPrefs.GetString("SelectedWeapon", "");
            TMP_Text btnText = _selectButton.GetComponentInChildren<TMP_Text>();

            // Lấy Image riêng
            Image btnImage = _selectButton.GetComponent<Image>();

            if (weaponSO.name == selectedWeaponName)
            {
                if (btnText != null) btnText.text = "Equipped";

                // Đổi màu ngay
                if (btnImage != null)
                    btnImage.color = new Color(0.0f, 0.2f, 0.4f, 1f);
            }
            else
            {
                if (btnText != null) btnText.text = "Select";

                if (btnImage != null)
                    btnImage.color = Color.white;

                ColorBlock colors = _selectButton.colors;
                colors.normalColor = Color.white;
                colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
                _selectButton.colors = colors;
            }
        }
        public void OnSelectItem()
        {
            if (_selectedWeapon == null || _selectedWeapon.Data.DepSO == null)
            {
                Debug.LogWarning("Chưa chọn vũ khí hoặc vũ khí chưa gán DepSO!");
                return;
            }

            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.SetDepSO(_selectedWeapon.Data.DepSO);
                Debug.Log("Đã đổi dép sang: " + _selectedWeapon.Data.DepSO.Data.DepName);
            }

            // Lưu vũ khí đã chọn
            PlayerPrefs.SetString("SelectedWeapon", _selectedWeapon.name);
            PlayerPrefs.Save();

            // Refresh lại UI toàn bộ slot để cập nhật nút
            PopulateSlots();
            Setup(_selectedWeapon);
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
