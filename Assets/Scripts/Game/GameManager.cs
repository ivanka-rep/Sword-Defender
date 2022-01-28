using System;
using UnityEngine;

namespace SwordDefender.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance = null;
        #endregion
        
        [SerializeField] private EnemySpawner enemySpawner;

        #region Unity Methods

        private void Awake()
        { 
            if (Instance != null) Destroy(Instance); 
            Instance = this;
        }

        private void Start()
        {
            StartAction();
        }

        #endregion

        #region Public Methods
        
        public void StartAction()
        {
            enemySpawner.StartAction(10);
        }
        
        public void StopEnemiesAction()
        {
            enemySpawner.StopAction();
        }
        #endregion
    }
}