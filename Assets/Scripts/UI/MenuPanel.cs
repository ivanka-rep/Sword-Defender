using System;
using SwordDefender.Game;
using UnityEngine;

namespace SwordDefender.UI
{
    public class MenuPanel : PanelBase
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
        
        #endregion
    }
}