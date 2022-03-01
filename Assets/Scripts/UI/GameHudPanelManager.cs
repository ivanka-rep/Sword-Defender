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
            GameEventManager.OnGameProcessEnded.AddListener(() => StartCoroutine(GameEndDelay()));
        }

        #endregion

        #region Coroutines

        private IEnumerator GameEndDelay()
        {
            yield return new WaitForSeconds(1f);
            PanelsManager.Instance.ActivateMenuPanel(this);
        }
        #endregion
    }
}