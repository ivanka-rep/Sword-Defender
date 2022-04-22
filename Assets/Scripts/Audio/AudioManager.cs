using UnityEngine;

namespace SwordDefender.Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource gameAudioSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip clickSound;
        [SerializeField] private AudioClip menuMusic;
        #endregion

        #region Public
        public static AudioManager Instance;

        public enum AudioType { MenuMusic, Click }

        public AudioSource MusicAudioSource 
        {
            get => musicAudioSource;
            set => musicAudioSource = value;
        }

        public AudioSource GameAudioSource
        {
            get => gameAudioSource;
            set => gameAudioSource = value;
        }
        #endregion

        #region Unity Methods
        void Awake()
        {
            if (Instance == null) { DontDestroyOnLoad(gameObject); Instance = this; }
            else if (Instance != this) { Destroy(gameObject); }

            Refresh();
        }
        #endregion

        #region Public Methods

        public void Refresh()
        {
            musicAudioSource.volume = PlayerPrefs.GetFloat("MUSIC_VOLUME", 0.75f);
            gameAudioSource.volume = PlayerPrefs.GetFloat("MASTER_VOLUME", 0.75f);
            
            musicAudioSource.mute = PlayerPrefs.GetInt("MUSIC_MUTE", 0) == 1;
            gameAudioSource.mute = PlayerPrefs.GetInt("MASTER_MUTE", 0) == 1;
        }
        
        public void PlayMusic(AudioType source)
        {
            AudioClip clip = menuMusic;
            musicAudioSource.loop = true;
            clip = source switch
            {
                AudioType.MenuMusic => menuMusic,
                _ => clip
            };

            musicAudioSource.Stop();
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void PlaySoundEffect(AudioType effect)
        {
            AudioClip clip = null;
            clip = effect switch
            {
                AudioType.Click => clickSound,
                _ => null
            };

            gameAudioSource.PlayOneShot(clip);
        }
        #endregion
    }
}