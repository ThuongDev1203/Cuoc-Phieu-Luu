using System.Collections;
using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [Header("Sound Clip")]
        public AudioClip backgroundMusic;
        public AudioClip clickButton;
        public AudioClip jumpButton;
        public AudioClip run;
        public AudioClip coinCollect;
        public AudioClip hitPig;
        public AudioClip trap_falling;
        public AudioClip nemdep;
        public AudioClip attackone;
        public AudioClip boxBreak;
        public AudioClip win;
        public AudioClip lose;
        public AudioClip bom;


        private AudioSource musicSource;
        private AudioSource sfxSource;

        // Key để lưu âm lượng vào PlayerPrefs
        private const string MUSIC_VOLUME_KEY = "music_volume";
        private const string SOUND_VOLUME_KEY = "sound_volume";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitAudioSources(); // Tạo và cấu hình AudioSource
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Load âm lượng đã lưu
            SetMusicVolume(GetMusicVolume());
            SetSoundVolume(GetSoundVolume());

            // Bắt đầu phát nhạc nền mặc định
            PlayBackgroundMusic();
        }

        /// <summary>
        /// Tạo và cấu hình AudioSources cho nhạc nền và hiệu ứng
        /// </summary>
        private void InitAudioSources()
        {
            // Music Source
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;

            // SFX Source
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }

        /// <summary>
        /// Phát nhạc nền mặc định
        /// </summary>
        public void PlayBackgroundMusic()
        {
            if (backgroundMusic == null) return;

            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }

        /// <summary>
        /// Đổi nhạc nền
        /// </summary>
        public void ChangeBackgroundMusic(AudioClip newClip)
        {
            if (newClip == null) return;

            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.Play();
        }

        /// <summary>
        /// Trở về nhạc nền mặc định
        /// </summary>
        public void RestoreDefaultMusic()
        {
            ChangeBackgroundMusic(backgroundMusic);
        }

        /// <summary>
        /// Phát âm thanh hiệu ứng
        /// </summary>
        public void PlayClickSound()
        {
            if (clickButton == null) return;

            sfxSource.PlayOneShot(clickButton);
        }

        /// <summary>
        /// Jump
        /// </summary>
        public void PlayJumpSound()
        {
            if (jumpButton == null) return;
            sfxSource.PlayOneShot(jumpButton);
        }

        public void PlayCoinColect()
        {
            if (coinCollect == null) return;
            sfxSource.PlayOneShot(coinCollect);
        }

        public void PlayRun()
        {
            if (run == null) return;
            sfxSource.PlayOneShot(run);
        }

        public void PlayHitPig()
        {
            if (hitPig == null) return;
            sfxSource.PlayOneShot(hitPig);
        }

        public void PlayTrapFlalling()
        {
            if (trap_falling == null) return;
            sfxSource.PlayOneShot(trap_falling);
        }

        public void PlayNemDep()
        {
            if (nemdep == null) return;
            sfxSource.PlayOneShot(nemdep);
        }

        public void PlayAttackOne()
        {
            if (attackone == null) return;
            sfxSource.PlayOneShot(attackone);
        }

        public void PLayBoxBreak()
        {
            if (boxBreak == null) return;
            sfxSource.PlayOneShot(boxBreak);
        }

        public void PLayWin()
        {
            if (win == null) return;
            sfxSource.PlayOneShot(win);
        }

        public void PLayLose()
        {
            if (lose == null) return;
            sfxSource.PlayOneShot(lose);
        }

        public void PlayBom()
        {
            if (bom == null) return;
            sfxSource.PlayOneShot(bom);
        }

        // -----------------------------
        // Xử lý âm lượng + lưu vào PlayerPrefs
        // -----------------------------

        /// <summary>
        /// Lấy âm lượng nhạc nền từ PlayerPrefs
        /// </summary>
        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f); // Mặc định 100%
        }

        /// <summary>
        /// Lấy âm lượng hiệu ứng từ PlayerPrefs
        /// </summary>
        public float GetSoundVolume()
        {
            return PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, 1f); // Mặc định 100%
        }

        /// <summary>
        /// Cập nhật và lưu âm lượng nhạc nền
        /// </summary>
        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }

        /// <summary>
        /// Cập nhật và lưu âm lượng hiệu ứng
        /// </summary>
        public void SetSoundVolume(float volume)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, volume);
        }
    }
}
