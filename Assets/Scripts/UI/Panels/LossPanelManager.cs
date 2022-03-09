using SwordDefender.Game;

namespace SwordDefender.UI
{
    public class LossPanelManager : PanelBase
    {
        #region Public Methods

        public void OnNextButtonPressed() =>
            PanelsManager.Instance.ActivateMenuPanel(this);

        public void OnTryAgainButtonPressed()
        {
            PanelsManager.Instance.ActivateGameHudPanel(this);
            GameManager.Instance.StartAction();
        }

        #endregion
    }
}