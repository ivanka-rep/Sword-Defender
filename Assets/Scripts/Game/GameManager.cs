using Data;
using SwordDefender.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordDefender.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private GameConfig config = null;
        [SerializeField] private bool isTest = false;
        #endregion

        #region Public
        public static GameManager Instance = null;
        public GameConfig Config => config;
        public bool IsGameActive { get; private set; } = false;
        public PlayerData PlayerData => m_playerData;
        #endregion

        #region Private
        private PlayerData m_playerData = null;
        #endregion
        
        #region Unity Methods

        private void Awake()
        { 
            if (Instance != null) Destroy(Instance); 
            Instance = this;
            DontDestroyOnLoad(Instance);

            if (PlayerPrefs.GetInt("FIRST_START", 1) == 1)
            {
                // m_playerData =...
                PlayerPrefs.SetInt("FIRST_START", 0);
            }
            else
            {
                //m_playerData =...
            }
            
            GameEventManager.OnGameProcessEnded.AddListener(() => IsGameActive = false);
            SceneManager.LoadScene(isTest ? "TestScene" : "Main");
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