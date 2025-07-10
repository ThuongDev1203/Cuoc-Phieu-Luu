using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    /// <summary>
    /// UISetting class for managing settings UI elements
    /// </summary>
    public class UISetting : UIDialog
    {
        [Header("Settings UI")]
        public Slider soundSlider;
        public Slider musicSlider;
        public Button closeButton;

        private void Start()
        {
            if (soundSlider != null)
                soundSlider.onValueChanged.AddListener(OnSoundSlider);

            if (musicSlider != null)
                musicSlider.onValueChanged.AddListener(OnMusicSlider);

            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);
        }

        /// <summary>
        /// Update UI when opening Setting
        /// </summary>
        public override void Show()
        {
            Debug.Log("Đang mở UI Setting");
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            // Save settings if needed
        }

        private void OnSoundSlider(float value)
        {
            // Xử lý điều chỉnh âm lượng sound
            Debug.Log("Sound volume: " + value);
            // AudioManager.Instance.SetSoundVolume(value);
        }

        private void OnMusicSlider(float value)
        {
            // Xử lý điều chỉnh âm lượng nhạc nền
            Debug.Log("Music volume: " + value);
            // AudioManager.Instance.SetMusicVolume(value);
        }

        private void OnMusicToggle(bool isOn)
        {
            // Xử lý bật/tắt nhạc nền
            Debug.Log("Music: " + (isOn ? "On" : "Off"));
            // AudioManager.Instance.SetMusic(isOn);
        }
    }
}
