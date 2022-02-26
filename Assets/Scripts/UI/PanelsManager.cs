using UnityEngine;

namespace SwordDefender.UI
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
        [SerializeField] private PanelBase menuPanel = null;
        [SerializeField] private PanelBase gameHudPanel = null;
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
            ChangeActivePanel(currentPanel, menuPanel);
        
        public void ActivateGameHudPanel(PanelBase currentPanel) => 
            ChangeActivePanel(currentPanel, gameHudPanel);

        #endregion

        #region Private Methods

        private void ChangeActivePanel(PanelBase currentPanel, PanelBase targetPanel)
        {
            currentPanel.SetActive(false, transitionTime);
            targetPanel.SetActive(true, transitionTime);
        }

        #endregion
    }
}