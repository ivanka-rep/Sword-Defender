using SwordDefender.Game;
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

        [SerializeField] private Color winBackground = Color.green;
        [SerializeField] private Color looseBackground = Color.red;

        
        [Header("Panels")]
        [SerializeField] private Image background = null;
        [SerializeField] private PanelBase menuPanel = null;
        [SerializeField] private PanelBase settingsPanel = null;
        [SerializeField] private PanelBase shopPanel = null;
        [SerializeField] private PanelBase gameHudPanel = null;
        [SerializeField] private PanelBase winPanel = null;
        [SerializeField] private PanelBase lossPanel = null;
        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            if (Instance != null) Destroy(Instance); 
            Instance = this;
            
            GameEventManager.OnGameProcessStarted.AddListener(() => background.color = Color.clear);
        }
        #endregion

        #region Public Methods

        public void ActivateMenuPanel(PanelBase currentPanel) => 
            ChangeActivePanel(currentPanel, menuPanel);

        public void ActivateSettingsPanel(PanelBase currentPanel) =>
            ChangeActivePanel(currentPanel, settingsPanel);
        
        public void ActivateShopPanel(PanelBase currentPanel) =>
            ChangeActivePanel(currentPanel, shopPanel);

        public void ActivateGameHudPanel(PanelBase currentPanel) => 
            ChangeActivePanel(currentPanel, gameHudPanel);
        
        public void ActivateWinPanel(PanelBase currentPanel)
        {
            background.color = winBackground;
            ChangeActivePanel(currentPanel, winPanel, false);
        }

        public void ActivateLoosePanel(PanelBase currentPanel)
        {
            background.color = looseBackground;
            ChangeActivePanel(currentPanel, lossPanel, false);
        }

        #endregion

        #region Private Methods

        private void ChangeActivePanel(PanelBase currentPanel, PanelBase targetPanel, bool changeBackground = true)
        {
            if (changeBackground) background.color = Color.white;
            
            currentPanel.SetActive(false, transitionTime);
            targetPanel.SetActive(true, transitionTime);
        }

        #endregion
    }
}