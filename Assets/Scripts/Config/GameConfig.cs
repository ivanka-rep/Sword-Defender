using UnityEngine;

namespace SwordDefender.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private CharacterStats playerStats;
        [SerializeField] private CharacterStats enemyStats;

        public CharacterStats PlayerStats => playerStats;
        public CharacterStats EnemyStats => enemyStats;
    }
}