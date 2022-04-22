using UnityEngine;
using UnityEngine.UI;

namespace SwordDefender.UI.Panels
{
    public class PanelsManager : MonoBehaviour
    {
        #region Public
        public static PanelsManager Instance = null;
        #endregion

        #region Serialized Fields
        [Header("Settings")]
        [SerializeField] private float transitionTime = 1f;
        
        [Header("Panels")]
        [SerializeField] private Image background = null;
        [SerializeField] private PanelBase menuPanel = null;
        [SerializeField] private PanelBase settingsPanel = null;
        [SerializeField] private PanelBase gameHudPanel = null;
        [SerializeField] private PanelBase winPanel = null;
        [SerializeField] private PanelBase lossPanel = null;
        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            if (Instance != null) Destroy(Instance); 
            Instance = this;
        }
        #endregion

        #region Public Methods

        public void ActivateMenuPanel(PanelBase currentPanel) => 
            ChangeActivePanel(currentPanel, menuPanel, true);

        public void ActivateSettingsPanel(PanelBase currentPanel) =>
            ChangeActivePanel(currentPanel, settingsPanel, true);
        
        public void ActivateGameHudPanel(PanelBase currentPanel) => 
            ChangeActivePanel(currentPanel, gameHudPanel);
        
        public void ActivateWinPanel(PanelBase currentPanel) => 
            ChangeActivePanel(currentPanel, winPanel);
        
        public void ActivateLoosePanel(PanelBase currentPanel) => 
            ChangeActivePanel(currentPanel, lossPanel);

        #endregion

        #region Private Methods

        private void ChangeActivePanel(PanelBase currentPanel, PanelBase targetPanel, bool enableBackground = false)
        {
            background.color = enableBackground ? Color.white : Color.clear;
            
            currentPanel.SetActive(false, transitionTime);
            targetPanel.SetActive(true, transitionTime);
        }

        #endregion
    }
}