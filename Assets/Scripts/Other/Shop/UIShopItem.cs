using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects.Shop;

namespace Other.Shop
{
    public class UIShopItem : MonoBehaviour
    {
        [SerializeField] private Button selectButton;
        [SerializeField] private WeaponSO weaponSO;
        private UIDepInfo depInfoPanel;

        public void Init(WeaponSO so, UIDepInfo depInfo)
        {
            weaponSO = so;
            depInfoPanel = depInfo;

            if (selectButton != null)
                selectButton.onClick.AddListener(OnSelect);
        }

        private void OnSelect()
        {
            if (depInfoPanel != null && weaponSO != null)
            {
                depInfoPanel.Setup(weaponSO);
                depInfoPanel.Show();
            }
        }
    }
}
