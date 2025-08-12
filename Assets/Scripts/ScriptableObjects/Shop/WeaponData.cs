using System;
using UnityEngine;

namespace ScriptableObjects.Shop
{
    public enum CurrencyType
    {
        Coins,
        Diamonds
    }

    [Serializable]
    public class WeaponData
    {
        [Header("Basic Info")]
        [SerializeField] private string _weaponName;
        [SerializeField] private Sprite _icon;

        [Header("Rarity")]
        [SerializeField] private string _rarityName;
        [SerializeField] private Sprite _rarityIcon;

        [TextArea]
        [SerializeField] private string _description;

        [Header("Detail")]
        [SerializeField] private int _damage;
        [SerializeField] private string _moveSpeed;

        [Header("Price")]
        [SerializeField] private int _price;
        [SerializeField] private CurrencyType _currencyType;

        [Header("Change Dep")]
        [SerializeField] private DepSO _depSO;

        // Getter
        public string WeaponName => _weaponName;
        public Sprite Icon => _icon;

        public string RarityName => _rarityName;
        public Sprite RarityIcon => _rarityIcon;

        public string Description => _description;
        public int Damage => _damage;
        public string MoveSpeed => _moveSpeed;
        public int Price => _price;
        public CurrencyType Currency => _currencyType;
        public DepSO DepSO => _depSO;
    }
}
