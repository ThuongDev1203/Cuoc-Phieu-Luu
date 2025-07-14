using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using Animation.Player.Controller;

namespace UIs
{
    /// <summary>
    /// UIGame class for managing game UI elements
    /// </summary>
    public class UIGame : UIPanel
    {
        [Header("Game UI Elements")]
        public Button pauseButton;
        public Button jumpButton;
        public Button attackButton;
        public Button attack2Button;
        public FloatingJoystick joystick;
        private PlayerController playerController;

        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();

            pauseButton.onClick.AddListener(OnPause);
            jumpButton.onClick.AddListener(OnJump);
            attackButton.onClick.AddListener(OnAttack);
            attack2Button.onClick.AddListener(OnAttack2);
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

        /// <summary>
        /// Handle pause button click event.
        /// </summary>
        private void OnPause()
        {
            GameManager.Instance.uiManager.uiPause.Show();
        }

        /// <summary>
        /// Handle jump button click event.
        /// </summary>
        private void OnJump()
        {
            if (playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }

            if (playerController != null)
            {
                playerController.Jump(); // Gọi jump trực tiếp
            }
            else
            {
                Debug.LogWarning("PlayerController still not found!");
            }
        }

        private void OnAttack()
        {
            if (playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }

            if (playerController != null)
            {
                playerController.TriggerAttack1();
            }
            else
            {
                Debug.LogWarning("PlayerController still not found!");
            }
        }


        private void OnAttack2()
        {
            if (playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }

            if (playerController != null)
            {
                playerController.TriggerAttack2();
            }
            else
            {
                Debug.LogWarning("PlayerController still not found!");
            }
        }

    }
}
