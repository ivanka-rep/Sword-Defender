
namespace SwordDefender.UI
{
    public class WinPanelManager : PanelBase
    {
        #region Public Methods

        public void OnNextButtonPressed() =>
            PanelsManager.Instance.ActivateMenuPanel(this);

        #endregion
    }
}