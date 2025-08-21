using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Manager;
using TMPro;

namespace UIs
{
    /// <summary>
    /// UILobby class for managing lobby UI elements
    /// </summary>
    public class UILobby : UIPanel
    {
        [Header("Game UI Elements")]
        public Button battleButton;
        public Button stageButton;
        public Button settingButton;
        public Button shopButton;
        public Button inventoryButton;
        public Button rewardsButton;
        public Button rankingButton;
        public GameObject userInfo;
        public GameObject statusBar;

        [Header("Coin UI")]
        public TMP_Text coinText;
        public TMP_Text diamondText;

        [Header("Level UI")]
        public TMP_Text levelText; // hiển thị level hiện tại

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            battleButton.onClick.AddListener(() => { OnButtonEffect(battleButton); OnPlayButtonClicked(); });
            stageButton.onClick.AddListener(() => { OnButtonEffect(stageButton); OnStage(); });
            settingButton.onClick.AddListener(() => { OnButtonEffect(settingButton); OnSetting(); });
            shopButton.onClick.AddListener(() => { OnButtonEffect(shopButton); OnShop(); });
            inventoryButton.onClick.AddListener(() => { OnButtonEffect(inventoryButton); OnInventory(); });
            //rewardsButton.onClick.AddListener(() => { OnButtonEffect(rewardsButton); OnRewards(); });
        }

        private void OnButtonEffect(Button btn)
        {
            SoundManager.Instance.PlayClickSound();
            // Pop scale animation on click
            btn.transform.DOKill(); // Stop previous tweens if any
            btn.transform.localScale = Vector3.one;
            btn.transform.DOScale(1.1f, 0.15f).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    btn.transform.DOScale(1f, 0.15f).SetEase(Ease.InCubic);
                });
        }

        /// <summary>
        /// Battle button click handler → vào thẳng level hiện tại
        /// </summary>
        private void OnPlayButtonClicked()
        {
            int currentLevel = GameManager.Instance.GetCurrentLevel();
            Debug.Log($"Bắt đầu chơi Level {currentLevel}");

            Hide();
            GameManager.Instance.uiManager.uiGame.Show();
            GameManager.Instance.uiManager.uiGame.SetLevelText(currentLevel);
            GameManager.Instance.LoadGame(currentLevel); // load level hiện tại
        }


        /// <summary>
        /// Setting button click handler
        /// </summary>
        private void OnSetting()
        {
            Debug.Log("Đang mở UI Setting");
            GameManager.Instance.uiManager.uiSetting.Show();
        }

        /// <summary>
        /// Shop button click handler
        /// </summary>
        private void OnShop()
        {
            Debug.Log("Đang mở UI Shop");
            GameManager.Instance.uiManager.uiShop.Show();
        }

        private void OnInventory()
        {
            Debug.Log("Đang mở UI Inventory");
            GameManager.Instance.uiManager.uiInventory.Show();
        }

        private void OnStage()
        {
            Debug.Log("Đang mở UI Stage");
            GameManager.Instance.uiManager.uiStage.Show();
        }

        /// <summary>
        /// Show UI Lobby
        /// </summary>
        public override void Show()
        {
            base.Show();

            SetCoinText(GameManager.Instance.coinManager.TotalCoins);
            SetDiamondText(GameManager.Instance.diamondManager.TotalDiamond);

            int currentLevel = GameManager.Instance.GetCurrentLevel();
            SetLevelText(currentLevel);
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

        public void SetLevelText(int level)
        {
            if (levelText != null)
                levelText.text = $"Level {level}";
        }
    }
}
