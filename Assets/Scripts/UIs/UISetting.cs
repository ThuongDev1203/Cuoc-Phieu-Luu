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
                soundSlider.value = SoundManager.Instance.GetSoundVolume();

            if (musicSlider != null)
                musicSlider.value = SoundManager.Instance.GetMusicVolume();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void OnSoundSlider(float value)
        {
            Debug.Log("Sound volume: " + value);
            SoundManager.Instance.SetSoundVolume(value);
        }

        private void OnMusicSlider(float value)
        {
            Debug.Log("Music volume: " + value);
            SoundManager.Instance.SetMusicVolume(value);
        }
    }
}
