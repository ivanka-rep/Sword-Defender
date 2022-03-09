using UnityEngine.Events;

namespace SwordDefender.Game
{
    public class GameEventManager
    {
        public static readonly UnityEvent OnGameProcessStarted = new UnityEvent();
        public static readonly UnityEvent OnGameProcessEnded = new UnityEvent();
        public static readonly UnityEvent<bool> OnPlayerGameOver = new UnityEvent<bool>();
        public static readonly UnityEvent<int> OnPlayerHealthChanged = new UnityEvent<int>();
        public static readonly UnityEvent OnEnemyKilled = new UnityEvent();
        
        public static void SendGameProcessStarted() =>
            OnGameProcessStarted.Invoke();
        
        public static void SendGameProcessEnded() =>
            OnGameProcessEnded.Invoke();
        
        public static void SendPlayerGameOver(bool win) =>
            OnPlayerGameOver.Invoke(win);
        
        public static void SendPlayerHealthChanged(int health) =>
            OnPlayerHealthChanged.Invoke(health);

        public static void SendEnemyKilled() =>
            OnEnemyKilled.Invoke();
    }
}