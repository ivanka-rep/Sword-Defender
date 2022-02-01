using System;
using System.Collections;
using System.Collections.Generic;
using SwordDefender.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SwordDefender.UI
{
    public class GameHoodViewManager : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Slider healthSlider = null;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            GameEventManager.OnPlayerHealthChanged.AddListener(health => healthSlider.value = health / 100f);
        }

        #endregion
    }
}