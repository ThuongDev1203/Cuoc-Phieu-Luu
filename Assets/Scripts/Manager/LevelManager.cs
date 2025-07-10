using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        public List<int> sceneBuildIndices;

        public int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt("CurrentLevel", 0);
        }

        public void CompleteLevel()
        {
            int nextLevel = GetCurrentLevel() + 1;
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            PlayerPrefs.Save();
        }

        public void LoadLevel(int index)
        {
            if (index >= 0 && index < sceneBuildIndices.Count)
            {
                int sceneIndex = sceneBuildIndices[index];
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.LogWarning("Invalid level index!");
            }
        }
    }
}
