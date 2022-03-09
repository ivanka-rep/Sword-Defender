using System.Collections;
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
            GameEventManager.OnPlayerGameOver.AddListener(win => StartCoroutine(GameOverDelay(win)));
        }

        #endregion

        #region Coroutines

        private IEnumerator GameOverDelay(bool win)
        {
            yield return new WaitForSeconds(1f);
            if (win) PanelsManager.Instance.ActivateWinPanel(this);
            else PanelsManager.Instance.ActivateLoosePanel(this);
        }
        #endregion
    }
}