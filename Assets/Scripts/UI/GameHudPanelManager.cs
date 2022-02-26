using SwordDefender.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SwordDefender.UI
{
    public class GameHudPanelManager : PanelBase
    {
        #region Serialized Fields

        [SerializeField] private Slider healthSlider = null;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            GameEventManager.OnPlayerHealthChanged.AddListener(health => healthSlider.value = health / 100f);
            GameEventManager.OnGameProcessEnded.AddListener(() => PanelsManager.Instance.ActivateMenuPanel(this));
        }

        #endregion
    }
}