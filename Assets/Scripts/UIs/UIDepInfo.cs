using UIs;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableObjects.Shop;
using Manager;

public class UIDepInfo : UIPanel
{
    [Header("UI Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text priceText;

    [Header("Rarity")]
    [SerializeField] private TMP_Text rarityText;
    [SerializeField] private Image rarityIcon;

    [Header("Stats")]
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text moveSpeedText;

    [Header("Buttons")]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button buyButton;

    private UIShop shopPanel;
    private WeaponSO currentWeaponSO;

    private void Start()
    {
        if (returnButton != null)
            returnButton.onClick.AddListener(OnReturnToShop);

        if (buyButton != null)
            buyButton.onClick.AddListener(OnBuyItem);

        shopPanel = transform.root.GetComponentInChildren<UIShop>(true);
    }

    public override void Show()
    {
        base.Show();
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
        icon.sprite = weapon.Icon;
        nameText.text = weapon.WeaponName;
        descriptionText.text = weapon.Description;
        priceText.text = weapon.Price.ToString();

        if (rarityText != null) rarityText.text = weapon.RarityName;
        if (rarityIcon != null) rarityIcon.sprite = weapon.RarityIcon;

        damageText.text = weapon.Damage.ToString();
        moveSpeedText.text = weapon.MoveSpeed;
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

        if (GameManager.Instance.coinManager != null &&
            GameManager.Instance.coinManager.SpendCoin(currentWeaponSO.Data.Price))
        {
            InventoryManager.Instance.AddItem(currentWeaponSO);
            Debug.Log("Mua thành công: " + currentWeaponSO.name);
        }
        else
        {
            Debug.Log("Không đủ coin hoặc coinManager null");
        }
    }
}
