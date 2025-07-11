using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    /// <summary>
    /// UIPause class for managing the pause UI elements
    /// </summary>
    public class UIPause : UIDialog
    {
        [Header("ElementsPause UI")]
        public Button restartButton;

        void Start()
        {
            if (restartButton != null)
                restartButton.onClick.AddListener(Hide);
        }
        /// <summary>
        /// Update UI when opening Setting
        /// </summary>
        public override void Show()
        {
            Debug.Log("Đang mở UI Pause");
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            // Save settings if needed
        }
    }
}
