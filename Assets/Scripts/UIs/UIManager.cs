using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIs;
using DG.Tweening;

namespace UIs
{
    /// <summary>
    /// UIManager class for managing different UI elements in the game
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        public UIGame uiGame;
        public UILobby uiLobby;
        public UIShop uiShop;
        public UIStage uiStage;
        public UIDepInfo uiDepInfo;
        public UIInventory uiInventory;
        public UITutorial uiTutorial;

        [Header("UI Dialogs")]
        public UISetting uiSetting;
        public UIPause uiPause;
        public UIDeath uiDeath;
        public UIWin uiWin;
        public UIHealthBar uiHealthBar;

        public void ShowPanel(UIPanel panel)
        {
            HideAllPanels();
            panel.Show();
        }

        public void HideAllPanels()
        {
            if (uiGame != null)
                uiGame.Hide();

            if (uiLobby != null)
                uiLobby.Hide();

            if (uiSetting != null)
                uiSetting.Hide();

            if (uiShop != null)
                uiShop.Hide();

            if (uiDepInfo != null)
                uiDepInfo.Hide();

            if (uiInventory != null)
                uiInventory.Hide();

            if (uiStage != null)
                uiStage.Hide();

            if (uiPause != null)
                uiPause.Hide();

            if (uiDeath != null)
                uiDeath.Hide();

            if (uiWin != null)
                uiWin.Hide();

            if (uiHealthBar != null)
                uiHealthBar.Hide();

            if (uiTutorial != null)
                uiTutorial.Hide();
        }

        public void ShowDialog(UIDialog dialog)
        {
            dialog.Show();
        }

        public void HideDialog(UIDialog dialog)
        {
            dialog.Hide();
        }
    }
}
