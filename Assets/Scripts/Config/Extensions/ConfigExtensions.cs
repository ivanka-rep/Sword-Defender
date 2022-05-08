using System.Collections.Generic;
using System.Linq;
using SwordDefender.Data.ShopItemsData;

namespace SwordDefender.Config
{
    public static class ConfigExtensions
    {
        public static List<WeaponProductData> GetAllWeaponsList(this IEnumerable<WeaponsDataConfig> weaponProductConfig)
        {
            var dataList = weaponProductConfig.Select(serializedData => new WeaponProductData()
            {
                ProductId = serializedData.productId,
                ProductType = ProductType.Weapon,
                Name = serializedData.name,
                Price = serializedData.price,
                IconId = serializedData.iconId,
                Damage = serializedData.damage,
                Weight = serializedData.weight
            }).ToList();
           
            return dataList;
        }
        
        public static List<SkinProductData> GetAllSkinsList(this IEnumerable<SkinsDataConfig> skinProductsConfig)
        {
            var dataList = skinProductsConfig.Select(serializedData => new SkinProductData()
            {
                ProductId = serializedData.productId,
                ProductType = ProductType.Skin,
                Name = serializedData.name,
                Price = serializedData.price,
                IconId = serializedData.iconId,
                Health = serializedData.health,
                AttackSpeed = serializedData.attackSpeed
            }).ToList();
           
            return dataList;
        }
    }
}