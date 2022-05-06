using System;
using Data.ShopItemsData.Interfaces;

namespace Data.ShopItemsData
{
    [Serializable] public class WeaponProductData : IWeaponProduct
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string IconId { get; set; }
        public string Damage { get; set; }
        public string Weight { get; set; }
    }
}