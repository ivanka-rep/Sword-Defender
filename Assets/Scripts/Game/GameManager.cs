using SwordDefender.Config;
using SwordDefender.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordDefender.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private GameConfig config = null;
        [SerializeField] private GameDataManager dataManager = null;
        [SerializeField] private bool isTest = false;
        #endregion

        #region Public
        public static GameManager Instance = null;
        public GameConfig Config => config;
        public GameDataManager DataManager => dataManager;
        public bool IsGameActive { get; private set; } = false;
        #endregion

        #region Unity Methods

        private void Awake()
        { 
            if (Instance != null) Destroy(Instance); 
            Instance = this;
            DontDestroyOnLoad(Instance);
            
            GameEventManager.OnGameProcessEnded.AddListener(() => IsGameActive = false);
            //todo: initialize by event
            SceneManager.LoadScene(isTest ? "TestScene" : "Main");

            dataManager.Init();
        }
        #endregion

        #region Public Methods
        public void StartAction()
        {
            IsGameActive = true;
            GameEventManager.SendGameProcessStarted();
        }
        #endregion
    }
}