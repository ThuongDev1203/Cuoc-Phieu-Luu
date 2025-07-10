using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIs;

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
        }

        private void Start()
        {
            uiManager.uiLobby.Show();
        }

        public int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1);
        }

        public void SaveCurrentLevel(int currentLevel)
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }

        public int GetCurrentHardLevel()
        {
            return PlayerPrefs.GetInt("PlayerHardLevel", 1);
        }

        public void LoadGame()
        {
            int currentLevel = GetCurrentLevel();

            if (currentLevel < 0 || currentLevel >= levelManager.sceneBuildIndices.Count)
            {
                Debug.LogWarning($"CurrentLevel {currentLevel} vượt giới hạn! Reset về 0.");
                currentLevel = 0;
            }

            levelManager.LoadLevel(currentLevel);
        }

    }
}
