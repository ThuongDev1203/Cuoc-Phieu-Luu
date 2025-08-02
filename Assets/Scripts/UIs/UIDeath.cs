using System.Collections;
using System.Collections.Generic;
using UIs;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class UIDeath : UIDialog
    {
        [Header("ElementsPause UI")]
        public Button btnCountinue;

        void Start()
        {
            if (btnCountinue != null)
                btnCountinue.onClick.AddListener(Hide);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1f;
            // Save settings if needed
        }
    }
}
