using System.Collections.Generic;
using SwordDefender.Data.ShopItemsData;
using UnityEngine;

namespace SwordDefender.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private PlayerParams playerParams = null;
        [SerializeField] private EnemyParams enemyParams = null;
        [SerializeField] private CombatParams combatParams = null;

        [Header("Shop products")]
        [SerializeField] private List<SkinsDataConfig> skinsConfig = null;
        [SerializeField] private List<WeaponsDataConfig> weaponsConfig = null;


        public PlayerParams PlayerParams => playerParams;
        public EnemyParams EnemyParams => enemyParams;
        public CombatParams CombatParams => combatParams;
        
        public List<SkinProductData> GetAllSkinsList() => skinsConfig.GetAllSkinsList();
        public List<WeaponProductData> GetAllWeaponsList() => weaponsConfig.GetAllWeaponsList();
    }
}