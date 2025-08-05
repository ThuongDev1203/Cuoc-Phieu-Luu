using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace UIs
{
    /// <summary>
    /// UIPause class for managing the pause UI elements
    /// </summary>
    public class UIPause : UIDialog
    {
        [Header("ElementsPause UI")]
        public Button settingButton;
        public Button continueButton;
        public Button restartButton;
        public Button giveUpButton;

        void Start()
        {
            if (continueButton != null)
                continueButton.onClick.AddListener(Hide);

            if (settingButton != null)
                settingButton.onClick.AddListener(OpenSetting);

            if (restartButton != null)
                restartButton.onClick.AddListener(RestartLevel);

            if (giveUpButton != null)
                giveUpButton.onClick.AddListener(ReturnToLobby);
        }

        public override void Show()
        {
            Debug.Log("Đang mở UI Pause");
            base.Show();
            StartCoroutine(DelayPause());
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1f;
        }

        private IEnumerator DelayPause()
        {
            yield return new WaitForSeconds(0.2f);
            Time.timeScale = 0f;
        }

        private void OpenSetting()
        {
            this.Hide();
            GameManager.Instance.uiManager.uiSetting.Show();
        }

        private void RestartLevel()
        {
            Hide(); // Ẩn UI Pause
            Time.timeScale = 1f;
            GameManager.Instance.levelManager.ReloadCurrentLevel();
        }

        private void ReturnToLobby()
        {
            Time.timeScale = 1f;

            // Ẩn các UI đang mở
            Hide();
            GameManager.Instance.uiManager.uiGame.Hide();
            GameManager.Instance.uiManager.uiSetting.Hide();

            // Hiện Lobby UI
            GameManager.Instance.uiManager.uiLobby.Show();

            // Xóa level hiện tại
            GameManager.Instance.levelManager.UnloadCurrentLevel();
        }
    }
}
