using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace UIs
{
    public class UISetting : UIDialog
    {
        [Header("Settings UI")]
        public Slider soundSlider;
        public Slider musicSlider;
        public Button closeButton;

        [Header("Icons")]
        public Image soundIcon;
        public Image musicIcon;
        public Sprite soundOnSprite;
        public Sprite soundOffSprite;
        public Sprite musicOnSprite;
        public Sprite musicOffSprite;

        private void Start()
        {
            if (soundSlider != null)
                soundSlider.onValueChanged.AddListener(OnSoundSlider);

            if (musicSlider != null)
                musicSlider.onValueChanged.AddListener(OnMusicSlider);

            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);
        }

        public override void Show()
        {
            Debug.Log("Đang mở UI Setting");
            base.Show();

            // Load volume từ SoundManager
            if (soundSlider != null)
            {
                soundSlider.value = SoundManager.Instance.GetSoundVolume();
                UpdateSoundIcon(soundSlider.value);
            }

            if (musicSlider != null)
            {
                musicSlider.value = SoundManager.Instance.GetMusicVolume();
                UpdateMusicIcon(musicSlider.value);
            }
        }

        private void OnSoundSlider(float value)
        {
            Debug.Log("Sound volume: " + value);
            SoundManager.Instance.SetSoundVolume(value);
            UpdateSoundIcon(value);
        }

        private void OnMusicSlider(float value)
        {
            Debug.Log("Music volume: " + value);
            SoundManager.Instance.SetMusicVolume(value);
            UpdateMusicIcon(value);
        }

        private void UpdateSoundIcon(float value)
        {
            if (soundIcon != null)
                soundIcon.sprite = value <= 0 ? soundOffSprite : soundOnSprite;
        }

        private void UpdateMusicIcon(float value)
        {
            if (musicIcon != null)
                musicIcon.sprite = value <= 0 ? musicOffSprite : musicOnSprite;
        }
    }
}
