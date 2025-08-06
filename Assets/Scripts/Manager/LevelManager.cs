using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Other;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Tên các prefab level (trong Resources/Level/)")]
        public List<string> levelNames;

        [Header("Vị trí spawn level")]
        public Transform levelParent;

        private GameObject currentLevelInstance;
        private int currentLevelIndex;

        public void LoadLevel(int index)
        {
            if (index < 0 || index >= levelNames.Count)
            {
                Debug.LogError("Level index không hợp lệ!");
                return;
            }

            currentLevelIndex = index;

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
                StartCoroutine(AssignCameraToPlayer());

                //Load nhạc nền tương ứng từ Resources/Audio/Level
                string musicPath = $"Audio/Level{index + 1}";
                AudioClip levelMusic = Resources.Load<AudioClip>(musicPath);
                if (levelMusic != null)
                {
                    SoundManager.Instance.ChangeBackgroundMusic(levelMusic);
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy nhạc nền: " + musicPath);
                }
            }
            else
            {
                Debug.LogError("Không tìm thấy prefab tại: " + levelPath);
            }
        }


        public void ReloadCurrentLevel()
        {
            LoadLevel(currentLevelIndex);
        }

        public int GetCurrentLevelIndex()
        {
            return currentLevelIndex;
        }

        public void UnloadCurrentLevel()
        {
            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
                currentLevelInstance = null;
            }
        }

        private IEnumerator AssignCameraToPlayer()
        {
            yield return new WaitForSeconds(0.05f); // đợi Player Awake

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
                if (camFollow != null)
                {
                    camFollow.SetTarget(player.transform);
                }
            }
            else
            {
                Debug.LogWarning("Không tìm thấy player để gán cho camera.");
            }
        }
    }
}
