using SwordDefender.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SwordDefender.UI
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

        #region Unity Methods
        private void Awake()
        {
            // SLIDERS SETTINGS
            sensitivity.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("SENSITIVITY", value);
                GameEventManager.SendGameSettingsChanged(SettingType.Sensitivity);
            });
            
            masterVolume.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("MASTER_VOLUME", value);
                GameEventManager.SendGameSettingsChanged(SettingType.MasterVolume);
            });

            musicVolume.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("MUSIC_VOLUME", value);
                GameEventManager.SendGameSettingsChanged(SettingType.MusicVolume);
            });

            // TOGGLES SETTINGS
            musicMuteToggle.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetInt("MUSIC_MUTE", value ? 1 : 0 );
                GameEventManager.SendGameSettingsChanged(SettingType.MusicMute);
                
            });
            
            masterMuteToggle.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetInt("MASTER_MUTE", value ? 1 : 0 );
                GameEventManager.SendGameSettingsChanged(SettingType.MasterMute);
            });
            
            //Event from base class
            m_onPanelActivated.AddListener(Refresh);
        }
        #endregion

        #region Public Methods

        public void OnBackButtonClick() =>
            PanelsManager.Instance.ActivateMenuPanel(this);

        #endregion
        
        #region Private Methods
        private void Refresh()
        {
            sensitivity.value = PlayerPrefs.GetFloat("SENSITIVITY", 0.75f);
            musicVolume.value = PlayerPrefs.GetFloat("MUSIC_VOLUME", 0.75f);
            masterVolume.value = PlayerPrefs.GetFloat("MASTER_VOLUME", 0.75f);
            
            musicMuteToggle.isOn = PlayerPrefs.GetInt("MUSIC_MUTE", 1) == 1;
            masterMuteToggle.isOn = PlayerPrefs.GetInt("MASTER_MUTE", 1) == 1;
        }
        #endregion
    }
}