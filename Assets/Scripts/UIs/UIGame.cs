using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using Animation.Player.Controller;
using TMPro;

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

        private bool isAttack2OnCooldown = false;
        private float attack2Cooldown = 5f;
        private float attack2CooldownTimer;

        [SerializeField] private Image attack2CooldownTile;
        [SerializeField] private TMP_Text attack2CooldownText;

        [Header("Value UI")]
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private TMP_Text diamondText;
        [SerializeField] private TMP_Text healthText;

        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();

            pauseButton.onClick.AddListener(OnPause);
            jumpButton.onClick.AddListener(OnJump);
            attackButton.onClick.AddListener(OnAttack);
            attack2Button.onClick.AddListener(OnAttack2);
        }

        void Update()
        {
            if (isAttack2OnCooldown)
            {
                attack2CooldownTimer -= Time.deltaTime;

                float percent = attack2CooldownTimer / attack2Cooldown;
                attack2CooldownTile.fillAmount = percent;

                if (attack2CooldownText != null)
                {
                    attack2CooldownText.text = Mathf.CeilToInt(attack2CooldownTimer).ToString();
                }

                if (attack2CooldownTimer <= 0f)
                {
                    isAttack2OnCooldown = false;
                    attack2Button.interactable = true;

                    // Ẩn tile và text
                    attack2CooldownTile.gameObject.SetActive(false);
                    if (attack2CooldownText != null)
                        attack2CooldownText.gameObject.SetActive(false);
                }
            }
        }


        /// <summary>
        /// Show UI Game when entering the game.
        /// </summary>
        public override void Show()
        {
            base.Show();
            ResetLevelUI();
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
                playerController = FindObjectOfType<PlayerController>();

            if (playerController != null && !isAttack2OnCooldown)
            {
                playerController.TriggerAttack2();

                // Bắt đầu cooldown
                isAttack2OnCooldown = true;
                attack2CooldownTimer = attack2Cooldown;
                attack2Button.interactable = false;

                // Hiện tile và text khi vừa nhấn
                attack2CooldownTile.gameObject.SetActive(true);
                if (attack2CooldownText != null)
                {
                    attack2CooldownText.gameObject.SetActive(true);
                    attack2CooldownText.text = attack2Cooldown.ToString("F0");
                }

                // Reset lại fill
                attack2CooldownTile.fillAmount = 1f;
            }
        }

        public void SetCoinText(int coin)
        {
            if (coinText != null)
                coinText.text = coin.ToString();
        }

        public void SetDiamondText(int diamond)
        {
            if (diamondText != null)
                diamondText.text = diamond.ToString();
        }

        public void SetHealthText(int health)
        {
            if (healthText != null)
                healthText.text = health.ToString();
        }

        public void ResetLevelUI()
        {
            SetCoinText(0);
            SetDiamondText(0);
        }

    }
}
