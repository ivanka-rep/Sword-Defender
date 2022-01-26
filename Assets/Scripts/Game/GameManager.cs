using UnityEngine;

namespace SwordDefender.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance = null;
        #endregion
        
        [SerializeField] private EnemySpawner enemySpawner;
        private void Awake()
        { 
            if (Instance != null) Destroy(Instance); 
            Instance = this;
        }

        private void Start()
        {
            enemySpawner.StartAction(10);
        }
        
        public void StopEnemiesAction()
        {
            enemySpawner.StopAction();
        }
    }
}