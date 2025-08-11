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

        public int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1); // default level 1
        }

        public void SaveCurrentLevel(int level)
        {
            PlayerPrefs.SetInt("CurrentLevel", level);
        }

        public void LoadGame()
        {
            int currentLevel = GetCurrentLevel();
            levelManager.LoadLevel(currentLevel - 1); // vì index bắt đầu từ 0
        }
        public void UpdateCoinUI(int coin)
        {
            uiManager.uiGame.SetCoinText(coin); // gọi trực tiếp
        }

        public void UpdateDiamondUI(int diamond)
        {
            uiManager.uiGame.SetDiamondText(diamond); // gọi trực tiếp
        }
    }
}
