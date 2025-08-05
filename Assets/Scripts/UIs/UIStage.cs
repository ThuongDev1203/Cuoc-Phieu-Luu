using UnityEngine;
using Manager;

namespace UIs
{
    public class UIStage : UIPanel
    {
        [Header("Prefab UIStageItem")]
        public GameObject stageItemPrefab;

        [Header("Container để chứa item")]
        public Transform container;

        [Header("Tổng số level")]
        public int totalLevels = 15;

        private void Start()
        {
            int currentLevel = GameManager.Instance.GetCurrentLevel();

            for (int i = 1; i <= totalLevels; i++)
            {
                GameObject go = Instantiate(stageItemPrefab, container);
                UIStageItem item = go.GetComponent<UIStageItem>();

                int starCount = PlayerPrefs.GetInt($"Level_{i}_Star", 0);
                bool isUnlocked = i <= currentLevel;
                bool isCurrent = i == currentLevel;

                item.Setup(i, starCount, isUnlocked, isCurrent);
            }
        }

        /// <summary>
        /// Show UI Game when entering the game.
        /// </summary>
        public override void Show()
        {
            base.Show();
        }

        /// <summary>
        /// Hide UI Game when switching to another screen.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
        }
    }
}
