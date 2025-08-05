using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    /// <summary>
    /// UIShop class for managing shop UI elements
    /// </summary>
    public class UIShop : UIPanel
    {
        [Header("Element UI")]
        public Button closeButton;

        void Start()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);
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
