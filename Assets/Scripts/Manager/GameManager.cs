using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIs;
using Animation.Player.Controller;

namespace Manager
{
    /// <summary>
    /// GameManager class for managing the game state and logic
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Managers")]
        public UIManager uiManager;
        public LevelManager levelManager;
        public FloatingJoystick Joystick => uiManager.uiGame.joystick;
        public CoinManager coinManager;
        public DiamondManager diamondManager;

        // ---------------- LEVEL STATE ----------------
        private int selectedLevel = 1; // level mà người chơi chọn để chơi

        private void Awake()
        {
            Application.targetFrameRate = 60;

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (coinManager == null)
                coinManager = gameObject.AddComponent<CoinManager>();

            if (diamondManager == null)
                diamondManager = gameObject.AddComponent<DiamondManager>();
        }

        private void Start()
        {
            uiManager.uiLobby.Show();
        }

        // ---------------- LEVEL LOGIC ----------------

        /// <summary>
        /// Trả về level cao nhất đã mở khóa
        /// </summary>
        public int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1); // mặc định level 1
        }

        /// <summary>
        /// Lưu currentLevel (level cao nhất đã mở)
        /// </summary>
        private void SaveCurrentLevel(int level)
        {
            PlayerPrefs.SetInt("CurrentLevel", level);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Set selectedLevel khi người chơi chọn trong UIStage
        /// </summary>
        public void SetSelectedLevel(int level)
        {
            selectedLevel = level;
        }

        /// <summary>
        /// Lấy selectedLevel hiện tại
        /// </summary>
        public int GetSelectedLevel()
        {
            return selectedLevel;
        }

        /// <summary>
        /// Load game theo selectedLevel
        /// </summary>
        public void LoadGame(int levelToLoad = -1)
        {
            // Nếu không truyền level, load selectedLevel
            int level = levelToLoad > 0 ? levelToLoad : GetSelectedLevel();
            levelManager.LoadLevel(level - 1); // vì index bắt đầu từ 0
        }


        /// <summary>
        /// Gọi khi hoàn thành một level để mở khóa level tiếp theo
        /// </summary>
        public void CompleteLevel(int level)
        {
            int savedLevel = GetCurrentLevel();

            if (level >= savedLevel)
            {
                // mở khóa level tiếp theo
                SaveCurrentLevel(level + 1);
            }
        }

        // ---------------- CURRENCY ----------------

        public void UpdateCoinUI(int coin)
        {
            if (uiManager.uiGame != null)
                uiManager.uiGame.SetCoinText(coin);

            if (uiManager.uiLobby != null)
                uiManager.uiLobby.SetCoinText(coin);
        }

        public void UpdateDiamondUI(int diamond)
        {
            if (uiManager.uiGame != null)
                uiManager.uiGame.SetDiamondText(diamond);
        }
    }
}
