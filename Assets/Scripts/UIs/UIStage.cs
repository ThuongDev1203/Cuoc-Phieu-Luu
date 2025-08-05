using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    /// <summary>
    /// UIStage class for managing stage UI elements
    /// </summary>
    public class UIStage : UIPanel
    {
        [Header("Element UI")]
        public Button closeButton;
        public GameObject stageItemPrefab;
        public Transform stageListParent;
        public int maxLevel = 15;

        void Start()
        {
            GenerateStageList();

            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);
        }
        /// <summary>
        /// Show Stage UI when entering the stage.
        /// </summary>
        public override void Show()
        {
            base.Show();
        }

        /// <summary>
        /// Hide Stage UI when switching to another screen.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
        }

        void GenerateStageList()
        {
            int currentLevel = GameManager.Instance.GetCurrentLevel();

            for (int i = 1; i <= maxLevel; i++)
            {
                GameObject item = Instantiate(stageItemPrefab, stageListParent);
                var stageItem = item.GetComponent<UIStageItem>();

                bool isUnlocked = i <= currentLevel;
                bool isCurrent = i == currentLevel;

                int star = PlayerPrefs.GetInt($"Level_{i}_Star", 0);

                stageItem.Setup(i, star, isUnlocked, isCurrent);
            }
        }
    }
}
