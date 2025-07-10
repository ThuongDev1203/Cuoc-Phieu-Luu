using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Manager;

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
            // inventoryButton.onClick.AddListener(() => { OnButtonEffect(inventoryButton); OnInventory(); });
            //rewardsButton.onClick.AddListener(() => { OnButtonEffect(rewardsButton); OnRewards(); });

        }

        private void OnButtonEffect(Button btn)
        {
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
        /// Show UI Game when entering the game.
        /// </summary>
        private void OnPlayButtonClicked()
        {
            Hide();
            GameManager.Instance.LoadGame();
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

        // private void OnInventory()
        // {
        //     Debug.Log("Đang mở UI Inventory");
        //     GameManager.Instance.uiManager.uiInventory.Show();
        // }

        private void OnStage()
        {
            Debug.Log("Đang mở UI Stage");
            GameManager.Instance.uiManager.uiStage.Show();
        }

        /// <summary>
        /// Show UI Game when entering the game.
        /// </summary>
        public override void Show()
        {
            base.Show();
        }

    }
}
