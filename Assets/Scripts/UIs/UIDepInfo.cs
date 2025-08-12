using UIs;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableObjects.Shop;
using Manager;

public class UIDepInfo : UIPanel
{
    [Header("UI Elements")]
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _currencyIcon;

    [Header("Rarity")]
    [SerializeField] private TMP_Text _rarityText;
    [SerializeField] private Image _rarityIcon;

    [Header("Stats")]
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _moveSpeedText;

    [Header("Buttons")]
    [SerializeField] private Button _returnButton;
    [SerializeField] private Button _buyButton;

    [Header("Text Price")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text diamondText;


    private UIShop shopPanel;
    private WeaponSO currentWeaponSO;

    // Tham chiếu icon cho Coins & Diamonds
    [Header("Currency Icons")]
    [SerializeField] private Sprite coinSprite;
    [SerializeField] private Sprite diamondSprite;

    private void Start()
    {
        if (_returnButton != null)
            _returnButton.onClick.AddListener(OnReturnToShop);

        if (_buyButton != null)
            _buyButton.onClick.AddListener(OnBuyItem);

        shopPanel = transform.root.GetComponentInChildren<UIShop>(true);
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

    public void Setup(WeaponSO weaponSO)
    {
        if (weaponSO == null) return;
        currentWeaponSO = weaponSO;

        WeaponData weapon = weaponSO.Data;
        _icon.sprite = weapon.Icon;
        _nameText.text = weapon.WeaponName;
        _descriptionText.text = weapon.Description;

        bool purchased = IsPurchased(weaponSO.name);

        if (purchased)
        {
            _priceText.text = "Purchased";
            if (_currencyIcon != null) _currencyIcon.enabled = false;
            _buyButton.interactable = false;
        }
        else
        {
            _priceText.text = weapon.Price.ToString();
            if (_currencyIcon != null)
            {
                _currencyIcon.enabled = true;
                _currencyIcon.sprite = (weapon.Currency == CurrencyType.Coins) ? coinSprite : diamondSprite;
            }
            _buyButton.interactable = true;
        }

        if (_rarityText != null) _rarityText.text = weapon.RarityName;
        if (_rarityIcon != null) _rarityIcon.sprite = weapon.RarityIcon;

        _damageText.text = weapon.Damage.ToString();
        _moveSpeedText.text = weapon.MoveSpeed;
    }

    private void OnReturnToShop()
    {
        Hide();
        if (shopPanel != null)
            shopPanel.Show();
    }

    public void OnBuyItem()
    {
        if (currentWeaponSO == null)
        {
            Debug.LogError("Chưa gán WeaponSO cho UIDepInfo");
            return;
        }

        WeaponData weapon = currentWeaponSO.Data;
        bool success = false;

        if (weapon.Currency == CurrencyType.Coins)
        {
            if (GameManager.Instance.coinManager != null &&
                GameManager.Instance.coinManager.SpendCoin(weapon.Price))
            {
                success = true;
            }
            else
            {
                Debug.Log("Không đủ coin hoặc coinManager null");
            }
        }
        else if (weapon.Currency == CurrencyType.Diamonds)
        {
            if (GameManager.Instance.diamondManager != null &&
                GameManager.Instance.diamondManager.SpendDiamond(weapon.Price))
            {
                success = true;
            }
            else
            {
                Debug.Log("Không đủ gem hoặc gemManager null");
            }
        }

        if (success)
        {
            InventoryManager.Instance.AddItem(currentWeaponSO);
            Debug.Log("Mua thành công: " + currentWeaponSO.name);

            // Cập nhật UI
            _priceText.text = "Purchased";
            if (_currencyIcon != null) _currencyIcon.enabled = false;
            _buyButton.interactable = false;

            // Lưu trạng thái đã mua
            SavePurchasedState(currentWeaponSO.name);

            UpdateCurrencyUI();
        }
    }

    private void SavePurchasedState(string weaponName)
    {
        PlayerPrefs.SetInt("WeaponPurchased_" + weaponName, 1);
        PlayerPrefs.Save();
    }

    private bool IsPurchased(string weaponName)
    {
        return PlayerPrefs.GetInt("WeaponPurchased_" + weaponName, 0) == 1;
    }

    private void UpdateCurrencyUI()
    {
        SetCoinText(GameManager.Instance.coinManager.TotalCoins);
        SetDiamondText(GameManager.Instance.diamondManager.TotalDiamond);
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
