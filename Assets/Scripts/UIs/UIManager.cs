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
        //public UIGame uiGame;
        public UILobby uiLobby;
        public UIShop uiShop;
        public UIStage uiStage;

        [Header("UI Dialogs")]
        public UISetting uiSetting;

        public void ShowPanel(UIPanel panel)
        {
            HideAllPanels();
            panel.Show();
        }

        public void HideAllPanels()
        {
            // if (uiGame != null)
            //     uiGame.Hide();

            if (uiLobby != null)
                uiLobby.Hide();

            if (uiSetting != null)
                uiSetting.Hide();

            if (uiShop != null)
                uiShop.Hide();

            if (uiStage != null)
                uiStage.Hide();
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
