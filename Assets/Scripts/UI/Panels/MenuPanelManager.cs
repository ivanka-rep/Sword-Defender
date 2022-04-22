using SwordDefender.Game;

namespace SwordDefender.UI.Panels
{
    public class MenuPanelManager : PanelBase
    {
        #region Private
        private GameManager m_gameManager = null;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            m_gameManager = GameManager.Instance;
        }
        #endregion
        
        #region Public Methods

        public void OnPlayButtonPressed()
        {
            if (m_gameManager.IsGameActive) return;
            
            PanelsManager.Instance.ActivateGameHudPanel(this);
            GameManager.Instance.StartAction();
        }

        public void OnSettingsButtonPressed() =>
            PanelsManager.Instance.ActivateSettingsPanel(this);

        #endregion
    }
}