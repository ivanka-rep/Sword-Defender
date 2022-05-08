using System.Collections.Generic;
using SwordDefender.Data.ShopItemsData;
using UnityEngine;

namespace SwordDefender.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public PlayerParams PlayerParams = null;
        public EnemyParams EnemyParams = null;
        public CombatParams CombatParams = null;

        [Header("Shop products")]
        public List<SkinsDataConfig> SkinsConfig = null;
        public List<WeaponsDataConfig> WeaponsConfig = null;
    }
}