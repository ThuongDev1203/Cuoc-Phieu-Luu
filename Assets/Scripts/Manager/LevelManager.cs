using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Other;
using UIs;
using SriptableObjects.PlayerSO;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Tên các prefab level (trong Resources/Level/)")]
        public List<string> levelNames;

        [Header("Vị trí spawn level")]
        public Transform levelParent;

        [Header("Player Data")]
        [SerializeField] private PlayerSO playerSO; // gán trong Inspector

        private GameObject currentLevelInstance;
        private int currentLevelIndex;

        // Load level bình thường
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

                // Bổ sung: gọi làm mới Boss sau khi load level xong
                StartCoroutine(RefreshBossesAfterLevelLoad());

                // Load nhạc nền tương ứng từ Resources/Audio/Level
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

                // **Hiển thị UI Tutorial
                if (currentLevelIndex == 0)
                {
                    if (PlayerPrefs.GetInt("TutorialShown", 0) == 0)
                    {
                        StartCoroutine(ShowTutorialNextFrame());
                        PlayerPrefs.SetInt("TutorialShown", 1);
                        PlayerPrefs.Save();
                    }
                }

                // 🔥 Reset máu player mỗi khi load level
                if (playerSO != null)
                {
                    playerSO.ResetData();
                    Debug.Log($"[LevelManager] Reset Player máu = {playerSO.Data.Health}/{playerSO.Data.MaxHealth}");
                }
            }
            else
            {
                Debug.LogError("Không tìm thấy prefab tại: " + levelPath);
            }
        }

        private IEnumerator ShowTutorialNextFrame()
        {
            yield return null; // đợi 1 frame
            UITutorial tutorialUI = FindObjectOfType<UITutorial>();
            if (tutorialUI != null)
            {
                tutorialUI.Show();
            }
            else
            {
                Debug.LogWarning("⚠ Không tìm thấy UITutorial trong scene!");
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

        //fix Boss không nhận Player
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

        private IEnumerator RefreshBossesAfterLevelLoad()
        {
            yield return null; // đợi 1 frame để level instantiate xong

            BossController[] bosses = GameObject.FindObjectsOfType<BossController>();
            if (bosses.Length == 0)
            {
                Debug.LogWarning("Không tìm thấy Boss trong scene để refresh.");
            }
            else
            {
                foreach (var boss in bosses)
                {
                    boss.RefreshPlayerReference();
                    boss.ResetBoss();
                }
                Debug.Log($"Đã refresh và reset trạng thái cho {bosses.Length} boss.");
            }
        }
    }
}
