using System;
using Data.ShopItemsData.Interfaces;

namespace Data.ShopItemsData
{
    public class WeaponProductData : IWeaponProduct
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string IconId { get; set; }
        public string Damage { get; set; }
        public string Weight { get; set; }
    }
}