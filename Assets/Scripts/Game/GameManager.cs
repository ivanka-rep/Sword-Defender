using System;
using SwordDefender.Config;
using UnityEngine;

namespace SwordDefender.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Public
        public static GameManager Instance = null;
        public GameConfig GameConfig => gameConfig;
        #endregion

        #region Serialized Fields
        
        [SerializeField] private EnemySpawner enemySpawner = null;
        [SerializeField] private GameConfig gameConfig = null;
        
        #endregion

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