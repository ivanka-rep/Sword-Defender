using SwordDefender.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordDefender.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Public
        public static GameManager Instance = null;
        public GameConfig Config => config;
        
        [HideInInspector] public bool IsGameActive = false;
        #endregion

        #region Serialized Fields
        [SerializeField] private GameConfig config = null;
        #endregion
        
        #region Unity Methods

        private void Awake()
        { 
            if (Instance != null) Destroy(Instance); 
            Instance = this;
            DontDestroyOnLoad(Instance);

            GameEventManager.OnGameProcessEnded.AddListener(() => { IsGameActive = false; });
            SceneManager.LoadScene("Main");
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