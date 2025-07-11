using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Tên các prefab level (trong Resources/Level/)")]
        public List<string> levelNames;

        [Header("Vị trí spawn level")]
        public Transform levelParent;

        private GameObject currentLevelInstance;

        public void LoadLevel(int index)
        {
            if (index < 0 || index >= levelNames.Count)
            {
                Debug.LogError("Level index không hợp lệ!");
                return;
            }

            // Xoá level cũ
            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
            }

            // Load prefab từ Resources
            string levelPath = "Level/" + levelNames[index];
            GameObject prefab = Resources.Load<GameObject>(levelPath);

            if (prefab != null)
            {
                currentLevelInstance = Instantiate(prefab, levelParent);
            }
            else
            {
                Debug.LogError("Không tìm thấy prefab tại: " + levelPath);
            }
        }
    }
}
