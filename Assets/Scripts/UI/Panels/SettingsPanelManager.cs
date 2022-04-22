using SwordDefender.Audio;
using SwordDefender.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SwordDefender.UI.Panels
{
    public class SettingsPanelManager : PanelBase
    {
        #region Serialized Fields
        [SerializeField] private Slider sensitivity;
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Slider masterVolume;

        [Space(10)]
        
        [SerializeField] private Toggle musicMuteToggle; 
        [SerializeField] private Toggle masterMuteToggle;
        #endregion

        #region Private

        private AudioManager m_audioManager = null;

        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            m_audioManager = AudioManager.Instance;
            
            // SLIDERS SETTINGS
            sensitivity.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("SENSITIVITY", value < 0.1f ? 0.1f : value);
                GameEventManager.SendGameSettingsChanged(SettingType.Sensitivity);
            });
            
            masterVolume.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("MASTER_VOLUME", value);
                GameEventManager.SendGameSettingsChanged(SettingType.MasterVolume);
                m_audioManager.Refresh();
            });

            musicVolume.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("MUSIC_VOLUME", value);
                GameEventManager.SendGameSettingsChanged(SettingType.MusicVolume);
                m_audioManager.Refresh();
            });

            // TOGGLES SETTINGS
            musicMuteToggle.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetInt("MUSIC_MUTE", value ? 0 : 1 );
                GameEventManager.SendGameSettingsChanged(SettingType.MusicMute);
                m_audioManager.Refresh();
            });
            
            masterMuteToggle.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetInt("MASTER_MUTE", value ? 0 : 1 );
                GameEventManager.SendGameSettingsChanged(SettingType.MasterMute);
                m_audioManager.Refresh();
            });
            
            //Event from base class
            m_onPanelActivated.AddListener(isActivated => Refresh());
        }
        #endregion

        #region Public Methods

        public void OnBackButtonClick() =>
            PanelsManager.Instance.ActivateMenuPanel(this);

        #endregion
        
        #region Private Methods
        private void Refresh()
        {
            sensitivity.value = PlayerPrefs.GetFloat("SENSITIVITY", 0.5f);
            musicVolume.value = PlayerPrefs.GetFloat("MUSIC_VOLUME", 0.75f);
            masterVolume.value = PlayerPrefs.GetFloat("MASTER_VOLUME", 0.75f);
            
            musicMuteToggle.isOn = PlayerPrefs.GetInt("MUSIC_MUTE", 0) == 0;
            masterMuteToggle.isOn = PlayerPrefs.GetInt("MASTER_MUTE", 0) == 0;
            
            m_audioManager.Refresh();
        }
        #endregion
    }
}