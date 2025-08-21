using UnityEngine;
using Manager;
using UnityEngine.UI;
using TMPro;

namespace UIs
{
    public class UIStage : UIPanel
    {
        [Header("Prefab UIStageItem")]
        public GameObject stageItemPrefab;

        [Header("Container để chứa item")]
        public Transform container;

        [Header("Tổng số level")]
        public int totalLevels = 15;

        [Header("Element UI")]
        public Button closeButton;

        [Header("Coin UI")]
        public TMP_Text coinText;
        public TMP_Text diamondText;

        private void Start()
        {
            int currentLevel = GameManager.Instance.GetCurrentLevel();

            for (int i = 1; i <= totalLevels; i++)
            {
                GameObject go = Instantiate(stageItemPrefab, container);
                UIStageItem item = go.GetComponent<UIStageItem>();

                bool isUnlocked = i <= currentLevel;   // đã mở nếu <= current
                bool isCurrent = i == currentLevel;    // level hiện tại

                item.Setup(i, isUnlocked, isCurrent);
            }

            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);
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
