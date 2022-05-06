using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.ShopItemsData
{
    [CreateAssetMenu(fileName = "SkinsData", menuName = "ScriptableObjects/SkinsData", order = 1)]
    public class SkinsDataObject : ScriptableObject
    {
        [SerializeField] private List<SkinProductDataSerializable> weaponProductData = null;

        public List<SkinProductData> GetAllSkinsList()
        {
            var dataList = weaponProductData.Select(serializedData => new SkinProductData()
            {
                Name = serializedData.name,
                Price = serializedData.price,
                IconId = serializedData.iconId,
                Health = serializedData.health,
                AttackSpeed = serializedData.attackSpeed
            }).ToList();
           
            return dataList;
        }
    }

    [Serializable] public class SkinProductDataSerializable
    {
        public string name;
        public int price;
        public string iconId;
        public string health;
        public string attackSpeed;
    }
}