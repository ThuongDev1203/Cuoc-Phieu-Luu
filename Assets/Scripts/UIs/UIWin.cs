using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace UIs
{
    public class UIWin : UIDialog
    {
        public Button nextLvButton;
        public Button homeBtn;

        [Header("Reward in this Level")]
        public TMP_Text textCoinsCurrentLevel;
        public TMP_Text textDiamondCurrentLevel;

        [Header("Total After Reward")]
        public TMP_Text textCoinsCurrent;
        public TMP_Text textDiamondCurrent;

        private void Start()
        {
            if (nextLvButton != null)
                nextLvButton.onClick.AddListener(() =>
                {
                    ClaimReward();
                    NextLevel();
                });

            if (homeBtn != null)
                homeBtn.onClick.AddListener(() =>
                {
                    ClaimReward();
                    ReturnToLobby();
                });
        }

        public override void Show()
        {
            base.Show();
            UpdateUI();
        }

        private void UpdateUI()
        {
            int coinLevel = GameManager.Instance.coinManager.CoinsThisLevel;
            int diamondLevel = GameManager.Instance.diamondManager.DiamondsThisLevel;

            if (textCoinsCurrentLevel != null)
                textCoinsCurrentLevel.text = coinLevel.ToString();

            if (textDiamondCurrentLevel != null)
                textDiamondCurrentLevel.text = diamondLevel.ToString();

            if (textCoinsCurrent != null)
                textCoinsCurrent.text = GameManager.Instance.coinManager.GetTotalCoinsAfterLevel(coinLevel).ToString();

            if (textDiamondCurrent != null)
                textDiamondCurrent.text = GameManager.Instance.diamondManager.GetTotalDiamondsAfterLevel(diamondLevel).ToString();
        }

        private void ClaimReward()
        {
            GameManager.Instance.coinManager.ApplyLevelCoins();
            GameManager.Instance.diamondManager.ApplyLevelDiamonds();

            // Đồng bộ UI Lobby
            // GameManager.Instance.uiManager.uiLobby.SetCoinText(GameManager.Instance.coinManager.TotalCoins);
            // GameManager.Instance.uiManager.uiLobby.SetDiamondText(GameManager.Instance.diamondManager.TotalDiamond);
        }

        private void NextLevel()
        {
            SoundManager.Instance.PlayClickSound();
            Hide();

            int selectedLevel = GameManager.Instance.GetSelectedLevel();
            int nextLevel = selectedLevel + 1;

            if (nextLevel <= GameManager.Instance.levelManager.levelNames.Count)
            {
                // Mở khóa level hiện tại
                GameManager.Instance.CompleteLevel(selectedLevel);

                // Refresh highlight level mới
                GameManager.Instance.uiManager.uiStage.RefreshHighlight();

                GameManager.Instance.uiManager.uiGame.SetLevelText(nextLevel);

                // Gán level tiếp theo và load game
                GameManager.Instance.SetSelectedLevel(nextLevel);
                GameManager.Instance.LoadGame();
            }
            else
            {
                ReturnToLobby();
            }
        }


        private void ReturnToLobby()
        {
            SoundManager.Instance.PlayClickSound();
            Time.timeScale = 1f;

            Hide();
            GameManager.Instance.uiManager.uiGame.Hide();
            GameManager.Instance.uiManager.uiDeath.Hide();
            GameManager.Instance.uiManager.uiLobby.Show();

            GameManager.Instance.levelManager.UnloadCurrentLevel();
            SoundManager.Instance.RestoreDefaultMusic();
        }
    }
}
