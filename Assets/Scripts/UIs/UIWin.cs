using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class UIWin : UIDialog
    {
        [Header("ElementsPause UI")]
        public Button nextLvButton;
        public Button homeBtn;
        public TMP_Text textCoinsCurrentLevel;
        public TMP_Text textDiamondCurrentLevel;

        void Start()
        {
            if (nextLvButton != null)
                nextLvButton.onClick.AddListener(NextLevel);

            if (homeBtn != null)
                homeBtn.onClick.AddListener(ReturnToLobby);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1f;
            // Save settings if needed
        }

        private void NextLevel()
        {
            SoundManager.Instance.PlayClickSound();
            Hide();

            int currentLevel = GameManager.Instance.levelManager.GetCurrentLevelIndex();
            int nextLevel = currentLevel + 1;

            if (nextLevel < GameManager.Instance.levelManager.levelNames.Count)
            {
                GameManager.Instance.SaveCurrentLevel(nextLevel + 1);
                GameManager.Instance.levelManager.LoadLevel(nextLevel);
            }
        }

        private void ReturnToLobby()
        {
            SoundManager.Instance.PlayClickSound();
            Time.timeScale = 1f;

            // Ẩn các UI đang mở
            Hide();
            GameManager.Instance.uiManager.uiGame.Hide();
            GameManager.Instance.uiManager.uiDeath.Hide();

            // Hiện Lobby UI
            GameManager.Instance.uiManager.uiLobby.Show();

            // Xóa level hiện tại
            GameManager.Instance.levelManager.UnloadCurrentLevel();

            // Trở lại nhạc nền mặc định
            SoundManager.Instance.RestoreDefaultMusic();
        }
    }
}

