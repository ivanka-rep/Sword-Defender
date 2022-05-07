using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SwordDefender.Data.ShopItemsData
{
    [CreateAssetMenu(fileName = "WeaponsData", menuName = "ScriptableObjects/WeaponsData", order = 1)]
    public class WeaponsDataObject : ScriptableObject
    {
       [SerializeField] private List<WeaponProductDataSerializable> weaponProductData = null;

       public List<WeaponProductData> GetAllWeaponsList()
       {
           var dataList = weaponProductData.Select(serializedData => new WeaponProductData()
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
    }

    [Serializable] public class WeaponProductDataSerializable
    {
        public string productId;
        public string name;
        public int price;
        public string iconId;
        public string damage;
        public string weight;
    }
}